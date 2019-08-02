using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Common.Exceptions
{
    public class NameDuplicateException:Exception
    {
        public NameDuplicateException()
        {

        }

        public NameDuplicateException(string message):base(message)
        {

        }

        public NameDuplicateException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
