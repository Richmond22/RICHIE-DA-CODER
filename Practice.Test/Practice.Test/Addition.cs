using System;
using Xunit;
using Calculator;

namespace Practice.Test
{
    public class Addition
    {
        [Fact]
        public void Add_Two_Numbers()
        {
            //Arrange
            var num1 = 2;
            var num2 = 4;

            //act
            var calculator = new Calculator.Calculator();
            var result = calculator.Add(num1, num2);

            //Assert
            Assert.Equal(6, result);
            
        }
    }
}
