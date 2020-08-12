using System;
using System.Threading;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Windowing.GraphicsLibraryFramework;

namespace HtxGraphics.Rendering
{
    public sealed class Renderer : IThreadProcess
    {
        private readonly bool _multiThreaded;
        private readonly Thread? _thread;
        private readonly Window _window;

        public event Action? OnDraw;

        internal Renderer(Window window, bool multiThreaded)
        {
            _window = window;
            _multiThreaded = multiThreaded;
            if (multiThreaded)
            {
                _thread = new Thread(RunThread);
                _thread.Start();
            }
            else
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            _window.MakeCurrent();
            GL.LoadBindings(new GLFWBindingsContext());
        }
        
        void IThreadProcess.Update()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            OnDraw?.Invoke();
            
            _window.SwapBuffers();
        }

        public void Draw(IDrawable drawable)
        {
            drawable.Draw();
        }
        
        public void Dispose()
        {
        }

        private void RunThread()
        {
            Logger.Info("Initializing rendering thread!");
            Initialize();
            while (!_window.IsExiting)
            {
                
            }
            Logger.Info("Input thread shut down!");
        }

        public bool MultiThreaded => _multiThreaded;
    }
}