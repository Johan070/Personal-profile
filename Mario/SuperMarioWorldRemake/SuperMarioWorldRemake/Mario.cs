using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    public class Mario : MovingGameObject
    {
        //scoreboard is player bound, every player can have its own scoreboard
        public ScoreBoard sc;
        //public for testing purposes
        public float maxJumpSpeed;
        private float _beginSpeed;
        //public for testing purposes
        public float jumpSpeed;
        private bool _canJump;
        private bool _prevJumpState;
        private bool _jumping;
        public Texture2D LeftTexture { get; set; }
        public Texture2D RightTexture { get; set; }
        public Texture2D FallingTexture { get; set; }
        public Texture2D FallingTextureLeft { get; set; }
        public Texture2D BigFallingTexture { get; set; }
        public Texture2D BigFallingTextureLeft { get; set; }
        public Texture2D SmallFallingTexture { get; set; }
        public Texture2D SmallFallingTextureLeft { get; set; }
        public Texture2D WalkingTexture { get; set; }
        public Texture2D SmallWalkingTexture { get; set; }
        public Texture2D BigWalkingTexture { get; set; }
        public Texture2D SmallWalkingTextureLeft { get; set; }
        public Texture2D BigWalkingTextureLeft { get; set; }
        public Texture2D WalkingTextureLeft { get; set; }
        public Texture2D JumpTexture { get; set; }
        public Texture2D JumpTextureLeft { get; set; }
        public Texture2D BigJumpTexture { get; set; }
        public Texture2D BigJumpTextureLeft { get; set; }
        public Texture2D SmallJumpTexture { get; set; }
        public Texture2D SmallJumpTextureLeft { get; set; }
        public Texture2D StoopTexture { get; set; }
        public Texture2D StoopTextureLeft { get; set; }

        override
       public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(size * Position.X), (int)(size * Position.Y), ObjectTexture.Width / Columns, ObjectTexture.Height / Rows);
            }
        }
        private bool textureDirection;
        public int jumpframes;
        public Mario(Texture2D walkingTextureLeft, Texture2D walkingTextureRight, Texture2D fallingTextureLeft, Texture2D fallingTexture, Texture2D jumpTextureLeft, Texture2D jumpTexture, Texture2D stoopTextureLeft, Texture2D stoopTexture, Texture2D walkingTextureBig, Texture2D walkingTextureBigLeft, Texture2D marioTextureBigFallLeft, Texture2D marioTextureBigFallRight, Texture2D marioTextureBigJumpLeft, Texture2D marioTextureBigJump, int posX, int posY)
        {
            Stage = 0;
            sc = new ScoreBoard();
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = walkingTextureRight;
            FallingTexture = fallingTexture;
            FallingTextureLeft = fallingTextureLeft;
            LeftTexture = walkingTextureLeft;
            WalkingTextureLeft = walkingTextureLeft;
            WalkingTexture = walkingTextureRight;
            RightTexture = walkingTextureRight;
            StoopTexture = stoopTexture;
            StoopTextureLeft = stoopTextureLeft;
            JumpTexture = jumpTexture;
            JumpTextureLeft = jumpTextureLeft;
            BigWalkingTexture = walkingTextureBig;
            SmallWalkingTexture = walkingTextureRight;
            BigWalkingTextureLeft = walkingTextureBigLeft;
            SmallWalkingTextureLeft = walkingTextureLeft;
            BigFallingTexture = marioTextureBigFallRight;
            BigFallingTextureLeft = marioTextureBigFallLeft;
            SmallFallingTexture = fallingTexture;
            SmallFallingTextureLeft = fallingTextureLeft;
            BigJumpTexture = marioTextureBigJump;
            BigJumpTextureLeft = marioTextureBigJumpLeft;
            SmallJumpTexture = jumpTexture;
            SmallJumpTextureLeft = jumpTextureLeft;
            textureDirection = false;
            Rows = 1;
            Columns = 2;
            onGround = false;
            _canJump = true;
            totalFrames = Rows * Columns;
            millisecondsPerFrame = 100;
            fallSpeed = 0f;
            jumpSpeed = 0f;
            maxFallSpeed = 0.5f;
            maxJumpSpeed = -0.8f;
            speed = 0f;
            maxSpeed = 0.25f;
            forcedJump = false;
            _prevJumpState = false;
            lostLife = false;
        }
        /// <summary>
        /// Decreases mario's life total by 1
        /// </summary>
        public override void Die()
        {
            sc.UpdateLives(-1);
        }
        /// <summary>
        /// On addition of a stage with heavy wind or something similar this can be used for forced movements of mario
        /// </summary>
        public override void Move()
        {
        }
        /// <summary>
        /// This method specifies the movement of mario based on the player input
        /// </summary>
        /// <param name="keyState"></param>
        /// <param name="prevKeyState"></param>
        public void Move(KeyboardState keyState, KeyboardState prevKeyState)
        {
            UpdateTextures();
            if (keyState.IsKeyDown(Keys.Right))
            {
                MoveRight();
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                MoveLeft();
            }
            else
            {
                speed -= speed * 0.1f;
                currentFrame = 0;
            }
            if (forcedJump || (keyState.IsKeyDown(Keys.Up) && _canJump)||(_jumping))
            {
                if (prevKeyState.IsKeyUp(Keys.Up) && !forcedJump)
                {
                    _beginSpeed = speed;
                    if (_beginSpeed < 0)
                        maxJumpSpeed += 2f * _beginSpeed;
                    else if (_beginSpeed > 0)
                        maxJumpSpeed -= 2f * _beginSpeed;
                }
                if (forcedJump && !_prevJumpState)
                {
                    maxJumpSpeed = -1.0f;
                }
                Jump();
                Position.Y += jumpSpeed;
            }

            else if (!onGround)
            {
                Fall();
            }
            else
            {
                _canJump = true;
                fallSpeed = 0;
                maxJumpSpeed = -0.8f;
                jumpSpeed = 0;
                LeftTexture = WalkingTextureLeft;
                RightTexture = WalkingTexture;
                Rows = 1;
                if (Stage == 0)
                    Columns = 2;
                if (Stage == 1)
                    Columns = 3;
            }
            _prevJumpState = forcedJump;
            SetObjectTexture();
            Position.X += speed;
            Position.Y += fallSpeed;
        }
        /// <summary>
        /// selects the correct textures based on mario's stage
        /// </summary>
        private void UpdateTextures()
        {
            if (this.Stage == 0)
            {
                WalkingTextureLeft = SmallWalkingTextureLeft;
                WalkingTexture = SmallWalkingTexture;
                FallingTexture = SmallFallingTexture;
                FallingTextureLeft = SmallFallingTextureLeft;
                JumpTexture = SmallJumpTexture;
                JumpTextureLeft = SmallJumpTextureLeft;
            }
            else if (this.Stage == 1)
            {
                WalkingTextureLeft = BigWalkingTextureLeft;
                WalkingTexture = BigWalkingTexture;
                FallingTexture = BigFallingTexture;
                FallingTextureLeft = BigFallingTextureLeft;
                JumpTexture = BigJumpTexture;
                JumpTextureLeft = BigJumpTextureLeft;
            }
        }
        // public for testing T_T
        /// <summary>
        /// Specifies mario's way of moving to the right
        /// </summary>
        public void MoveRight()
        {
            if (speed < maxSpeed)
            {
                speed += 0.004f + speed * 0.001f;
                LeftTexture = WalkingTextureLeft;
                RightTexture = WalkingTexture;
                if (this.Stage == 0)
                {
                    Rows = 1;
                    Columns = 2;
                }
                else if (this.Stage == 1)
                {

                    Rows = 1;
                    Columns = 3;
                }
                textureDirection = false;
            }
        }
       // public for testing
       /// <summary>
       /// specifies mario's way of moving to the left
       /// </summary>
        public void MoveLeft()
        {
            if (speed > -maxSpeed)
            {
                speed -= 0.004f + speed * 0.001f;
                LeftTexture = WalkingTextureLeft;
                if (this.Stage == 0)
                {
                    Rows = 1;
                    Columns = 2;
                }
                else if (this.Stage == 1)
                {
                    Rows = 1;
                    Columns = 3;
                }
                textureDirection = true;
            }
        }
        //public for testing
        /// <summary>
        /// If mario is not in contact with ground this method could be used to make mario fall, if mario's location is below the map mario dies
        /// </summary>
        public override void Fall()
        {
            if (fallSpeed < maxFallSpeed)
            {
                fallSpeed += 0.01f + fallSpeed * 0.04f;
                Rows = 1;
                Columns = 1;
                LeftTexture = FallingTextureLeft;
                RightTexture = FallingTexture;
                currentFrame = 0;
            }
            if (Position.Y >= 25)
            {
                Die();
                lostLife = true;
                UpdatePosition(1, 20);
            }
        }
        //public for testing purposes
        /// <summary>
        /// Mario's jump decreases by 20% every time this method gets called
        /// </summary>
        public void Jump()
        {
            if (!_jumping)
            {
                _jumping = true;
            }
            jumpSpeed = maxJumpSpeed;
            maxJumpSpeed -= 0.2f * maxJumpSpeed;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            LeftTexture = JumpTextureLeft;
            RightTexture = JumpTexture;
            _canJump = false;
            forcedJump = false;
            if (jumpSpeed > -0.2f || Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                _jumping = false;
            }
        }
        /// <summary>
        /// sets the texture based on the direction mario is facing
        /// </summary>
        public void SetObjectTexture()
        {
            if (textureDirection == true)
            {
                ObjectTexture = LeftTexture;
            }
            else
            {
                ObjectTexture = RightTexture;
            }
        }

        //not yet used
        public void Stoop()
        {
            currentFrame = 0;
            Rows = 1;
            Columns = 1;
            RightTexture = StoopTexture;
            LeftTexture = StoopTextureLeft;
            //disable movement
        }

        /// <summary>
        /// A slightly different draw for mario, works mostly the same as GameObject.Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        override public void Draw(SpriteBatch spriteBatch)
        {
            int width = ObjectTexture.Width / Columns;
            int height = ObjectTexture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            //-size+height omdat marios begin draw positie een paar pixels hoger dan de 16 pixels start en rectangles geen floats accepteren waardoor did na afronding anders fout gaat
            Rectangle destinationRectangle = new Rectangle((int)(size * Position.X), (int)(size * Position.Y) - 4, width, height);
            spriteBatch.Draw(ObjectTexture, destinationRectangle, sourceRectangle, Color.White);
        }
        /// <summary>
        /// Gets the position on the screen multiplied by size which is currently 16
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return size * Position;
        }
        /// <summary>
        /// checks the collisions between mario and all objects in the 2D array level
        /// </summary>
        /// <param name="level"></param>
        public override void checkCollisions(GameObject[,] level)
        {
            //higer numbers to make sure large bounding boxes get checked
            int minBoundaryX = 0;
            int minBoundaryY = 0;
            int maxBoundaryX = level.GetLength(0);
            int maxBoundaryY = level.GetLength(1);
            bool noCollisions = true;
            for (int i = minBoundaryX; i <= maxBoundaryX; i++)
            {
                for (int j = minBoundaryY; j <= maxBoundaryY; j++)
                {
                    if (i >= 0 && i <= level.GetLength(0) - 1 && j >= 0 && j <= level.GetLength(1) - 1)
                        if (level[i, j] != null)
                        {
                            bool col = level[i, j].Colliding(this);
                            if (col == true)
                            {
                                makeMeNull = level[i, j];
                                level[i, j].OnCollision(this, level[i, j]);
                                if (level[i, j] is MysteryBlock)
                                {
                                    level[i, j].OnCollision(this, level[i, j], level);
                                }
                                level[i, j].UpdateScoreBoard(sc);
                                level[i, j] = makeMeNull;
                                noCollisions = false;
                            }
                        }
                }
            }
            if (noCollisions)
            {
                onGround = false;
            }
        }
        /// <summary>
        /// Mario currently does nothing on collision
        /// </summary>
        /// <param name="mario"></param>
        /// <param name="col"></param>
        public override void OnCollision(MovingGameObject mario, GameObject col)
        {
        }
        /// <summary>
        /// Mario has no influence on the scoreboard at the moment
        /// </summary>
        /// <param name="sc"></param>
        public override void UpdateScoreBoard(ScoreBoard sc)
        {
        }
    }
}
