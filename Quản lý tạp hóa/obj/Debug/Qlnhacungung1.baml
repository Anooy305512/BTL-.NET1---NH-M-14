   M S B A M L   `   `   `  ����P  LQuản lý tạp hóa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Quản_lý_tạp_hóaQuản_lý_tạp_hóa  5      )    #Quản_lý_tạp_hóa.Qlnhacungung1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      5local#clr-namespace:Quản_lý_tạp_hóa     5      .��5	      � .+�5
   
   (   "Quản_lý_tạp_hóa.MenuControl1  $	��Top=�6   $��0,20,0,0q�65   �� 5   
   -   5        $��SearchSupplierTextBox��5      $	��200��6/   $��30��6;   $��	10,76,0,0q�6G   $	��Top=�6Z   $
��Left=�6r   �� 5   
   -   5        $��SearchSupplierButton��5      $��Tìm kiếm��6-   $	��100��6@   $��30��6L   $��
220,76,0,0q�6X   $	��Top=�6l   $
��Left=�6�   6�   �� 5   
   $��Mã nhà cung ưng:��6   $
��Left=�6+   $	��Top=�6F   $��
20,136,0,0q�6^   �� 5   
   -   5        $��SupplierIDTextBox��5      $
��Left=�6+   $	��Top=�6F   $	��200��6^   $��132,140,0,0q�6j   �� 5   
   $��Tên nhà cung ưng:��6   $
��Left=�6,   $	��Top=�6G   $��
10,166,0,0q�6_   �� 5   
   -   5        $��SupplierNameTextBox��5      $
��Left=�6-   $	��Top=�6H   $	��200��6`   $��132,174,0,0q�6l   �� 5   
   $��Thông tin liên hệ:��6   $
��Left=�6-   $	��Top=�6H   $��
30,197,0,0q�6`     J� RenderTransformOrigin$  
0.62,-0.64&�6t   �� 5   
   -   5        $��ContactInfoTextBox��5      $
��Left=�6,   $	��Top=�6G   $	��200��6_   $��132,201,0,0q�6k   �� 5   
   -   5        $��AddSupplierButton��5      $��Thêm Nhà Cung Ứng��6*   $��
354,78,0,0q�6F   $
��Left=�6z   $	��Top=�6�   $��28��6�   �� 5   
   -   5        $��UpdateSupplierButton��5      $��Cập Nhật��6-   $��354,141,0,0q�6@   $
��Left=�6x   $	��Top=�6�   $	��116��6�   $��35��6�   �� 5   
   -   5        $��DeleteSupplierButton��5      $
��Xóa��6-   $��33��6;   $��354,186,0,0q�6G   $  0.599,0.986&�6   $
��Left=�6�   $	��Top=�6�   $	��116��6�   &   System.Windows.Controls.DataGrid  5!   
   -	   5        $��SuppliersDataGrid��5!      $	��Top=�6,   $	��200��6D   $��
20,256,0,0q�6Q      AutoGenerateColumns . 6e   5#      5$      