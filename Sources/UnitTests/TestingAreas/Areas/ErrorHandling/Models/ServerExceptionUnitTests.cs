using System;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Models;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.UnitTests.TestingAreas.Areas.ErrorHandling.Models
{
    [TestFixture]
    public class ServerExceptionUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            const string Message = "Message";
            const string TypeName = "TypeName";
            const string StackTrace = "StackTrace";

            ConstructorTestBuilderFactory.Constructing<ServerException>()
                .UsingDefaultConstructor()
                .WithArgumentValues(Message, TypeName, StackTrace)
                .Succeeds()
                .WithArgumentValues(Message, TypeName, StackTrace)
                .Maps()
                .ToProperty(f => f.Message).WithValue(Message)
                .ToProperty(f => f.TypeName).WithValue(TypeName)
                .ToProperty(f => f.StackTrace).WithValue(StackTrace)
                .BuildMaps()
                .WithArgumentValues(string.Empty, TypeName, StackTrace)
                .Fails()
                .WithArgumentValues(Message, string.Empty, StackTrace)
                .Fails()
                .WithArgumentValues(Message, TypeName, string.Empty)
                .Succeeds()
                .Assert();
        }

        [Test]
        public void CreatingFromException_MapsProperties_FromMostInnerException()
        {
            // Arrange
            const string InnerMessage = "InnerMessage";
            var innerException = new ArgumentException(InnerMessage);
            var middleException = new ArgumentNullException("MiddleMessage", innerException);
            var outerException = new Exception("OuterMessage", middleException);

            // Act
            var actualServerException = ServerException.CreateFromException(outerException);

            // Assert
            Assert.AreEqual(InnerMessage, actualServerException.Message);
            Assert.AreEqual(innerException.GetType().Name, actualServerException.TypeName);
        }
    }
}