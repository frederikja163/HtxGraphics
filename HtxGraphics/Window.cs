using HtxGraphics.Rendering;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;

namespace HtxGraphics
{
    //TODO: Create own glfw implementation
    public sealed class Window : GameWindow
    {
        private IThreadProcess? _renderer;
        private IThreadProcess? _input;
        
        public Window(int width, int height, string title = "HtxGraphicsApp") :
            this()
        {
            Size = new Vector2i(width, height);
            Title = title;
        }
        
        public Window(GameWindowSettings? gameWindowSettings = null, NativeWindowSettings? nativeWindowSettings = null) : 
            base(gameWindowSettings ?? GameWindowSettings.Default, nativeWindowSettings ?? NativeWindowSettings.Default)
        {
            UpdateFrame += OnUpdateFrame;
            Logger.Instantiate();
        }

        private void OnUpdateFrame(FrameEventArgs obj)
        {
            if (!_input.MultiThreaded)
            {
                _renderer.Update();
            }
            //Call update event
            if (!_renderer.MultiThreaded)
            {
                _renderer.Update();
            }
        }

        public Renderer GetOrCreateRenderer(bool createMultiThreaded = false)
        {
            if (_renderer == null)
            {
                _renderer = new Renderer(this, createMultiThreaded);
            }

            return (Renderer)_renderer;
        }

        public Input GetOrCreateInput(bool createMultiThreaded = false)
        {
            if (_input == null)
            {
                _input = new Input(this, createMultiThreaded);
            }

            return (Input)_input;
        }
    }
    
    // public class Window : IDisposable
    // {
    //     private GameWindow _window;
    //
    //     public Window(int width = 800, int height = 600, string title = "HtxGraphicsApp")
    //     {
    //         var windowSettings = GameWindowSettings.Default;
    //         var nativeSettings = NativeWindowSettings.Default;
    //         nativeSettings.Size = new Vector2i(width, height);
    //         nativeSettings.Title = title;
    //         _window = new GameWindow(windowSettings, nativeSettings);
    //     }
    //
    //     public Window(GameWindowSettings windowSettings, NativeWindowSettings nativeSettings)
    //     {
    //         _window = new GameWindow(windowSettings, nativeSettings);
    //     }
    //
    //     public void Run()
    //     {
    //         _window.Run();
    //     }
    //
    //     public void Dispose()
    //     {
    //         _window.Dispose();
    //     }
    // }
}
