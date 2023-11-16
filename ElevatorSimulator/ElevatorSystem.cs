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
    public void FindAndMoveElevator(int floorNumber)
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
        this.FindAndMoveElevator(floorNumber);
    }
}