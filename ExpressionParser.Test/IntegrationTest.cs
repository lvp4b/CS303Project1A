using System;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ExpressionParser.Evaluator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionParser.Test
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly IEvaluator _evaluator;

        public IntegrationTest()
        {
            using (var container = new WindsorContainer())
            {
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
                container.Install(FromAssembly.Containing<IEvaluator>());

                _evaluator = container.Resolve<IEvaluator>();
            }
        }

        private void Test(string expression, int expected)
        {
            Assert.AreEqual(expected, _evaluator.Evaluate(expression));
        }

        private void TestFailure(string expression, string message)
        {
            try
            {
                var result = _evaluator.Evaluate(expression);
                Assert.Fail($"'{expression}' evaluated to {result}");
            }
            catch (Exception e)
            {
                Assert.AreEqual(message, e.Message);
            }
        }

        [TestMethod]
        public void Addition() => Test("1+3", 4);

        [TestMethod]
        public void Subtraction() => Test("1-3", -2);

        [TestMethod]
        public void Mutiplication() => Test("8*4", 32);

        [TestMethod]
        public void Division() => Test("100/20", 5);
        
        [TestMethod]
        public void Modulus() => Test("8%3", 2);

        [TestMethod]
        public void Whitespace() => Test("1 + 8", 9);
        
        [TestMethod]
        public void Power() => Test("8^2", 64);

        [TestMethod]
        public void Parenthesis()
        {
            Test("8*(4+2)", 48);
            Test("8*(4+2*2)", 64);
            Test("4*((4+2)*2)", 48);
            Test("(4*5)+(2-2)*7", 20);
        }

        [TestMethod]
        public void GreaterThan()
        {
            Test("1>3", 0);
            Test("1>1", 0);
            Test("3>1", 1);
        }

        [TestMethod]
        public void GreaterThanOrEqual()
        {
            Test("1>=3", 0);
            Test("1>=1", 1);
            Test("3>=1", 1);
        }

        [TestMethod]
        public void LessThan()
        {
            Test("1<3", 1);
            Test("1<1", 0);
            Test("3<1", 0);
        }

        [TestMethod]
        public void LessThanOrEqual()
        {
            Test("1<=3", 1);
            Test("1<=1", 1);
            Test("3<=1", 0);
        }


        [TestMethod]
        public void Equality()
        {
            Test("1==3", 0);
            Test("1==1", 1);
        }

        [TestMethod]
        public void Inequality()
        {
            Test("1!=3", 1);
            Test("1!=1", 0);
        }

        [TestMethod]
        public void Not()
        {
            Test("!1", 0);
            Test("!0", 1);
        }

        [TestMethod]
        public void And()
        {
            Test("1 && 1", 1);
            Test("1 && 0", 0);
        }

        [TestMethod]
        public void Or()
        {
            Test("1 || 1", 1);
            Test("1 || 0", 1);
            Test("0 || 0", 0);
        }

        [TestMethod]
        public void Increment() => Test("++4", 5);

        [TestMethod]
        public void Decrement() => Test("--4", 3);

        [TestMethod]
        public void DecrementNegative() => Test("---4", -5);

        [TestMethod]
        public void EmptyString() => Test("", 0);

        [TestMethod]
        public void ProjectExamples()
        {
            Test("1+2*3", 7);
            Test("2+2^2*3", 14);
            Test("1==2", 0);
            Test("1+3 > 2", 1);
            Test("(4>=4) && 0", 0);
            Test("(1+2)*3", 9);
            Test("++++2-5*(3^2)", -41);
        }

        [TestMethod]
        public void CantStartClosing()
        {
            TestFailure(")3+2", "Expression can't start with a closing parenthesis @ char: 0");
        }

        [TestMethod]
        public void CantStartBinary()
        {
            TestFailure("<3+2", "Expression can't start with a binary operator @ char: 0");
        }

        [TestMethod]
        public void TwoBinaryOperatorsInARow()
        {
            TestFailure("3&&&& 5", "Two binary operators in a row @ char 3");
        }
        
        [TestMethod]
        public void TwoOperandsInARow()
        {
            TestFailure("15+3 2", "Two operands in a row @ char 5");
        }

        [TestMethod]
        public void UnaryFollowedByBinary()
        {
            TestFailure("10+ ++<3", "A unary operand can't be followed by a binary operator @ char 6");
        }

        [TestMethod]
        public void DivisionByZero()
        {
            TestFailure("1/0", "Division by zero @ char 2");
        }
    }
}
