namespace csharp.enums
{
public enum TypeDette {
    ENCOURS, ANNULER, ACCEPTER, ARCHIVER
    }

     public static class TypeDetteHelper
    {
        public static TypeDette? GetValue(string value)
        {
            foreach (TypeDette t in Enum.GetValues(typeof(TypeDette)))
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
