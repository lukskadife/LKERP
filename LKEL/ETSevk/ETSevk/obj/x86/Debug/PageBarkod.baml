   M S B A M L   `   `   `  ����
  ETSevk    ETSevk.PageBarkod   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $	��314��5   &   $	��234��63   $	��314��6?   $	��234��6O     � 
ResizeMode$  NoResize=�6^    � WindowStyle$ 
ToolWindow=�6t    � Topmost . 
 � Top$ 0��6�    � Left$ 0��6�    � Title 6	   �� *
MusteriAdi+.��5      �  � ColumnDefinitions 5   
   �� 5      $	��93* �6   �� 5      $
��119* �6    � RowDefinitions 5
   
   �� 5      $	��31* �6   �� 5      $
��196* �6   �� 5      $��25 �6   �� 5      $	��27* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��DGridOkutulan��5   A      AutoGenerateColumns .  	   
IsReadOnly	 . $��1��6V   $��2��6c   
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Barkod��6@   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Barkod+6@     5      $ Tip No��6?    6%   �� *TipNo+6?     5      $ Brüt Metre��6?    6%   �� *Metre+6?     5      $ Renk No��6@    6%   �� *RenkNo+6@     5      $
 Tür��6=    6%   �� *Tur+6=     5      $ Parça Metreler��6%   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Parça Sayısı��6)     5      $ 
Tam Parça��6%     5      $ 	Net Metre��6B    6%   �� *
NetMetre+6B     5      $ Dye Batch No��6%   5      �� 5   
   -   5        $��	TxtBarkod��5      $��28��6   $	��0,1q�6/   $��2��6X   �� 5   
   -   5        $��BtnBarkodSil��5   1   $	��Sil��6   $��59,0,0,0q�6   $��3��6E   $��1��6m   �� 5    
   -   5        $��
BtnKontrol��5    [   $��Kontrol��6   $��3��6#   $��0��60   $
��Left=�6@   $��50��6�   �� 5!   
   -   5        $��TxtCount��5!      $��2��6    $
��Left=�6-   $��50��6H   $��11(�6S   �� 5"   
   -   5        $��TxtSum��5"      $��2��6   $��1��6+   $
��Left=�6;   $	��100��6V   $��11(�6b   5#      5$      