using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLesson
{
    internal class SolutionNotFoundException : Exception
    {
        public SolutionNotFoundException(string message)
        : base(message) { }
    }
}
