using HtxGraphics;
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new Window();
            window.GetOrCreateRenderer();
            window.GetOrCreateInput();
            window.Run();
        }
    }
}
