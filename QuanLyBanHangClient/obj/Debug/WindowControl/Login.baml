   M S B A M L   `   `   `  ����N  JQuanLyBanHangClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:QuanLyBanHangClient.WindowControl!QuanLyBanHangClient.WindowControl  5   	   I)clr-namespace:LoadingPanelSample.ControlsLoadingPanelSample.Controls  5   	   -    'QuanLyBanHangClient.WindowControl.Login   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:QuanLyBanHangClient.WindowControl     5   	   >controls)clr-namespace:LoadingPanelSample.Controls     5   	     � Title$+  %Quản lý nhà hàng - Đăng nhập��5	   	   $	��320��6.   $	��500��6;    � 
ResizeMode$ NoResize=�6G      MainSolidColor#��[  6^    � WindowStartupLocation$ CenterScreen=�6�   .��5
      � .+�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��2* �6    �� 5      $��* �6    .+�5      �� $��0,12,0,0q�6   $��UVN Ke Chuyen1)�6*   $
��Bold"�6F   $��22(�6X   $��LeftToRight=�6g   
�������$��Center=�6�    �� TextAlignment$ Center=�6�   ��5      �� 5       �� EndPoint$ 0,0.5&�6*    �� 
StartPoint$ 1,0.5&�6;   .��5      �  � Offset$ 0Q�6'   � 5       � Color$ 	#FFBC4444��6'   $	 0.4Q�69   � 5      $ 	#FFBC4444��6'   $	 0.6Q�69   � 5      $ 1Q�6'   5      Restauran Manager5      �� 5      $$��/Resource/Image/app-logo-2.png��6   $
��Left=�6<   $��Center=�6W   $	��100��6r   $��33,0,0,8q�6~   �� 5      $��1��6   .+�5      �� 	 �� Orientation$	 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��0,20,0,0q�6n   .+�5      �� 
�������$
��Bold"�63   $	��120��6E   Tên đăng nhập :6b   �� 5       -   5        
 ��Name$
 TextBoxUserName��5         TextBoxRoundedTemplate#��[ 67   $	��250��6j    �� 	MaxLength$ 64��6v   $��Center=�6�   $��Center=�6�   .��6�   admin6�   5!      �� 5"      $	 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��0,10,0,0q�6n   .+�5#      �� 
�������$
��Bold"�63   $	��120��6E   Mật khẩu :6]   G� 5$      -   5         G�Name$ PasswordBoxPassword��5$   "     PasswordBoxRoundedTemplate#��[ 6?   $	��250��6v    G� 	MaxLength$ 32��6�   $��Center=�6�   $��Center=�6�    G� Password$ admin��6�   6�   5%      �� 5&      -   5         ��Name$ CheckBoxRememberPass��5&      $��Center=�69   $��Center=�6V   $��Center=�6q   $��Center=�6�   $��0,15,0,0q�6�   
�������
�������.��6A  Nhớ mật khẩu6O  �� 5'      -   5        $ CheckBoxKeepLogin��5'      $��Center=�66   $��Center=�6S   $��Center=�6n   $��Center=�6�   $��28,8,0,0q�6�   
�������
�������.��68  Tự động đăng nhập6K  �� 5(      -   5         ��Name$ TextBlockWarining��5(      $��Center=�67   $��Center=�6T   $��0,5,0,0q�6o   ���	#FFF4FF00$y�Hidden=�6�   42Mật khẩu không khớp, vui lòng nhập lại6�   5)      �� 5*      -   5         ��Name$ BtnLogin��5*        BtnTemplate#��[ 6'   $��2��6O   $	��120��6\   $��40��6h   .��6�   Đăng nhập6�   5+      � 5,   
   -   5         �Name$ loadingAnim��5,      ���	#5FA5A5A5$y�Hidden=�6;   .+�5-      5   /LoadingPanelSample.Controls.CircularProgressBar  -   5          Name$ progressBar��5-   +   $��Center=�5.      $��45��69   $��45��6D   $��Center=�6P   $��Center=�6t   $��Center=�6�   
 �� Top$ 0��6�   5/      50      51      