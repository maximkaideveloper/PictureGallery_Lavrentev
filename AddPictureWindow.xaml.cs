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
    /// Логика взаимодействия для AddPictureWindow.xaml
    /// </summary>
    public partial class AddPictureWindow : Window
    {
        PictureGallery_LavrentevEntities10 db;
        UserGallery_Window parentWindow;
        Post picture;

        public AddPictureWindow(PictureGallery_LavrentevEntities10 db, UserGallery_Window parentWindow)
        {
            InitializeComponent();
            DialogWindowOpenChecker.dialogWindowIsOpen = true;
            this.db = db;
            this.parentWindow = parentWindow;
            Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DialogWindowOpenChecker.dialogWindowIsOpen = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите отменить данное действие?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                DialogWindowOpenChecker.dialogWindowIsOpen = false;
                Close();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckField_Class.checkFieldWithVoids(nameTextBox.Text, "Название картины", 50, 2))
            {
                if(CheckField_Class.checkFieldWithVoids(authorTextBox.Text, "Автор картины", 50, 6))
                {
                    if(fileNameTextBox.Text != "")
                    {
                        if (CheckField_Class.checkFieldWithVoids(descTextBox.Text, "Описание картины", 1000, 10))
                        {
                            Exception testException = null;

                            try
                            {
                                Image testPicture = new Image();
                                testPicture.Source = new BitmapImage(new Uri(@"" + parentWindow.projectPath + "database_pictures/" + fileNameTextBox.Text, UriKind.RelativeOrAbsolute));
                            } catch(System.IO.FileNotFoundException ex)
                            {
                                testException = ex;
                                MessageBox.Show("Данного названия файла изображения не существует в директиве проекта");
                            }

                            if(testException == null)
                            {
                                picture = new Post();

                                if (parentWindow.getId_object.getLastId_Post() == 0)
                                {
                                    picture.Post_Id = 1;
                                }
                                else
                                {
                                    picture.Post_Id = parentWindow.getId_object.getLastId_Post() + 1;
                                }

                                picture.Name = nameTextBox.Text;
                                picture.Author = authorTextBox.Text;
                                picture.Image_FileName = fileNameTextBox.Text;
                                picture.Text = descTextBox.Text;
                                picture.Upload_Date = DateTime.Now;
                                picture.Like_Number = 0;

                                foreach (Post_Type p in db.Post_Type)
                                {
                                    if (p.Post_Type_Name == "Картина")
                                    {
                                        picture.Post_Type_Id = p.Post_Type_Id;
                                    }
                                }

                                parentWindow.addPictureString(picture);

                                db.Post.Add(picture);
                                db.SaveChanges();

                                DialogWindowOpenChecker.dialogWindowIsOpen = false;
                                Close();
                            }
                        }
                    } else
                    {
                        MessageBox.Show("Пустое значение одного из полей");
                    }
                }
            }
        }
    }
}
