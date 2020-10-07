using InterviewRound3.Writer;
using Moq;
using System;
using System.Reflection;
using InterviewRound3.UnitTests.Helpers;
using Xunit;

namespace InterviewRound3.UnitTests.Application
{
    public class ConsoleWriterTests
    {
        private const string TestOutputString = "ABC123";
        private class TestConsoleWriter: ConsoleWriter
        {
            public override string GetOutput() => TestOutputString;
        }

        [Fact]
        public void ConsoleWritingVisitor_Throws_On_Null_Input()
        {
            #region Arrange/Act
            void create() => new ConsoleWritingVisitor(null);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(create);
            #endregion
        }

        [Theory, AutoMoqData]
        public void ConsoleWritingVisitor_WriteLine_Throws_On_Null_Input(Mock<IConsole> consoleMock)
        {
            #region Arrange
            var visitor = new ConsoleWritingVisitor(consoleMock.Object);
            #endregion

            #region Act
            void act() => visitor.Visit(null);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(act);
            #endregion
        }

        [Theory, AutoMoqData]
        public void ConsoleWriter_Accept_Writes_GetOutput_Value(Mock<IConsole> consoleMock)
        {
            #region Arrange
            var visitor = new ConsoleWritingVisitor(consoleMock.Object);
            var writer = new TestConsoleWriter();
            #endregion

            #region Act
            writer.Accept(visitor);
            #endregion

            #region Assert
            consoleMock.Verify(o => o.WriteLine(TestOutputString), Times.Once);
            #endregion
        }

        [Fact]
        public void ConsoleWriter_Accept_Throws_On_Null_Input()
        {
            #region Arrange
            var writer = new TestConsoleWriter();
            #endregion

            #region Act
            void act() => writer.Accept(null);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(act);
            #endregion
        }

        [Theory]
        [InlineData(typeof(PurchaseSummary))]
        [InlineData(typeof(PurchaseDetail))]
        [InlineData(typeof(ProductDetail))]
        public void ConsoleWriters_Throw_On_Null_Input(Type consoleWriterType)
        {
            #region Arrange/Act
            void create()
            {
                try
                {
                    Activator.CreateInstance(consoleWriterType, new object[] { null });
                }
                catch(TargetInvocationException e) when(e?.InnerException is ArgumentNullException)
                {
                    throw e.InnerException;
                }
            }
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(create);
            #endregion
        }
    }
}
