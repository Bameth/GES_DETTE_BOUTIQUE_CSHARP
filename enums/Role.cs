namespace csharp.enums
{
    public enum Role
    {
        ADMIN, BOUTIQUIER, CLIENT
    }

    public static class RoleHelper
    {
        public static Role? GetValue(string value)
        {
            foreach (Role r in Enum.GetValues(typeof(Role)))
            {
                if (string.Equals(r.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    return r;
                }
            }
            return null;
        }
    }
}