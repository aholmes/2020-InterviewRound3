namespace UCLARound3.Writer
{
    /// <summary>
    /// An interface for mocking <see cref="System.Console"/>
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// <see cref="Console.WriteLine()"/>
        /// </summary>
        void WriteLine();

        /// <summary>
        /// <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="value"></param>
        void WriteLine(string value);
    }
}
