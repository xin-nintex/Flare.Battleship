namespace Flare.Battleship.Domain.Battleships;

public record Ship(Guid Id, int Length, string Description)
{
    //Predefined ship
    public static Ship Battleship = new Ship(Guid.Parse("4453E93B-E2B2-4072-AED1-1F26F6F5DE0A"), 4, nameof(Battleship));
    public static Ship Carrier = new Ship(Guid.Parse("9D58A3F3-50E7-4284-861F-F8EA17ABA9B1"), 5, nameof(Carrier));
    public static Ship Cruiser = new Ship(Guid.Parse("1F9FFE10-018B-4C48-ADE0-ADFCF04A6558"), 3, nameof(Cruiser));
    public static Ship Submarine = new Ship(Guid.Parse("D26309B2-660E-488C-954A-7C0458ECB6C5"), 3, nameof(Submarine));
    public static Ship Destroyer = new Ship(Guid.Parse("4F102A97-82C1-4EEF-896B-ECDE8DA0B4D7"), 2, nameof(Destroyer));
}