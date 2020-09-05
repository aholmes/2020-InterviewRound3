using System;
namespace UCLARound3.Writer
{
    /// <summary>
    /// An interface for mocking <see cref="System.Console"/>
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// <see cref="Console.Write(string)"/>
        /// </summary>
        /// <param name="value"></param>
        void Write(string value);

        /// <summary>
        /// <see cref="Console.WriteLine()"/>
        /// </summary>
        void WriteLine();

        /// <summary>
        /// <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="value"></param>
        void WriteLine(string value);

        public ConsoleKeyInfo ReadKey();

        /// <summary>
        /// <see cref="Console.Read()"/>
        /// </summary>
        /// <returns></returns>
        public int Read();

        /// <summary>
        /// <see cref="Console.ReadLine()"/>
        /// </summary>
        /// <returns></returns>
        public string ReadLine();
    }
}
