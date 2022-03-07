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
    /// Логика взаимодействия для AddNewsWindow.xaml
    /// </summary>
    public partial class AddNewsWindow : Window
    {
        PictureGallery_LavrentevEntities10 db;
        UserGallery_Window parentWindow;
        Post news;

        public AddNewsWindow(PictureGallery_LavrentevEntities10 db, UserGallery_Window parentWindow)
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

            if (result == MessageBoxResult.Yes)
            {
                DialogWindowOpenChecker.dialogWindowIsOpen = false;
                Close();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField_Class.checkFieldWithVoids(nameTextBox.Text, "Название новости", 50, 8))
            {
                if (fileNameTextBox.Text != "")
                {
                    if (CheckField_Class.checkFieldWithVoids(textNewsTextBox.Text, "Текст новости", 1000, 50))
                    {
                        Exception testException = null;

                        try
                        {
                            Image testPicture = new Image();
                            testPicture.Source = new BitmapImage(new Uri(@"" + parentWindow.projectPath + "database_pictures_news/" + fileNameTextBox.Text, UriKind.RelativeOrAbsolute));
                        }
                        catch (System.IO.FileNotFoundException ex)
                        {
                            testException = ex;
                            MessageBox.Show("Данного названия файла изображения не существует в директиве проекта");
                        }

                        if (testException == null)
                        {
                            news = new Post();

                            if (parentWindow.getId_object.getLastId_Post() == 0)
                            {
                                news.Post_Id = 1;
                            }
                            else
                            {
                                news.Post_Id = parentWindow.getId_object.getLastId_Post() + 1;
                            }

                            news.Name = nameTextBox.Text;
                            news.Author = parentWindow.currentUserLogin;
                            news.Image_FileName = fileNameTextBox.Text;
                            news.Text = textNewsTextBox.Text;
                            news.Upload_Date = DateTime.Now;
                            news.Like_Number = 0;

                            foreach (Post_Type p in db.Post_Type)
                            {
                                if (p.Post_Type_Name == "Новость")
                                {
                                    news.Post_Type_Id = p.Post_Type_Id;
                                }
                            }

                            parentWindow.addNewsString(news);

                            db.Post.Add(news);
                            db.SaveChanges();

                            DialogWindowOpenChecker.dialogWindowIsOpen = false;
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пустое значение одного из полей");
                }
            }
        }
    }
}
