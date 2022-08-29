using System;
using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.Physics
{
    public static class Lines
    {
        /// <summary>
        /// Calculates a line formula in the form of y=m*x+b from the provided two points.
        /// </summary>
        /// <param name="foo">The first point to be referenced.</param>
        /// <param name="bar">The second point to be referenced.</param>
        /// <returns>Double tuple containing m (the line slope) in its first item and b (the y intercept) in the second item.
        /// If the points form a vertical line then m is equal to double.NaN and b is the x intercept.</returns>
        public static Tuple<double, double> LineFormula(Point foo, Point bar)
        {
            double m;
            double b;
            if (foo.X != bar.X)
            {
                m = (double)(foo.Y - bar.Y) / (foo.X - bar.X);
                b = -((foo.X * m) - foo.Y);
            }
            else
            {
                m = double.NaN;
                b = foo.X;
            }
            return new Tuple<double, double>(m, b);
        }
        /// <summary>
        /// Calculates a corrasponding y value for a provided x value and line formula. 
        /// </summary>
        /// <param name="lineFormula">Double tuple containing m (the line slope) in its first item and b (the y intercept) in the second item.
        /// If it is a vertical line then the first item should be double.NaN and the second item should be the x intercept.</param>
        /// <param name="x">The x value to be used on the line.</param>
        /// <returns>The closest integer y value on the provided line for the provided x value.</returns>
        public static int GetY(Tuple<double, double> lineFormula, int x)
        {
            if (lineFormula.Item1 == double.NaN) //vertical line.
            {
                return (int)Math.Round(lineFormula.Item2);
            }

            return (int)Math.Round((lineFormula.Item1 * x) + lineFormula.Item2);
        }
        /// <summary>
        /// Calculates a corrasponding y value for a provided x value and line formula. 
        /// </summary>
        /// <param name="lineFormula">Double tuple containing m (the line slope) in its first item and b (the y intercept) in the second item.
        /// If it is a vertical line then the first item should be double.NaN and the second item should be the x intercept.</param>
        /// <param name="x">The x value to be used on the line.</param>
        /// <returns>The y value on the provided line for the provided x value.</returns>
        public static double GetY(Tuple<double, double> lineFormula, double x)
        {
            if (lineFormula.Item1 == double.NaN) //vertical line.
            {
                return lineFormula.Item2;
            }

            return (lineFormula.Item1 * x) + lineFormula.Item2;
        }
        /// <summary>
        /// Calculates a corrasponding x value for a provided y value and line formula. 
        /// </summary>
        /// <param name="lineFormula">Double tuple containing m (the line slope) in its first item and b (the y intercept) in the second item.
        /// If it is a vertical line then the first item should be double.NaN and the second item should be the x intercept.</param>
        /// <param name="y">The y value on the line.</param>
        /// <returns>The closest integer x value on the provided line for the provided y value. If the provided line is vertical then returns 0.</returns>
        public static int GetX(Tuple<double, double> lineFormula, int y)
        {
            if (lineFormula.Item1 == double.NaN) //vertical line.
            {
                return 0;
            }

            return (int)Math.Round((y - lineFormula.Item2) / lineFormula.Item1);
        }
        /// <summary>
        /// Calculates a corrasponding x value for a provided y value and line formula. 
        /// </summary>
        /// <param name="lineFormula">Double tuple containing m (the line slope) in its first item and b (the y intercept) in the second item.
        /// If it is a vertical line then the first item should be double.NaN and the second item should be the x intercept.</param>
        /// <param name="y">The y value on the line.</param>
        /// <returns>The closest integer x value on the provided line for the provided y value. If the provided line is vertical then returns double.NaN.</returns>
        public static double GetX(Tuple<double, double> lineFormula, double y)
        {
            if (lineFormula.Item1 == double.NaN) //vertical line.
            {
                return double.NaN;
            }

            return (y - lineFormula.Item2) / lineFormula.Item1;
        }
        /// <summary>
        /// Determines if a provided Point is on a provided linear line segment.
        /// </summary>
        /// <param name="line">Tuple containing two points which describe either a vertical or horizontal line segment.</param>
        /// <param name="foo">The point to be investigated.</param>
        /// <returns>True if foo is on the provided linear line segment, False if not. False if the provided line is not veritcal or horizontal.</returns>
        public static bool PointOnLinearAxisLineSegment(Tuple<Point, Point> line, Point foo)
        {
            if (line.Item1.X == line.Item2.X && line.Item1.X == foo.X)
            {
                return (Math.Min(line.Item1.Y, line.Item2.Y) <= foo.Y && foo.Y <= Math.Max(line.Item1.Y, line.Item2.Y));
            }
            else if (line.Item1.Y == line.Item2.Y && line.Item1.Y == foo.Y)
            {
                return (Math.Min(line.Item1.X, line.Item2.X) <= foo.X && foo.X <= Math.Max(line.Item1.X, line.Item2.X));
            }
            return false; //lines are not linear.
        }
    }
}
