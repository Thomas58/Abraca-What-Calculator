using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abraca_What_AI.Exceptions
{
    class InvalidTileOperationException : Exception
    {
        private string Text;

        public InvalidTileOperationException(string text)
        {
            this.Text = text;
        }

        public override string ToString() => Text;
    }
}
