using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HotelSimulatie.Utility;
using HotelEvents;

namespace HotelSimulatie.Areas
{
    /// <summary>
    /// Elevator area used to transport persons
    /// </summary>
    public class Elevator : TransportationArea
    {
        private bool[] _waitingFloors;
        /// <summary>
        /// the speed of the elevator
        /// </summary>
        public float Speed { get; set; }
        private float _movedDistance;
        /// <summary>
        /// the current floor of the elevator
        /// </summary>
        public int CurrentFloor { get; set; }
        /// <summary>
        /// The floor the elevator is moving towards
        /// </summary>
        public int DestinationFloor { get; set; }
        /// <summary>
        /// The state of the elevator, moving, stopped or starting
        /// </summary>
        public ElevatorState State;
        private float _passedTimeSinceUpdate;
        private List<IObserver> _observers;
        private bool _movingUp;
        /// <summary>
        /// The state of the elevator
        /// </summary>
        public enum ElevatorState
        {
            /// <summary>
            /// Can go to a different location the next update
            /// </summary>
            MOVING,
            /// <summary>
            /// Needs to start for next update
            /// </summary>
            STOPPED,
            /// <summary>
            /// can move again next update
            /// </summary>
            STARTING,
        }
        /// <summary>
        /// initialize the elevator
        /// </summary>
        public Elevator()
        {
            State = ElevatorState.STOPPED;
            AreaType = "Elevator";
            Speed = 1;
            CurrentFloor = 0;
            DestinationFloor = 0;
            Position = new Vector2(0, 0);
            _observers = new List<IObserver>();
            _movedDistance = 0;
            _movingUp = false;
            Weight = 2;

        }
        /// <summary>
        /// initializes the _waitingFloors array if it isn't initialized for the current elevator
        /// </summary>
        public void InitWaitingFloors()
        {
            if (_waitingFloors == null || _waitingFloors.Length != (int)Dimension.Y - (int)Position.Y + 1)
            {
                _waitingFloors = new bool[(int)Dimension.Y - (int)Position.Y + 1];
                for (int i = 0; i < _waitingFloors.Length; i++)
                {
                    _waitingFloors[i] = false;
                }
            }
        }
        /// <summary>
        /// lets a person register that he's waiting on a certain floor
        /// </summary>
        /// <param name="floor"></param>
        public void RegisterWaitingFloor(int floor)
        {
            _waitingFloors[floor] = true;
        }
        /// <summary>
        ///  calls the base draw and draws a sprite on the current floor
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Rectangle destination = new Rectangle((int)Position.X * Size.SCALE, -CurrentFloor * Size.SCALE, 1 * Size.SCALE, 1 * Size.SCALE);
            if (Sprites[10] != null)
            {
                spriteBatch.Draw(Sprites[10], destinationRectangle: destination);
            }
        }
        /// <summary>
        /// loads the elevator sprites
        /// </summary>
        /// <param name="Content"></param>
        override public void LoadContent(ContentManager Content)
        {
            Sprites.Add(1, Content.Load<Texture2D>("Elevator"));
            Sprites.Add(4, Content.Load<Texture2D>("Elevator"));
            Sprites.Add(7, Content.Load<Texture2D>("Elevator"));
            Sprites.Add(10, Content.Load<Texture2D>("ElevatorLocation"));
        }
        /// <summary>
        /// Calls the MoveTo function every time update
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _passedTimeSinceUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (_passedTimeSinceUpdate > 1 / HotelEventManager.HTE_Factor)
            {
                _passedTimeSinceUpdate -= 1 / HotelEventManager.HTE_Factor;
                Move();
            }
        }

        /// <summary>
        /// Makes the elevator move to the specified floor
        /// </summary>
        public void Move()
        {
            Notify();
            if (CurrentFloor == DestinationFloor && State == ElevatorState.MOVING)
            {
                //Check what direction has waiting persons
                if (_movingUp)
                {
                    for (int i = CurrentFloor; i < _waitingFloors.Count(); i++)
                    {
                        if (_waitingFloors[i])
                        {
                            DestinationFloor = i;
                            break;
                        }
                    }
                }
                if ((_movingUp && CurrentFloor == DestinationFloor) || !_movingUp)
                {
                    for (int i = CurrentFloor; i >= 0; i--)
                    {
                        if (_waitingFloors[i])
                        {
                            DestinationFloor = i;
                            break;
                        }
                    }
                }
                if ((!_movingUp && CurrentFloor == DestinationFloor) || _movingUp)
                {
                    for (int i = CurrentFloor; i < _waitingFloors.Count(); i++)
                    {
                        if (_waitingFloors[i])
                        {
                            DestinationFloor = i;
                            break;
                        }
                    }
                }
                if (CurrentFloor == DestinationFloor)
                {
                    DestinationFloor = (int)Math.Floor((double)((_waitingFloors.Count() - 1) / 2));
                }
            }
            int floorNumber = DestinationFloor;
            Speed = HotelEventManager.HTE_Factor / Size.SCALE;
            //elevator stands still for 1 HTE Time if stopped
            if (State == ElevatorState.STOPPED)
            {
                State = ElevatorState.STARTING;
                return;
            }
            if (State == ElevatorState.STARTING)
            {
                State = ElevatorState.MOVING;
                return;
            }

            //choose one of the move directions depending on the targetfloor
            if (floorNumber > CurrentFloor)
            {
                _movingUp = true;
                _movedDistance += Speed;
                if (_movedDistance > 1)
                {
                    Ascend(floorNumber);
                    _movedDistance = 0;
                }
            }
            else if (floorNumber < CurrentFloor)
            {
                _movingUp = false;
                _movedDistance -= Speed;
                if (_movedDistance < -1)
                {
                    Descend(floorNumber);
                    _movedDistance = 0;
                }
            }
            else
            {
                Wait(floorNumber);
            }
            //move one floor down
            void Descend(int num)
            {
                CheckFloor(num, CurrentFloor - 1);
            }
            //move one floor up
            void Ascend(int num)
            {
                CheckFloor(num, CurrentFloor + 1);
            }
            //wait on current floor
            void Wait(int num)
            {
                CheckFloor(num, CurrentFloor);
            }
            //Stops on the current floor and sets the waiting state of the floor to false
            void Stop(int num)
            {
                State = ElevatorState.STOPPED;
                _waitingFloors[num] = false;
                return;
            }
            //checks if the floor traveled to is a floor you have to wait on
            void CheckFloor(int num, int currentFloor)
            {
                CurrentFloor = currentFloor;
                Notify();
                if (_waitingFloors[currentFloor])
                {
                    Stop(num);
                }
            }
        }
        /// <summary>
        /// attatches the observer to the elevator
        /// </summary>
        /// <param name="observer"></param>
        public void Attach(IObserver observer)

        {

            _observers.Add(observer);

        }


        /// <summary>
        /// removes the observer from the elevatorobservers
        /// </summary>
        /// <param name="observer"></param>
        public void Detach(IObserver observer)

        {

            _observers.Remove(observer);

        }


        /// <summary>
        /// Notify all observers
        /// </summary>
        public void Notify()

        {

            foreach (IObserver observer in _observers)

            {

                observer.Update(this);

            }
            //Console.WriteLine("Notify");
        }
        /// <summary>
        /// Returns the elevator as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()

        {
            return AreaType + "\t" + "Speed: " + Speed + "\tCurrentFloor: " + CurrentFloor + "\tState: " + State;
        }
    }
}
