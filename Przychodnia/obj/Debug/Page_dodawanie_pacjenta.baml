   M S B A M L   `   `   `  ����F  BPrzychodnia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PrzychodniaPrzychodnia  5      )    #Przychodnia.Page_dodawanie_pacjenta   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Przychodnia     5        N� Title$  Page_dodawanie_pacjenta��5	       N� 
Background
 �����$	��635��6>   $
��1067��6K   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      �� 5      $	��200 �6   .+�5   
   �� $��ID��6   $��1��6   $
��Left=�6-   $��221.119,40,0,0q�6H   $	��Top=�6`   $��87��6x   $��18(�6�   �� 5   
   $��Imię��6   $��1��6   $
��Left=�6/   $��221.119,71,0,0q�6J   $	��Top=�6b   $��87��6z   $��18(�6�   �� 5   
   $��Nazwisko��6   $��1��6#   $
��Left=�63   $��221.119,102,0,0q�6N   $	��Top=�6g   $��87��6   $��18(�6�   �� 5   
   $��Dodaj nowego pacjenta��6   $��1��60   $��Center=�6@   $	��Top=�6]   $��20(�6u   $	��222��6�   �� 5   
   -   5         ��Name$ txt_rejestracja_ID��5      $��1��6.   $��0��6>   $
��Left=�6K   $��23��6f   $��313.119,47,0,0q�6r    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ txt_rejestracja_imie��5      $��1��60   $��1��6A   $
��Left=�6N   $��23��6i   $��313.119,78,0,0q�6u   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ txt_rejestracja_nazwisko��5      $��1��64   $��3��6D   $
��Left=�6Q   $��23��6l   $��313.119,110,0,0q�6x   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ btn_rejestracja_zapisz��5      $��1��61   $��14��6A   $��Zapisz��6O   $
��Left=�6`   $��224.119,149,0,0q�6{   $	��Top=�6�   $	��102��6�   $��27��6�   ���	#FF2B7CD4$��16(�6�   
�������6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid_pacjenci��5      $��1��6.   $	��269��6>   $	��209��6L   $!��224.119,195,227.418,170.746q�6X   �� 5   
   -   5        $ btn_rejestracja_anuluj��5      $��1��61   $��18��6A   $��	Wyczyść��6O   $
��Left=�6a   $��331.119,149,0,0q�6|   $	��Top=�6�   $	��102��6�   $��27��6�   ���	#FF2B7CD4$��16(�6�   
�������6�   5      5      