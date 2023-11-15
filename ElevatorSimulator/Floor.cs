using ElevatorSimulator.Enums;

namespace ElevatorSimulator;

public class Floor
{
    public int Number { get; set; }
    public List<Elevator> Elevators { get; set; } = [];

    public Floor(int number)
    {
        Number = number;
    }

    public override string ToString() =>
        $"floor:{Number} | elevators:{Elevators.Count}";
}