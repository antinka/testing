using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.Tests.Calculation
{
    public class CalculateTestsResult
    {
        public double CalculateMark(int countQuestionInTest, int countRightAnsw)
        {
            double mark = 0.0;
            return mark = (double)(100 / countQuestionInTest) * countRightAnsw;
        }
    }
}
