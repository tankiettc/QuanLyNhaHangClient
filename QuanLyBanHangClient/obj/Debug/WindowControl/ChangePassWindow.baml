   M S B A M L   `   `   `  ����N  JQuanLyBanHangClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:QuanLyBanHangClient.WindowControl!QuanLyBanHangClient.WindowControl  5   	   I)clr-namespace:LoadingPanelSample.ControlsLoadingPanelSample.Controls  5   	   8    2QuanLyBanHangClient.WindowControl.ChangePassWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:QuanLyBanHangClient.WindowControl     5   	   >controls)clr-namespace:LoadingPanelSample.Controls     5   	     � Title$  Đổi mật khẩu��5	   	   $	��250��6   $	��400��6+    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ NoResize=�6\      MainSolidColor#��[  6r   .��5
      � .+�5   
   �  � RowDefinitions 5      �� 5      $
��1.3* �6    �� 5      $��2* �6    �� 5      $
��1.3* �6    .+�5      �� $��0,5,0,0q�6   $
��Bold"�6)   $��15(�6;   $��LeftToRight=�6J   
�������$��Center=�6y    �� TextAlignment$ Center=�6�   ��5      �� 5       �� EndPoint$ 0,0.5&�6*    �� 
StartPoint$ 1,0.5&�6;   .��5      �  � Offset$ 0Q�6'   � 5       � Color$ 	#FFBC4444��6'   $	 0.4Q�69   � 5      $ 	#FFBC4444��6'   $	 0.6Q�69   � 5      $ 1Q�6'   5      Đổi mật khẩu5      �� 5      $��1��6   .+�5      �� 	 �� Orientation$	 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��0,20,0,0q�6n   .+�5      �� 
�������$
��Bold"�63   $	��120��6E   Mật khẩu mới :6a   G� 5      -   5        
 G�Name$ 
 PasswordBoxConfirmPassword��5   "     PasswordBoxRoundedTemplate#��[ 6F   $	��250��6}    G� 	MaxLength$ 32��6�   $��Center=�6�   $��Center=�6�   6�   5       �� 5!      $	 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��0,10,0,0q�6n   .+�5"      �� 
�������$
��Bold"�63   $	��120��6E   Nhập lại :6]   G� 5#      -   5        $
 PasswordBoxNewPassword��5#   "   #��[ 6B   $	��250��6y   $ 32��6�   $��Center=�6�   $��Center=�6�   6�   5$      �� 5%      -   5         ��Name$ TextBlockWarining��5%      $��Center=�67   $��Center=�6T   $��0,5,0,0q�6o   ���	#FFF4FF00$y�Hidden=�6�   42Mật khẩu không khớp, vui lòng nhập lại6�   5&      �� 5'      -   5         ��Name$ 
BtnConfirm��5'        BtnTemplate#��[ 6)   $��2��6Q   $	��120��6^   $��40��6j   $��0,0,0,10q�6�   .��6�   Xác nhận6�   5(      � 5)   
   -   5         �Name$ loadingAnim��5)      ���	#5FA5A5A5$y�Hidden=�6;   .+�5*      5   /LoadingPanelSample.Controls.CircularProgressBar  -   5          Name$ progressBar��5*   +   $��Center=�5+      $��45��69   $��45��6D   $��Center=�6P   $��Center=�6t   $��Center=�6�   
 �� Top$ 0��6�   5,      5-      5.      