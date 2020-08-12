using System;

namespace HtxGraphics
{
    internal interface IThreadProcess : IDisposable
    {
        public bool MultiThreaded { get; }
        internal void Update();
    }
}