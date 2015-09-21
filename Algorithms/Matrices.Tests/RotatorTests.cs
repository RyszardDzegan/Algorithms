using System;
using NUnit.Framework;
using System.Linq;

namespace Algorithms.Matrices.Tests
{
    [TestFixture]
    public class RotatorTests
    {
        [Test]
        public void Test0()
        {
            var matrix = new int[0, 0];
            var expected = new int[0, 0];
            var sut = new Rotator<int>(matrix);
            sut.Rotate();
            AssertMatrixIsRotated(expected, matrix);
        }

        [Test]
        public void Test1()
        {
            var matrix = new[,] { { 1 } };
            var expected = new[,] { { 1 } };
            var sut = new Rotator<int>(matrix);
            sut.Rotate();
            AssertMatrixIsRotated(expected, matrix);
        }

        [Test]
        public void Test2()
        {
            var matrix = new[,]
                {{ 1, 2 },
                 { 4, 3 }};

            var expected = new[,]
                {{ 4, 1 },
                 { 3, 2 }};

            var sut = new Rotator<int>(matrix);
            sut.Rotate();
            AssertMatrixIsRotated(expected, matrix);
        }

        [Test]
        public void Test3()
        {
            var matrix = new[,]
                {{ 1, 2, 3 },
                 { 8, 9, 4 },
                 { 7, 6, 5 }};

            var expected = new[,]
                {{ 8, 1, 2 },
                 { 7, 9, 3 },
                 { 6, 5, 4 }};

            var sut = new Rotator<int>(matrix);
            sut.Rotate();
            AssertMatrixIsRotated(expected, matrix);
        }

        [Test]
        public void Test4()
        {
            var matrix = new[,]
                {{  1,  2,  3,  4 },
                 { 12, 13, 14,  5 },
                 { 11, 16, 15,  6 },
                 { 10,  9,  8,  7 }};

            var expected = new[,]
                {{ 12,  1,  2,  3 },
                 { 11, 16, 13,  4 },
                 { 10, 15, 14,  5 },
                 {  9,  8,  7,  6 }};

            var sut = new Rotator<int>(matrix);
            sut.Rotate();
            AssertMatrixIsRotated(expected, matrix);
        }

        [Test]
        public void Test5()
        {
            var matrix = new[,]
                {{ 1, 2 },
                 { 4, 3 }};

            var expected = new[,]
                {{ 3, 4 },
                 { 2, 1 }};

            var sut = new Rotator<int>(matrix);
            sut.Rotate();
            sut.Rotate();
            AssertMatrixIsRotated(expected, matrix);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Parameter \"matrix\" must have the same number of columns and rows, but it has 3 rows and 2 columns.")]
        public void Test6()
        {
            var matrix = new[,]
                {{ 1, 2 },
                 { 3, 4 },
                 { 5, 6 }};

            var sut = new Rotator<int>(matrix);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), UserMessage = "Parameter \"matrix\" must have the same number of columns and rows, but it has 2 rows and 3 columns.")]
        public void Test7()
        {
            var matrix = new[,]
                {{ 1, 2, 3 },
                 { 4, 5, 6 }};

            var sut = new Rotator<int>(matrix);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), UserMessage = "Parameter \"matrix\" can't be null.")]
        public void Test8()
        {
            var sut = new Rotator<int>(null);
        }

        private static void AssertMatrixIsRotated<T>(T[,] expected, T[,] actual)
        {
            Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));

            var expectedString = MatrixToString(expected);
            var actualString = MatrixToString(actual);

            Assert.AreEqual(expectedString, actualString);
        }

        private static string MatrixToString<T>(T[,] matrix) =>
            string.Join(Environment.NewLine, Enumerable.Range(0, matrix.GetLength(0)).Select(i => string.Join(" ", Enumerable.Range(0, matrix.GetLength(1)).Select(j => matrix[i, j]))));
    }
}
