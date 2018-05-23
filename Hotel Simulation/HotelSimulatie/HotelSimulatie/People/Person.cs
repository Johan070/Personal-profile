using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HotelSimulatie.Utility;
using HotelEvents;
using HotelSimulatie.Areas;

namespace HotelSimulatie.People
{
    /// <summary>
    /// The persons in the hotel that move around and perform actions
    /// </summary>
    public abstract class Person : IPerson, IObserver
    {
        /// <summary>
        /// The identifier of the person
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The speed of the person
        /// </summary>
        public float WalkSpeed { get; set; }
        /// <summary>
        /// The current postion of the person
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The current destination of the person
        /// </summary>
        public Vector2 Destination { get; set; }
        /// <summary>
        /// The route from position to destination
        /// </summary>
        public Stack<Node> Route { get; set; }
        /// <summary>
        /// The sprite of the person
        /// </summary>
        public Texture2D Sprite { get; set; }
        /// <summary>
        /// The passed time since the last update of person
        /// </summary>
        protected float passedTimeSinceUpdate;
        /// <summary>
        /// The color of the person sprite, default white
        /// </summary>
        public Color CurrentColor { get; set; }
        private int _elevatorFloor;
        private float _elevatorRange;
        private bool _waitingForElevator;
        /// <summary>
        /// Initialize the person
        /// </summary>
        public Person()
        {
            _elevatorFloor = -1;
            _elevatorRange = -1;
            _waitingForElevator = false;
        }
        /// <summary>
        /// Draw the person
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Position != Destination && Position.X != _elevatorRange - 0.25)
            {
                Rectangle destination = new Rectangle((int)(Position.X * Size.SCALE), (int)(-Position.Y * Size.SCALE), Size.SCALE, Size.SCALE);
                spriteBatch.Draw(Sprite, destinationRectangle: destination, color: Color.White);
            }
        }
        /// <summary>
        /// Update the persons time and move the person if it's time for a new move
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            passedTimeSinceUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (passedTimeSinceUpdate > 1 / HotelEventManager.HTE_Factor)
            {
                passedTimeSinceUpdate -= 1 / HotelEventManager.HTE_Factor;
                Move();
            }

        }
        /// <summary>
        /// Person walks to the next node in his route. 
        /// </summary>
        public void Move()
        {
            Node nextNode = null;
            float step = 0;
            WalkSpeed = HotelEventManager.HTE_Factor / Size.SCALE;
            if (Route != null)
                if (Route != null && Route.Count > 0 && Position != Destination)
                {
                    nextNode = Route.Peek();
                    if (Position.X == _elevatorRange - 0.25)
                    {
                        if (Position.Y == _elevatorFloor)
                        {
                            Route.Pop();
                            nextNode = Route.Peek();
                            _waitingForElevator = false;
                        }
                        else
                        {
                            _waitingForElevator = true;
                            return;
                        }
                    }
                    if (Position.X < _elevatorRange && Position.X >= nextNode.Value.X)
                    {
                        if (Position.Y != _elevatorFloor)
                        {
                            _waitingForElevator = true;
                            return;
                        }
                        else
                        {
                            while (Route.Peek().Value.X < _elevatorRange)
                            {
                                nextNode = Route.Pop();
                            }
                            Position = new Vector2(_elevatorRange - 0.25f, nextNode.Value.Y);
                            Route.Push(nextNode);
                            _waitingForElevator = false;
                            return;
                        }
                    }

                    if (Position.X > nextNode.Value.X)
                    {
                        step = -WalkSpeed;
                    }
                    else if (Position.X < nextNode.Value.X)
                    {
                        step = WalkSpeed;
                    }
                    if (Position.X != nextNode.Value.X)
                    {
                        Position = new Vector2(Position.X + step, Position.Y);
                    }
                    if (Position.Y > nextNode.Value.Y)
                    {
                        step = -WalkSpeed;
                    }
                    else if (Position.Y < nextNode.Value.Y)
                    {
                        step = WalkSpeed;
                    }
                    if (Position.Y != nextNode.Value.Y)
                    {
                        Position = new Vector2(Position.X, Position.Y + step);
                    }
                    if (Position == nextNode.Value)
                    {
                        Route.Pop();
                    }

                }

        }
        /// <summary>
        /// Load the sprites of the person
        /// </summary>
        /// <param name="Content"></param>
        public abstract void LoadContent(ContentManager Content);
        /// <summary>
        /// Return the important information of the person as a string
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
        /// <summary>
        /// exchange information with the elevator
        /// </summary>
        /// <param name="elevator"></param>
        public void Update(Elevator elevator)

        {
            _elevatorFloor = elevator.CurrentFloor;
            _elevatorRange = elevator.Position.X + 0.25f;
            Console.WriteLine(elevator.CurrentFloor);
            if (_waitingForElevator)
            {
                elevator.RegisterWaitingFloor((int)this.Position.Y);
            }
        }
        /// <summary>
        /// Round your position to the neares valid position based on the hte factor
        /// </summary>
        public void RoundPosition()
        {
            Position = new Vector2((float)Math.Round(Position.X * HotelEventManager.HTE_Factor) / HotelEventManager.HTE_Factor, Position.Y);
        }

    }
}
