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
    /// Логика взаимодействия для UserGallery_Window.xaml
    /// </summary>
    public partial class UserGallery_Window : Window
    {
        StackPanel MaterialsPanel, NewsPanel, ProfilePanel, SignInPanel, SignUpPanel;

        public string projectPath;
        PictureGallery_LavrentevEntities10 db;

        FormedLikePanelClass likePanelClass;
        public Get_Id_Class getId_object;

        User currentUser;
        public string currentUserLogin;
        string userRole;

        public UserGallery_Window()
        {
            InitializeComponent();

            projectPath = Environment.CurrentDirectory;
            projectPath = projectPath.Substring(0, projectPath.Length - 9);

            likePanelClass = new FormedLikePanelClass(projectPath);

            db = new PictureGallery_LavrentevEntities10();

            getId_object = new Get_Id_Class(db);

            setPanelsDefault();

            MainScrollViewer.Content = SignInPanel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из системы?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void MaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainScrollViewer.Content != MaterialsPanel)
            {
                MainScrollViewer.Content = MaterialsPanel;
            }
        }

        private void NewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainScrollViewer.Content != NewsPanel)
            {
                MainScrollViewer.Content = NewsPanel;
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (userRole == null)
            {
                if (MainScrollViewer.Content != SignInPanel && MainScrollViewer.Content != SignUpPanel)
                {
                    MainScrollViewer.Content = SignInPanel;
                }
            }
            else if (MainScrollViewer.Content != ProfilePanel)
            {
                MainScrollViewer.Content = ProfilePanel;
            }
        }


        private void setPanelsDefault()
        {
            SignInPanel = new StackPanel();
            SignUpPanel = new StackPanel();
            ProfilePanel = new StackPanel();
            setSignInPanel();
            setSignUpPanel();
            setMaterialsPanel();
            setNewsPanel();
        }

        private void setAddPostPanel(string userRole)
        {
            Grid addGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            addGrid.ColumnDefinitions.Add(column1);
            addGrid.ColumnDefinitions.Add(column2);

            column1.Width = new GridLength(60);

            RowDefinition row1 = new RowDefinition();
            addGrid.RowDefinitions.Add(row1);

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/adding.jpg", UriKind.RelativeOrAbsolute));
            image.Height = 35;
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);
            addGrid.Children.Add(image);

            Button addButton = new Button();
            addButton.Content = "Добавить новую публикацию";
            Grid.SetRow(addButton, 0);
            Grid.SetColumn(addButton, 1);
            addGrid.Children.Add(addButton);

            image.MouseDown += (S, E) =>
            {
                if (!DialogWindowOpenChecker.dialogWindowIsOpen)
                {
                    if (userRole == "Администратор")
                    {
                        AddPictureWindow addPictureWindow = new AddPictureWindow(db, this);
                    } else if(userRole == "Редактор новостей")
                    {
                        AddNewsWindow addNewsWindow = new AddNewsWindow(db, this);
                    }
                }
                else
                {
                    MessageBox.Show("Вами уже открыто другое диалоговое окно. Нельзя вызвать новое диалоговое окно, если открыто старое");
                }
            };

            addButton.Click += (S, E) =>
            {
                if (!DialogWindowOpenChecker.dialogWindowIsOpen)
                {
                    if (userRole == "Администратор")
                    {
                        AddPictureWindow addPictureWindow = new AddPictureWindow(db, this);
                    }
                    else if (userRole == "Редактор новостей")
                    {
                        AddNewsWindow addNewsWindow = new AddNewsWindow(db, this);
                    }
                }
                else
                {
                    MessageBox.Show("Вами уже открыто другое диалоговое окно. Нельзя вызвать новое диалоговое окно, если открыто старое");
                }
            };

            if (userRole == "Администратор")
            {
                MaterialsPanel.Children.Add(addGrid);
            } else if (userRole == "Редактор новостей")
            {
                NewsPanel.Children.Add(addGrid);
            }

            addGrid.Margin = new Thickness(40, 0, 40, 0);

            addButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            addButton.FontSize = 15;
            addButton.Padding = new Thickness(15, 0, 0, 0);

            image.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void setMaterialsPanel()
        {
            MaterialsPanel = new StackPanel();

            if(userRole == "Администратор")
            {
                setAddPostPanel(userRole);
            }

            foreach (Post p in db.Post)
            {
                if (p.Post_Type_Id == getId_object.getId_PostType("Картина"))
                {
                    addPictureString(p);
                }
            }
        }

        private void setNewsPanel()
        {
            NewsPanel = new StackPanel();

            if (userRole == "Редактор новостей")
            {
                setAddPostPanel(userRole);
            }

            foreach (Post p in db.Post)
            {
                if (p.Post_Type_Id == getId_object.getId_PostType("Новость"))
                {
                    addNewsString(p);
                }
            }
        }

        private void setSignInPanel()
        {
            Grid signInGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            signInGrid.ColumnDefinitions.Add(column1);
            signInGrid.ColumnDefinitions.Add(column2);

            column1.Width = new GridLength(250);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            signInGrid.RowDefinitions.Add(row1);
            signInGrid.RowDefinitions.Add(row2);
            signInGrid.RowDefinitions.Add(row3);
            signInGrid.RowDefinitions.Add(row4);

            SignInPanel.Children.Add(signInGrid);

            Grid titleGrid = new Grid();

            ColumnDefinition column1_Grid2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(column1_Grid2);

            ColumnDefinition row1_Grid2 = new ColumnDefinition();
            ColumnDefinition row2_Grid2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(row1_Grid2);
            titleGrid.ColumnDefinitions.Add(row2_Grid2);

            Grid.SetRow(titleGrid, 0);
            Grid.SetColumn(titleGrid, 0);
            Grid.SetColumnSpan(titleGrid, 2);
            signInGrid.Children.Add(titleGrid);

            Image profileImage = new Image();
            profileImage.Height = 60;
            profileImage.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Account.jpg", UriKind.RelativeOrAbsolute));
            Grid.SetRow(profileImage, 0);
            Grid.SetColumn(profileImage, 0);
            titleGrid.Children.Add(profileImage);

            TextBlock profileTitle = new TextBlock();
            profileTitle.Text = "Раздел авторизации";
            Grid.SetRow(profileTitle, 0);
            Grid.SetColumn(profileTitle, 1);
            titleGrid.Children.Add(profileTitle);

            TextBlock loginTitle = new TextBlock();
            loginTitle.Text = "Логин или Email:";
            Grid.SetRow(loginTitle, 1);
            Grid.SetColumn(loginTitle, 0);
            signInGrid.Children.Add(loginTitle);

            TextBox loginValue = new TextBox();
            Grid.SetRow(loginValue, 1);
            Grid.SetColumn(loginValue, 1);
            signInGrid.Children.Add(loginValue);

            TextBlock passwordTitle = new TextBlock();
            passwordTitle.Text = "Пароль пользователя:";
            Grid.SetRow(passwordTitle, 2);
            Grid.SetColumn(passwordTitle, 0);
            signInGrid.Children.Add(passwordTitle);

            PasswordBox passwordValue = new PasswordBox();
            Grid.SetRow(passwordValue, 2);
            Grid.SetColumn(passwordValue, 1);
            signInGrid.Children.Add(passwordValue);

            Button goToSignUp = new Button();
            goToSignUp.Content = "Перейти к регистрации";
            Grid.SetRow(goToSignUp, 3);
            Grid.SetColumn(goToSignUp, 0);
            signInGrid.Children.Add(goToSignUp);

            goToSignUp.Click += (S, E) =>
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите перейти в раздел регистрации аккаунта?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    loginValue.Text = "";
                    passwordValue.Password = "";
                    MainScrollViewer.Content = SignUpPanel;
                }
            };

            Button okButton = new Button();
            okButton.Content = "Подтвердить";
            Grid.SetRow(okButton, 3);
            Grid.SetColumn(okButton, 1);
            signInGrid.Children.Add(okButton);

            okButton.Click += (S, E) =>
            {
                if (loginValue.Text == "" || passwordValue.Password == "")
                {
                    MessageBox.Show("Пустое значение логина или пароля. Чтобы авторизоваться, необходимо заполнить пустые поля");
                }
                else
                {
                    bool loginExist = false;
                    bool passwordCorrect = false;

                    foreach (User u in db.User)
                    {
                        if (u.User_Login == loginValue.Text || u.Email == loginValue.Text)
                        {
                            loginExist = true;

                            if (u.Password == passwordValue.Password)
                            {
                                passwordCorrect = true;
                                userInitialized(u);
                            }
                        }
                    }

                    if (passwordCorrect)
                    {
                        MessageBox.Show("Авторизация прошла успешно! Теперь Вы находитесь в разделе личного кабинета");
                        setPanelsDefault();
                        setProfilePanel();
                        MainScrollViewer.Content = ProfilePanel;
                    }
                    else
                    {
                        if (loginExist)
                        {
                            MessageBox.Show("Неверный пароль аккаунта. Попробуйте перепроверить введённые данные и ввести их снова");
                        }
                        else
                        {
                            MessageBox.Show("Данного логина, либо адреса электронной почты не существует в системе. Попробуйте перепроверить введённые данные и ввести их снова");
                        }
                    }
                }
            };

            signInGrid.Margin = new Thickness(20, -80, 20, 20);
            signInGrid.Width = 600;

            profileTitle.FontSize = 16;

            SignInPanel.VerticalAlignment = VerticalAlignment.Center;
            titleGrid.HorizontalAlignment = HorizontalAlignment.Center;
            loginTitle.VerticalAlignment = VerticalAlignment.Center;
            passwordTitle.VerticalAlignment = VerticalAlignment.Center;
            profileTitle.VerticalAlignment = VerticalAlignment.Center;

            loginValue.Height = 30;
            passwordValue.Height = 30;

            Thickness verticalMargins = new Thickness(0, 10, 0, 10);

            titleGrid.Margin = new Thickness(0, 10, 0, 40);
            profileTitle.Margin = new Thickness(20, 0, 0, 0);
            loginTitle.Margin = verticalMargins;
            loginValue.Margin = verticalMargins;
            loginValue.VerticalContentAlignment = VerticalAlignment.Center;

            passwordTitle.Margin = verticalMargins;
            passwordValue.Margin = verticalMargins;
            passwordValue.VerticalContentAlignment = VerticalAlignment.Center;

            goToSignUp.Margin = new Thickness(0, 40, 0, 0);
            okButton.Margin = new Thickness(0, 40, 0, 0);

            goToSignUp.Width = 250;
            goToSignUp.Height = 30;
            goToSignUp.HorizontalAlignment = HorizontalAlignment.Left;
            okButton.Width = 250;
            okButton.Height = 30;
            okButton.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void setSignUpPanel()
        {
            Grid signUpGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            signUpGrid.ColumnDefinitions.Add(column1);
            signUpGrid.ColumnDefinitions.Add(column2);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            RowDefinition row5 = new RowDefinition();
            RowDefinition row6 = new RowDefinition();
            signUpGrid.RowDefinitions.Add(row1);
            signUpGrid.RowDefinitions.Add(row2);
            signUpGrid.RowDefinitions.Add(row3);
            signUpGrid.RowDefinitions.Add(row4);
            signUpGrid.RowDefinitions.Add(row5);
            signUpGrid.RowDefinitions.Add(row6);

            SignUpPanel.Children.Add(signUpGrid);

            Grid titleGrid = new Grid();

            ColumnDefinition column1_Grid2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(column1_Grid2);

            ColumnDefinition row1_Grid2 = new ColumnDefinition();
            ColumnDefinition row2_Grid2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(row1_Grid2);
            titleGrid.ColumnDefinitions.Add(row2_Grid2);

            Grid.SetRow(titleGrid, 0);
            Grid.SetColumn(titleGrid, 0);
            Grid.SetColumnSpan(titleGrid, 2);
            signUpGrid.Children.Add(titleGrid);

            Image profileImage = new Image();
            profileImage.Height = 60;
            profileImage.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Account.jpg", UriKind.RelativeOrAbsolute));
            Grid.SetRow(profileImage, 0);
            Grid.SetColumn(profileImage, 0);
            titleGrid.Children.Add(profileImage);

            TextBlock profileTitle = new TextBlock();
            profileTitle.Text = "Раздел регистрации";
            Grid.SetRow(profileTitle, 0);
            Grid.SetColumn(profileTitle, 1);
            titleGrid.Children.Add(profileTitle);

            TextBlock loginTitle = new TextBlock();
            loginTitle.Text = "Логин:";
            Grid.SetRow(loginTitle, 1);
            Grid.SetColumn(loginTitle, 0);
            signUpGrid.Children.Add(loginTitle);

            TextBox loginValue = new TextBox();
            Grid.SetRow(loginValue, 1);
            Grid.SetColumn(loginValue, 1);
            signUpGrid.Children.Add(loginValue);

            TextBlock emailTitle = new TextBlock();
            emailTitle.Text = "Email:";
            Grid.SetRow(emailTitle, 2);
            Grid.SetColumn(emailTitle, 0);
            signUpGrid.Children.Add(emailTitle);

            TextBox emailValue = new TextBox();
            Grid.SetRow(emailValue, 2);
            Grid.SetColumn(emailValue, 1);
            signUpGrid.Children.Add(emailValue);

            TextBlock passwordTitle = new TextBlock();
            passwordTitle.Text = "Пароль:";
            Grid.SetRow(passwordTitle, 3);
            Grid.SetColumn(passwordTitle, 0);
            signUpGrid.Children.Add(passwordTitle);

            PasswordBox passwordValue = new PasswordBox();
            Grid.SetRow(passwordValue, 3);
            Grid.SetColumn(passwordValue, 1);
            signUpGrid.Children.Add(passwordValue);

            TextBlock repeatPasswordTitle = new TextBlock();
            repeatPasswordTitle.Text = "Повторённый пароль:";
            Grid.SetRow(repeatPasswordTitle, 4);
            Grid.SetColumn(repeatPasswordTitle, 0);
            signUpGrid.Children.Add(repeatPasswordTitle);

            PasswordBox repeatPasswordValue = new PasswordBox();
            Grid.SetRow(repeatPasswordValue, 4);
            Grid.SetColumn(repeatPasswordValue, 1);
            signUpGrid.Children.Add(repeatPasswordValue);

            Button goToSignIn = new Button();
            goToSignIn.Content = "Перейти к авторизации";
            Grid.SetRow(goToSignIn, 5);
            Grid.SetColumn(goToSignIn, 0);
            signUpGrid.Children.Add(goToSignIn);

            goToSignIn.Click += (S, E) =>
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите перейти в раздел регистрации аккаунта?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MainScrollViewer.Content = SignInPanel;
                    loginValue.Text = "";
                    emailValue.Text = "";
                    passwordValue.Password = "";
                    repeatPasswordValue.Password = "";
                }
            };

            Button okButton = new Button();
            okButton.Content = "Подтвердить";
            Grid.SetRow(okButton, 5);
            Grid.SetColumn(okButton, 1);
            signUpGrid.Children.Add(okButton);

            okButton.Click += (S, E) =>
            {
                if (CheckField_Class.checkField(loginValue.Text, "Логин", 50, 4))
                {
                    if (CheckField_Class.checkField(emailValue.Text, "Email", 100, 12))
                    {
                        if (CheckField_Class.checkField(passwordValue.Password, "Пароль", 50, 8))
                        {
                            if (CheckField_Class.checkField(repeatPasswordValue.Password, "Повторённый пароль", 50))
                            {
                                if (!emailValue.Text.Contains("@") || !emailValue.Text.Contains("."))
                                {
                                    MessageBox.Show("Поле \"Email\" обязательно должно содержать символ \"@\" и \".\"");
                                }
                                else
                                {
                                    string login = loginValue.Text;
                                    string email = emailValue.Text;

                                    foreach (User u in db.User)
                                    {
                                        if (u.User_Login == login || u.Email == email)
                                        {
                                            login = null;
                                            email = null;
                                            MessageBox.Show("Данный логин, либо Email уже существует в системе");
                                            break;
                                        }
                                    }

                                    if (login != null && email != null)
                                    {
                                        if (passwordValue.Password != repeatPasswordValue.Password)
                                        {
                                            MessageBox.Show("Поля \"Пароль\" и \"Повторённый пароль\" не совпадают по значению");
                                        }
                                        else
                                        {
                                            User user = new User();

                                            user.User_Login = login;
                                            user.Email = email;
                                            user.Password = passwordValue.Password;
                                            user.Signup_Date = DateTime.Now;

                                            foreach (Role r in db.Role)
                                            {
                                                if (r.Role_Name == "Пользователь")
                                                {
                                                    user.Role_Id = r.Role_Id;
                                                    userRole = "Пользователь";
                                                }
                                            }

                                            currentUserLogin = login;
                                            currentUser = user;

                                            db.User.Add(user);
                                            db.SaveChanges();

                                            MessageBox.Show("Регистрация прошла успешно! Теперь Вы находитесь в разделе личного кабинета");
                                            setPanelsDefault();
                                            setProfilePanel();
                                            MainScrollViewer.Content = ProfilePanel;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };


            signUpGrid.Margin = new Thickness(20, -80, 20, 20);
            signUpGrid.Width = 600;

            profileTitle.FontSize = 16;

            SignUpPanel.VerticalAlignment = VerticalAlignment.Center;
            titleGrid.HorizontalAlignment = HorizontalAlignment.Center;
            loginTitle.VerticalAlignment = VerticalAlignment.Center;
            emailTitle.VerticalAlignment = VerticalAlignment.Center;
            passwordTitle.VerticalAlignment = VerticalAlignment.Center;
            repeatPasswordTitle.VerticalAlignment = VerticalAlignment.Center;
            profileTitle.VerticalAlignment = VerticalAlignment.Center;

            loginValue.Height = 30;
            emailValue.Height = 30;
            passwordValue.Height = 30;
            repeatPasswordValue.Height = 30;

            Thickness verticalMargins = new Thickness(0, 10, 0, 10);

            titleGrid.Margin = new Thickness(0, 10, 0, 40);
            profileTitle.Margin = new Thickness(20, 0, 0, 0);

            loginTitle.Margin = verticalMargins;
            loginValue.Margin = verticalMargins;
            loginValue.VerticalContentAlignment = VerticalAlignment.Center;

            emailTitle.Margin = verticalMargins;
            emailValue.Margin = verticalMargins;
            emailValue.VerticalContentAlignment = VerticalAlignment.Center;

            passwordTitle.Margin = verticalMargins;
            passwordValue.Margin = verticalMargins;
            passwordValue.VerticalContentAlignment = VerticalAlignment.Center;

            repeatPasswordTitle.Margin = verticalMargins;
            repeatPasswordValue.Margin = verticalMargins;
            repeatPasswordValue.VerticalContentAlignment = VerticalAlignment.Center;

            goToSignIn.Margin = new Thickness(0, 40, 0, 0);
            okButton.Margin = new Thickness(0, 40, 0, 0);

            goToSignIn.Width = 250;
            goToSignIn.Height = 30;
            goToSignIn.HorizontalAlignment = HorizontalAlignment.Left;
            okButton.Width = 250;
            okButton.Height = 30;
            okButton.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void setProfilePanel()
        {
            Grid profileGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            profileGrid.ColumnDefinitions.Add(column1);
            profileGrid.ColumnDefinitions.Add(column2);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            RowDefinition row5 = new RowDefinition();
            RowDefinition row6 = new RowDefinition();
            RowDefinition row7 = new RowDefinition();
            profileGrid.RowDefinitions.Add(row1);
            profileGrid.RowDefinitions.Add(row2);
            profileGrid.RowDefinitions.Add(row3);
            profileGrid.RowDefinitions.Add(row4);
            profileGrid.RowDefinitions.Add(row5);
            profileGrid.RowDefinitions.Add(row6);
            profileGrid.RowDefinitions.Add(row7);

            ProfilePanel.Children.Add(profileGrid);

            Grid titleGrid = new Grid();

            ColumnDefinition column1_Grid2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(column1_Grid2);

            ColumnDefinition row1_Grid2 = new ColumnDefinition();
            ColumnDefinition row2_Grid2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(row1_Grid2);
            titleGrid.ColumnDefinitions.Add(row2_Grid2);

            Grid.SetRow(titleGrid, 0);
            Grid.SetColumn(titleGrid, 0);
            Grid.SetColumnSpan(titleGrid, 2);
            profileGrid.Children.Add(titleGrid);

            Image profileImage = new Image();
            profileImage.Height = 60;
            profileImage.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Account.jpg", UriKind.RelativeOrAbsolute));
            Grid.SetRow(profileImage, 0);
            Grid.SetColumn(profileImage, 0);
            titleGrid.Children.Add(profileImage);

            TextBlock profileTitle = new TextBlock();
            profileTitle.Text = "Личный кабинет";
            Grid.SetRow(profileTitle, 0);
            Grid.SetColumn(profileTitle, 1);
            titleGrid.Children.Add(profileTitle);

            TextBlock loginTitle = new TextBlock();
            loginTitle.Text = "Логин пользователя:";
            Grid.SetRow(loginTitle, 1);
            Grid.SetColumn(loginTitle, 0);
            profileGrid.Children.Add(loginTitle);

            TextBlock loginValue = new TextBlock();
            loginValue.Text = currentUserLogin;
            Grid.SetRow(loginValue, 1);
            Grid.SetColumn(loginValue, 1);
            profileGrid.Children.Add(loginValue);

            TextBlock emailTitle = new TextBlock();
            emailTitle.Text = "Email пользователя:";
            Grid.SetRow(emailTitle, 2);
            Grid.SetColumn(emailTitle, 0);
            profileGrid.Children.Add(emailTitle);

            TextBlock emailValue = new TextBlock();
            emailValue.Text = currentUser.Email;
            Grid.SetRow(emailValue, 2);
            Grid.SetColumn(emailValue, 1);
            profileGrid.Children.Add(emailValue);

            TextBlock dateTitle = new TextBlock();
            dateTitle.Text = "Дата регистрации пользователя:";
            Grid.SetRow(dateTitle, 3);
            Grid.SetColumn(dateTitle, 0);
            profileGrid.Children.Add(dateTitle);

            TextBlock dateValue = new TextBlock();
            dateValue.Text = getStringDate(currentUser.Signup_Date);
            Grid.SetRow(dateValue, 3);
            Grid.SetColumn(dateValue, 1);
            profileGrid.Children.Add(dateValue);

            Button changePassword = new Button();
            changePassword.Content = "Смена пароля";
            Grid.SetRow(changePassword, 4);
            Grid.SetColumn(changePassword, 0);
            Grid.SetColumnSpan(changePassword, 2);
            profileGrid.Children.Add(changePassword);

            ChangePasswordWindow changePasswordWindow = null;

            changePassword.Click += (S, E) =>
            {
                if (!DialogWindowOpenChecker.dialogWindowIsOpen)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить пароль своего аккаунта?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        changePasswordWindow = new ChangePasswordWindow(db, currentUser);
                    }
                }
                else
                {
                    MessageBox.Show("Вами уже открыто другое диалоговое окно. Нельзя вызвать новое диалоговое окно, если открыто старое");
                }
            };

            Button logout = new Button();
            logout.Content = "Выйти из аккаунта";
            Grid.SetRow(logout, 5);
            Grid.SetColumn(logout, 0);
            Grid.SetColumnSpan(logout, 2);
            profileGrid.Children.Add(logout);

            logout.Click += (S, E) =>
            {
                if (!DialogWindowOpenChecker.dialogWindowIsOpen)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        currentUser = null;
                        userRole = null;
                        currentUserLogin = null;
                        setPanelsDefault();
                        MainScrollViewer.Content = SignInPanel;
                    }
                }
                else
                {
                    MessageBox.Show("Вы не можете выйти из аккаунт, не закрыв открытое вами диалоговое окно");
                }
            };

            profileGrid.Margin = new Thickness(20, -80, 20, 20);
            profileGrid.Width = 500;

            profileTitle.FontSize = 16;

            ProfilePanel.VerticalAlignment = VerticalAlignment.Center;
            titleGrid.HorizontalAlignment = HorizontalAlignment.Center;
            loginTitle.VerticalAlignment = VerticalAlignment.Center;
            loginValue.VerticalAlignment = VerticalAlignment.Center;
            emailTitle.VerticalAlignment = VerticalAlignment.Center;
            emailValue.VerticalAlignment = VerticalAlignment.Center;
            dateTitle.VerticalAlignment = VerticalAlignment.Center;
            dateValue.VerticalAlignment = VerticalAlignment.Center;
            profileTitle.VerticalAlignment = VerticalAlignment.Center;

            Thickness verticalMargins = new Thickness(0, 10, 0, 10);

            titleGrid.Margin = new Thickness(0, 10, 0, 40);
            profileTitle.Margin = new Thickness(20, 0, 0, 0);

            loginTitle.Margin = verticalMargins;
            loginValue.Margin = verticalMargins;
            loginValue.VerticalAlignment = VerticalAlignment.Center;

            emailTitle.Margin = verticalMargins;
            emailValue.Margin = verticalMargins;
            emailValue.VerticalAlignment = VerticalAlignment.Center;

            dateTitle.Margin = verticalMargins;
            dateValue.Margin = verticalMargins;
            dateValue.VerticalAlignment = VerticalAlignment.Center;

            changePassword.Margin = new Thickness(0, 40, 0, 10);
            logout.Margin = verticalMargins;

            changePassword.Height = 30;

            logout.Height = 30;
        }


        public void addPictureString(Post p)
        {
            int pictureId = p.Post_Id;
            string pictureName = p.Name;
            string pictureAuthor = p.Author;
            DateTime pictureDate = p.Upload_Date;
            string pictureFilename = p.Image_FileName;
            string pictureDesc = p.Text;
            int pictureLikeNumber = p.Like_Number;

            Grid pictureGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            pictureGrid.ColumnDefinitions.Add(column1);
            pictureGrid.ColumnDefinitions.Add(column2);
            pictureGrid.ColumnDefinitions.Add(column3);

            column2.Width = new GridLength(1.5, GridUnitType.Star);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            pictureGrid.RowDefinitions.Add(row1);
            pictureGrid.RowDefinitions.Add(row2);
            pictureGrid.RowDefinitions.Add(row3);
            pictureGrid.RowDefinitions.Add(row4);

            row1.Height = new GridLength(80);
            row2.Height = new GridLength(220);
            row3.Height = new GridLength(60);
            row4.Height = new GridLength(60);

            pictureGrid.Margin = new Thickness(40, 20, 40, 20);

            if (userRole == "Администратор")
            {
                ColumnDefinition column4 = new ColumnDefinition();
                pictureGrid.ColumnDefinitions.Add(column4);

                column4.Width = new GridLength(30);

                Image deletePictureIcon = new Image();
                deletePictureIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Cross.jpg", UriKind.RelativeOrAbsolute));
                deletePictureIcon.Height = 20;
                Grid.SetRow(deletePictureIcon, 0);
                Grid.SetColumn(deletePictureIcon, 3);
                pictureGrid.Children.Add(deletePictureIcon);

                deletePictureIcon.VerticalAlignment = VerticalAlignment.Top;
                deletePictureIcon.HorizontalAlignment = HorizontalAlignment.Right;

                deletePictureIcon.MouseDown += (S, E) =>
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную публикацию вместо со всеми комментариями, оставленными под ней?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (Post_Likes pl in db.Post_Likes)
                        {
                            if (pl.Post_Id == pictureId)
                            {
                                db.Post_Likes.Remove(pl);
                            }
                        }

                        db.SaveChanges();

                        foreach (Comment co in db.Comment)
                        {
                            if (co.Post_Id == pictureId)
                            {
                                foreach (Comment_Likes cl in db.Comment_Likes)
                                {
                                    if (cl.Comment_Id == co.Comment_Id)
                                    {
                                        db.Comment_Likes.Remove(cl);
                                    }
                                }

                                db.Comment.Remove(co);
                            }
                        }

                        db.SaveChanges();

                        db.Post.Remove(p);
                        db.SaveChanges();

                        MaterialsPanel.Children.Remove(pictureGrid);
                    }
                };
            }

            TextBlock title = new TextBlock();
            title.Text = "Картина: " + pictureName + "\nАвтор: " + pictureAuthor + "\nДата публикации: " + getStringDate(pictureDate);
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);
            pictureGrid.Children.Add(title);

            TextBlock desc = new TextBlock();
            desc.Text = pictureDesc;
            Grid.SetRow(desc, 1);
            Grid.SetColumn(desc, 0);
            Grid.SetRowSpan(desc, 3);
            pictureGrid.Children.Add(desc);

            Image pictureImage = new Image();
            pictureImage.Source = new BitmapImage(new Uri(@"" + projectPath + "database_pictures/" + pictureFilename, UriKind.RelativeOrAbsolute));
            Grid.SetRow(pictureImage, 0);
            Grid.SetColumn(pictureImage, 1);
            Grid.SetRowSpan(pictureImage, 3);
            pictureGrid.Children.Add(pictureImage);

            Grid likePanelGrid = new Grid();

            ColumnDefinition column1_grid2 = new ColumnDefinition();
            ColumnDefinition column2_grid2 = new ColumnDefinition();
            likePanelGrid.ColumnDefinitions.Add(column1_grid2);
            likePanelGrid.ColumnDefinitions.Add(column2_grid2);

            column1_grid2.Width = new GridLength(40);
            column1_grid2.Width = new GridLength(40);

            RowDefinition row1_grid2 = new RowDefinition();
            likePanelGrid.RowDefinitions.Add(row1_grid2);

            // Create like block for picture block

            StackPanel likeIconPanel = likePanelClass.createLikeIconPanel(likePanelGrid, 20);
            Image likeBlackIcon = likePanelClass.createBlackLike(20);
            Image likeRedIcon = likePanelClass.createRedLike(20);
            TextBlock likeNumber = likePanelClass.createLikeNumber(likePanelGrid, 15);

            likeNumber.Text = pictureLikeNumber.ToString();

            bool status_isLike = false;
            Post_Likes currentLike = new Post_Likes();

            foreach (Post_Likes l in db.Post_Likes)
            {
                if (l.Post_Id == pictureId && l.User_Login == currentUserLogin)
                {
                    status_isLike = true;
                    currentLike = l;
                }
            }

            likePanelClass.setDefaultLikeIcon(status_isLike, likeBlackIcon, likeRedIcon, likeIconPanel);

            likeIconPanel.MouseDown += (S, E) =>
            {
                if (currentUser != null)
                {
                    if (!status_isLike)
                    {
                        currentLike = new Post_Likes();

                        if (getId_object.getLastId_Post_Likes() == 0)
                        {
                            currentLike.Post_Like_Id = 1;
                        }
                        else
                        {
                            currentLike.Post_Like_Id = getId_object.getLastId_Post_Likes() + 1;
                        }

                        currentLike.Post_Id = pictureId;
                        currentLike.User_Login = currentUserLogin;

                        db.Post_Likes.Add(currentLike);
                        p.Like_Number++;
                        status_isLike = true;

                        likePanelClass.addGUILike(likeNumber, likeBlackIcon, likeRedIcon, likeIconPanel);
                    }
                    else
                    {
                        db.Post_Likes.Remove(currentLike);
                        p.Like_Number--;
                        status_isLike = false;

                        likePanelClass.deleteGUILike(likeNumber, likeBlackIcon, likeRedIcon, likeIconPanel);
                    }
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            FormedLikePanelClass.setAlignmentLikeElements(likeBlackIcon, likeRedIcon, likePanelGrid, likeNumber, likeIconPanel);

            // Create comment block for picture block

            ScrollViewer commentSection = new ScrollViewer();
            Grid.SetRow(commentSection, 0);
            Grid.SetColumn(commentSection, 2);
            Grid.SetRowSpan(commentSection, 2);

            commentSection.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            commentSection.Margin = new Thickness(0, 0, 0, 20);

            StackPanel commentSectionPanel = new StackPanel();
            commentSection.Content = commentSectionPanel;

            foreach (Comment c in db.Comment)
            {
                if (c.Post_Id == pictureId)
                {
                    addCommentString(c, commentSectionPanel);
                }
            }

            pictureGrid.Children.Add(commentSection);

            Grid addCommentGrid = new Grid();

            ColumnDefinition column1_grid3 = new ColumnDefinition();
            ColumnDefinition column2_grid3 = new ColumnDefinition();
            ColumnDefinition column3_grid3 = new ColumnDefinition();
            ColumnDefinition column4_grid3 = new ColumnDefinition();
            addCommentGrid.ColumnDefinitions.Add(column1_grid3);
            addCommentGrid.ColumnDefinitions.Add(column2_grid3);
            addCommentGrid.ColumnDefinitions.Add(column3_grid3);
            addCommentGrid.ColumnDefinitions.Add(column4_grid3);

            RowDefinition row1_grid3 = new RowDefinition();
            RowDefinition row2_grid3 = new RowDefinition();
            addCommentGrid.RowDefinitions.Add(row1_grid3);
            addCommentGrid.RowDefinitions.Add(row2_grid3);

            column1_grid3.Width = new GridLength(90);

            column3_grid3.Width = new GridLength(2, GridUnitType.Star);
            column4_grid3.Width = new GridLength(3.5, GridUnitType.Star);

            Image addCommentAccIcon = new Image();
            addCommentAccIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Account.jpg", UriKind.RelativeOrAbsolute));
            addCommentAccIcon.Height = 40;
            Grid.SetRow(addCommentAccIcon, 0);
            Grid.SetColumn(addCommentAccIcon, 0);
            Grid.SetRowSpan(addCommentAccIcon, 2);
            addCommentGrid.Children.Add(addCommentAccIcon);

            addCommentAccIcon.VerticalAlignment = VerticalAlignment.Top;

            TextBox textComment = new TextBox();
            Grid.SetRow(textComment, 0);
            Grid.SetColumn(textComment, 1);
            Grid.SetColumnSpan(textComment, 3);
            addCommentGrid.Children.Add(textComment);

            textComment.TextWrapping = TextWrapping.Wrap;
            textComment.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            if (currentUser == null)
            {
                textComment.IsEnabled = false;
            }

            Button addComment = new Button();
            addComment.Content = "Оставить комментарий";
            Grid.SetRow(addComment, 1);
            Grid.SetColumn(addComment, 3);
            addCommentGrid.Children.Add(addComment);

            addComment.Click += (S, E) =>
            {
                if (currentUser != null)
                {
                    if (textComment.Text != "")
                    {
                        if (textComment.Text.Length < 500)
                        {
                            int voidNumber = 0;
                            char[] textChar = textComment.Text.ToCharArray();
                            foreach (char ch in textChar)
                            {
                                if (ch == ' ')
                                {
                                    voidNumber++;
                                }
                            }

                            if (voidNumber != textComment.Text.Length)
                            {
                                Comment newComment = new Comment();

                                if (getId_object.getLastId_Comment() == 0)
                                {
                                    newComment.Comment_Id = 1;
                                }
                                else
                                {
                                    newComment.Comment_Id = getId_object.getLastId_Comment() + 1;
                                }

                                newComment.Post_Id = pictureId;
                                newComment.User_Login = currentUserLogin;
                                newComment.Text = textComment.Text;
                                newComment.Date = DateTime.Now;
                                newComment.Like_Number = 0;

                                db.Comment.Add(newComment);
                                db.SaveChanges();

                                textComment.Text = "";
                                addCommentString(newComment, commentSectionPanel);
                            }
                            else
                            {
                                MessageBox.Show("Нельзя опубликовать комментарий, состоящий только из пробелов");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Текст комментария превышает допустимое количество символов");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нельзя опубликовать комментарий без текста");
                    }
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            Button cancelComment = new Button();
            cancelComment.Content = "Отмена";
            Grid.SetRow(cancelComment, 1);
            Grid.SetColumn(cancelComment, 2);
            addCommentGrid.Children.Add(cancelComment);

            cancelComment.Click += (S, E) =>
            {
                if (currentUser != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите отменить данное действие?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        textComment.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            Grid.SetRow(likePanelGrid, 3);
            Grid.SetColumn(likePanelGrid, 1);
            pictureGrid.Children.Add(likePanelGrid);

            Grid.SetRow(addCommentGrid, 2);
            Grid.SetColumn(addCommentGrid, 2);
            Grid.SetRowSpan(addCommentGrid, 2);
            pictureGrid.Children.Add(addCommentGrid);

            MaterialsPanel.Children.Add(pictureGrid);

            addComment.Height = 30;
            cancelComment.Height = 30;
            cancelComment.Margin = new Thickness(0, 0, 10, 0);

            title.FontSize = 15;
            desc.FontSize = 14;
            desc.TextWrapping = TextWrapping.Wrap;
        }

        public void addNewsString(Post p)
        {
            int newsId = p.Post_Id;
            string newsName = p.Name;
            string newsAuthor = p.Author;
            DateTime newsDate = p.Upload_Date;
            string newsFilename = p.Image_FileName;
            string newsText = p.Text;
            int newsLikeNumber = p.Like_Number;

            Grid newsGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            newsGrid.ColumnDefinitions.Add(column1);
            newsGrid.ColumnDefinitions.Add(column2);
            newsGrid.ColumnDefinitions.Add(column3);

            column2.Width = new GridLength(1.5, GridUnitType.Star);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            newsGrid.RowDefinitions.Add(row1);
            newsGrid.RowDefinitions.Add(row2);
            newsGrid.RowDefinitions.Add(row3);
            newsGrid.RowDefinitions.Add(row4);

            row1.Height = new GridLength(60);
            row3.Height = new GridLength(60);
            row4.Height = new GridLength(60);

            newsGrid.Margin = new Thickness(40, 20, 40, 20);

            if (userRole == "Редактор новостей")
            {
                ColumnDefinition column4 = new ColumnDefinition();
                newsGrid.ColumnDefinitions.Add(column4);

                column4.Width = new GridLength(30);

                Image deleteNewsIcon = new Image();
                deleteNewsIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Cross.jpg", UriKind.RelativeOrAbsolute));
                deleteNewsIcon.Height = 20;
                Grid.SetRow(deleteNewsIcon, 0);
                Grid.SetColumn(deleteNewsIcon, 3);
                newsGrid.Children.Add(deleteNewsIcon);

                deleteNewsIcon.VerticalAlignment = VerticalAlignment.Top;
                deleteNewsIcon.HorizontalAlignment = HorizontalAlignment.Right;

                deleteNewsIcon.MouseDown += (S, E) =>
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную публикацию вместо со всеми комментариями, оставленными под ней?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (Post_Likes pl in db.Post_Likes)
                        {
                            if (pl.Post_Id == newsId)
                            {
                                db.Post_Likes.Remove(pl);
                            }
                        }

                        db.SaveChanges();

                        foreach (Comment co in db.Comment)
                        {
                            if (co.Post_Id == newsId)
                            {
                                foreach (Comment_Likes cl in db.Comment_Likes)
                                {
                                    if (cl.Comment_Id == co.Comment_Id)
                                    {
                                        db.Comment_Likes.Remove(cl);
                                    }
                                }

                                db.Comment.Remove(co);
                            }
                        }

                        db.SaveChanges();

                        db.Post.Remove(p);
                        db.SaveChanges();

                        NewsPanel.Children.Remove(newsGrid);
                    }
                };
            }


            Grid titleGrid = new Grid();

            ColumnDefinition column1_titleGrid = new ColumnDefinition();
            ColumnDefinition column2_titleGrid = new ColumnDefinition();
            ColumnDefinition column3_titleGrid = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(column1_titleGrid);
            titleGrid.ColumnDefinitions.Add(column2_titleGrid);
            titleGrid.ColumnDefinitions.Add(column3_titleGrid);

            RowDefinition row1_titleGrid = new RowDefinition();
            RowDefinition row2_titleGrid = new RowDefinition();
            titleGrid.RowDefinitions.Add(row1_titleGrid);
            titleGrid.RowDefinitions.Add(row2_titleGrid);

            Grid.SetColumn(titleGrid, 1);
            Grid.SetRow(titleGrid, 0);

            newsGrid.Children.Add(titleGrid);

            TextBlock titleName = new TextBlock();
            titleName.Text = newsName;
            Grid.SetRow(titleName, 1);
            Grid.SetColumn(titleName, 0);
            Grid.SetColumnSpan(titleName, 3);
            titleGrid.Children.Add(titleName);

            TextBlock titleAuthor = new TextBlock();
            titleAuthor.Text = "Автор: " + newsAuthor;
            Grid.SetRow(titleAuthor, 0);
            Grid.SetColumn(titleAuthor, 0);
            titleGrid.Children.Add(titleAuthor);

            TextBlock titleDate = new TextBlock();
            titleDate.Text = getStringDate(newsDate);
            Grid.SetRow(titleDate, 0);
            Grid.SetColumn(titleDate, 2);
            titleGrid.Children.Add(titleDate);

            TextBlock text = new TextBlock();
            text.Text = newsText;
            Grid.SetRow(text, 1);
            Grid.SetColumn(text, 1);
            Grid.SetRowSpan(text, 3);
            newsGrid.Children.Add(text);

            if (text.Text.Length < 500)
            {
                row2.Height = new GridLength(80);
            } else
            {
                row2.Height = new GridLength(200);
            }

            Image newsImage = new Image();
            newsImage.Source = new BitmapImage(new Uri(@"" + projectPath + "database_pictures_news/" + newsFilename, UriKind.RelativeOrAbsolute));
            Grid.SetRow(newsImage, 0);
            Grid.SetColumn(newsImage, 0);
            Grid.SetRowSpan(newsImage, 3);
            newsGrid.Children.Add(newsImage);

            Grid likePanelGrid = new Grid();

            ColumnDefinition column1_grid2 = new ColumnDefinition();
            ColumnDefinition column2_grid2 = new ColumnDefinition();
            likePanelGrid.ColumnDefinitions.Add(column1_grid2);
            likePanelGrid.ColumnDefinitions.Add(column2_grid2);

            column1_grid2.Width = new GridLength(40);
            column1_grid2.Width = new GridLength(40);

            RowDefinition row1_grid2 = new RowDefinition();
            likePanelGrid.RowDefinitions.Add(row1_grid2);

            // Create like block for picture block

            StackPanel likeIconPanel = likePanelClass.createLikeIconPanel(likePanelGrid, 20);
            Image likeBlackIcon = likePanelClass.createBlackLike(20);
            Image likeRedIcon = likePanelClass.createRedLike(20);
            TextBlock likeNumber = likePanelClass.createLikeNumber(likePanelGrid, 15);

            likeNumber.Text = newsLikeNumber.ToString();

            bool status_isLike = false;
            Post_Likes currentLike = new Post_Likes();

            foreach (Post_Likes l in db.Post_Likes)
            {
                if (l.Post_Id == newsId && l.User_Login == currentUserLogin)
                {
                    status_isLike = true;
                    currentLike = l;
                }
            }

            likePanelClass.setDefaultLikeIcon(status_isLike, likeBlackIcon, likeRedIcon, likeIconPanel);

            likeIconPanel.MouseDown += (S, E) =>
            {
                if (currentUser != null)
                {
                    if (!status_isLike)
                    {
                        currentLike = new Post_Likes();

                        if (getId_object.getLastId_Post_Likes() == 0)
                        {
                            currentLike.Post_Like_Id = 1;
                        }
                        else
                        {
                            currentLike.Post_Like_Id = getId_object.getLastId_Post_Likes() + 1;
                        }

                        currentLike.Post_Id = newsId;
                        currentLike.User_Login = currentUserLogin;

                        db.Post_Likes.Add(currentLike);
                        p.Like_Number++;
                        status_isLike = true;

                        likePanelClass.addGUILike(likeNumber, likeBlackIcon, likeRedIcon, likeIconPanel);
                    }
                    else
                    {
                        db.Post_Likes.Remove(currentLike);
                        p.Like_Number--;
                        status_isLike = false;

                        likePanelClass.deleteGUILike(likeNumber, likeBlackIcon, likeRedIcon, likeIconPanel);
                    }
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            FormedLikePanelClass.setAlignmentLikeElements(likeBlackIcon, likeRedIcon, likePanelGrid, likeNumber, likeIconPanel);

            // Create comment block for picture block

            ScrollViewer commentSection = new ScrollViewer();
            Grid.SetRow(commentSection, 0);
            Grid.SetColumn(commentSection, 2);
            Grid.SetRowSpan(commentSection, 2);

            commentSection.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            commentSection.Margin = new Thickness(0, 0, 0, 20);

            StackPanel commentSectionPanel = new StackPanel();
            commentSection.Content = commentSectionPanel;

            foreach (Comment c in db.Comment)
            {
                if (c.Post_Id == newsId)
                {
                    addCommentString(c, commentSectionPanel);
                }
            }

            newsGrid.Children.Add(commentSection);

            Grid addCommentGrid = new Grid();

            ColumnDefinition column1_grid3 = new ColumnDefinition();
            ColumnDefinition column2_grid3 = new ColumnDefinition();
            ColumnDefinition column3_grid3 = new ColumnDefinition();
            ColumnDefinition column4_grid3 = new ColumnDefinition();
            addCommentGrid.ColumnDefinitions.Add(column1_grid3);
            addCommentGrid.ColumnDefinitions.Add(column2_grid3);
            addCommentGrid.ColumnDefinitions.Add(column3_grid3);
            addCommentGrid.ColumnDefinitions.Add(column4_grid3);

            RowDefinition row1_grid3 = new RowDefinition();
            RowDefinition row2_grid3 = new RowDefinition();
            addCommentGrid.RowDefinitions.Add(row1_grid3);
            addCommentGrid.RowDefinitions.Add(row2_grid3);

            column1_grid3.Width = new GridLength(90);

            column3_grid3.Width = new GridLength(2, GridUnitType.Star);
            column4_grid3.Width = new GridLength(3.5, GridUnitType.Star);

            Image addCommentAccIcon = new Image();
            addCommentAccIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Account.jpg", UriKind.RelativeOrAbsolute));
            addCommentAccIcon.Height = 40;
            Grid.SetRow(addCommentAccIcon, 0);
            Grid.SetColumn(addCommentAccIcon, 0);
            Grid.SetRowSpan(addCommentAccIcon, 2);
            addCommentGrid.Children.Add(addCommentAccIcon);

            addCommentAccIcon.VerticalAlignment = VerticalAlignment.Top;

            TextBox textComment = new TextBox();
            Grid.SetRow(textComment, 0);
            Grid.SetColumn(textComment, 1);
            Grid.SetColumnSpan(textComment, 3);
            addCommentGrid.Children.Add(textComment);

            textComment.TextWrapping = TextWrapping.Wrap;
            textComment.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            if (currentUser == null)
            {
                textComment.IsEnabled = false;
            }

            Button addComment = new Button();
            addComment.Content = "Оставить комментарий";
            Grid.SetRow(addComment, 1);
            Grid.SetColumn(addComment, 3);
            addCommentGrid.Children.Add(addComment);

            addComment.Click += (S, E) =>
            {
                if (currentUser != null)
                {
                    if (textComment.Text != "")
                    {
                        if (textComment.Text.Length < 500)
                        {
                            int voidNumber = 0;
                            char[] textChar = textComment.Text.ToCharArray();
                            foreach (char ch in textChar)
                            {
                                if (ch == ' ')
                                {
                                    voidNumber++;
                                }
                            }

                            if (voidNumber != textComment.Text.Length)
                            {
                                Comment newComment = new Comment();

                                if (getId_object.getLastId_Comment() == 0)
                                {
                                    newComment.Comment_Id = 1;
                                }
                                else
                                {
                                    newComment.Comment_Id = getId_object.getLastId_Comment() + 1;
                                }

                                newComment.Post_Id = newsId;
                                newComment.User_Login = currentUserLogin;
                                newComment.Text = textComment.Text;
                                newComment.Date = DateTime.Now;
                                newComment.Like_Number = 0;

                                db.Comment.Add(newComment);
                                db.SaveChanges();

                                textComment.Text = "";
                                addCommentString(newComment, commentSectionPanel);
                            }
                            else
                            {
                                MessageBox.Show("Нельзя опубликовать комментарий, состоящий только из пробелов");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Текст комментария превышает допустимое количество символов");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нельзя опубликовать комментарий без текста");
                    }
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            Button cancelComment = new Button();
            cancelComment.Content = "Отмена";
            Grid.SetRow(cancelComment, 1);
            Grid.SetColumn(cancelComment, 2);
            addCommentGrid.Children.Add(cancelComment);

            cancelComment.Click += (S, E) =>
            {
                if (currentUser != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите отменить данное действие?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        textComment.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            Grid.SetRow(likePanelGrid, 3);
            Grid.SetColumn(likePanelGrid, 1);
            newsGrid.Children.Add(likePanelGrid);

            Grid.SetRow(addCommentGrid, 2);
            Grid.SetColumn(addCommentGrid, 2);
            Grid.SetRowSpan(addCommentGrid, 2);
            newsGrid.Children.Add(addCommentGrid);

            NewsPanel.Children.Add(newsGrid);

            addComment.Height = 30;
            cancelComment.Height = 30;
            cancelComment.Margin = new Thickness(0, 0, 10, 0);

            titleName.FontSize = 17;
            titleName.HorizontalAlignment = HorizontalAlignment.Center;
            titleAuthor.FontSize = 13;
            titleAuthor.HorizontalAlignment = HorizontalAlignment.Left;
            titleDate.FontSize = 13;
            titleDate.HorizontalAlignment = HorizontalAlignment.Right;

            text.FontSize = 14;
            text.TextWrapping = TextWrapping.Wrap;

            titleGrid.Margin = new Thickness(20, 0, 20, 0);
            text.Margin = new Thickness(20, 0, 20, 0);
        }

        private void addCommentString(Comment c, StackPanel commentSectionPanel)
        {
            int commentId = c.Comment_Id;
            string commentLogin = c.User_Login;
            string commentText = c.Text;
            int commentLikeNumber = c.Like_Number;
            string commentDate = getStringDate(c.Date);

            Grid commentGrid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            commentGrid.ColumnDefinitions.Add(column1);
            commentGrid.ColumnDefinitions.Add(column2);

            column1.Width = new GridLength(60);

            TextBlock text = new TextBlock();
            text.Text = commentText;
            Grid.SetRow(text, 1);
            Grid.SetColumn(text, 1);
            commentGrid.Children.Add(text);

            if (c.User_Login == currentUserLogin)
            {
                ColumnDefinition column3 = new ColumnDefinition();
                commentGrid.ColumnDefinitions.Add(column3);

                column3.Width = new GridLength(40);

                Grid changeCommentGrid = new Grid();

                ColumnDefinition column1_grid4 = new ColumnDefinition();
                changeCommentGrid.ColumnDefinitions.Add(column1_grid4);

                RowDefinition row1_grid4 = new RowDefinition();
                RowDefinition row2_grid4 = new RowDefinition();
                RowDefinition row3_grid4 = new RowDefinition();
                changeCommentGrid.RowDefinitions.Add(row1_grid4);
                changeCommentGrid.RowDefinitions.Add(row2_grid4);
                changeCommentGrid.RowDefinitions.Add(row3_grid4);

                Grid.SetRow(changeCommentGrid, 0);
                Grid.SetColumn(changeCommentGrid, 2);
                Grid.SetRowSpan(changeCommentGrid, 3);
                commentGrid.Children.Add(changeCommentGrid);

                Image deleteCommentIcon = new Image();
                deleteCommentIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Cross.jpg", UriKind.RelativeOrAbsolute));
                deleteCommentIcon.Height = 15;
                Grid.SetRow(deleteCommentIcon, 0);
                Grid.SetColumn(deleteCommentIcon, 0);
                changeCommentGrid.Children.Add(deleteCommentIcon);

                deleteCommentIcon.MouseDown += (S, E) =>
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить комментарий?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (Comment_Likes cl in db.Comment_Likes)
                        {
                            if (cl.Comment_Id == c.Comment_Id)
                            {
                                db.Comment_Likes.Remove(cl);
                            }
                        }
                        db.SaveChanges();

                        db.Comment.Remove(c);
                        db.SaveChanges();

                        commentSectionPanel.Children.Remove(commentGrid);
                    }
                };

                Image changeCommentIcon = new Image();
                changeCommentIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Change.png", UriKind.RelativeOrAbsolute));
                changeCommentIcon.Height = 15;
                Grid.SetRow(changeCommentIcon, 1);
                Grid.SetColumn(changeCommentIcon, 0);
                changeCommentGrid.Children.Add(changeCommentIcon);

                Image okCommentIcon = new Image();
                okCommentIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Ok.jpg", UriKind.RelativeOrAbsolute));
                okCommentIcon.Height = 15;
                Grid.SetRow(okCommentIcon, 2);
                Grid.SetColumn(okCommentIcon, 0);

                bool changeCommentStatus = false;
                TextBox textCommentBox = new TextBox();

                changeCommentIcon.MouseDown += (S, E) => {

                    if (!changeCommentStatus)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить комментарий?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            textCommentBox.Text = c.Text;
                            Grid.SetRow(textCommentBox, 1);
                            Grid.SetColumn(textCommentBox, 1);

                            commentGrid.Children.Remove(text);
                            commentGrid.Children.Add(textCommentBox);

                            changeCommentGrid.Children.Add(okCommentIcon);

                            changeCommentStatus = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, нажмите на иконку подтверждения");
                    }
                };

                okCommentIcon.MouseDown += (S, E) =>
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить комментарий?", "Диалоговое окно", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        if (textCommentBox.Text != "")
                        {
                            if (textCommentBox.Text.Length < 500)
                            {
                                int voidNumber = 0;
                                char[] textChar = textCommentBox.Text.ToCharArray();
                                foreach (char ch in textChar)
                                {
                                    if (ch == ' ')
                                    {
                                        voidNumber++;
                                    }
                                }

                                if (voidNumber != textCommentBox.Text.Length)
                                {
                                    c.Text = textCommentBox.Text;
                                    db.SaveChanges();

                                    text.Text = textCommentBox.Text;
                                    textCommentBox.Text = "";

                                    commentGrid.Children.Remove(textCommentBox);
                                    commentGrid.Children.Add(text);
                                    changeCommentGrid.Children.Remove(okCommentIcon);

                                    changeCommentStatus = false;
                                }
                                else
                                {
                                    MessageBox.Show("Нельзя опубликовать комментарий, состоящий только из пробелов");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Текст комментария превышает допустимое количество символов");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Нельзя опубликовать комментарий без текста");
                        }
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        textCommentBox.Text = "";

                        commentGrid.Children.Remove(textCommentBox);
                        commentGrid.Children.Add(text);
                        changeCommentGrid.Children.Remove(okCommentIcon);

                        changeCommentStatus = false;
                    }
                };

                textCommentBox.Margin = new Thickness(0, 10, 0, 10);
                textCommentBox.TextWrapping = TextWrapping.Wrap;
            }

            if (c.User_Login != currentUserLogin && userRole == "Администратор")
            {
                ColumnDefinition column3 = new ColumnDefinition();
                commentGrid.ColumnDefinitions.Add(column3);

                column3.Width = new GridLength(40);

                Image deleteCommentIcon = new Image();
                deleteCommentIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Cross.jpg", UriKind.RelativeOrAbsolute));
                deleteCommentIcon.Height = 15;
                Grid.SetRow(deleteCommentIcon, 0);
                Grid.SetColumn(deleteCommentIcon, 3);
                commentGrid.Children.Add(deleteCommentIcon);

                deleteCommentIcon.MouseDown += (S, E) =>
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данный комментарий?", "Диалоговое окно", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (Comment_Likes cl in db.Comment_Likes)
                        {
                            if (cl.Comment_Id == commentId)
                            {
                                db.Comment_Likes.Remove(cl);
                            }
                        }
                        db.SaveChanges();

                        db.Comment.Remove(c);
                        db.SaveChanges();

                        commentSectionPanel.Children.Remove(commentGrid);
                    }
                };
            }

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            commentGrid.RowDefinitions.Add(row1);
            commentGrid.RowDefinitions.Add(row2);
            commentGrid.RowDefinitions.Add(row3);

            Image commentAccIcon = new Image();
            commentAccIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/Account.jpg", UriKind.RelativeOrAbsolute));
            commentAccIcon.Height = 40;
            Grid.SetRow(commentAccIcon, 0);
            Grid.SetColumn(commentAccIcon, 0);
            Grid.SetRowSpan(commentAccIcon, 3);
            commentGrid.Children.Add(commentAccIcon);

            TextBlock login = new TextBlock();
            login.Text = commentLogin;
            Grid.SetRow(login, 0);
            Grid.SetColumn(login, 1);
            commentGrid.Children.Add(login);

            Grid likePanelGrid = new Grid();

            ColumnDefinition column1_grid2 = new ColumnDefinition();
            ColumnDefinition column2_grid2 = new ColumnDefinition();
            ColumnDefinition column3_grid2 = new ColumnDefinition();
            likePanelGrid.ColumnDefinitions.Add(column1_grid2);
            likePanelGrid.ColumnDefinitions.Add(column2_grid2);
            likePanelGrid.ColumnDefinitions.Add(column3_grid2);

            column1_grid2.Width = new GridLength(20);
            column2_grid2.Width = new GridLength(40);

            RowDefinition row1_grid2 = new RowDefinition();
            likePanelGrid.RowDefinitions.Add(row1_grid2);

            // Create like block for comment block

            StackPanel likeIconPanel = likePanelClass.createLikeIconPanel(likePanelGrid, 15);
            Image likeBlackIcon = likePanelClass.createBlackLike(15);
            Image likeRedIcon = likePanelClass.createRedLike(15);
            TextBlock likeNumber = likePanelClass.createLikeNumber(likePanelGrid);

            likeNumber.Text = commentLikeNumber.ToString();

            bool status_isLike = false;
            Comment_Likes currentLike = new Comment_Likes();

            foreach (Comment_Likes cl in db.Comment_Likes)
            {
                if (cl.Comment_Id == commentId && cl.User_Login == currentUserLogin)
                {
                    status_isLike = true;
                    currentLike = cl;
                }
            }

            likePanelClass.setDefaultLikeIcon(status_isLike, likeBlackIcon, likeRedIcon, likeIconPanel);

            likeIconPanel.MouseDown += (S, E) =>
            {
                if (currentUser != null)
                {
                    if (!status_isLike)
                    {
                        currentLike = new Comment_Likes();

                        if (getId_object.getLastId_Post_Likes() == 0)
                        {
                            currentLike.Comment_Like_Id = 1;
                        }
                        else
                        {
                            currentLike.Comment_Like_Id = getId_object.getLastId_Comment_Likes() + 1;
                        }

                        currentLike.Comment_Id = commentId;
                        currentLike.User_Login = currentUserLogin;

                        db.Comment_Likes.Add(currentLike);
                        c.Like_Number++;
                        status_isLike = true;

                        likePanelClass.addGUILike(likeNumber, likeBlackIcon, likeRedIcon, likeIconPanel);
                    }
                    else
                    {
                        db.Comment_Likes.Remove(currentLike);
                        c.Like_Number--;
                        status_isLike = false;

                        likePanelClass.deleteGUILike(likeNumber, likeBlackIcon, likeRedIcon, likeIconPanel);
                    }
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Данная функция недоступна для неавторизованных пользователей");
                }
            };

            TextBlock date = new TextBlock();
            date.Text = commentDate;
            Grid.SetRow(date, 0);
            Grid.SetColumn(date, 2);
            likePanelGrid.Children.Add(date);

            Grid.SetRow(likePanelGrid, 2);
            Grid.SetColumn(likePanelGrid, 1);
            commentGrid.Children.Add(likePanelGrid);

            commentSectionPanel.Children.Add(commentGrid);

            commentGrid.Margin = new Thickness(40, 10, 20, 10);

            login.FontSize = 14;

            text.Margin = new Thickness(0, 5, 0, 10);
            text.TextWrapping = TextWrapping.Wrap;

            likeNumber.Margin = new Thickness(10, 0, 0, 0);

            commentAccIcon.VerticalAlignment = VerticalAlignment.Top;
            commentAccIcon.Margin = new Thickness(0, 5, 0, 0);
        }


        private void userInitialized(User user)
        {
            currentUser = user;
            currentUserLogin = currentUser.User_Login;
            int userRole_Id = user.Role_Id;

            foreach (Role r in db.Role)
            {
                if (r.Role_Id == userRole_Id)
                {
                    userRole = r.Role_Name;
                }
            }
        }

        private string getStringDate(DateTime date)
        {
            string day, month;

            if (date.Day / 10 <= 1)
            {
                day = "0" + date.Day.ToString();
            }
            else
            {
                day = date.Day.ToString();
            }

            if (date.Month / 10 <= 1)
            {
                month = "0" + date.Month.ToString();
            }
            else
            {
                month = date.Month.ToString();
            }

            return day + "." + month + "." + date.Year;
        }

    }
}
