namespace Ett.Vdt.Extensions
{
    public static class ExtensionsToString
    {
        public static string CloneWithCharArray(this string src)
        {
            if (src == null)
                return null;

            var srcChars = src.ToCharArray();

            var tempChar = new char[srcChars.Length];

            for (var i = 0; i < tempChar.Length; i++)
                tempChar[i] = srcChars[i];
            
            return new string(tempChar);
        }
    }
}