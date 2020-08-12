using HtxGraphics;
using HtxGraphics.Rendering;
using HtxGraphics.Rendering.LowLevel;
using OpenToolkit.Mathematics;

namespace HelloWorld
{
    class Program
    {
        private static Window _window;
        
        static void Main(string[] args)
        {
            _window = new Window();
            _window.GetOrCreateRenderer().OnDraw += OnDraw;
            _window.GetOrCreateInput();
            _window.Run();
        }

        private static void OnDraw()
        {
            var renderer = _window.GetOrCreateRenderer();
            var geometry = new Geometry(new Vertex(Vector2.UnitX, Color4.Aqua), new Vertex(Vector2.UnitX, Color4.Aqua), new Vertex(Vector2.One, Color4.Aqua));
            
            renderer.Draw(geometry);
        }
    }
}
