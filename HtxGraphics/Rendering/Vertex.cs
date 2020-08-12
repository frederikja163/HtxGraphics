using OpenToolkit.Mathematics;

namespace HtxGraphics.Rendering
{
    public struct Vertex
    {
        public Vector2 Position { get; }
        public Vector2 TextureCoordinate { get; }
        public Color4 Color { get; }

        public Vertex(Vector2 position, Color4 color)
        {
            Position = position;
            TextureCoordinate = Vector2.Zero;
            Color = color;
        }
    }
}