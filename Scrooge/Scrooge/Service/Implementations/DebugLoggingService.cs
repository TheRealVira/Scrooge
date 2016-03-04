using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations
{
    public class DebugLoggingService : LoggingService
    {
        public override void Write(object o)
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method?.DeclaringType;
            Debug.Write($"[{DateTime.Now.ToLongTimeString()}/{type?.Name ?? "unknown"}.{method?.Name ?? "unknown"}] {o}");
        }

        public override void WriteLine(object o)
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method?.DeclaringType;
            Debug.WriteLine($"[{DateTime.Now.ToLongTimeString()}/{type?.Name ?? "unknown"}.{method?.Name ?? "unknown"}] {o}");
        }
    }
}