   M S B A M L   `   `   `  ����M  IKalkulator kalorii, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Kalklulator_kaloriiKalklulator_kalorii  5   	   #    Kalkulator_kalorii.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Kalklulator_kalorii     5   	     � Title$   ��5   	   $	��450��6   $	��800��6   .��5	      � 
�������.+�5
   
   �� 
�������$��Center=�6,   $	��414��6I   $��Center=�6V   $	��780��6q   .@�5      �� $��
Obliczanie��6   .��5      � 
�������.+�5      ��  �� Orientation$ Vertical=�6!   $��20q�68   .+�5      �� $��Wybierz produkty:��6    $��20(�6<   �� 5      -   5         ��Name$ ProduktyComboBox��5   #   $	��200��6=   $��	0,10,0,20q�6I   6\   &   System.Windows.Controls.DataGrid  5      -   5          Name$ WybraneProduktyDataGrid��5   #      AutoGenerateColumns$ False��6D      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   "   ,  &System.Windows.Controls.DataGridColumn   Header$ Nazwa��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Name+  5   "   $ Białko��65    6E   �� *	Protein+  5   "   $ 	Tłuszcze��65    6G   �� *Fat+  5   "   $ Węglowodany��65    6J   �� *Carbohydrates+  5   "   $ Kalorie��65    6F   �� *TotalCalories+4  .System.Windows.Controls.DataGridTemplateColumn  5   "   $ Ilość (100g)��69      CellTemplate 5   &   �� 5   *   .R�5   .   �� 	 ��Name$	 IloscTextBox��66   -   5        $��50��5   6   $��Center=�6A   ��5   L   �� *
Quantity+
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5   S   5   +   5    #     5!   "    5"   &   �� 5#   *   .R�5$   .   �� -   5        $��Usuń��5$   5   6D   5%   +   5'   #   5)      5*      �� 5+      -   5        $	 SumaBialkoTextBox��5+      $
��Left=�69   $��
70,370,0,0q�6T   $	��Top=�6h   $��60��6�    �� 
IsReadOnly$
 True��6�   �� 5,      -   5        $	 SumaTluszczeTextBox��5,      $
��Left=�6;   $��140,370,0,0q�6V   $	��Top=�6k   $��60��6�   $
 True��6�   �� 5-      -   5        $	 SumaWeglowodanyTextBox��5-      $
��Left=�6>   $��210,370,0,0q�6Y   $	��Top=�6n   $��60��6�   $
 True��6�   �� 5.      -   5        $	 SumaKalorieTextBox��5.      $
��Left=�6:   $��280,370,0,0q�6U   $	��Top=�6j   $��60��6�   $
 True��6�   �� 5/      $��W��6   $
��Left=�6(   $��232,350,0,0q�6C   $	��Top=�6X   �� 50      $��T��6   $
��Left=�6(   $��162,350,0,0q�6C   $	��Top=�6X    J� RenderTransformOrigin$ 1.903,0.195&�6p   �� 51      $��K��6   $
��Left=�6(   $��299,350,0,0q�6C   $	��Top=�6X   �� 52      $��B��6   $
��Left=�6(   $��
92,350,0,0q�6C   $	��Top=�6W   54      55      �� 56      $��Dodaj produkt��6   .��57      � 
�������.+�58      �� $��Dodaj Produkt��6   $��20(�64   $
��Bold"�6B   $��Center=�6T   $
��auto��6q   $��0,64,0,0q�6   $	��Top=�6�   $
��auto��6�   �� 59      -	   5        $	 NazwaTextBox��59      $
��Left=�64   $��387,150,0,0q�6O    �� TextWrapping$
 Wrap=�6d   $��
Podaj Dane��6x   $	��Top=�6�   $	��120��6�   6�   �� 5:      -
   5        $	 BiałkoTextBox��5:      $
��Left=�65   $��387,200,0,0q�6P   $
 Wrap=�6e   $��
Podaj Dane��6y   $	��Top=�6�   $	��120��6�   6�   �� 5;      -   5        $	 WeglowodanyTextBox��5;      $
��Left=�6:   $��387,250,0,0q�6U   $
 Wrap=�6j   $��
Podaj Dane��6~   $	��Top=�6�   $	��120��6�   6�   �� 5<      -   5        $	 TluszczeTextBox��5<      $
��Left=�67   $��387,300,0,0q�6R   $
 Wrap=�6g   $��
Podaj Dane��6{   $	��Top=�6�   $	��120��6�   6�   �� 5>      $��Nazwa:��6   $��17(�6-   $
��Left=�6;   $��250,140,0,0q�6V   $	��Top=�6k   �� 5?      $��Białko:��6   $��17(�6.   $
��Left=�6<   $��250,190,0,0q�6W   $	��Top=�6l   �� 5@      $��Węglowodany:��6   $��17(�63   $
��Left=�6A   $��250,240,0,0q�6\   $	��Top=�6q   �� 5A      $��
Tłuszcze:��6   $��17(�60   $
��Left=�6>   $��250,290,0,0q�6Y   $	��Top=�6n   �� 5C      -   5        $��Dodaj��5C      $
��Left=�6-   $��550,225,0,0q�6H   $	��Top=�6]   $��60��6�   $��20��6�   5D      5E      �� 5F      $��Baza Produktów��6   .��5G      � 
�������.+�5H        -   5        $ ProduktyDataGrid��5H      $ False��69    5I        5J      $ Nazwa produktu��61    6I   �� *Name+  5K      $ Ilość białka��61    6G   �� *	Protein+  5L      $ Ilość węglowodanów��61    6M   �� *Carbohydrates+  5M      $ Ilość tłuszczów��61    6J   �� *Fat+  5N      $ Ilość kalorii��61    6H   �� *TotalCalories+  5O       5P   "   �� 5Q   &   .R�5R   *   �� -   5        $��Usuń��5R   1   6@   5S   '   5U      5W      5X      5Y      5Z      5[      5\      