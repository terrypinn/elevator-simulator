using System.Net.WebSockets;
using ElevatorSimulator.Enums;

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
        // move elevator to source/calling floor
        // passengers load elevator
        // check passenger load does not exceed capactity - OverloadedExceptionx

        var elevator = FindNearestElevator(request);
    }

    private Elevator FindNearestElevator(ElevatorRequest request)
    {
        var direction = request.GetDirection();

        // find closest floor with idle elevators or going in same direction
        var closest = _floors
            .Where(f =>  f.HasIdleElevators() || f.HasMovingElevatorsIn(direction))
            .OrderBy(f => f.Difference(request.SourceFloor))
            .First();
        
        // select elevator going in same direction or take first idle elevator
        var elevator = closest.Elevators.FirstOrDefault(e => e.IsMovingIn(direction)) ?? 
            closest.Elevators.First();
       
       return elevator;
    }
}