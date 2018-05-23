using HotelSimulatie.People;
using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using HotelEvents;
using HotelSimulatie.Areas;

namespace HotelSimulatie
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HotelSimulation : Game
    {
        private EventListener listener;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Hotel _hotel;
        private Camera2D _camera;
        private List<IPerson> _persons;
        private Reception _reception;
        private SimplePath _simplePath;
        private Matrix viewMatrix;
        private List<Cleaner> _cleaners;
        private List<Customer> _customers;
        private Cleaner _cleaner;
        private Cleaner _cleaner2;
        private Cinema _huidigeCinema;
        private EventChecker _eventChecker;
        private MenuForm _menuForm;
        //pause
        private bool _isPaused;
        private Texture2D _pauseMenuTexture;
        private Rectangle _pauseMenuRectangle;

        private Elevator _elevator;
        private Lobby _lobby;
        private Stairs _stairs;
        private Queue<Room> _roomQueue;
        /// <summary>
        /// imitialize hotelsimulation
        /// </summary>
        public HotelSimulation()
        {
            _roomQueue = new Queue<Room>();
            _eventChecker = new EventChecker();
            _huidigeCinema = new Cinema();
            _customers = new List<Customer>();
            _reception = new Reception();
            _hotel = new Hotel();
            _simplePath = new SimplePath();
            listener = new EventListener();
            HotelEventManager.Register(listener);
            HotelEventManager.HTE_Factor = 4;
            HotelEventManager.Start();
            graphics = new GraphicsDeviceManager(this);
            //uncomment for fullscreen
            //graphics.IsFullScreen = true;
            //graphics.HardwareModeSwitch = false;
            _isPaused = false;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _camera = new Camera2D(GraphicsDevice.Viewport);
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IsMouseVisible = true;

            _pauseMenuTexture = Content.Load<Texture2D>("PauseTexture");
            _pauseMenuRectangle = new Rectangle(0, 0, _pauseMenuTexture.Width, _pauseMenuTexture.Height);
            _cleaner = new Cleaner() { Position = new Vector2(5, 2), VasteLocatie = new Vector2(5, 2) };
            _cleaner2 = new Cleaner() { Position = new Vector2(5, 5), VasteLocatie = new Vector2(5, 5) };
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _persons = new List<IPerson>();
            _cleaners = new List<Cleaner>();
            _hotel.Areas = new LayoutReader().GetAllObjects(@"Content\Hotel3.layout");
            AreaFactory areaFactory = new AreaFactory();
            _elevator = (Elevator)areaFactory.Create("Elevator");
            _stairs = (Stairs)areaFactory.Create("Stairs");
            _lobby = (Lobby)areaFactory.Create("Lobby");
            _elevator.Dimension = new Vector2(1, _hotel.Areas.OrderByDescending(x => x.Dimension.Y + x.Position.Y).First().Dimension.Y + _hotel.Areas.OrderByDescending(x => x.Dimension.Y + x.Position.Y).First().Position.Y);
            _stairs.Position = new Vector2(_hotel.Areas.OrderByDescending(x => x.Position.X + x.Dimension.X).First().Dimension.X + _hotel.Areas.OrderByDescending(x => x.Position.X + x.Dimension.X).First().Position.X, 0);
            _stairs.Dimension = new Vector2(1, _hotel.Areas.OrderByDescending(x => x.Dimension.Y + x.Position.Y).First().Dimension.Y + _hotel.Areas.OrderByDescending(x => x.Dimension.Y + x.Position.Y).First().Position.Y);
            _lobby.Dimension = new Vector2(_stairs.Position.X - 1, 1);
            _hotel.Areas.Add(_elevator);
            _hotel.Areas.Add(_stairs);
            _hotel.Areas.Add(_lobby);
            _persons.Add(_cleaner);
            _persons.Add(_cleaner2);
            _cleaners.Add(_cleaner);
            _cleaners.Add(_cleaner2);
            foreach (IArea area in _hotel.Areas)
            {
                area.LoadContent(Content);
            }
            _hotel.Load();
            _hotel.AddToGraph(_simplePath);
            PeopleFactory peopleFactory = new PeopleFactory();
            // nog even uitzoeken waar dit het best kan
            foreach (IPerson person in _persons)
            {
                person.LoadContent(Content);
            }
            _elevator.Attach(_cleaner);
            _elevator.Attach(_cleaner2);
            _menuForm = new MenuForm(_hotel, _cleaners, _customers, _persons, _stairs, _simplePath);
            _menuForm.Show();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            _elevator.Detach(_cleaner);
            _elevator.Detach(_cleaner2);
            HotelEventManager.Stop();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                _isPaused = false;
                _menuForm.Visible = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) && !_isPaused)
            {
                _isPaused = true;
                _menuForm.Visible = true;
                _menuForm.ReInitListboxes();
                if (HotelEventManager.Running)
                {
                    HotelEventManager.Pauze();
                }
            }
            if (_isPaused)
            {
                _isPaused = _menuForm.Visible;
            }
            else
            {
                if (HotelEventManager.Running)
                {
                    HotelEventManager.Pauze();
                }
                ReloadAfterForm();

                _menuForm.Visible = false;
                _eventChecker.CheckEvents(_simplePath, _hotel, _persons, _reception, _customers, listener, _lobby, _elevator, _cleaner, _cleaners, gameTime, _roomQueue);

                _cleaner.SearchRoom(_hotel, _roomQueue);
                _cleaner2.SearchRoom(_hotel, _roomQueue);

                _cleaner.GetRoom(gameTime, _simplePath, _roomQueue);
                _cleaner2.GetRoom(gameTime, _simplePath, _roomQueue);

                _roomQueue.Clear();

                foreach (IPerson person in _persons)
                {
                    if (person.Sprite == null)
                    {
                        person.LoadContent(Content);
                    }
                    person.Update(gameTime);
                }
                _reception.HelpQueue(_hotel, _simplePath);
                _elevator.InitWaitingFloors();
                _elevator.Update(gameTime);

                _huidigeCinema.CheckCinema(gameTime, _hotel, _huidigeCinema, _simplePath, _customers);

                if (_customers.Count != 0)
                {
                    foreach (Customer customer in _customers)
                    {
                        customer.ReturnToRoom(gameTime, _simplePath, _hotel);
                        customer.CheckCinema(_simplePath, _huidigeCinema);
                    }
                }
                foreach (Person person in _persons)
                {
                    if (person.Position != null && person.Destination != null && person.Route != null)
                    {
                        if (person.Position != person.Destination && person.Route.Count == 0)
                        {
                            person.Route = _simplePath.GetRoute(new Vector2((float)Math.Round(person.Position.X, 0), (float)Math.Round(person.Position.Y, 0)), person.Destination);
                        }
                    }
                }
                base.Update(gameTime);
                if (!HotelEventManager.Running)
                {
                    HotelEventManager.Pauze();
                }
            }

        }


        private void ReloadAfterForm()
        {
            _stairs = _menuForm.Stairs;
            _simplePath = _menuForm.SimplePath;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            IArea bottomRight = _hotel.Areas.OrderBy(x => x.Position.Y).OrderByDescending(x => x.Position.X).First();
            //FIXME: less hacky code
            //Focus camera on the middle of where the hotel is gonna be
            //waar komt die laatste vector vandaan?
            //camera ergens anders plaatsen?
            //_camera.Position = new Vector2(bottomRight.Position.X, -bottomRight.Position.Y) / 2f - _camera.Origin
            //float zoomX = GraphicsDevice.Viewport.Width / ((bottomRight.Position.X + bottomRight.Dimension.X));
            //float zoomY = GraphicsDevice.Viewport.Height / ((bottomRight.Position.Y + bottomRight.Dimension.Y));
            //_camera.Zoom = new Vector2(zoomX, zoomY);
            _camera.Position = new Vector2(Size.SCALE * bottomRight.Position.X, -Size.SCALE * bottomRight.Position.Y) / 2f - _camera.Origin - new Vector2(0, 150);
            viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);
            foreach (IArea area in _hotel.Areas)
            {
                area.Draw(spriteBatch);
            }
            spriteBatch.End();

            foreach (Person person in _persons)
            {
                spriteBatch.Begin(transformMatrix: viewMatrix);
                if (person.Sprite != null)
                    person.Draw(spriteBatch);
                spriteBatch.End();
            }

            // TODO: draw persons
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

