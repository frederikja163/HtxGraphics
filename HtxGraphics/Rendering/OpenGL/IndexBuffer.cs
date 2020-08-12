using System;
using OpenToolkit.Graphics.OpenGL4;

namespace HtxGraphics.Rendering.OpenGL
{
    public sealed class IndexBuffer : IDisposable
    {
        private readonly int _handle;
        public uint[] Data { get; }

        public IndexBuffer(uint[] data)
        {
            Data = data;
            _handle = GL.GenBuffer();
            
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * data.Length, data, BufferUsageHint.StaticDraw);
        }

        internal void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
        }

        internal void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}