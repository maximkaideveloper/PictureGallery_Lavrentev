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
    /// <summary>
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        PictureGallery_LavrentevEntities10 db;
        User thisUser;

        public ChangePasswordWindow(PictureGallery_LavrentevEntities10 db, User thisUser)
        {
            InitializeComponent();
            DialogWindowOpenChecker.dialogWindowIsOpen = true;
            this.db = db;
            this.thisUser = thisUser;
            Show();

            Closed += ChangePasswordWindow_Closed;
        }

        private void ChangePasswordWindow_Closed(object sender, EventArgs e)
        {
            DialogWindowOpenChecker.dialogWindowIsOpen = false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите отменить данное действие?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DialogWindowOpenChecker.dialogWindowIsOpen = false;
                Close();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckField_Class.checkField(NewPasswordBox.Password, "Новый пароль", 50, 8))
            {
                if(CheckField_Class.checkField(OldPasswordBox.Password, "Старый пароль", 50))
                {
                    if(CheckField_Class.checkField(RepeatPasswordBox.Password, "Повторённый старый пароль", 50))
                    {
                        string oldPassword = null;

                        foreach (User u in db.User)
                        {
                            if (u.User_Login == thisUser.User_Login && u.Password == OldPasswordBox.Password)
                            {
                                oldPassword = OldPasswordBox.Password;
                            }
                        }

                        if (oldPassword != null)
                        {
                            if (OldPasswordBox.Password == RepeatPasswordBox.Password)
                            {
                                foreach (User u in db.User)
                                {
                                    if (u.User_Login == thisUser.User_Login)
                                    {
                                        u.Password = NewPasswordBox.Password;
                                        thisUser.Password = NewPasswordBox.Password;
                                    }
                                }

                                db.SaveChanges();

                                MessageBox.Show("Пароль пользователя был успешно изменён!");
                                DialogWindowOpenChecker.dialogWindowIsOpen = false;
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Поля \"Старый пароль\" и \"Повторённый старый пароль\" не совпадают по значению");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный старый пароль пользователя");
                        }
                    }
                }
            } 
        }
    }
}
