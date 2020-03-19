namespace flighttracker.functions
{
    public static class StringExtensions
    {
        public static float? GetFloatValue(this string data)
        {
            return string.IsNullOrEmpty(data) ? (float?)null : float.Parse(data.Trim());
        }

        public static string GetStringValue(this string data)
        {
            return data == null ? string.Empty : data.Trim();
        }
    }
}