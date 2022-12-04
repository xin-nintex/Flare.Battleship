using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Domain.Services;
using Moq;

namespace Flare.Battleship.Test;

public class GameContextTests
{
    private Mock<IShipStatusService> _shipStatusServiceMock = null!;
    private Mock<IBoardService<ShipPlacement>> _shipPlacementServiceMock = null!;
    private Mock<IBoardService<AttackPlacement>> _attackPlacementServiceMock = null!;
    private GameContext _subject = null!;

    [SetUp]
    public void Setup()
    {
        _attackPlacementServiceMock = new Mock<IBoardService<AttackPlacement>>();
        _shipPlacementServiceMock = new Mock<IBoardService<ShipPlacement>>();
        _shipStatusServiceMock = new Mock<IShipStatusService>();
        _subject = new GameContext(
            _shipPlacementServiceMock.Object,
            _attackPlacementServiceMock.Object,
            _shipStatusServiceMock.Object
        );
    }

    [Test]
    public void GameContext_ShouldSave_AttackPlacement_When_NotPlacedBefore()
    {
        // Arrange
        _attackPlacementServiceMock.Reset();
        _attackPlacementServiceMock
            .Setup(s => s.IsPlaced(It.IsAny<AttackPlacement>()))
            .Returns(false);

        // Act
        _subject.Save(
            new AttackPlacement(
                new Cell(BoardColumn.C, BoardRow.Eight),
                OccupationType.HitPlacement
            ),
            null
        );

        // Assert
        _attackPlacementServiceMock.Verify(x => x.Place(It.IsAny<AttackPlacement>()), Times.Once);
    }

    [Test]
    public void GameContext_ShouldThrow_WhenAttack_HasBeenPlacedBefore()
    {
        // Arrange
        _attackPlacementServiceMock.Reset();
        _attackPlacementServiceMock
            .Setup(s => s.IsPlaced(It.IsAny<AttackPlacement>()))
            .Returns(true);

        // Act
        Assert.Throws<InvalidAttackPlacementException>(
            () =>
                _subject.Save(
                    new AttackPlacement(
                        new Cell(BoardColumn.C, BoardRow.Eight),
                        OccupationType.HitPlacement
                    ),
                    null
                )
        );
    }

    [Test]
    public void GameContext_ShouldRecord_ShipHit()
    {
        // Arrange
        _attackPlacementServiceMock.Reset();
        _attackPlacementServiceMock
            .Setup(s => s.IsPlaced(It.IsAny<AttackPlacement>()))
            .Returns(false);

        // Act
        _subject.Save(
            new AttackPlacement(
                new Cell(BoardColumn.C, BoardRow.Eight),
                OccupationType.HitPlacement
            ),
            Ship.Battleship
        );

        // Assert
        _shipStatusServiceMock.Verify(
            x => x.RecordShipHit(It.Is<Ship>(y => y == Ship.Battleship)),
            Times.Once
        );
    }

    [Test]
    public void GameContext_ShouldSave_ShipPlacement()
    {
        // Arrange
        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock
            .Setup(x => x.GetEnumerator())
            .Returns(
                new List<ShipPlacement>()
                {
                    new(
                        Ship.Battleship,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Horizontal,
                        Swing.Right
                    )
                }.GetEnumerator()
            );
        _shipPlacementServiceMock.Setup(x => x.IsPlaced(It.IsAny<ShipPlacement>())).Returns(false);

        // Act
        _subject.Save(
            new ShipPlacement(
                Ship.Carrier,
                new Cell(BoardColumn.B, BoardRow.Five),
                Orientation.Horizontal,
                Swing.Right
            )
        );

        // Assert
        _shipPlacementServiceMock.Verify(x => x.Place(It.IsAny<ShipPlacement>()), Times.Once);
    }

    [Test]
    public void GameContext_ShouldThrow_WhenShipPlacement_IntersectsWithExisting()
    {
        // Arrange
        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock
            .Setup(x => x.GetEnumerator())
            .Returns(
                new List<ShipPlacement>()
                {
                    new(
                        Ship.Battleship,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Horizontal,
                        Swing.Right
                    )
                }.GetEnumerator()
            );

        // Act
        Assert.Throws<InvalidShipPlacementException>(
            () =>
                _subject.Save(
                    new ShipPlacement(
                        Ship.Carrier,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Vertical,
                        Swing.Up
                    )
                )
        );
    }
    
