using System;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.Matrices
{
    /// <summary>
    /// Performs a clockwise matrix rotation.
    /// </summary>
    class Rotator<T>
    {
        /// <summary>
        /// A mutable matrix provided in constructor.
        /// Its value is updated after each <see cref="Rotate"/> invocation.
        /// </summary>
        public T[,] Matrix { get; }

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="matrix">
        /// A matrix to rotate.
        /// Its row count must equal its column count.
        /// </param>
        public Rotator(T[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException($"Parameter \"{nameof(matrix)}\" can't be null.");

            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException($"Parameter \"{nameof(matrix)}\" must have the same number of columns and rows, but it has {matrix.GetLength(0)} rows and {matrix.GetLength(1)} columns.");

            Matrix = matrix;
        }

        /// <summary>
        /// Rotates the <see cref="Matrix"/> in a clockwise manner.
        /// It uses "divide and conquer" algorithm.
        /// The matrix is divided into unrelated rings like an onion.
        /// Then it rotates all rings in parallel.
        /// </summary>
        public void Rotate()
        {
            var rotations =
                from level in Enumerable.Range(0, Matrix.GetLength(0) / 2)
                select new Action(() => RotateRing(level));

            Parallel.ForEach(rotations, rotation => rotation());
        }

        /// <summary>
        /// An asynchronuos version of <see cref="Rotate"/>.
        /// </summary>
        /// <returns></returns>
        public Task RotateAsync() =>
            Task.Run(() => Rotate());

        private void RotateRing(int level)
        {
            var begin = level;
            var count = Matrix.GetLength(0) - level;
            var end = count - 1;

            var temp = Matrix[begin, begin];

            for (var i = 0; i < count - 1; i++)
                Matrix[begin + i, begin] = Matrix[begin + i + 1, begin];

            for (var i = 0; i < count - 1; i++)
                Matrix[end, begin + i] = Matrix[end, begin + i + 1];

            for (var i = 0; i < count - 1; i++)
                Matrix[end - i, end] = Matrix[end - i - 1, end];

            for (var i = 0; i < count - 2; i++)
                Matrix[begin, end - i] = Matrix[begin, end - i - 1];

            Matrix[begin, begin + 1] = temp;
        }
    }
}
