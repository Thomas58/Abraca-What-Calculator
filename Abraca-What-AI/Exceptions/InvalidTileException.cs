using System;

namespace Abraca_What_AI.Exceptions
{
    class InvalidTileException : Exception
    {
        private string Text;

        public InvalidTileException(string text)
        {
            this.Text = text;
        }

        public override string ToString() => Text;
    }
}
