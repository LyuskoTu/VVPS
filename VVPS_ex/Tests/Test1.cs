using System;
using Xunit;
using MVC_TU.Core.Interface;

namespace MVCTest.Tests
{
	public class Test1 : ITests
	{
		[Fact]
		public void WhyTheCrack()
		{
            // Arrange
            int a = 5;
            int b = 12;

            // Act
            int result = a + b;

            // Assert
            Assert.Equal(17, result);
        }

        [Theory]
        [InlineData(4, 5, 9)]
        public void Sum_Two_Numbers(int chislo_1, int chislo_2, int result)
        {
            // Act
            int output = chislo_1 + chislo_2;

            // Assert
            Assert.Equal(result, output);
        }
    }
}

