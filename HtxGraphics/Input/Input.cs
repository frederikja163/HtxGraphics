using System;
using System.Threading;

namespace HtxGraphics
{
    public class Input : IThreadProcess
    {
        private readonly bool _multiThreaded;
        private readonly Thread? _thread;
        private readonly Window _window;

        internal Input(Window window, bool multiThreaded)
        {
            _window = window;
            _multiThreaded = multiThreaded;
            if (multiThreaded)
            {
                _thread = new Thread(RunThread);
                _thread.Start();
            }
        }

        private void Initialize()
        {
            
        }
        
        void IThreadProcess.Update()
        {
            
        }

        public void Dispose()
        {
        }

        private void RunThread()
        {
            Logger.Info("Initializing input thread!");
            Initialize();
            while (!_window.IsExiting)
            {
                
            }
            Logger.Info("Input thread shut down!");
        }

        public bool MultiThreaded => _multiThreaded;
    }
}