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

    public bool HasIdleElevators() =>
        Elevators.Any(e => e.IsIdle());

    public bool HasMovingElevatorsIn(Direction direction) =>
        Elevators.Any(e => e.IsMovingIn(direction));

    public int Difference(int other) =>
        Math.Abs(Number - other);

    public override string ToString() =>
        $"floor:{Number} | elevators:{Elevators.Count}";
}