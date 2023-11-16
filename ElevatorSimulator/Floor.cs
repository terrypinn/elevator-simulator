using ElevatorSimulator.Enums;

namespace ElevatorSimulator;

public class Floor
{
    public int Number { get; set; }
    public List<Elevator> Elevators { get; set; } = [];

    public Floor(int number)
    {
        this.Number = number;
    }

    public bool HasIdleElevators() =>
        this.Elevators.Any(e => e.IsIdle());

    // public bool HasMovingElevatorsIn(Direction direction) =>
    //     Elevators.Any(e => e.IsMovingIn(direction));

    public bool HasMovingElevatorsTo(int sourceFloor) =>
        this.Elevators.Any(e => e.IsMovingIn(this.GetDirection(sourceFloor)));

    public Direction GetDirection(int sourceFloor) =>
        this.Number == sourceFloor
        ? Direction.None : this.Number < sourceFloor
        ? Direction.Up : Direction.Down;

    // select elevator going in same direction or take first idle elevator
    // todo: how to handle more than 1 elevators travelling in the same direction
    public Elevator SelectElevator(int sourceFloor) =>
        Elevators.FirstOrDefault(e => e.IsMovingIn(this.GetDirection(sourceFloor)))
            ?? this.Elevators.First(e => e.IsIdle());

    // todo: does not handle negative numbers
    public int Difference(int other) =>
        Math.Abs(Number - other);

    public void MoveElevator(Floor nextFloor, Elevator elevator)
    {
        this.Elevators.Remove(elevator);
        nextFloor.Elevators.Add(elevator);
    }

    public override string ToString() =>
        $"floor:{Number} | elevators:{Elevators.Count}";
}