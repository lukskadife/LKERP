   M S B A M L   `   `   `  ����
  ETSevk    ETSevk.PageBarkodKontrol   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � 
ResizeMode$  NoResize=�5   @    � WindowStyle$ 
ToolWindow=�6V    � Topmost . 
 � Top$ 0��6~    � Left$ 0��6�    � Title$ Barkod Kontrol��5   	   $	��314��6    $	��234��6-   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��25 �6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��DGridBarkodlar��5
   A      AutoGenerateColumns .     
IsReadOnly . $��0��6W   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Barkod��6@   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Barkod+6@     5      $
 Tip No��6?    6%   �� *TipNo+6?     5      $
 Brüt Metre��6?    6%   �� *Metre+6?     5      $
 Renk No��6@    6%   �� *RenkNo+6@     5      $

 Tür��6=    6%   �� *Tur+6=     5      $
 	Net Metre��6B    6%   �� *
NetMetre+6B   5      �� 5   
   -   5        $��TxtCount��5      $
��Left=�6    $��50��6;   $��11(�6F   $��1��6T   �� 5   
   -   5        $��TxtSum��5      $��Right=�6   $	��100��6:   $��11(�6F   $��1��6T   $��0,0,10,0q�6a   5      5      