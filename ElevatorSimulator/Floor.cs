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

    public bool HasMovingElevatorsTo(int floorNumber) =>
        this.Elevators.Any(e => e.IsMovingIn(this.GetDirection(floorNumber)));

    public Direction GetDirection(int floorNumber) =>
        this.Number == floorNumber
        ? Direction.None : this.Number < floorNumber
        ? Direction.Up : Direction.Down;

    // select elevator going in same direction or take first idle elevator
    // todo: how to handle more than 1 elevators travelling in the same direction
    public Elevator SelectElevator(int floorNumber) =>
        this.Elevators.FirstOrDefault(e => e.IsMovingIn(this.GetDirection(floorNumber)))
            ?? this.Elevators.First(e => e.IsIdle());

    // todo: does not handle negative numbers
    public int Difference(int other) =>
        Math.Abs(Number - other);

    public void MoveElevator(Floor nextFloor, int destinationFloor)
    {
        // find available elevator
        var elevator = this.SelectElevator(destinationFloor);

        // update elevator status
        elevator.SetFloor(nextFloor.Number);
        elevator.SetDirection(this.GetDirection(nextFloor.Number));

        // move elevator between floors
        this.Elevators.Remove(elevator);
        nextFloor.Elevators.Add(elevator);

        // idle elevator once it has reached destination
        elevator.SetDirection(nextFloor.GetDirection(destinationFloor));
    }

    // todo: need to confirm available elevator on floor
    public void LoadElevator(int load) =>
        this.SelectElevator(this.Number).AddPassengers(load);

    public void UnloadElevator(int load) =>
       this.SelectElevator(this.Number).RemovePassengers(load);

    public override string ToString() =>
        $"floor:{Number} | elevators:{Elevators.Count}";
}