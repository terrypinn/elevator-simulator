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
}