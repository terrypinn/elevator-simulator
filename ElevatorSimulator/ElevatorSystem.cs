namespace ElevatorSimulator;

public class ElevatorSystem
{
    public readonly List<Floor> _floors = [];

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

    public void CallElevator(ElevatorRequest request)
    {
        // find closest floor with idle elevators or going in same direction
        var closestFloor = _floors
            .Where(f => f.HasIdleElevators() || f.HasMovingElevatorsTo(request.SourceFloor))
            .OrderBy(f => f.Difference(request.SourceFloor))
            .First();

        // move elevator to respective floor and load elevator
        this.MoveElevator(closestFloor, request.SourceFloor)
            .LoadElevator(request.Passengers);
    }

    public void DropPassengers(ElevatorRequest request)
    {
        // get floor passengers are on
        var currentFloor = _floors[request.SourceFloor];

        // move elevator to respective floor and unload elevator
        this.MoveElevator(currentFloor, request.DestinationFloor)
            .UnloadElevator(request.Passengers);
    }

    private Floor MoveElevator(Floor currentFloor, int destinationFloor)
    {
        // determine direction to move elevator
        var direction = currentFloor.GetDirection(destinationFloor);

        // select next floor to move elevator to
        var nextFloor = _floors[
            _floors.IndexOf(currentFloor) + (int)direction];

        // stop if elevator reached destination
        if (currentFloor.Equals(nextFloor))
            return currentFloor;

        // move elevator between floors
        currentFloor.MoveElevator(nextFloor, destinationFloor);

        // run again if elevator has not reached destination
        return this.MoveElevator(nextFloor, destinationFloor);
    }
}