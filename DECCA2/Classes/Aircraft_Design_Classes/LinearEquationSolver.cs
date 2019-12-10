using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DECCA2
{
    public class LinearEquationSolver
    {
        List<LinearEquation> rows = new List<LinearEquation>();
        double[] solution;

        public void AddLinearEquation(double result, params double[] coefficients)
        {
            rows.Add(new LinearEquation(result, coefficients));
        }

        public IList<double> Solve()  //Returns a list of coefficients for the variables in the same order they were entered
        {
            solution = new double[rows[0].Coefficients.Count()];

            for (int pivotM = 0; pivotM < rows.Count() - 1; pivotM++)
            {
                int pivotN = rows[pivotM].IndexOfFirstNonZero;

                for (int i = pivotN + 1; i < rows.Count(); i++)
                {
                    LinearEquation rowToReduce = rows[i];
                    double pivotFactor = rowToReduce[pivotN] / -rows[pivotM][pivotN];
                    rowToReduce.AddCoefficients(rows[pivotM], pivotFactor);
                }
            }

            while (rows.Any(r => r.Result != 0))
            {
                LinearEquation row = rows.FirstOrDefault(r => r.NonZeroCount == 1);
                if (row == null)
                {
                    break;
                }

                int solvedIndex = row.IndexOfFirstNonZero;
                double newSolution = row.Result / row[solvedIndex];

                AddToSolution(solvedIndex, newSolution);
            }

            

            return solution;
        }

        private void AddToSolution(int index, double value)
        {
            foreach (LinearEquation row in rows)
            {
                double coefficient = row[index];
                row[index] -= coefficient;
                row.Result -= coefficient * value;
            }

            solution[index] = value;
        }

        private class LinearEquation
        {
            public double[] Coefficients { get; set; }
            public double Result { get; set; }
            

            public LinearEquation(double result, params double[] coefficients)
            {
                //this.Coefficients = null;
                this.Coefficients = coefficients;
                this.Result = result;
            }

            
            public double this[int i]
            {                
                get
                {                    
                    return Coefficients[i];                    
                }
                set { Coefficients[i] = value; }                
            }
            

            public void AddCoefficients(LinearEquation pivotEquation, double factor)
            {
                for (int i = 0; i < this.Coefficients.Length; i++)
                {
                    Coefficients[i] += pivotEquation.Coefficients[i] * factor;
                    if (Math.Abs(this[i]) < 0.000000001)    //Because sometimes rounding errors mean it's not quite zero, and it needs to be
                    {
                        Coefficients[i] = 0;
                    }
                }

                this.Result += pivotEquation.Result * factor;
            } 

            public int IndexOfFirstNonZero
            {
                get
                {
                    for (int i = 0; i < Coefficients.Length; i++)
                    {
                        if (this[i] != 0) return i;
                    }
                    return -1;
                }
            }

            public int NonZeroCount
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < Coefficients.Length; i++)
                    {
                        if (this[i] != 0) count++;
                    }
                    return count;
                }
            }
            
        }
    }
}
