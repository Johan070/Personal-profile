using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HotelSimulatie.People;
using HotelSimulatie.Utility;

namespace HotelSimulatie.Areas.Tests
{
    [TestClass()]
    public class ElevatorTests
    {
        Elevator elevator;

        [TestInitialize()]
        public void Testinit()
        {
            elevator = new Elevator();
        }
        [TestMethod()]
        public void MoveTest_MovingUPOnce()
        {
            elevator.Position = new Vector2(0, 0);
            elevator.CurrentFloor = (int)elevator.Position.Y;
            elevator.State = Elevator.ElevatorState.MOVING;
            elevator.DestinationFloor = 1;
            elevator.Move();
            Assert.IsFalse(elevator.CurrentFloor > 0);
        }
        [TestMethod()]
        public void MoveTest_MovingUPRepeat()
        {
            elevator.Position = new Vector2(0, 0);
            elevator.Dimension = new Vector2(1, 2);
            elevator.CurrentFloor = (int)elevator.Position.Y;
            elevator.State = Elevator.ElevatorState.MOVING;
            Customer customer = new Customer();
            elevator.Attach(customer);
            customer.Position = new Vector2(0.1f, 0);
            customer.Destination = new Vector2(0, 1);
            customer.Route = new Stack<Node>();
            customer.Route.Push(new Node(customer.Destination));
            customer.Update(elevator);

            elevator.InitWaitingFloors();
            while (elevator.CurrentFloor != 1)
                elevator.Move();
            Assert.IsTrue(elevator.CurrentFloor > 0);
        }

        [TestMethod()]
        public void MoveTest_MovingDownOnce()
        {
            elevator.Position = new Vector2(0, 0);
            elevator.CurrentFloor = (int)elevator.Position.Y;
            elevator.State = Elevator.ElevatorState.MOVING;
            elevator.DestinationFloor = -1;
            elevator.Move();
            Assert.IsFalse(elevator.CurrentFloor < 0);
        }
        [TestMethod()]
        public void MoveTest_MovingDownRepeat()
        {
            elevator.Position = new Vector2(0, 0);
            elevator.Dimension = new Vector2(1, 7);
            elevator.CurrentFloor = 5;
            elevator.DestinationFloor = 5;
            elevator.State = Elevator.ElevatorState.MOVING;
            elevator.InitWaitingFloors();

            while (elevator.CurrentFloor != 3)
            {
                elevator.Move();
            }
            Assert.IsTrue(elevator.CurrentFloor == 3);
        }
    }
}