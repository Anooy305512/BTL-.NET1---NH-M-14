   M S B A M L   `   `   `  ����  Quản lý tạp hóa$    Quản_lý_tạp_hóa.Doanhthu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	Doanh Thu��5   	   $	��450��6   $	��800��6(   .��5      � .+�5   
   �� .+�5      �� $��Doanh thu theo ngày:��6   $��16(�64   $
��Bold"�6B   $��0,20,0,0q�6T   $��24��6f   $	��793��6r   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ SelectedDatePicker��5      �� 5      -   5         ��Name$ DailyRevenueTextBlock��5      $��14(�67   �� 5      $(��"Chọn tháng để xem doanh thu:��6   $��16(�6<   $
��Bold"�6J   $��10, 20, 0, 0q�6\     5      -   5        $ SelectedMonthPicker��5         DisplayDateStart$ 
01/01/2000X�66      DisplayDateEnd$ 
12/31/2099X�6T   &   System.Windows.Controls.DataGrid  5      -   5          Name$ RevenueDataGrid��5         AutoGenerateColumns .  $
��Auto��6L   $
��Auto��6Y      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Sản phẩm��6)   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6[   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *ProductName+6[     5      $ Tổng số lượng bán��6)   $	 * 6f   
 6D   �� *TotalQuantity+6f     5      $ 	Doanh thu��6)   $	 * 6]   
 6<   �� *TotalRevenue+6]   5      �� 5!      -   5        $ TotalRevenueTextBlock��5!      $��14(�67   5"      �� 5%   
   -   5        $��Xem doanh thu��5%      $	��150��6C   $
��Left=�6O   $	��Top=�6j   $��626,298,0,0q�6�   5)      5*      