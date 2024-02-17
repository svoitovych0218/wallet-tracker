namespace Wallet.Tracker.Sqs.Logging;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using System.IO;

public class CustomConsoleFormatter : ConsoleFormatter
{
    public CustomConsoleFormatter() : base("CustomConsoleFormatter")
    {
        
    }
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var logLevel = logEntry.LogLevel;
        var category = logEntry.Category;
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

        string scopeInfo = "";
        if (scopeProvider != null)
        {
            scopeProvider.ForEachScope((scope, state) =>
            {
                state += scope;
            }, scopeInfo);
        }

        textWriter.WriteLine($"[{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz}] {scopeInfo} {category}: {message}");
        if (logEntry.Exception != null)
        {
            textWriter.WriteLine(logEntry.Exception.ToString());
        }
    }
}
