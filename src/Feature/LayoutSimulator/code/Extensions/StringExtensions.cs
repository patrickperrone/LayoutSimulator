namespace Sitecore.Feature.LayoutSimulator.Extensions
{
    public static class StringExtensions
    {
        public static string ToStringForLogging(this string value)
        {
            return $"{Configuration.Settings.LogMessagePrefix} {value}";
        }
    }
}