using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace HtxGraphics
{
    public enum MessageSeverity
    {
        Info,
        Warning,
        Error,
        Fatal
    }

    public readonly struct MessageInfo
    {
        private readonly MessageSeverity _severity;
        private readonly DateTime _time;
        private readonly string _message;
        private readonly int _lineNumber;
        private readonly string _filePath;

        public MessageInfo(MessageSeverity severity, DateTime time, string message, int lineNumber, string filePath)
        {
            _severity = severity;
            _time = time;
            _message = message;
            _lineNumber = lineNumber;
            _filePath = Path.GetFileName(filePath);
        }

        public MessageSeverity Severity => _severity;
        public DateTime Time => _time;
        public string Message => _message;
        public int LineNumber => _lineNumber;
        public string FilePath => _filePath;
        

        public override string ToString()
        {
            return $"[{_severity} {_time.ToString("yyyy-MM-dd hh:mm:ss:fffff")} {_filePath}#{_lineNumber}] {_message}";
        }
    }
    
    public sealed class Logger : IDisposable
    {
        public sealed class FatalException : Exception
        {
            public FatalException(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
            {
                Fatal(message, lineNumber, filePath);
            }
        }

        private readonly FileStream _fileStream;

        private static Logger? _instance;

        public static event Action<MessageInfo>? OnLogMessage;
        
        public static MessageSeverity MinSeverity { get; set; }

        private Logger()
        {
            _instance = this;
            _fileStream = File.Create("log.txt");
        }

        public void Dispose()
        {
            _instance = null;
            _fileStream.Dispose();
        }

        internal static void Instantiate()
        {
            _instance = new Logger();
        }

        public static void Info(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            _instance?.WriteLog(new MessageInfo(MessageSeverity.Info, DateTime.Now, message, lineNumber, filePath));
        
        public static void Warning(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            _instance?.WriteLog(new MessageInfo(MessageSeverity.Warning, DateTime.Now, message, lineNumber, filePath));
        
        public static void Error(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            _instance?.WriteLog(new MessageInfo(MessageSeverity.Error, DateTime.Now, message, lineNumber, filePath));
        
        public static void Fatal(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            _instance?.WriteLog(new MessageInfo(MessageSeverity.Fatal, DateTime.Now, message, lineNumber, filePath));

        private void WriteLog(in MessageInfo info)
        {
            if (info.Severity < MinSeverity)
            {
                return;
            }
            OnLogMessage?.Invoke(info);
            byte[] data = Encoding.UTF8.GetBytes(info.ToString());
            _fileStream.Write(data, 0, data.Length);
            _fileStream.WriteByte((byte)'\n');
            Console.WriteLine(info.ToString());
        }

        private static ConsoleColor SeverityToColor(MessageSeverity severity) =>
        severity switch
        {
            MessageSeverity.Info => ConsoleColor.White,
            MessageSeverity.Warning => ConsoleColor.Yellow,
            MessageSeverity.Error => ConsoleColor.Red,
            MessageSeverity.Fatal => ConsoleColor.DarkRed,
            _ => ConsoleColor.White
        };
    }
}