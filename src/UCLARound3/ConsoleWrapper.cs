using System;
using System.Collections.Generic;
using System.Text;

namespace UCLARound3
{
    public class ConsoleWrapper : IConsole
    {
        public void WriteLine() => Console.WriteLine();
        public void WriteLine(string value) => Console.WriteLine(value);
    }
}
