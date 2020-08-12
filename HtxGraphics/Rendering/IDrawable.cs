using System;

namespace HtxGraphics.Rendering
{
    public interface IDrawable : IDisposable
    {
        internal void Draw();
    }
}