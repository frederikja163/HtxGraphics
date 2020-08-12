using System.Threading;
using OpenToolkit.Graphics.ES11;
using OpenToolkit.Windowing.GraphicsLibraryFramework;

namespace HtxGraphics.Rendering
{
    public class Renderer : IThreadProcess
    {
        private readonly bool _multiThreaded;
        private readonly Thread? _thread;
        private readonly Window _window;

        internal Renderer(Window window, bool multiThreaded)
        {
            _window = window;
            _multiThreaded = multiThreaded;
            if (multiThreaded)
            {
                _thread = new Thread(RunThread);
                _thread.Start();
            }
        }

        private void Initialize()
        {
            GL.LoadBindings(new GLFWBindingsContext());
        }
        
        void IThreadProcess.Update()
        {
            
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