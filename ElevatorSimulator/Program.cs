using ElevatorSimulator;

var system = new ElevatorSystem(5, 3);

var request = new ElevatorRequest(1, 4, 2);
system.CallElevator(request);
system.DropPassengers(request);
