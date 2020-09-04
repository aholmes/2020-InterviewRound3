using System;

namespace UCLARound3.Writer
{
    public class ConsoleWrapper: IConsole
    {
        public void WriteLine() => Console.WriteLine();
        public void WriteLine(string value) => Console.WriteLine(value);
    }
}
