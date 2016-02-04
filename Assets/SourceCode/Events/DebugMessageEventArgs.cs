using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Events {
    public class DebugMessageEventArgs : EventArgs {
        public enum SeverityLevel {
            Error,
            Warning,
            Info
        }
        public string ThreadName { get; private set; }
        public string MethodName { get; private set; }
        public string Message { get; private set; }
        public SeverityLevel Severity { get; private set; }
        public DebugMessageEventArgs(string methodName, string message) {
            this.ThreadName = Thread.CurrentThread.Name;
            this.MethodName = methodName;
            this.Message = message;
            this.Severity = SeverityLevel.Info;
        }

        public DebugMessageEventArgs(string methodName, string message, SeverityLevel severity) {
            this.ThreadName = Thread.CurrentThread.Name;
            this.MethodName = methodName;
            this.Message = message;
            this.Severity = severity;
        }
    }
}
