   M S B A M L   `   `   `  ����P  LQuản lý tạp hóa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Quản_lý_tạp_hóaQuản_lý_tạp_hóa  5      &     Quản_lý_tạp_hóa.Qlsanpham1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      5local#clr-namespace:Quản_lý_tạp_hóa     5      .��5	      � .+�5   
   (   "Quản_lý_tạp_hóa.MenuControl1  $	��Top=�6   $��0,20,0,0q�65   �� 5   
   -   5        $��SearchTextBox��5      $	��205��6'   $��25��63   $��	30,83,0,0q�6?   $	��Top=�6R   $
��Left=�6j   �� 5   
   -   5        $��SearchButton��5      $��Tìm kiếm��6%   $	��101��68   $��25��6D   $��
240,83,0,0q�6P   $	��Top=�6d   $
��Left=�6|   6�   �� 5   
   $��Tên sản phẩm:��6   $
��Left=�6(   $	��Top=�6C   $��
40,105,0,0q�6[     J� RenderTransformOrigin$  0.485,0.216&�6o   �� 5   
   -   5        $��ProductNameTextBox��5      $
��Left=�6,   $	��Top=�6G   $	��200��6_   $��141,113,0,0q�6k   �� 5   
   $��Giá:��6   $
��Left=�6   $	��Top=�6:   $��
48,131,0,0q�6R   $  0.886,-0.384&�6f   �� 5   
   -   5        $��UnitPriceTextBox��5      $
��Left=�6*   $	��Top=�6E   $	��200��6]   $��140,136,0,0q�6i   �� 5   
   $��Số lượng trong kho:��6   $
��Left=�6.   $	��Top=�6I   $��
25,154,0,0q�6a   $  0.475,0.213&�6u   �� 5   
   -   5        $��QuantityInStockTextBox��5      $
��Left=�60   $	��Top=�6K   $	��200��6c   $��141,158,0,0q�6o   �� 5   
   $��Mã vạch:��6   $
��Left=�6#   $	��Top=�6>   $��
40,182,0,0q�6V   �� 5   
   -   5        $��MavachTextBox��5      $
��Left=�6'   $	��Top=�6B   $	��200��6Z   $��141,181,0,0q�6f   �� 5   
   $��Mã nhà cung ứng:��6   $
��Left=�6+   $	��Top=�6F   $��
25,204,0,0q�6^   �� 5   
   -   5        $��SupplierIDTextBox��5      $
��Left=�6+   $	��Top=�6F   $	��200��6^   $��141,208,0,0q�6j   �� 5    
   -   5         ��Name$ 	AddButton��5       $��Thêm sản phẩm��6$   $	��100��6=   $��25��6I   $��
365,83,0,0q�6U   $	��Top=�6i   $
��Left=�6�   6�   �� 5!   
   -	   5        $ UpdateButton��5!      $��Cập nhật sản phẩm��6'   $	��100��6D   $��30��6P   $��365,158,0,0q�6\   $	��Top=�6q   $
��Left=�6�   6�   �� 5"   
   -
   5        $ DeleteButton��5"      $
��Xóa��6'   $	��100��65   $��26��6A   $��365,118,0,0q�6M   $  0.548,2.223&�6}   $
��Left=�6�   $	��Top=�6�   �� 5#   
   -   5        $ tkkhoButton��5#      $��Sản phẩm sắp hết��6&   $	��100��6A   $��26��6M   $��365,204,0,0q�6Y   $  0.548,2.223&�6�   $
��Left=�6�   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5&   
   -   5        $��ProductsDataGrid��5&      $	��Top=�6+   $	��200��6C   $��30,252,29,0q�6P      AutoGenerateColumns . $  0.624,-0.086&�6�   6�   5'      5(      