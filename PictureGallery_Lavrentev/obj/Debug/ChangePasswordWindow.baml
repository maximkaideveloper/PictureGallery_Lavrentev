   M S B A M L   `   `   `  ����Z  VPictureGallery_System_Lavrentev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:PictureGallery_System_LavrentevPictureGallery_System_Lavrentev  5   	   :    4PictureGallery_System_Lavrentev.ChangePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:PictureGallery_System_Lavrentev     5   	     � Title$  Смена пароля��5   	   $	��300��6   $	��500��6+   $	��300��67   $	��500��6G   $	��300��6V   $	��500��6f    � WindowStartupLocation$ CenterScreen=�6u    � 	Resources 5
       �� 
TargetType �� Property �� Value��%B  '��       '�� �     'G� a    �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$
 Left=�64   �� 5       � ��$ 20 0 0 0q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 20q�6'   �� 5       � ��$	 150��6&   �� 5       � �� �	#FFE6F6FF6+   �� 5       � �� �	#FFD2D9DE6,   5      �� 5   
   " G�6   .�5      ��  � ��$ 	20 0 20 0q�6'   �� 5       � ��$ 20��6'   �� 5       � ��$ Center=�62   �� 5       � ��$ Center=�69   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5       �� 5!      $
��1.5* �6    � RowDefinitions 5#   
   �� 5$      �� 5%      �� 5&      �� 5'      .+�5*   
   �� $��Новый пароль:��6   $��0��6)   $��0��66   �� 5+   
   $ ��Старый пароль:��6   $��1��6*   $��0��67   �� 5,   
   $7��1Повторённый старый пароль:��6   $��2��66   $��0��6C   G� 5-   
   -   5        $��NewPasswordBox��5-      $��0��6,   $��1��69   G� 5.   
   -   5        $��OldPasswordBox��5.      $��1��6,   $��1��69   G� 5/   
   -   5        $��RepeatPasswordBox��5/      $��2��6/   $��1��6<   �� 51   
   -   5        $��CloseButton��51      $��Отмена��6$   $��3��65   $��0��6B   $
��Left=�6l   �� 52   
   -   5        $��OkButton��52      $��Подтвердить��6!   $��3��67   $��1��6D   $��Right=�6k   54      55      