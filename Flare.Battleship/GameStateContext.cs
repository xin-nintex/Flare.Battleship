using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Domain.Services;

namespace Flare.Battleship;

public class GameStateContext
{
    private readonly IBoardService<ShipPlacement> _shipBoardService;
    private readonly IBoardService<AttackPlacement> _attackBoardService;
    private readonly IShipStatusService _shipStatusService;

    public GameStateContext(){} //for unit test
    public GameStateContext(
        IBoardService<ShipPlacement> shipBoardService,
        IBoardService<AttackPlacement> attackBoardService,
        IShipStatusService shipStatusService
    )
    {
        _shipBoardService = shipBoardService;
        _attackBoardService = attackBoardService;
        _shipStatusService = shipStatusService;
    }

    public virtual IEnumerable<ShipPlacement> ShipPlacements => _shipBoardService;
    public virtual IEnumerable<AttackPlacement> AttackPlacements => _attackBoardService;
    public virtual IEnumerable<ShipStatus> ShipStatus
    {
        get
        {
            var placedShips = ShipPlacements.Select(s => s.Ship).ToArray();
            return !placedShips.Any()
                ? Enumerable.Empty<ShipStatus>()
                : placedShips.Select(s => new ShipStatus(s, _shipStatusService.IsSunk(s)));
        }
    }

    public virtual void Save(AttackPlacement attackPlacement, Ship? shipHit)
    {
        if (_attackBoardService.IsPlaced(attackPlacement))
            throw new InvalidAttackPlacementException(
                $"attack at {attackPlacement.Cell} has already been placed"
            );
        if (shipHit != null)
            _shipStatusService.RecordShipHit(shipHit);
        _attackBoardService.Place(attackPlacement);
    }

    public virtual void Save(ShipPlacement shipPlacement)
    {
        var isIntersecting = ShipPlacements.Any(s => s.IsIntersecting(shipPlacement));
        if (!shipPlacement.IsValid || _shipBoardService.IsPlaced(shipPlacement) || isIntersecting)
            throw new InvalidShipPlacementException("ship placement is invalid")
            {
                Data = { { nameof(ShipPlacement), shipPlacement } }
            };
        _shipBoardService.Place(shipPlacement);
    }

    public virtual void Initialize()
    {
        _attackBoardService.ClearBoard();
        _shipBoardService.ClearBoard();
        _shipStatusService.ResetStatuses();
    }
}
