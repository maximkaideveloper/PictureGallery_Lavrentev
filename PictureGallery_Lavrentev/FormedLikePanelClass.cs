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
    public class FormedLikePanelClass
    {
        private string projectPath;

        public FormedLikePanelClass(string projectPath)
        {
            this.projectPath = projectPath;
        }

        public TextBlock createLikeNumber(Grid likeGrid)
        {
            TextBlock likeNumber = new TextBlock();
            Grid.SetRow(likeNumber, 0);
            Grid.SetColumn(likeNumber, 1);
            likeGrid.Children.Add(likeNumber);
            return likeNumber;
        }

        public TextBlock createLikeNumber(Grid likeGrid, int fontSize)
        {
            TextBlock likeNumber = new TextBlock();
            likeNumber.FontSize = fontSize;
            Grid.SetRow(likeNumber, 0);
            Grid.SetColumn(likeNumber, 1);
            likeGrid.Children.Add(likeNumber);
            return likeNumber;
        }

        public Image createBlackLike(int height)
        {
            Image likeBlackIcon = new Image();
            likeBlackIcon.Height = height;
            likeBlackIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/BlackLike.png", UriKind.RelativeOrAbsolute));
            return likeBlackIcon;
        }

        public Image createRedLike(int height)
        {
            Image likeRedIcon = new Image();
            likeRedIcon.Height = height;
            likeRedIcon.Source = new BitmapImage(new Uri(@"" + projectPath + "app_icons/RedLike.png", UriKind.RelativeOrAbsolute));
            return likeRedIcon;
        }

        public StackPanel createLikeIconPanel(Grid likeGrid, int height)
        {
            StackPanel likeIconPanel = new StackPanel();
            likeIconPanel.Height = height;
            likeGrid.Children.Add(likeIconPanel);
            return likeIconPanel;
        }

        public void setDefaultLikeIcon(bool status_isLike, Image likeBlackIcon, Image likeRedIcon, StackPanel likeIconPanel)
        {
            if (!status_isLike)
            {
                Grid.SetRow(likeBlackIcon, 0);
                Grid.SetColumn(likeBlackIcon, 0);
                likeIconPanel.Children.Add(likeBlackIcon);
            }
            else
            {
                Grid.SetRow(likeRedIcon, 0);
                Grid.SetColumn(likeRedIcon, 0);
                likeIconPanel.Children.Add(likeRedIcon);
            }
        }

        public void addGUILike(TextBlock likeNumber, Image likeBlackIcon, Image likeRedIcon, StackPanel likeIconPanel)
        {
            int n = Int32.Parse(likeNumber.Text);
            n++;
            likeNumber.Text = n.ToString();

            likeIconPanel.Children.Remove(likeBlackIcon);
            likeIconPanel.Children.Add(likeRedIcon);
        }

        public void deleteGUILike(TextBlock likeNumber, Image likeBlackIcon, Image likeRedIcon, StackPanel likeIconPanel)
        {
            int n = Int32.Parse(likeNumber.Text);
            n--;
            likeNumber.Text = n.ToString();

            likeIconPanel.Children.Remove(likeRedIcon);
            likeIconPanel.Children.Add(likeBlackIcon);
        }

        public static void setAlignmentLikeElements(Image likeBlackIcon, Image likeRedIcon, Grid likePanelGrid, TextBlock likeNumber, StackPanel likeIconPanel)
        {
            likeIconPanel.VerticalAlignment = VerticalAlignment.Center;
            likeBlackIcon.VerticalAlignment = VerticalAlignment.Center;
            likeRedIcon.VerticalAlignment = VerticalAlignment.Center;
            likeNumber.VerticalAlignment = VerticalAlignment.Center;
            likePanelGrid.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }
}
