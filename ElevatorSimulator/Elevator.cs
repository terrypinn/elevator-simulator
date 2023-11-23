using ElevatorSimulator.Enums;

namespace ElevatorSimulator;

public class Elevator
{
    private readonly int _id;
    private int _floor;
    private Direction _direction;
    private int _load;

    public Elevator(int id)
    {
        _id = id;
    }

    public int GetId() =>
        _id;

    public int GetFloor() =>
        _floor;

    public void SetFloor(int floor) =>
        _floor = floor;

    public void SetDirection(Direction direction) =>
        _direction = direction;

    public void Idle() =>
        _direction = Direction.None;

    public int GetLoad() =>
        _load;

    public void AddPassengers(int load) =>
        _load += load;

    public void RemovePassengers(int load) =>
        _load -= load;

    public bool IsIdle() =>
        _direction == Direction.None;

    public bool IsMovingIn(Direction direction) =>
        _direction == direction;

    public override string ToString() =>
        $"id:{_id} | floor:{_floor} | state:{_direction} | load:{_load}";
}