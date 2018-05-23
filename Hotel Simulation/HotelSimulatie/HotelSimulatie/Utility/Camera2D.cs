using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// TODO: Use this class to the fullest
    /// A cameraclass wich can be used to zoom in and change camera positions in future versions
    /// </summary>
    public class Camera2D
    {
        private readonly Viewport _viewport;
        /// <summary>
        /// The current camera position
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The rotation of the camera
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// The current zoom factor of the camera
        /// </summary>
        public Vector2 Zoom { get; set; }
        /// <summary>
        /// The position of the origin of the camera
        /// </summary>
        public Vector2 Origin { get; set; }
        /// <summary>
        /// Initialize the camera properties based on the viewport of the simulation/game
        /// </summary>
        /// <param name="viewport"></param>
        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;
            Rotation = 0;
            Zoom = new Vector2(1, 1);
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Gets the viewmatrix of the camera by translating the matrix with all camera properties
        /// </summary>
        /// <returns>viewmatrix</returns>
        public Matrix GetViewMatrix()
        {
            return

                Matrix.CreateTranslation(new Vector3(Origin, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom.X, Zoom.Y, 1);
        }
    }
}
