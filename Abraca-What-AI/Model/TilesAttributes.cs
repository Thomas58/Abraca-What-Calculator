using System;

namespace Abraca_What_AI.Model
{
    class TilesName : Attribute
    {
        public string Name;
        public TilesName(string name) { this.Name = name; }
    }
    class TilesText : Attribute
    {
        public string Text;
        public TilesText(string text) { this.Text = text; }
    }
    class TilesNumber : Attribute
    {
        public byte Number;
        public TilesNumber(byte number) { this.Number = number; }
    }
}
