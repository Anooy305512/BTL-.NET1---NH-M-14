   M S B A M L   `   `   `  ����P  LQuản lý tạp hóa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Quản_lý_tạp_hóaQuản_lý_tạp_hóa  5      $    Quản_lý_tạp_hóa.Thungan1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Quản_lý_tạp_hóa     5        N� Title$  Thungan1��5	      .��5      � .+�5   
   (   "Quản_lý_tạp_hóa.MenuControl1  $	��Top=�6   $��0,20,0,0q�65   �� 5   
   -   5         ��Name$ BarcodeTextBox��5      $	��225��6*   $��30��66   $	��Top=�6B   $
��Left=�6Z   $��
108,85,0,0q�6u   �� 5   
   -   5        $��Tìm kiếm��5      $	��100��6$   $��30��60   $	��Top=�6<   $
��Left=�6T   $��
361,85,0,0q�6o    J� RenderTransformOrigin$ 
0.03,0.435&�6�   �� 5   
   -   5        $��Xóa sản phẩm��5      $	��100��6(   $��30��64   $	��Top=�6@   $
��Left=�6X   $��233,383,0,0q�6s   6�   �� 5   
   $	��Top=�6   $
��Left=�6-   $��118,136,0,0q�6H   .+�5      �� $��ID sản phẩm:��6   �� 5      -   5         ��Name$ ProductIDTextBlock��5      $
��Bold"�64   �� 5      $��Tên sản phẩm:��6   �� 5      -   5        $ ProductNameTextBlock��5      $
��Bold"�66   �� 5      $��Giá sản phẩm:��6   �� 5      -   5        $ ProductPriceTextBlock��5      $
��Bold"�67   5      �� 5   
   $��Số lượng:��6   $	��Top=�6$   $
��Left=�6<   $��108,256,0,0q�6W   �� 5   
   -   5        $ QuantityTextBox��5      $��98��6+   $��26��66   $	��Top=�6B   $
��Left=�6Z   $��174,256,0,0q�6u   �� 5!   
   -   5        $��Thêm vào giỏ hàng��5!      $	��150��6-   $��30��69   $	��Top=�6E   $
��Left=�6]   $��183,331,0,0q�6x   6�   &   System.Windows.Controls.DataGrid  5$   
   -	   5          Name$ CartDataGrid��5$      $	��Top=�6)   $
��Left=�6A   $��358,136,0,0q�6\   $	��555��6q   $	��429��6}   �� 5&   
   $��Tổng thành tiền:��6   $��368,578,0,0q�6,   $��60��6A   $��26��6L   $
��Left=�6X   $	��Top=�6s   �� 5'   
   -
   5        $ TotalPriceTextBlock��5'      $
��Bold"�61   $��60��6C   $��26��6N   $
��Left=�6Z   $	��Top=�6u   $��455,578,0,0q�6�   $ 
1.494,0.41&�6�   �� 5/   
   -   5        $��Thanh toán��5/      $	��100��6&   $��30��62   $��813,578,0,0q�6>   $
��Left=�6j   $	��Top=�6�   $ 0.707,0.133&�6�   51      52      