   M S B A M L   `   `   `  ����Z  VPictureGallery_System_Lavrentev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:PictureGallery_System_LavrentevPictureGallery_System_Lavrentev  5   	   3    -PictureGallery_System_Lavrentev.AddNewsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:PictureGallery_System_Lavrentev     5   	   -   5          � Title$4  .Публикация новой новости��5   	   $	��450��6*   $	��650��67   $	��450��6C   $	��650��6S   $	��450��6b   $	��650��6r    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%B  '��       '�� �     '�� a    �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$
 Left=�64   �� 5       � ��$ 20 0 0 0q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 20q�6'   �� 5       � ��$	 150��6&   �� 5       � �� �	#FFE6F6FF6+   �� 5       � �� �	#FFD2D9DE6,   5      �� 5   
   " ��6   .�5      ��  � ��$ 	20 0 20 0q�6'   �� 5       � ��$ 20��6'   �� 5       � ��$ Center=�62   �� 5       � ��$ Center=�69   5      5      .��5       �  � ColumnDefinitions 5!   
   �� 5"      $	��200 �6   �� 5#       � RowDefinitions 5%   
   �� 5&      �� 5'      �� 5(      $	��220 �6   �� 5)      .+�5,   
   �� $&�� Название новости:��6   $��0��6-   $��0��6=   $��2��6J   �� 5-   
   $9��3Название файла изображения:��6   $��0��67   $��1��6G   $��2��6T   �� 5.   
   $ ��Текст новости:��6   $��0��6*   $��2��6:   $��2��6G   $	��Top=�6[   $��
20 20 20 0q�6s   �� 50   
   -   5         ��Name$ nameTextBox��50      $��2��6'   $��0��67   �� 51   
   -   5        $ fileNameTextBox��51      $��2��6+   $��1��6;   �� 52   
   -   5        $ textNewsTextBox��52      $��2��6+   $��2��6;   $	��Top=�6H   $��
20 20 20 0q�6`   $	��190��6t   	 �� TextWrapping$
	 Wrap=�6�   $	��Top=�6�   "
 �� VerticalScrollBarVisibility$

 Auto=�6�   �� 54   
   -   5        $��CancelButton��54      $��Отмена��6%   $��0��66   $��3��6F   $
��Left=�6n   �� 55   
   -   5        $��OkButton��55      $��Опубликовать��6!   $��2��68   $��3��6H   $��Right=�6l   56      57      