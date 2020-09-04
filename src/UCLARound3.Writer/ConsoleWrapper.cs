using System;

namespace UCLARound3.Writer
{
    /// <summary>
    /// Wraps System.Console
    /// </summary>
    public class ConsoleWrapper: IConsole
    {
        /// <inheritdoc/>
        public void WriteLine() => Console.WriteLine();

        /// <inheritdoc/>
        public void WriteLine(string value) => Console.WriteLine(value);
    }
}
