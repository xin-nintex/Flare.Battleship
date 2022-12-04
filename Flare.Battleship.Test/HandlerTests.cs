using Flare.Battleship.Application.Command;
using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Handlers;
using Moq;

namespace Flare.Battleship.Test;

public class HandlerTests
{
    [Test]
    public void AddBattleshipCommand_ShouldPlaceBattleShip_ToContext()
    {
        var contextMock = new Mock<GameStateContext>();

        var subject = new AddBattleshipCommandHandler(contextMock.Object);

        subject.Handle(
            new AddBattleshipCommand()
            {
                Orientation = Orientation.Horizontal,
                Ship = Ship.Battleship,
                Swing = Swing.Right,
                StartCell = new Cell(BoardColumn.A, BoardRow.Eight)
            }
        );

        contextMock.Verify(
            x =>
                x.Save(
                    It.Is<ShipPlacement>(
                        x =>
                            x
                            == new ShipPlacement(
                                Ship.Battleship,
                                new Cell(BoardColumn.A, BoardRow.Eight),
                                Orientation.Horizontal,
                                Swing.Right
                            )
                    )
                ),
            Times.Once
        );
    }

    [TestCaseSource(nameof(AttackResultData))]
    public void AttackResultQueryHandle_ShouldReturn_Result(IEnumerable<AttackPlacement> data, AttackResultQuery query, AttackResult? expect)
    {
        var ctx = new Mock<GameStateContext>();
        ctx.Setup(x => x.AttackPlacements)
            .Returns(data);
        var result = new AttackResultQueryHandler(ctx.Object).Handle(query);
        Assert.That(result, Is.EqualTo(expect));
    }

    private static readonly object[] AttackResultData =
    {
        new object[]
        {
            new List<AttackPlacement>()
            {
                new(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement)
            },
            new AttackResultQuery()
            {
                AttackPosition = new Cell(BoardColumn.A, BoardRow.Eight)
            },
            new AttackResult(true)
        },
        new object[]
        {
            new List<AttackPlacement>()
            {
                new(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.MissPlacement)
            },
            new AttackResultQuery()
            {
                AttackPosition = new Cell(BoardColumn.A, BoardRow.Eight)
            },
            new AttackResult(false)
        },
        new object[]
        {
            new List<AttackPlacement>()
            {
                new(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.MissPlacement)
            },
            new AttackResultQuery()
            {
                AttackPosition = new Cell(BoardColumn.A, BoardRow.Seven)
            },
            (AttackResult)null
        },
    };

    [Test]
    public void CreateBoardCommand_ShouldInitialize_GameContext()
    {
        var ctx = new Mock<GameStateContext>();
        new CreateBoardCommandHandler(ctx.Object).Handle(new CreateBoardCommand());
        
        ctx.Verify(x => x.Initialize(), Times.Once);
    }

    [TestCaseSource(nameof(GameResultData))]
    public void GameResultQuery_ShouldReturn_Result(IEnumerable<ShipStatus> data, GameResult expect)
    {
        var ctx = new Mock<GameStateContext>();
        ctx.Setup(x => x.ShipStatus)
            .Returns(data);
        var result = new GameResultQueryHandler(ctx.Object).Handle(new GameResultQuery());
        Assert.That(result.IsLost, Is.EqualTo(expect.IsLost));
        Assert.That(result.LiveShips, Is.EquivalentTo(expect.LiveShips));
    }

    private static readonly object[] GameResultData =
    {
        new object[]
        {
            new List<ShipStatus>()
            {
                new(Ship.Battleship, true),
                new(Ship.Carrier, true)
            },
            new GameResult(true, Array.Empty<Ship>())
        },
        new object[]
        {
            new List<ShipStatus>()
            {
                new(Ship.Battleship, true),
                new(Ship.Carrier, false)
            },
            new GameResult(false, new []{Ship.Carrier})
        },
    };

    [TestCaseSource(nameof(TakeAttackData))]
    public void TakeAttackCommand_ShouldRemember_Attack(IEnumerable<ShipPlacement> data, TakeAttackCommand command, bool isHit)
    {
        var ctx = new Mock<GameStateContext>();
        ctx.Setup(x => x.ShipPlacements)
            .Returns(data);
        new TakeAttackCommandHandler(ctx.Object).Handle(command);
        if (isHit)
            ctx.Verify(x => x.Save(It.IsAny<AttackPlacement>(), It.Is<Ship?>(y => y != null)), Times.Once);
        else
            ctx.Verify(x => x.Save(It.IsAny<AttackPlacement>(), It.Is<Ship?>(y => y == null)), Times.Once);
    }

    private static readonly object[] TakeAttackData =
    {
        new object[]
        {
            new List<ShipPlacement>()
            {
                new (Ship.Battleship, new Cell(BoardColumn.A, BoardRow.Eight), Orientation.Horizontal, Swing.Right),
                new (Ship.Cruiser, new Cell(BoardColumn.A, BoardRow.Seven), Orientation.Horizontal, Swing.Right),
            },
            new TakeAttackCommand()
            {
                AttackPosition = new Cell(BoardColumn.A, BoardRow.Eight)
            },
            true
        },
        new object[]
        {
            new List<ShipPlacement>()
            {
                new (Ship.Battleship, new Cell(BoardColumn.A, BoardRow.Eight), Orientation.Horizontal, Swing.Right),
                new (Ship.Cruiser, new Cell(BoardColumn.A, BoardRow.Seven), Orientation.Horizontal, Swing.Right),
            },
            new TakeAttackCommand()
            {
                AttackPosition = new Cell(BoardColumn.A, BoardRow.Six)
            },
            false
        },
    };
}
