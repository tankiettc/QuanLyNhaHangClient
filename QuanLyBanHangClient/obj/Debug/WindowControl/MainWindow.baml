   M S B A M L   `   `   `  ����N  JQuanLyBanHangClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBanHangClientQuanLyBanHangClient  5   	   g8clr-namespace:QuanLyBanHangClient.AppUserControl.FoodTab*QuanLyBanHangClient.AppUserControl.FoodTab  5   	   i9clr-namespace:QuanLyBanHangClient.AppUserControl.OrderTab+QuanLyBanHangClient.AppUserControl.OrderTab  5   	   s>clr-namespace:QuanLyBanHangClient.AppUserControl.IngredientTab0QuanLyBanHangClient.AppUserControl.IngredientTab  5	   	   I)clr-namespace:LoadingPanelSample.ControlsLoadingPanelSample.Controls  5
   	   Dclr-namespace:QuanLyBanHangClient.AppUserControl.ImportIngredientTab6QuanLyBanHangClient.AppUserControl.ImportIngredientTab  5   	   $    QuanLyBanHangClient.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:QuanLyBanHangClient     5   	   Lfoodtab8clr-namespace:QuanLyBanHangClient.AppUserControl.FoodTab     5   	   Nordertab9clr-namespace:QuanLyBanHangClient.AppUserControl.OrderTab     5   	   Xingredienttab>clr-namespace:QuanLyBanHangClient.AppUserControl.IngredientTab     5	   	   >controls)clr-namespace:LoadingPanelSample.Controls     5
   	   dimportingredienttabDclr-namespace:QuanLyBanHangClient.AppUserControl.ImportIngredientTab     5   	     � Title$  
MainWindow��5   	    � WindowState$ 	Maximized=�6    � WindowStartupLocation$ CenterScreen=�64   $
��1100��6Y   $	��770��6i   $
��1100��6y   $	��770��6�   .��5      �� $��1q�6   
���   �._�5   
   � .+�5      �� $��/Resource/Image/smoke.png��6   $	��Top=�67   $��Right=�6O   $��	0,0,100,0q�6k   $	��300��6~   ]� 5      .+�5      �� $��Bottom=�6    �� CornerRadius$ 8,0,0,8��61   $��2,2,0,2q�6H      MainSolidColor#��[  6b   $��2,2,0,2q�6�   ._�5      �� $
��Bold"�6!   $3��-  Developed by DawningTeam - ©CopyRight 2017��63    � 	Resources 5       �� 
TargetType �� Property �� Value��%d   '��       �� 5      " ��6$   .�5   "   ��  �@��CornerRadius$ 8,8,8,8��6A   5      5      5      5      �� 5      -   5         ��Name$ TabControlMain��5      	 �� TabStripPlacement$		 Top=�65   $��0q�6M   $��Center=�6a   $	��Top=�6�    5      
 �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName �� EndPoint �� 
StartPoint  	MainColor � Color � Offset  HeaderTextBlockStyle �� Triggers �� Binding �� RelativeSource �� AncestorType �� Value��%m  '��       7  &	 (    �� 5      " ��6    .�5      ��  � ��$ 18(�69   �� 5       � ��$	 150��66   �� 5        � �� 5!   "   �� 5"   &   "
 ��66   .R�5#   *   �� $��Border��61   $��1,1,1,0q�6?   ���	#FF7A3030$ 4,4,0,0��6q   $��2,0,0,-4q�6�   ._�5$   .   �� $ ContentSite��6?   $��Center=�5%   )   $��Center=�5&   )   $��Header��5'   )   $
��10,2q�5(   )   5)   +    5*   *   P� 5+   .    �@��
IsSelected . . �5,   2   �� $ Border��69    � �� 5-   6   �� 5.   :   $ 0.5,1&�6N   $ 0.5,0&�6_   .��5/   >   � 8   $	 0.7Q�6n   � 50   >   $ 	#FFE2E2E2��6K   $ 0Q�6]   51   ;   53   3   54   /   P� 55   .    �@��
IsSelected .  . �56   2   �� $ Border��69    � ��
 �����6c   57   /   59   '   5;      5<      �� 5=      " ��6    .�5>      ��  � ��$ Center=�6B   �� 5?       � ��$ Center=�6D    5@      �� 5A   "    6.   �� *
IsSelected+ �� $ TabItemL� True5C      .a�5D   &   ��  � ��$
 Bold"�6C   �� 5E   &    � ��
 �����6C   5F   #   5H      5I      .@�5K      �� -   5         ��Name$ TabItemOrder��5K      ��5L      �� 5M      .+�5N   "   �� $��50��6(   $$��/Resource/Image/order-icon.png��63   �� 5O   "   #��[ 6,   Đặt món6c   5P      .��5R      B   <QuanLyBanHangClient.AppUserControl.OrderTab.OrderAndTableTab  5S      �� 5T      -   5        $ TabItemFood��5T      ��5U      �� 5V      .+�5W   "   �� $��50��6(   $#��/Resource/Image/food-icon.png��63   �� 5X   "   #��[ 6,   
Món ăn6b   5Y      .��5[      8   2QuanLyBanHangClient.AppUserControl.FoodTab.FoodTab  $
��Auto��6*   $
��Auto��68   5\      �� 5]      -   5        $ TabItemIngredient��5]      ��5^      �� 5_      .+�5`   "   �� $��50��6(   $)��#/Resource/Image/ingredient-icon.png��63   �� 5a   "   #��[ 6,   $��18(�6Z   Nguyên liệu6u   5b      .��5d      D   >QuanLyBanHangClient.AppUserControl.IngredientTab.IngredientTab  $
��Auto��66   $
��Auto��6D   5e      �� 5f      -   5        $ TabItemRespository��5f      ��5g      �� 5h      .+�5i   "   �� $��50��6(   $*��$/Resource/Image/respository-icon.png��63   �� 5j   "   #��[ 6,   Kho6_   5k      .��5m      P   JQuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportIngredientTab  $
��Auto��6B   $
��Auto��6P   5n      5o      5p      �� 5r       �� Orientation$ 
Horizontal=�6   $��RightToLeft=�62   $��0,0,30,0q�6N   $	��864��6`   $��Right=�6l   .+�5s      �� $$��/Resource/Image/app-logo-1.png��6   $
��0,10q�6@   $��Right=�6N   $��60��6j   $	��Top=�6v   �� 5t      $��	0,20,17,0q�6   $��UVN Ke Chuyen1)�6/   $
��Bold"�6K   $��22(�6]   $	��180��6k   $��LeftToRight=�6w   #��[  6�   $	��Top=�6�   Restauran Manager5v      5w      �� 5x      $��	20,88,0,0q�6   $ 
Horizontal=�6,   $
��Left=�6E   $	��Top=�6`   .+�5y      �� 
�������$��2q�6-     CornerRadiusValue# [ 6A   $	��150��6s   ._�5z      � -   5         �Name$ BtnShowBtnsAccount��5z      .+�5{      �� $ 
Horizontal=�6%   .+�5|      �� 
�������$��Bottom=�6;   $
��Left=�6V   $
��Bold"�6q   $��10,0,0,0q�6�   Tài khoản:6�   �� 5}      $��14(�6(   $
��Bold"�66   $��Italic$�6H   ���	#FFECFF0B$��10,0,0,0q�6r   $��	Underline{�6�   $��Center=�6�   $��Center=�6�   admin6�   �� 5~      -   5         ��Name$ ImageArrowAccount��5~   $   $$��/Resource/Image/arrow-down.png��6?   $��10,2,0,0q�6g   $��15��6y   $��Center=�6�   $��Center=�6�   5      �� 5�      -   5         ��Name$ BtnShowBtnsAccount1��5�   %    � Cursor$
 Hand��6h   ���	#020000006�   5�      5�      �� 5�      -	   5         ��Name$ BtnChangePass��5�        BtnTemplate#��[ 60   $	��100��6X   $��20,0,0,0q�6d   $y�Hidden=�6v   .��6�   Đổi mật khẩu6�   �� 5�      -
   5        $ 	BtnLogout��5�      #��[ 6,   $	��100��6T   $��20,0,0,0q�6`   $y�Hidden=�6r   .��6�   Đăng xuất6�   5�      � 5�      -   5        $ loadingAnim��5�      ���	#5FA5A5A5$y�Hidden=�6?   .+�5�      5   /LoadingPanelSample.Controls.CircularProgressBar  -   5           Name$  progressBar��5�   /   $��Center=�5�      $��45��69   $��45��6D   $��Center=�6P   $��Center=�6t   $��Center=�6�   
! �� Top$! 0��6�   5�      5�      5�      5�      