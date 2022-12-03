using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Services.Internals;

namespace Flare.Battleship;

public class GameContext
{
    private readonly ITrackingService<ShipPlacement> _shipTrackingService;
    private readonly ITrackingService<AttackPlacement> _attackTrackingService;

    public GameContext(ITrackingService<ShipPlacement> shipTrackingService, ITrackingService<AttackPlacement> attackTrackingService)
    {
        _shipTrackingService = shipTrackingService;
        _attackTrackingService = attackTrackingService;
    }
    public void OccupyCellByAttack(AttackPlacement attackPlacement)
    {
        if (_attackTrackingService.IsPlaced(attackPlacement))
            throw new AttackAlreadyPlacedException($"attack at {attackPlacement.Cell} has already been placed");
        _attackTrackingService.Track(attackPlacement);
    }

    public void OccupyCellByShip(ShipPlacement shipPlacement)
    {
        if (_shipTrackingService.IsPlaced(shipPlacement))
            throw new ShipAlreadyPlaceException($"{shipPlacement.Ship} has already been placed");
        _shipTrackingService.Track(shipPlacement);
    }
}