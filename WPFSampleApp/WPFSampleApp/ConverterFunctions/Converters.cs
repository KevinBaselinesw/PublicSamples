using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFSampleApp.ConverterFunctions
{
    public class EmployeePhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as byte[];

            byte[] original = value as byte[];
            byte[] adjusted = new byte[original.Length - 78];
            Array.Copy(original, 78, adjusted, 0, original.Length - 78);

            return adjusted;

            //MemoryStream ms = new MemoryStream(adjusted);
            //Image returnImage = Image.FromStream(ms);
            //return returnImage;

            //Bitmap bmp;
            //using (var ms = new MemoryStream(adjusted))
            //{
            //    bmp = new Bitmap(ms);
            //}

            //return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class CategoryPhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as byte[];

            byte[] original = value as byte[];
            byte[] adjusted = new byte[original.Length - 78];
            Array.Copy(original, 78, adjusted, 0, original.Length - 78);

            return adjusted;

            //MemoryStream ms = new MemoryStream(adjusted);
            //Image returnImage = Image.FromStream(ms);
            //return returnImage;

            //Bitmap bmp;
            //using (var ms = new MemoryStream(adjusted))
            //{
            //    bmp = new Bitmap(ms);
            //}

            //return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class EmployeeToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DatabaseAccessLib.Employee employee = value as DatabaseAccessLib.Employee;
            if (employee == null)
                return string.Empty;

            return string.Format($"{employee.LastName}, {employee.FirstName}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
