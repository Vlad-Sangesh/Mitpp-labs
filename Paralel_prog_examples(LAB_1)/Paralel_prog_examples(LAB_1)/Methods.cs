using System;
using System.Collections.Generic;
using System.Text;

namespace Paralel_prog_examples_LAB_1_
{
    public class Methods
    {
        public double function_pi(int i, int depth, int current_depth)
        {
            if (current_depth == depth)
            {
                return 1 + i / (2.0 * i + 1);
            }
            else
            {
                current_depth++;
                return 1 + i / (2.0 * i + 1) * function_pi(i + 1, depth, current_depth);
            }
        }
        public double factorial_Recursion(int number)
        {
            if (number == 1)
            {
                return 1;
            }
            else
            {
                return number * factorial_Recursion(number - 1);
            }
        }
    }
}
