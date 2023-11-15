namespace ElevatorSimulator;

public class ElevatorSystem
{
    private List<Floor> _floors = [];

    public ElevatorSystem(int floors, int elevators)
    {
        for (int i = 0; i < floors; i++)
            _floors.Add(new Floor(i));

        for (int i = 0; i < elevators; i++)
            _floors[0].Elevators.Add(new Elevator(i + 1)); // start all elevators on the ground floor
    }

    public void QueueRequest(ElevatorRequest request)
    {
        // Continue travelling in the same direction while there are remaining requests in that same direction.
        // If there are no further requests in that direction, then stop and become idle, or change direction if there are requests in the opposite direction. 
        
        // find nearest available elevator

        // find closest floor with idle elevators or going in same direction
        var closestFloor =  _floors 
            .Where(f =>  f.HasIdleElevators() || f.HasMovingElevatorsTo(request.SourceFloor))
            .OrderBy(f => f.Difference(request.SourceFloor))
            .First();

        var elevator =  closestFloor.SelectElevator(request.SourceFloor);
    }
}