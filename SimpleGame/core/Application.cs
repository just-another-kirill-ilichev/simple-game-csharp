using System;
using NLog;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using SimpleGame.ECS;

namespace SimpleGame.Core
{
    public class Application : GameWindow
    {
        public ResourceManager ResourceManager { get; }
        public EntityManager EntityManager { get; }
        public SystemManager SystemManager { get; }
        public SceneLoader SceneLoader { get; }

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public Application() : base(800, 600,
            GraphicsMode.Default,
            "loading...",
            GameWindowFlags.FixedWindow,
            DisplayDevice.Default,
            3, 3,
            GraphicsContextFlags.Default)
        {
            _logger.Info($"Vendor: {GL.GetString(StringName.Vendor)}");
            _logger.Info($"Renderer: {GL.GetString(StringName.Renderer)}");
            _logger.Info($"OpenGL version: {GL.GetString(StringName.Version)}");
            _logger.Info($"GLSL version: {GL.GetString(StringName.ShadingLanguageVersion)}");

            ResourceManager = new ResourceManager();
            SystemManager = new SystemManager();
            EntityManager = new EntityManager();
            SceneLoader = new SceneLoader(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            string path = "../resources/test.json"; // TODO

            try
            {
                SceneLoader.LoadScene(path);
            }
            catch (Exception ex) // TODO
            {
                _logger.Error(ex, "An exception occurred while loading scene from {0}", path);
            }
            

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"OpenGL version: {GL.GetString(StringName.Version)}; FPS: {(int)(1 / e.Time)}";

            var background = new Color4(0, 0, 0, 0);
            GL.ClearColor(background);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SystemManager.Redraw();

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            SystemManager.Update((uint)(e.Time * 1000));
            
            base.OnUpdateFrame(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            ResourceManager.Clear();
            SystemManager.Clear();
            EntityManager.Clear();

            base.OnUnload(e);
        }
    }
}