using ElevatorSimulator.Enums;

namespace ElevatorSimulator;

public class Elevator
{
    private int _id;
    private int _currentFloor;
    private Direction _direction;
    private int _load;

    public Elevator(int id)
    {
        _id = id;
    }

    public bool IsIdle() =>
        _direction == Direction.None;

    public bool IsMovingIn(Direction direction) =>
        _direction == direction;

    public override string ToString() =>
        $"id:{_id} | floor:{_currentFloor} | state:{_direction} | load:{_load}";
}