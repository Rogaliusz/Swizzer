using Swizzer.Shared.Common.Domain.Users.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Swizzer.Client.Windows.Framework.Converters
{
    public class UserDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var userDto = value as UserDto;
            return $"{userDto.Name} {userDto.Surname}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
