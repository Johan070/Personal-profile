using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class Camera2DTests
    {
        Viewport vp = new Viewport();
        Camera2D camera;
        Vector2 position = new Vector2(1, 1);
        float rotation=0;
        float zoom=0;
        Vector2 origin= new Vector2(1,1);
        

        [TestInitialize]
        public void TestInit()
        {
           camera = new Camera2D(vp);
            camera.Position = position;
            camera.Origin = origin;
            camera.Rotation = rotation;
            camera.Zoom = zoom;

        }
        [TestMethod()]
        public void Camera2DTest()
        {
           
            Matrix matrix=
            Matrix.CreateTranslation(new Vector3(-camera.Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-camera.Origin, 0.0f)) *
                Matrix.CreateRotationZ(camera.Rotation) *
                Matrix.CreateScale(camera.Zoom, camera.Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(camera.Origin, 0.0f));
            Assert.AreEqual(matrix, camera.GetViewMatrix());
        }
    }
}