   M S B A M L   `   `   `  ����N  JQuanLyBanHangClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullw@clr-namespace:QuanLyBanHangClient.AppUserControl.OrderTab.Models2QuanLyBanHangClient.AppUserControl.OrderTab.Models  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      B    <QuanLyBanHangClient.AppUserControl.OrderTab.Models.OrderInfo   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Rlocal@clr-namespace:QuanLyBanHangClient.AppUserControl.OrderTab.Models     5      9system&clr-namespace:System;assembly=mscorlib     5      $
��Auto��5	      $
��Auto��6   .��5
      9� -   5          9�Name$  ExpandOrderInfo��5
       9� 
IsExpanded . ��5   
   �� 5      $	��550��6   ��5      �� 5       �� EndPoint$ 1,0.5&�6*    �� 
StartPoint$ 0,0.5&�6;   .��5      �  � Color$ 	#FF7F9C8F��6'   � 5       � Offset$ 1Q�6'   5      ._�5      �� -   5        $��TextBlockHeader��5      
�������$��Order 1��6F   $
��Bold"�6U   $��	Underline{�6g   $��10,0,0,0q�6�   5      .��5   
   ]� .+�5      � $��Bottom=�6   $
��0,10q�6+   .+�5      �� -   5         ��Name$ 
BtnAddFood��5      $	��140��6-   $��30��69   $
��Left=�6E   $��70,0,0,0q�6`   ���	#00DDDDDD � Cursor$
 Hand��6�   
�������.��5      ��  �� Orientation$ 
Horizontal=�6!   $	��140��6:   .+�5      �� $$��/Resource/Image/add-icon-4.png��6    $��30��6H   $��5,0,0,0q�6S   �� 5      $��Center=�6$   $��8,0,0,0q�6?   ���	#FFF18527
Thêm món6q   5      5      �� 5      -   5        $ 	BtnAccept��5         BtnTemplate#��[  6,   $	��100��6T   $��
0,0,0,-0.1q�6x   .��6�   Xác nhận6�   �� 5      $��26��60   ��6   Z� ._�5       �� -   5        	 ��Name$	 TextBlockTotalOrder��5        $
��Wrap=�6=   $��Tổng cộng: 90000��6Q   $��Right=�6i   $��0,0,55,0q�6�   ���	#FFEAD732$
��Bold"�6�   $��Center=�6�   
 �� TextAlignment$
 Right=�6�   $	��200��6  ��6�   Z� 6  5!      �� 5"      -   5         ��Name$ AddFoodGroup��5"      ���	#FF7D4D4D$��	0,0,180,0q�6F   $��30��6Y   $	��395��6e   $y�Hidden=�6q   ._�5#      �� $ 
Horizontal=�6!   .+�5$      �� -   5         ��Name$ ComboBoxSelectFood��5$   #     ComboBoxFlatStyle#��[ 6?   $��20��6j   $	��180��6v   $��10(�6�   �� 5%      $��Center=�6$    �� Padding$ 0q�6?   $��5,0,0,0q�6K   x6_   �� 5&      -   5         ��Name$ TextBoxQuantity��5&   "     TextBoxRoundedTemplate#��[ 6g   $��20��6�   $��50��6�   $��2,0,0,0q�6�   $��10(�6�   $��Center=�6�   .��6�   16�   �� 5'      -	   5        $ BtnConfirmAdd��5'   !   #��[  68   $��50��6`   $��30��6k   $��30,0,0,0q�6w   $��10(�6�   .��6�   Thêm6�   �� 5(      -
   5        $ BtnConfirmExit��5(   !   #��[  69   $��50��6a   $��30��6l   $��20,0,0,0q�6x   $��10(�6�   .��6�   Thoát6�   5)      5*      5+      �� 5,      -   5        $��listViewOrderWithFood��5,      $	��Top=�64   $
��50,0q�6I   
������ ���	#00ABADB3��5-      �� 5.       �� 
TargetType" ��6   .�5/      ��  �� Property � �� �� Value$ Stretch=�6G   50      52      53      54      55      