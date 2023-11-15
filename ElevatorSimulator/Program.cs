using ElevatorSimulator;

var system = new ElevatorSystem(5, 3);

var request = new ElevatorRequest(sourceFloor: 1, destinationFloor: 4, passengers: 2);

system.QueueRequest(request);