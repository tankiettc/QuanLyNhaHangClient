   M S B A M L   `   `   `  ����N  JQuanLyBanHangClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:QuanLyBanHangClient.WindowControl!QuanLyBanHangClient.WindowControl  5   	   I)clr-namespace:LoadingPanelSample.ControlsLoadingPanelSample.Controls  5   	   2    ,QuanLyBanHangClient.WindowControl.FoodDetail   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:QuanLyBanHangClient.WindowControl     5   	   >controls)clr-namespace:LoadingPanelSample.Controls     5   	     � Title$  Thêm món ăn��5	   	   $	��550��6   $	��800��6*    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ Manual=�6L      MainSolidColor#��[  6k   .��5
      � -   5         �Name$ 
GridParent��5
       � 	Resources 5   
     ImageDefaultFood��%H   &	       ��5      *&/Resource/Image/default-food-image.jpg��6[   5      .+�5   
   � $��0,45,0,0q�6    � ColumnDefinitions 5      �� 5      $��2* �6#   �� 5      $��* �6#   .+�5      �� .+�5      ]� $��	50,30,0,0q�6   .+�5      �� -   5         ��Name$ BtnCopy��5        BtnTemplate#��[ 6.   $��11(�6V   $��80��6d   $��	Sao chép��6o   6�   �� 5      -   5         ��Name$ ComboBoxFoodCopy��5        ComboBoxFlatStyle#��[ 69   $��	40,0,62,0q�6d   $��Right=�6w   $
��Left=�6�   $	��295��6�   $��Center=�6�   �� 5      -   5         ��Name$ TextBlockId��5       $
��Bold"�65   $��80��6G   $y�	Collapsed=�6R   ID6m   �� 5      -   5        	 ��Name$	 	TextBoxId��5        TextBoxRoundedTemplate#��[ 61   $��	40,0,60,0q�6d   $��Right=�6w   
 �� 
IsReadOnly
 . $y�	Collapsed=�6�   $��Center=�6�   $��Center=�6�   6�   5      ]� 5      $��	50,15,0,0q�6   .+�5      �� $
��Bold"�6    $��80��62   
�������Tên món ăn6\   �� 5      -   5        $	 TextBoxName��5      #��[ 63   $��	40,0,60,0q�6f   $��Right=�6y   $��Center=�6�   5      ]� 5      $��	50,15,0,0q�6   .+�5      �� $
��Bold"�6    $��80��62   
�������Giá6U   �� 5       -   5        $	 TextBoxPrice��5       #��[ 64   $��	40,0,60,0q�6g   $��Right=�6z   $��Center=�6�   5!      ]� 5"      $��	50,15,0,0q�6   .+�5#      �� $
��Bold"�6    $��80��62   
�������Loại món ăn6]   �� 5$      -   5        $ ComboBoxCategory��5$      #��[ 69   $��	40,0,60,0q�6d   $��Right=�6w   $��Center=�6�   5%      �� 5'      $��	50,15,0,0q�6   $
��Bold"�6/   $	��180��6A   $
��Left=�6M   
�������Danh sách nguyên liệu6�   �� 5(      $��	30,15,0,0q�6   $	��180��6,    �� Padding$ 1q�69   
�������$��2q�6Y     CornerRadiusValue �� CornerRadius# [ 6m   ._�5)      ]� .+�5*      &   System.Windows.Controls.DataGrid  -	   5        $��DataGridIngredient��5*   9    w� Text$
 Cơm��6#      AutoGenerateColumns .     
IsReadOnly .  �� IsTextSearchEnabled . $��0q�6�   $	��180��6�   $	��320��6�      Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,   "   ,  &System.Windows.Controls.DataGridColumn   Header$ Id��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *Id+  5-   "   $ Nguyên liệu��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6c    6J   �� *Name+6c     5.   "   $ Số lượng��65    6G   �� *
Quantity+  5/   "   $ 
Đơn vị��65   $ * 6b    6E   �� *
UnitName+6b   51      �� 52      $��5q�6%   .+�53      �� -
   5        $ ComboBoxIngredient��53   '   #��[ 6C   $��0,20,0,0q�6n   6�   �� 54      $��0,10,0,0q�6)    �� Orientation$ 
Horizontal=�6;   .+�55   "   �� -   5        $	 TextBoxQuantity��55   *   #��[ 6C   $��60��6�   $
��Left=�6�   6�   �� 56   "   -   5        $ TextBlockUnit��56   ,   $��Italic$�6C   $��6,2,0,0q�6V   $��Kilogram��6g   $��WrapWithOverflow=�6w   $��10(�6�   $��Center=�6�   $
��Left=�6�   
�������6�   57      �� 58      -   5        $ BtnAdd��58   M   #��[ 6%   $��0,10,0,0q�6]   .��6�   Thêm6�   �� 59      -   5        $ 	BtnRemove��59   M   #��[ 6%   $��0,30,0,0q�6`   {�.  .��6�   Xóa6�   5:      5;      5<      �� 5=      -   5        $ 
BtnConfirm��5=      #��[ 6-   $	��220��6U   $��50��6a   $��16(�6m   $��0,20,0,0q�6{   $��Tạo mới��6�   $��Center=�6�   $��Right=�6�   5>      �� 5?      $��1��6   .+�5@      �� $��Center=�6   $��Center=�69   $��0,20,0,0q�6T   $
��Bold"�6f   $��14(�6x   ���	#FFFDFF00Hình ảnh món ăn6�   � 5A      .+�5B      �� 
�������$��2q�61   # [ 6E   $	��200��6w   $	��200��6�   $��0,10,0,0q�6�   ._�5C      �� -   5         ��Name$ 	ImageFood��5C       $	��176��63   $	��176��6?   #��[ 6L   5D      5E      �� 5F      -   5        $ BtnPickImage��5F   A   #��[ 6   $��
20,20,20,0q�6W   $��40��6�   .��6�   Chọn ảnh từ máy6�   5G      5H      �� 5I   
   -   5        $ TextBlockTile��5I      $��0,25,0,0q�6+   $
��Bold"�6=   $��15(�6O   $��LeftToRight=�6^   
������� �� TextAlignment$ Center=�6�   $	��Top=�6�   ��5J      �� 5K       �� EndPoint$ 0,0.5&�6&    �� 
StartPoint$ 1,0.5&�67   .��5L      �  � Offset$ 0Q�6#   � 5M       � Color$ 	#FFBC4444��6#   $	 0.4Q�65   � 5N      $ 	#FFBC4444��6#   $	 0.6Q�65   � 5O      $ 1Q�6#   5P      Thêm món ăn5S      � 5T   
   -   5        $ loadingAnim��5T      ���	#5FA5A5A5$y�Hidden=�6;   .+�5U      5   /LoadingPanelSample.Controls.CircularProgressBar  -   5          Name$ progressBar��5U   +   $��Center=�5V      $��45��69   $��45��6D   $��Center=�6P   $��Center=�6t   $��Center=�6�   
 �� Top$ 0��6�   5W      5X      5Y      