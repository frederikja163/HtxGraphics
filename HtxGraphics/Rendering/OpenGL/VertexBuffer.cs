using System;
using OpenToolkit.Graphics.OpenGL4;

namespace HtxGraphics.Rendering.OpenGL
{
    public sealed class VertexBuffer : IDisposable
    {
        private readonly int _handle;
        public float[] Data { get; }

        public VertexBuffer(float[] data)
        {
            Data = data;
            _handle = GL.GenBuffer();
            
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
        }

        internal void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        }

        internal void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}