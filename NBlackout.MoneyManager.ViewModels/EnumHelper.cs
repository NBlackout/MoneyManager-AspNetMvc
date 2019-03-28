using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NBlackout.MoneyManager.ViewModels
{
    public static class EnumHelper
    {
        public static string DisplayName(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                if (attributes != null)
                {
                    var attribute = attributes.FirstOrDefault();
                    if (attribute != null)
                        return attribute.GetName();
                }
            }

            return value.ToString();
        }
    }
}
