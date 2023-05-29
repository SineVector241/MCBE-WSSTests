using System.ComponentModel;

namespace MCBE_WSS
{
    public static class EnumExtension
    {
        public static string ToDescriptionString(this EventType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
