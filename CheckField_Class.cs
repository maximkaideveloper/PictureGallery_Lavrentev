using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PictureGallery_System_Lavrentev
{
    public static class CheckField_Class
    {
        public static bool checkField(string field, string fieldName, int maxSigns, int minFieldLength)
        {
            if (field == "")
            {
                MessageBox.Show("Пустое значение одного из полей");
                return false;
            }
            else if (field.Length >= maxSigns)
            {
                MessageBox.Show("Значение поля \"" + fieldName + "\" превышает допустимое количество символов");
                return false;
            }
            else if (field.Length < minFieldLength)
            {
                MessageBox.Show("Поле \"" + fieldName + "\" не может содержать менее, чем " + minFieldLength + " символов");
                return false;
            }
            else if (field.Contains(" "))
            {
                MessageBox.Show("Поле \"" + fieldName + "\" не может содержать пробелов");
                return false;
            }

            return true;
        }

        public static bool checkFieldWithVoids(string field, string fieldName, int maxSigns, int minFieldLength)
        {
            if (field == "")
            {
                MessageBox.Show("Пустое значение одного из полей");
                return false;
            }
            else if (field.Length >= maxSigns)
            {
                MessageBox.Show("Значение поля \"" + fieldName + "\" превышает допустимое количество символов");
                return false;
            }
            else if (field.Length < minFieldLength)
            {
                MessageBox.Show("Поле \"" + fieldName + "\" не может содержать менее, чем " + minFieldLength + " символов");
                return false;
            }

            return true;
        }

        public static bool checkField(string field, string fieldName, int maxSigns)
        {
            if (field == "")
            {
                MessageBox.Show("Пустое значение одного из полей");
                return false;
            }
            else if (field.Length >= maxSigns)
            {
                MessageBox.Show("Значение поля \"" + fieldName + "\" превышает допустимое количество символов");
                return false;
            }
            else if (field.Contains(" "))
            {
                MessageBox.Show("Поле \"" + fieldName + "\" не может содержать пробелов");
                return false;
            }

            return true;
        }
    }
}
