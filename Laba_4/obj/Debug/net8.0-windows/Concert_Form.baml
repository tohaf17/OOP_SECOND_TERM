   M S B A M L   `   `   `  ����A  =Laba_4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Laba_4Laba_4  5   	       Laba_4.Concert_Form   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   &localclr-namespace:Laba_4     5   	     � Title$  Concert_Form��5   	   $	��500��6   $	��800��6+   .��5	   
   � $��10q�6   .+�5
      ��  �� Orientation$ Vertical=�6   $	��Top=�64   .+�5      �� $��
Organizer:��6$   $
��Bold"�66   �� 5      $��0,0,0,10q�6b   ��6"   �� *	Organizer+ �� UpdateSourceTrigger$ PropertyChanged=�6b   �� 5      $��Date:��6$   $
��Bold"�61   (  "System.Windows.Controls.DatePicker  5      $��0,0,0,10q�6h      SelectedDate 6%   �� *Date+$ PropertyChanged=�6h   �� 5      $��Performances:��6$   $
��Bold"�69   6M   &   System.Windows.Controls.DataGrid  5      -   5        $��performancesGrid��5   #      AutoGenerateColumns$ False��5   #      
IsReadOnly$
 True��5   #   $
��Auto��5   #   $	��200��5   #   $��0,0,0,10q�5   #   ��5   !   �� *Performances+ �� SelectedItem 5   #   �� *SelectedPerformance+ �� Mode$ TwoWay=�   Columns 5   "   0  *System.Windows.Controls.DataGridTextColumn  5   *   ,  &System.Windows.Controls.DataGridColumn	   Header$	 Title��6=   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6L   �� *Title+  5   *   $	 	Performer��6=   
 6P   �� *PerformerFullName+  5   *   $	 	Work Type��6=   
 6P   �� *
The_Work+  5   *   $	 Duration��6=   
 6O   �� *
Duration+5"      �� 5%      $��Add performance��6!   $	��100��6;   $
��Left=�6G   $��5q�6b   ��6m   �� *AddPerformanceCommand+6�   �� 5&      $��OK��6!   $	��100��6.   $
��Left=�6:   $��5q�6U   ��6`   �� *	OkCommand+�� 5'      $��Edit performance��6!   $	��100��6<   $
��Left=�6H   $��5q�6c   ��6n   �� *EditPerformanceCommand+6�   5)      5*      5+      