   M S B A M L   `   `   `  ���� 
  ETSevk    ETSevk.PageBarkodKontrol   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � 
ResizeMode$  NoResize=� � WindowStyle$ 
ToolWindow=� � Topmost . 
 � Top$ 0�� � Left$ 0�� � Title$ Barkod Kontrol��$	��314��$	��234��.���  � RowDefinitions �� �� $��25 �.+�&   System.Windows.Controls.DataGrid  -   $��DGridBarkodlar��   AutoGenerateColumns .     
IsReadOnly . $��0��	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 Barkod��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Barkod+  $
 Tip No�� �� *TipNo+  $
 Brüt Metre�� �� *Metre+  $
 Renk No�� �� *RenkNo+  $

 Tür�� �� *Tur+  $
 	Net Metre�� �� *
NetMetre+�� -   $��TxtCount��$
��Left=�$��50��$��11(�$��1���� -   $��TxtSum��$��Right=�$	��100��$��11(�$��1��$��0,0,10,0q