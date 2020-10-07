using System;

namespace InterviewRound3.Writer
{
    /// <summary>
    /// Wraps System.Console
    /// </summary>
    public class ConsoleWrapper: IConsole
    {
        /// <inheritdoc/>
        public void Write(string value) => Console.Write(value);
        /// <inheritdoc/>
        public void WriteLine() => Console.WriteLine();
        /// <inheritdoc/>
        public void WriteLine(string value) => Console.WriteLine(value);
        /// <inheritdoc/>
        public ConsoleKeyInfo ReadKey() => Console.ReadKey();
        /// <inheritdoc/>
        public int Read() => Console.Read();
        /// <inheritdoc/>
        public string ReadLine() => Console.ReadLine();
    }
}
