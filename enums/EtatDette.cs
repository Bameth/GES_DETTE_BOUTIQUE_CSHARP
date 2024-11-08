namespace csharp.enums
{
    public enum EtatDette
    {
        SOLDEES,
        NONSOLDEES,
        ARCHIVER
    }
    public static class EtatDetteHelper
    {
        public static EtatDette? GetValue(string value)
        {
            foreach (EtatDette t in Enum.GetValues(typeof(EtatDette)))
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