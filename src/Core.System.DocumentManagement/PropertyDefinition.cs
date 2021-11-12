namespace Core.System.DocumentManagement
{
    public class PropertyDefinition
    {
        private readonly string _value;

        public PropertyDefinition(string propertyType, string value)
        {
            PropertyName = propertyType;
            _value = value;
        }

        public string PropertyName { get; }

        

        public string GetValue()
        {
            return _value;
        }
    }
}
