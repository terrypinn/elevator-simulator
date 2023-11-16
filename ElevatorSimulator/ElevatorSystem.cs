namespace ElevatorSimulator;

public class ElevatorSystem
{
    private readonly List<Floor> _floors = [];

    public ElevatorSystem(int floors, int elevators)
    {
        for (int i = 0; i < floors; i++)
            _floors.Add(new Floor(i));

        // start all elevators on the ground floor
        for (int i = 0; i < elevators; i++)
            _floors[0].Elevators.Add(new Elevator(i + 1));
    }

    // Continue travelling in the same direction while there are remaining 
    //  requests in that same direction.
    // If there are no further requests in that direction, then stop and
    //  become idle, or change direction if there are requests in the opposite 
    //  direction. 
    public void CallElevatorTo(int floorNumber)
    {
        // find closest floor with idle elevators or going in same direction
        var closestFloor = _floors
            .Where(f => f.HasIdleElevators() ||
                        f.HasMovingElevatorsTo(floorNumber))
            .OrderBy(f => f.Difference(floorNumber))
            .First();

        // determine direction to move elevator
        var direction = closestFloor.GetDirection(floorNumber);

        // select next floor to move elevator to
        var nextFloor = _floors[
            _floors.IndexOf(closestFloor) + (int)direction];

        // stop if elevator reached destination
        if (closestFloor.Equals(nextFloor))
            return;

        // find available elevator
        var elevator = closestFloor.SelectElevator(floorNumber);

        // set elevator direction it is moving in
        elevator.SetDirection(direction);

        // move elevator between floors
        closestFloor.MoveElevator(nextFloor, elevator);

        // idle elevator once it has reached destination
        if (_floors[floorNumber].Elevators.Contains(elevator))
            elevator.Idle();
        // elevator.SetDirection(Direction.None);

        // run again if elevator has not reached destination
        this.CallElevatorTo(floorNumber);
    }

    public void MoveElevatorTo(ElevatorRequest request)
    {
        // get idle elevator
        var elevator = _floors[request.SourceFloor].GetIdleElevator();

        // load passengers into elevator 
        elevator.AddPassengers(request.Passengers);

        // move elevator to destination floor
        this.MoveElevator(elevator, request.DestinationFloor);

        // unload passengers
        elevator.RemovePassengers(request.Passengers);
    }

    private void MoveElevator(Elevator elevator, int destinationFloor)
    {
        // find floor elevator is on
        var currentFloor = _floors.First(f => f.Elevators.Contains(elevator));

        // determine direction to move elevator
        var direction = currentFloor.GetDirection(destinationFloor);

        // select next floor to move elevator to
        var nextFloor = _floors[
            _floors.IndexOf(currentFloor) + (int)direction];

        // stop if elevator reached destination
        if (currentFloor.Equals(nextFloor))
            return;

        // set elevator direction it is moving in
        elevator.SetDirection(direction);

        // move elevator between floors
        currentFloor.MoveElevator(nextFloor, elevator);

        // idle elevator once it has reached destination
        if (_floors[destinationFloor].Elevators.Contains(elevator))
            elevator.Idle();

        // run again if elevator has not reached destination
        this.MoveElevator(elevator, destinationFloor);
    }
}