    [Test]
    public void GameContext_ShouldThrow_WhenShipPlacement_IsInvalid()
    {
        // Arrange
        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock
            .Setup(x => x.GetEnumerator())
            .Returns(
                new List<ShipPlacement>()
                {
                    new(
                        Ship.Battleship,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Horizontal,
                        Swing.Right
                    )
                }.GetEnumerator()
            );

        // Act
        Assert.Throws<InvalidShipPlacementException>(
            () =>
                _subject.Save(
                    new ShipPlacement(
                        Ship.Carrier,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Vertical,
                        Swing.Right
                    )
                )
        );
    }
    
    [Test]
    public void GameContext_ShouldThrow_WhenShipPlacement_IsPlacedBefore()
    {
        // Arrange
        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock
            .Setup(x => x.GetEnumerator())
            .Returns(
                new List<ShipPlacement>()
                {
                    new(
                        Ship.Battleship,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Horizontal,
                        Swing.Right
                    )
                }.GetEnumerator()
            );
        _shipPlacementServiceMock.Setup(x => x.IsPlaced(It.IsAny<ShipPlacement>()))
            .Returns(true);

        // Act
        Assert.Throws<InvalidShipPlacementException>(
            () =>
                _subject.Save(
                    new ShipPlacement(
                        Ship.Battleship,
                        new Cell(BoardColumn.A, BoardRow.Eight),
                        Orientation.Horizontal,
                        Swing.Right
                    )
                )
        );
    }

    [Test]
    public void GameContext_ShouldRest_WhenInitialize()
    {
        _subject.Initialize();
        
        _attackPlacementServiceMock.Verify(x => x.ClearBoard(), Times.Once);
        _shipPlacementServiceMock.Verify(x => x.ClearBoard(), Times.Once);
        _shipStatusServiceMock.Verify(x => x.ResetStatuses(), Times.Once);
    }

    [Test]
    public void GameContext_HasData()
    {
        _attackPlacementServiceMock.Reset();
        _attackPlacementServiceMock.Setup(x => x.GetEnumerator())
            .Returns(Enumerable.Empty<AttackPlacement>().GetEnumerator());
        _subject.AttackPlacements.ToArray();
        _attackPlacementServiceMock.Verify(x => x.GetEnumerator(), Times.Once);

        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock.Setup(x => x.GetEnumerator())
            .Returns(Enumerable.Empty<ShipPlacement>().GetEnumerator());
        _subject.ShipPlacements.ToArray();
        _shipPlacementServiceMock.Verify(x =>x.GetEnumerator(), Times.Once);
    }

    [Test]
    public void GameContext_ShouldShow_CorrectShipStatus()
    {
        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock.Setup(x => x.GetEnumerator())
            .Returns(new List<ShipPlacement>()
            {
                new(
                    Ship.Battleship,
                    new Cell(BoardColumn.A, BoardRow.Eight),
                    Orientation.Horizontal,
                    Swing.Right
                )
            }.GetEnumerator());
        _shipStatusServiceMock.Reset();
        _shipStatusServiceMock.Setup(x => x.IsSunk(Ship.Battleship))
            .Returns(true);
        
        // Act
        var shipStatusEnumerable = _subject.ShipStatus;
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(shipStatusEnumerable.Count(), Is.EqualTo(1));
            Assert.That(shipStatusEnumerable.First().IsSunk, Is.EqualTo(true));
        });
    }
    [Test]
    public void GameContext_ShouldShow_EmptyResults_WhenNoShipIsPlaced()
    {
        _shipPlacementServiceMock.Reset();
        _shipPlacementServiceMock.Setup(x => x.GetEnumerator())
            .Returns(Enumerable.Empty<ShipPlacement>().GetEnumerator());
        
        // Act
        var shipStatusEnumerable = _subject.ShipStatus;
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(shipStatusEnumerable.Count(), Is.Zero);
        });
    }
}
