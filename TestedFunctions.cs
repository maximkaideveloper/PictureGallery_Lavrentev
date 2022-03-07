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
    public class TestedFunctions
    {
        PictureGallery_LavrentevEntities10 db;

        public TestedFunctions()
        {
            db = new PictureGallery_LavrentevEntities10();
        }

        public bool signIn(string login, string password)
        {
            if (login == "" || password == "")
            {
                return false;
            }
            else
            {
                bool passwordCorrect = false;

                foreach(User u in db.User)
                {
                    if (u.User_Login == login || u.Email == login)
                    {
                        if (u.Password == password)
                        {
                            passwordCorrect = true;
                        }
                    }
                }

                if (passwordCorrect)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool signUp(string login, string email, string password, string repeatPassword)
        {
            if (CheckField_Class.checkField(login, "Логин", 50, 4))
            {
                if (CheckField_Class.checkField(email, "Email", 100, 12))
                {
                    if (CheckField_Class.checkField(password, "Пароль", 50, 8))
                    {
                        if (CheckField_Class.checkField(repeatPassword, "Повторённый пароль", 50))
                        {
                            if (!email.Contains("@") || !email.Contains("."))
                            {
                                return false;
                            }
                            else
                            {
                                foreach (User u in db.User)
                                {
                                    if (u.User_Login == login || u.Email == email)
                                    {
                                        return false;
                                    }
                                }

                                if (login != null && email != null)
                                {
                                    if (password != repeatPassword)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool changePassword(string newPassword, string oldPassword, string repeatPassword, string thisUserLogin)
        {
            if(!userIsExist(thisUserLogin))
            {
                return false;
            }

            if (CheckField_Class.checkField(newPassword, "Новый пароль", 50, 8))
            {
                if (CheckField_Class.checkField(oldPassword, "Старый пароль", 50))
                {
                    if (CheckField_Class.checkField(repeatPassword, "Повторённый старый пароль", 50))
                    {
                        foreach (User u in db.User)
                        {
                            if (u.User_Login == thisUserLogin && u.Password == oldPassword)
                            {
                                if (oldPassword == repeatPassword)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool addPicture(string name, string author, string fileName, string desc, string thisUserLogin)
        {
            if(!userIsAdmin(thisUserLogin))
            {
                return false;
            }

            if (CheckField_Class.checkFieldWithVoids(name, "Название картины", 50, 2))
            {
                if (CheckField_Class.checkFieldWithVoids(author, "Автор картины", 50, 6))
                {
                    if (fileName != "")
                    {
                        if (CheckField_Class.checkFieldWithVoids(desc, "Описание картины", 1000, 10))
                        {
                            Exception testException = null;

                            try
                            {
                                Image testPicture = new Image();
                                testPicture.Source = new BitmapImage(new Uri(@"C:\Users\Max\source\repos\PictureGallery_System_Lavrentev\database_pictures/" + fileName, UriKind.RelativeOrAbsolute));
                            }
                            catch (System.IO.FileNotFoundException ex)
                            {
                                return false;
                            }

                            if (testException == null)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool deletePicture(int pictureId, string thisUserLogin)
        {
            if (!userIsAdmin(thisUserLogin))
            {
                return false;
            }

            Get_Id_Class get_Id_Class = new Get_Id_Class(db);

            foreach (Post p in db.Post)
            {
                if(p.Post_Id == pictureId && p.Post_Type_Id == get_Id_Class.getId_PostType("Картина"))
                {
                    return true;
                }
            }

            return false;
        }

        public bool changeComment(string changedText, int commentId, string thisUserLogin)
        {
            if(!userIsExist(thisUserLogin))
            {
                return false;
            }

            bool commentCheck = false;

            foreach(Comment c in db.Comment)
            {
                if(c.Comment_Id == commentId && c.User_Login == thisUserLogin)
                {
                    commentCheck = true;
                }
            }

            if(!commentCheck)
            {
                return false;
            }

            if (changedText != "")
            {
                if (changedText.Length < 500)
                {
                    int voidNumber = 0;
                    char[] textChar = changedText.ToCharArray();
                    foreach (char ch in textChar)
                    {
                        if (ch == ' ')
                        {
                            voidNumber++;
                        }
                    }

                    if (voidNumber != changedText.Length)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            } else
            {
                    return false;
            }
        }




        private bool userIsExist(string login)
        {
            foreach(User u in db.User)
            {
                if(u.User_Login == login)
                {
                    return true;
                }
            }

            return false;
        }

        private string getUserRole(string login)
        {
            int roleId = 0;  

            foreach(User u in db.User)
            {
                if(u.User_Login == login)
                {
                    roleId = u.Role_Id;
                }
            }

            foreach(Role r in db.Role)
            {
                if(r.Role_Id == roleId)
                {
                    return r.Role_Name;
                }
            }

            return "";
        }

        private bool userIsAdmin(string login)
        {
            if (!userIsExist(login))
            {
                return false;
            }

            if (getUserRole(login) != "Администратор")
            {
                return false;
            }

            return true;
        }
    }
}
