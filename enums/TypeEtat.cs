namespace csharp.enums
{

    public enum TypeEtat
    {
        ACTIVER, DESACTIVER
    }
    public static class TypeEtatHelper
    {
        public static TypeEtat? GetValue(string value)
        {
            foreach (TypeEtat t in Enum.GetValues(typeof(TypeEtat)))
            {
                if (string.Equals(t.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    return t;
                }
            }
            return null;
        }
    }
}
