   M S B A M L   `   `   `  ����A  =Laba_3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Laba_3Laba_3  5   	       Laba_3.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Laba_3     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
      �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   �� 5      $��* �6   6+    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   .+�5   
   �� -   5         ��Name$ number_from��5   /   $��0��6   $��0��6    �� TextWrapping$
 Wrap=�5       �� AcceptsReturn$
 True��5      $��Stretch=�5      $��Stretch=�5      $��10q�5      $	��100��5      $��20��5      �� 5   
   -   5        $ 	number_to��5   /   $��0��6   $��1��6   $
 Wrap=�5      $
 True��5      $��Stretch=�5       $��Stretch=�5!      $��10q�5"      $	��100��5#      $��20��5$      �� 5%   
   -   5        $ number_step��5%   /   $��0��6   $��2��6   $
 Wrap=�5&      $
 True��5'      $��Stretch=�5(      $��Stretch=�5)      $��10q�5*      $	��100��5+      $��20��5,      �� 5-   
   -   5        $��0��5-      $��3��6   $��Stretch=�6.   $��Stretch=�5.      $��10q�5/      $	��150��50      $��50��51      $��I need more buttons!��52      53      �� 54   
   $��1��6   $��0��6   $��2��6/   $��50��55      $��Remove all multiples of��56      $��Center=�57      $��Center=�58      $��0q�59      6(   �� 5:   
   -   5        $ number_divide��5:   /   $��1��6   $��2��6   $
 Wrap=�5;      $
 True��5<      $��Stretch=�5=      $��Stretch=�5>      $��10q�5?      $	��100��5@      $��20��5A      �� 5B   
   -   5        $��1��5B      $��3��6   $��Stretch=�6.   $��Stretch=�5C      $��10q�5D      $	��150��5E      $��50��5F      $��Remove��5G      5H      �� 5I   
   $��2��6   $��4��6   
����� �._�5J      �� $
��Auto=�6   .��5K      �� -   5         ��Name$ buttonContainer��5K      ��5L      �� 5M      .R�5N      A�  A�Name$ 
buttonGrid��6*    A� Columns$ 10��6>   5O      5Q      5R      5S      5V      5W      