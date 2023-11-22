using ElevatorSimulator.Enums;

namespace ElevatorSimulator;

public class Floor
{
    public int Number { get; }
    public List<Elevator> Elevators { get; } = [];

    public Floor(int number)
    {
        this.Number = number;
    }

    public bool HasIdleElevators() =>
        this.Elevators.Any(e => e.IsIdle());

    public bool HasMovingElevatorsTo(Floor floor) =>
        this.Elevators.Any(e => e.IsMovingIn(this.GetDirection(floor)));

    private Direction GetDirection(Floor other) =>
        this.Number == other.Number
        ? Direction.None : this.Number < other.Number
        ? Direction.Up : Direction.Down;

    // select elevator going in same direction or take first idle elevator
    // todo: how to handle more than 1 elevators travelling in the same direction
    private Elevator SelectElevator(Floor floor) =>
        this.Elevators.FirstOrDefault(e => e.IsMovingIn(this.GetDirection(floor)))
            ?? this.Elevators.First(e => e.IsIdle());

    // todo: does not handle negative numbers
    public int Difference(Floor other) =>
        Math.Abs(Number - other.Number);

    public void MoveElevator(Floor nextFloor, Floor destinationFloor)
    {
        // find available elevator
        Elevator elevator = this.SelectElevator(destinationFloor);

        // update elevator status
        elevator.SetFloor(nextFloor.Number);
        elevator.SetDirection(this.GetDirection(nextFloor));

        // move elevator between floors
        this.Elevators.Remove(elevator);
        nextFloor.Elevators.Add(elevator);

        // idle elevator once if it has reached destination
        elevator.SetDirection(nextFloor.GetDirection(destinationFloor));
    }

    // todo: need to confirm available elevator on floor
    public void LoadElevator(int load) =>
        this.SelectElevator(this).AddPassengers(load);

    public void UnloadElevator(int load) =>
       this.SelectElevator(this).RemovePassengers(load);

    public int GetNextFloorNumber(Floor destinationFloor) =>
        this.Number + (int)this.GetDirection(destinationFloor);

    public override string ToString() =>
        $"floor:{Number} | elevators:{Elevators.Count}";
}