using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLesson
{
    internal class SolutionOutOfBordersException : Exception
    {
        public SolutionOutOfBordersException(string message)
        : base(message) { }
    }
}
