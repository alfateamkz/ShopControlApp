   M S B A M L   `   `   `  ����I  EShopControlApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ShopControlApp.GoodsAtWarehouseShopControlApp.GoodsAtWarehouse  5   	   <    6ShopControlApp.GoodsAtWarehouse.GoodsAtWarehouseUpdate   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:ShopControlApp.GoodsAtWarehouse     5   	      WindowColor#���   5        � Title$Q  KРедактирование данных о товаре на складе��5      $��198.81��68   $��436.197��6H    � 
ResizeMode$ NoResize=�6Y    � WindowStartupLocation$ CenterScreen=�6o   .��5	      �   GridBackground#���  6    � ColumnDefinitions 5
   
   �� 5      $
��219* �6   �� 5      $
��211* �6   .+�5   
   �� -   5         ��Name$ 
quantityTB��5   -   $
��Left=�6   $��32��6A   $��18(�6M   $��
218,75,0,0q�6[    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $	��202��6�   $��2��6�   ��6�   �� *GoodsAtWarehouseQuantity+ �� Mode$ TwoWay=�6�   �� 5   
     	TextColor#���  6   $
��Left=�6:   $��	20,75,0,0q�6U   $��18(�6i   $
��Wrap=�6x   $��Количество��6�   $	��Top=�6�   �� 5   
   #���  6   $
��Left=�6:   $��	21,30,0,0q�6U   $��18(�6i   $
��Wrap=�6x   $'��!ID товара на складе��6�   $	��Top=�6�   �� 5   
   -   5        $ idWarehouseTB��5   -   $
��Left=�6   $��32��6D   $��18(�6P   $��0.254,30,0,0q�6^   $
 Wrap=�6t   $	��Top=�6�   $	��202��6�   $��1��6�   ��6�   �� *GoodsAtWarehouseID+$ TwoWay=�6�   �� 5   
     ButtonStyle#���  6   $#��Изменить данные��67   $
��Left=�6t   $��18(�6�   $��
21,123,0,0q�6�   $	��Top=�6�   $	��399��6�   $��2��6�   ��6Q   �� *UpdateCommand+6�   5      5      