   M S B A M L   `   `   `  ����N  JQuanLyBanHangClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:QuanLyBanHangClient.WindowControl!QuanLyBanHangClient.WindowControl  5   	   I)clr-namespace:LoadingPanelSample.ControlsLoadingPanelSample.Controls  5   	   8    2QuanLyBanHangClient.WindowControl.IngredientDetail   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:QuanLyBanHangClient.WindowControl     5   	   >controls)clr-namespace:LoadingPanelSample.Controls     5   	     � Title$  IngredientDetail��5	   	   $
��220 ��6"   $	��300��5
       � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterOwner=�6%   .��5      � .+�5   
   �� $��0,30,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��15,0,0,0q�62   .+�5      �� $��83��6   Id6+   �� 5      -   5         ��Name$ 	TextBoxId��5       �� 
IsReadOnly . $	��160��6?   $��20,0,0,0q�6K   6_   5      �� 5      $ 
Horizontal=�6   $��	15,15,0,0q�62   .+�5      �� $��83��6   Tên nguyên liệu68   �� 5      -   5        $ TextBoxName��5      $	��160��6/   $��20,0,0,0q�6;   6O   5      �� 5      $ 
Horizontal=�6   $��	15,15,0,0q�62   .+�5      �� $��83��6   
Đơn vị6/   �� 5      -   5         ��Name$ ComboBoxUnit��5       �� 
IsReadOnly . $	��160��6C   $��20,0,0,0q�6O   6c   5      �� 5      -   5         ��Name$ 
BtnConfirm��5      $��30��6)   $��0,15,0,0q�65   $��80��6G   	 J� IsManipulationEnabled	 . .��6�   Tạo mới5      5      � 5   
   -   5        
 �Name$
 loadingAnim��5      ���	#5FA5A5A5$y�Hidden=�6;   .+�5      5   /LoadingPanelSample.Controls.CircularProgressBar  -   5          Name$ progressBar��5   +   $��Center=�5       $��45��69   $��45��6D   $��Center=�6P   $��Center=�6t   $��Center=�6�   
 �� Top$ 0��6�   5!      5"      5#      