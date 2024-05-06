namespace Shared.Common.Attributes
{
    public class StringAttribute : System.Attribute
    {

        private string _value;

        public StringAttribute(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}