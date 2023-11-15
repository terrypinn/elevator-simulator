using ElevatorSimulator.Enums;

namespace ElevatorSimulator;

public class ElevatorRequest
{
    public int SourceFloor { get; }
    public int DestinationFloor { get; }
    public int Passengers { get; }

    public ElevatorRequest(int sourceFloor, int destinationFloor, int passengers)
    {
        SourceFloor = sourceFloor;
        DestinationFloor = destinationFloor;
        Passengers = passengers;
    }

    public Direction GetDirection() =>
        SourceFloor < DestinationFloor
        ? Direction.Up
        : Direction.Down;
}