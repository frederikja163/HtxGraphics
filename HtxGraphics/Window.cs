using System;
using System.Collections.Generic;
using System.Text;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing;
using OpenToolkit.Windowing.Desktop;

namespace HtxGraphics
{
    public class Window : IDisposable
    {
        private GameWindow _window;

        public Window(int width = 800, int height = 600, string title = "HtxGraphicsApp")
        {
            var windowSettings = GameWindowSettings.Default;
            var nativeSettings = NativeWindowSettings.Default;
            nativeSettings.Size = new Vector2i(width, height);
            nativeSettings.Title = title;
            _window = new GameWindow(windowSettings, nativeSettings);
        }

        public Window(GameWindowSettings windowSettings, NativeWindowSettings nativeSettings)
        {
            _window = new GameWindow(windowSettings, nativeSettings);
        }

        public void Run()
        {
            _window.Run();
        }

        public void Dispose()
        {
            _window.Dispose();
        }
    }
}
