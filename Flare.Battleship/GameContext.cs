using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Domain.Services;

namespace Flare.Battleship;

public class GameContext
{
    private readonly IBoardService<ShipPlacement> _shipBoardService;
    private readonly IBoardService<AttackPlacement> _attackBoardService;
    private readonly IShipStatusService _shipStatusService;

    public GameContext(
        IBoardService<ShipPlacement> shipBoardService,
        IBoardService<AttackPlacement> attackBoardService,
        IShipStatusService shipStatusService
    )
    {
        _shipBoardService = shipBoardService;
        _attackBoardService = attackBoardService;
        _shipStatusService = shipStatusService;
    }

    public IEnumerable<ShipPlacement> ShipPlacements => _shipBoardService;
    public IEnumerable<AttackPlacement> AttackPlacements => _attackBoardService;

    public IEnumerable<ShipStatus> ShipStatus
    {
        get
        {
            return Ship.AllAvailableShips.Select(
                s => new ShipStatus(s, _shipStatusService.IsSunk(s))
            );
        }
    }

    public void Save(AttackPlacement attackPlacement, Ship? shipHit)
    {
        if (_attackBoardService.IsPlaced(attackPlacement))
            throw new AttackAlreadyPlacedException(
                $"attack at {attackPlacement.Cell} has already been placed"
            );
        if (shipHit != null)
            _shipStatusService.RecordShipHit(shipHit.Value);
        _attackBoardService.Record(attackPlacement);
    }

    public void Save(ShipPlacement shipPlacement)
    {
        if (_shipBoardService.IsPlaced(shipPlacement))
            throw new ShipAlreadyPlaceException($"{shipPlacement.Ship} has already been placed");
        _shipBoardService.Record(shipPlacement);
    }
}