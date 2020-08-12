using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;

namespace HtxGraphics.Rendering.LowLevel
{
    public class Geometry : IDrawable
    {
        private readonly int _vao, _vbo, _ebo;
        // private readonly int _indexCount;
        
        public Geometry(params Vertex[] vertices)
        {
            unsafe
            {
                _vao = GL.GenVertexArray();
                GL.BindVertexArray(_vao);

                _vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, sizeof(Vertex) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
                
                _ebo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
                //TODO: More clever triangulation https://en.wikipedia.org/wiki/Polygon_triangulation
                var indices = new uint[(vertices.Length - 2) * 3];
                for (uint i = 0; i < indices.Length / 3; i++)
                {
                    indices[i * 3 + 0] = 0;
                    indices[i * 3 + 1] = i + 1;
                    indices[i * 3 + 2] = i + 2;
                }
                GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);


                var offset = 0;
                var index = 0;
                void VertexAttributePointer(int size)
                {
                    GL.EnableVertexAttribArray(index);
                    GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, sizeof(Vertex), offset);
                    offset += size * sizeof(float);
                    index += 1;
                }
                VertexAttributePointer(2);
                VertexAttributePointer(2);
                VertexAttributePointer(4);
            }
        }
        
        void IDrawable.Draw()
        {
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            // GL.DrawElements(PrimitiveType.Triangles, 2, DrawElementsType.UnsignedInt, 0);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(_vao);
            GL.DeleteBuffer(_ebo);
            GL.DeleteBuffer(_vbo);
        }
    }
}