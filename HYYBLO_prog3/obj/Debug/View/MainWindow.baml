   M S B A M L   `   `   `  ����  Hyyblo_Prog3    Hyyblo_View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  WpfTTD��5   	   $	��300��6   $	��200��6%    � Icon$ ../icon.ico��5   	    � 
ResizeMode$ NoResize=�6   ��5	      �� 5
   
    �� ImageSource$ ../Images/background.png��6    j� Stretch$
 None=�6<   .��5      �  � ColumnDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��60 �6   �� 5      $��30 �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��50 �6   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��10 �6   �� 5      $��30 �6   �� 5      $��10 �6   �� 5      $��30 �6   �� 5      $��60 �6   .+�5   
   �� -   5         ��Name$ btnStart��5      $��1��6#   $��2��63   $��3��6G   $��
Start Game��6T   6i   �� 5   
   -   5        $ btnExit��5      $��1��6"   $��2��62   $��7��6F   $
��Exit��6S   6b   �� 5    
   -   5         ��Name$ lblTitle��5       $��WpfTTD��6"   $��2��63   $��1��6G   $��Center=�6W   $��1��6t   $��Center=�6�   $��36(�6�   $��	MS Gothic)�6�   $
��Bold"�6�   �� 5!   
   $��	Map size:��6   $��1��6$   $��5��64   6C   �� 5"   
   -   5        	 ��Name$	 txtSize��5"      $��40��6#   $��2��6-   $��5��6=   
 �� TextAlignment$
 Center=�6J   $��Medium"�6a   &   System.Windows.Media.TextOptions   TextHintingMode$ Fixed=�6u   $��Center=�6�   5#      5$      