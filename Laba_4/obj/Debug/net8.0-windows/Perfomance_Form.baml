   M S B A M L   `   `   `  ����A  =Laba_4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Laba_4Laba_4  5   	       Laba_4.Perfomance_Form   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   &localclr-namespace:Laba_4     5   	     � Title$  
Perfomance��5   	   $	��450��6   $	��400��6)    � 	Resources 5	   
    �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� RelativeSource	 �� Path��%�   '��       �� 5
      " ��6    5      P� 5   "    �@=�HasError$
 True��6I   . �5   *   ��  �@�ToolTip 5   1   ��  �� *Self+$)	 #(Validation.Errors)[0].ErrorContent�5   #   5      5      .��5   
   � $��10q�6   .+�5      �� 
 �� Orientation$
 Vertical=�6   $	��Top=�64   .+�5      �� $��Type of the work:��6$   $
��Bold"�6=   $��0,0,0,5q�6O   �� 5      $��0,0,0,10q�5   #   ��5   #   �� *	WorkTypes+ �� SelectedItem 5   #   �� *
The_Work+5   #   �� 5      $��Performer full name:��6$   $
��Bold"�6@   �� 5      $��0,0,0,10q�6�   ��6"   �� *PerformerFullName+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��6�   �� 5"      $��Duration of performance��6$   $
��Bold"�6C   6W   �� 5#      $��5q�6"   ��6-   �� *
Duration+$ PropertyChanged=�$
 True��6�   �� 5%      $��Title��6$   $
��Bold"�61   6E   �� 5&      $��5q�6"   ��6-   �� *Title+$ PropertyChanged=�$
 True��6�   �� 5(      $��OK��6!   $	��100��6.   $
��Left=�6:   ��6U   �� *	OkCommand+5*      5+      5,      