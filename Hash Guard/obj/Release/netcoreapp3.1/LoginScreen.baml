   M S B A M L   `   `   `  ���� D  @HashGuard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Password_ManagerPassword_Manager  "    Password_Manager.LoginScreen   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  *localclr-namespace:Password_Manager    � Title$  Login��$	��450��$	��800�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�.��� -   ����  �� 
StartPoint$ 0.1,0&� �� EndPoint$ 0.9,1&�.���  � Color$ #39bae6�� � Offset$ 0Q�� $ #6bedad��$ 1Q�.+��� -   $	��390��$	��Top=� �� CornerRadius$ 	0 0 180 0�����#151515�� 	 �� Orientation$	 
Horizontal=�.+��� $	��350��$	 Vertical=�.+��� $$��/img/password-manager-logo.png��$	��256��$	��Top=�$��30q�
 �� BitmapScalingMode$
 HighQuality=��� $	��200��$��50�����#b3b8c5$��36(� �� TextAlignment$ Center=�
Hash Guard�� $	��350��.+��� $��20 40q�.+��� $��
User Login��$��20q����#b3b8c5$ Center=�$��38(��� $	 
Horizontal=�$��10q�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�$��25��$��25�����#b3b8c5�� -    ��Name$ usernameField��$
��10 0q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Enter Username�����#b3b8c5$	��250�� �� 	MaxLength$ 50���� $	 
Horizontal=�$��10q�.+�  $ Password=�$��25��$��25�����#b3b8c5G� -    G�Name$ passwordField��$��10 0 3 0q�$ Enter Master Password�����#b3b8c5$	��235��  -     Name$ passwordFieldWarning��$	��0 4q�$ Warning=�$��16��$��16�����#e6b450 � ToolTip$T NWe do not consider this password secure, 
but you may use it at your own risk.��$y�Hidden=��� -    ��Name$ textBlockWarning��$y�Hidden=����#ff3333$ Center=��� $��10q�$��Center=�$	 
Horizontal=�.+��� -    ��Name$ buttonSignUp��$	��120��$��40��$
��10 0q�   MaterialDesignRaisedButton#��[  +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��$��2q����#e1af4b���#e6b450���#0a0e14$ Login��$��Create User���� -   $ buttonLogin��$	��120��$��40��$
��10 0q�#��[  $ 10��$��2q����#e1af4b���#e6b450���#0a0e14$ Login��$��Login���� $	��100��.+��� -	   $ 
buttonExit��$��10 20q� & "MaterialDesignFloatingActionButton#��[ $ Close����Z� .��  $ Close=�
�������$��30��$��30��