   M S B A M L   `   `   `  ����
  ETSevk    ETSevk.PageMain   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        $	��314��5      $	��234��6(   $	��314��5      $	��234��6   $	��314��6    $	��234��60     � 
ResizeMode$  NoResize=�6?    � WindowStyle$ 
ToolWindow=�6U    � Topmost . 
 � Top$ 0��6}    � Left$ 0��6�    � Title$ Sevkiyat��6�   .��5      �  � RowDefinitions 5	   
   �� 5
      $��26 �6   �� 5      $��0 �6   �� 5      $��0 �6   �� 5      �� 5      $��40 �6   .+�5   
   (  "System.Windows.Controls.DatePicker  -   5        $��DPBaslangic��5   0   $
��Left=�6   $��90��6C   $��0,0,0,0q�6N   6_     5   
   -   5        $��DPBitis��5   0   $
��Left=�6   $��90��6?   $��92,0,1,0q�6J   6\   �� 5   
   -   5        $��BtnEkle��5   e   $
��Ekle��6   $��1��6    $��24��6-   $
��Left=�69   $��1,3,0,0q�6T   $	��Top=�6t   $��50��6�   6�   �� 5   
   -   5        $��	BtnDuzelt��5   [   $��Düzelt��6   $��24��6"   $
��Left=�6.   $��54,3,0,0q�6I   $	��Top=�6l   $��50��6�   $��1��6�   6�   �� 5   
   -   5        $��BtnSil��5   Y   $	��Sil��6   $��24��6   $
��Left=�6+   $��	107,3,0,0q�6F   $	��Top=�6g   $��50��6   $��1��6�   6�   �� 5   
   -   5        $��	BtnTazele��5   \   $��Tazele��6   $��24��6"   $
��Left=�6.   $��	186,1,0,0q�6I   $	��Top=�6m   $��0��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DGridSevkBelge��5   G      AutoGenerateColumns .  $��3��6/   $��2q�6<      
IsReadOnly . 	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Tarih��6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Tarih+ �� StringFormat$ 
dd.MM.yyyy��6Z     5      $
 Sipariş No��6D    6%   �� *
SozlesmeNo+6D     5      $
 	Müşteri��6D    6%   �� *
MusteriAdi+6D     5      $
 
Açıklama��6B    6%   �� *
Aciklama+6B     5      $
 Personel��6E    6%   �� *SevkEdenAdi+6E     5      $
 Belge No��6A    6%   �� *	BelgeNo+6A   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $
 Farklı Sipariş Kumaşı��6O    6)   �� *FarkliSiparisOkut+6O     5      $
 Logoda��6I    6%   �� *LogoAktarildiMi+6I     5       $
 Müşteri Kodu��6E    6%   �� *MusteriKodu+6E   5"      �� 5#   
   -	   5        $��BtnSevkiyat��5#   ]   $��Sevkiyat��6   $��4��6$   $
��Left=�61   $��0,1,0,0q�6L   $��80��6p   6{   �� 5$   
   -
   5        $��BtnHamSayim��5$   M   $��4��6   $��Right=�6   $��	10,1,61,0q�6:   $��60��6`   .��5%      �� $
��Wrap=�6   $��
Ham Sayım��6,    �� TextAlignment$ Center=�6=   5&      �� 5'   
   -   5        $��BtnMamulSayim��5'   L   $��4��6   $��Right=�6   $��10,1,0,0q�6:   $��60��6a   .��5(      �� $
��Wrap=�6   $��Mamul Sayım��6,   $ Center=�6?   5)      5*      5+      