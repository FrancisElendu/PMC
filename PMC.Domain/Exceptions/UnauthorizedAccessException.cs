using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Domain.Exceptions
{
    public class UnauthorizedAccessException(string message) : Exception(message)
    {
    }
}
