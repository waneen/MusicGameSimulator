namespace MGS.Utility
{
    public static class Base36
    {
        static readonly string ValueMap = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static int StringToInt(string key)
        {
            key = key.ToUpper();
            return ValueMap.IndexOf(key[0]) + ValueMap.IndexOf(key[1]);
        }
    }
}
