   M S B A M L   `   `   `  ���� D  @HashGuard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Password_ManagerPassword_Manager  !    Password_Manager.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  *localclr-namespace:Password_Manager    � Title$  
Hash Guard��$	��800��$
��1400�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�.��� ����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,0&�.���  � Color$ #39bae6�� � Offset$ 0Q�� $ #6bedad��$ 1Q�.+��� -   $��74��$	��Top=����#191823 �� Orientation$ 
Horizontal=�.+��� $	��350��$ 
Horizontal=�$
��Left=�.+��� $$��/img/password-manager-logo.png��$��64��$	��Top=�$��	20 5 20 5q�	 �� BitmapScalingMode$	 HighQuality=��� $	��200��$��40�����#b3b8c5$��32(�
 �� TextAlignment$
 Center=�$��Center=�
Hash Guard�� $	��950��$��Center=�.+��� -    ��Name$ welcomeText��$	��900��$��40�����#b3b8c5$��32(�$
 Right=�$��Right=�$��0 0 20 0q�Welcome Back�� -   $ totalAccountsText��$	��900��$��30�����#86898f$��24(�$
 Right=�$��Right=�$��0 0 20 0q�you have n accounts�� $	��100��$��Center=�.+��� -    ��Name$ 
buttonExit��$
��5 10q� &  "MaterialDesignFloatingActionButton#��[   � ToolTip$ Close����Z� .��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�
�������$��30��$��30���� $	��720��$	��250��$��Bottom=�$
��Left=����#20202c$ Vertical=�.+��� $	��550��$��0 20 0 0q�.+��� -   $ 
buttonHome��$	��220��$��60��$
��15 5q�  MaterialDesignRaisedButton#��[ +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 15��$��2q����#191823���#292d36���#a8aebd$
 Home��.���� $ 
Horizontal=�.+�  $
 Home=�$��32��$��32�����#827db5$��4q��� $
 Center=�$��24(�$��32��Home�� -   $ buttonAddAccount��$	��220��$��60��$
��15 5q�#��[ $ 15��$��2q����#191823���#292d36���#a8aebd$ Add Account��.���� $ 
Horizontal=�.+�  $ PlusBold=�$��32��$��32�����#a4bd00$��4q��� $
 Center=�$��24(�$��32��Add Account�� $ 
Horizontal=�$��10q�$��30��.+�  $ Search=�$��32��$��32�����#b3b8c5�� -    ��Name$ searchField��$
��10 0q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Search�����#b3b8c5$	��190��$��16(��� $ 
Horizontal=�$��10q�$��30��.+�  $
 Sort=�$��32��$��32�����#b3b8c5�� -    ��Name$ sortBy��$
��10 0q�$
 Sort�����#b3b8c5$	��190��$��16(� �� SelectedIndex$ 0��.@��� $	��A-Z���� $��Most Visited���� $��Most Recent���� -	   $y�Hidden=�����  �� ElementName$ sortBy�� �� Path$
 Text��� .+��� -
   $ buttonSaveBackup��$	��220��$��60��$
��15 5q�#��[ $ 15��$��2q����#191823���#292d36���#a8aebd$ Save Backup��.���� $ 
Horizontal=�.+�  $ Export=�$��32��$��32�����#91b362$��4q��� $
 Center=�$��24(�$��32��Save Backup�� -   $ buttonLoadBackup��$	��220��$��60��$
��15 5q�#��[ $ 15��$��2q����#191823���#292d36���#a8aebd$ Load Backup��.���� $ 
Horizontal=�.+�  $ Import=�$��32��$��32�����#6994bf$��4q��� $
 Center=�$��24(�$��32��Load Backup�� -    ��Name$ AccountsListStackPanel��$	��720��$
��1150��$��Bottom=�$��Right=����#151515.+��� $	��710��$
��True��.���� -   $ accountsStackPanel��$
��0 10q��� -   $ AccountDetailsStackPanel��$	��720��$
��1150��$��Bottom=�$��Right=����#151515$y�Hidden=�.+�� $��60��$��0 20 0 0q����#191823$
��1100�� � ColumnDefinitions �� $��3* ��� $	��10* �.+��� $��50��$��32(�$��0�����#b3b8c5$
��20 0q�$��Center=�$
 Right=�Account Name�� -   $ accountDetailsAccountName��$��50��$��32(�$��1�����#b3b8c5$
��20 0q�$��Center=� �� TextAlignment$
 Left=����#222130
������ $
��10 0q� �� 	MaxLength$ 60��� $��60��$��0 10 0 0q����#191823$
��1100�� �� $��3* ��� $��9* ��� $��* �.+��� $��50��$��32(�$��0�����#b3b8c5$
��20 0q�$��Center=�$
 Right=�Url�� -   $ accountDetailsUrl��$��50��$��32(�$��1�����#b3b8c5$
��20 0q�$��Center=�$
 Left=����#222130
������ $
��10 0q�$	 250���� -   $��70��$��50��$��3��#��[ $ 5��$��2q����#191823���#292d36���#a8aebd$ Copy URL�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~�T�  T� X$	 -12Q�.��  $ ContentCopy=�$��32��$��32�����#6994bf� $��60��$��0 10 0 0q����#191823$
��1100�� �� $��3* ��� $��9* ��� $��* �.+��� $��50��$��32(�$��0�����#b3b8c5$
��20 0q�$��Center=�$
 Right=�Email�� -   $ accountDetailsEmail��$��50��$��32(�$��1�����#b3b8c5$
��20 0q�$��Center=�$
 Left=����#222130
������ $
��10 0q�$	 254���� -   $��70��$��50��$��3��#��[ $ 5��$��2q����#191823���#292d36���#a8aebd$ 
Copy Email��$ 0.5,0.5&�z�V� .~�T� $	 -12Q�.��  $ ContentCopy=�$��32��$��32�����#6994bf� $��60��$��0 10 0 0q����#191823$
��1100�� �� $��3* ��� $��9* ��� $��* �.+��� $��50��$��32(�$��0�����#b3b8c5$
��20 0q�$��Center=�$
 Right=�
Username�� -   $ accountDetailsUsername��$��50��$��32(�$��1�����#b3b8c5$
��20 0q�$��Center=�$
 Left=����#222130
������ $
��10 0q�$ 50���� -   $��70��$��50��$��3��#��[ $ 5��$��2q����#191823���#292d36���#a8aebd$ Copy Username��$ 0.5,0.5&�z�V� .~�T� $	 -12Q�.��  $ ContentCopy=�$��32��$��32�����#6994bf� $��60��$��0 10 0 0q����#191823$
��1100�� �� $��6* ��� $	��15* ��� $��* ��� $��2* ��� $��2* �.+��� $��50��$��32(�$��0�����#b3b8c5$
��20 0q�$��Center=�$
 Right=�
 � Tag$ password��
PasswordG� -    G�Name$ accountDetailsPassword��$��50��$��32(�$��1�����#b3b8c5$
��20 0q�$��Center=����#222130
������ $
��10 0q� G� 	MaxLength$	 100���� -   $  accountDetailsPasswordText��$��50��$��32(�$��1�����#b3b8c5$
��20 0q�$��Center=����#222130
������ $
��10 0q�$	 100��$y�Hidden=��� -    ��Name$$ accountDetailsPasswordCheckbox��$��16��$��16��$��2��$��36(�$��Center=�$ 0.5,0.5&�z�V� .~���   �� ScaleX$  2Q�! �� ScaleY$! 2Q�T� $	 -10Q��� -   $��70��$��50��$��3��#��[ $ 5��$��2q����#191823���#292d36���#a8aebd$ New Password��$ 0.5,0.5&�z�V� .~�T� $	 -12Q�.��  $ PasswordReset=�$��32��$��32�����#6994bf�� -   $��70��$��50��$��4��#��[ $ 5��$��2q����#191823���#292d36���#a8aebd$ Copy Password��$ 0.5,0.5&�z�V� .~�T� $	 -12Q�.��  $ ContentCopy=�$��32��$��32�����#6994bf� $	��270��$��0 10 0 0q����#191823$
��1100��.+��� -   $ accountDetailsNotes��$	��210��$��20(�$��0�����#a8aebd$��
20 0 20 15q�$��Bottom=�$
 Left=�" �� TextWrapping$
" Wrap=�# �� MaxLines$
# 1000��$ �� AcceptsReturn$
$ True��% �� 
AcceptsTab$
% True��"& �� VerticalScrollBarVisibility$
& Auto=�' �� 
CaretBrush' �#c3c3c3���#222130
������ $
��10 0q�$ 10000���� $��32��$��28(�$��0�����#b3b8c5$
��20 0q�$	��Top=�$

 Left=�$ 0.5,0.5&�z�V� .~�T� ( T� Y$( 5Q�Notes�� $ 
Horizontal=�$
��1120��$��Right=�.+�  -   )  Name$) saveWarning��$ ContentSaveAlert=�$��64��$��64�����#e6b450$��	20 0 50 0q�$��Center=�$ 	Not Saved��$y�Hidden=�  -   $) favoriteIcon��$ Favorite=�$��64��$��64�����#440011$��0 0 0 0q�$��Center=�$ 	Not Saved��$y�visible=�$ 	unchecked���� -   $	��400��$��60��$��30 10q�#��[ $ 10��$��2q����#191823���#292d36���#a8aebd$ Apply Changes��$
��Left=�.���� $ 
Horizontal=�.+�  $ ContentSave=�$��50��$��50�����#91b362$��0q��� $
 Center=�$��24(�$��32��Apply�� -   $	��400��$��60��$��30 10q�#��[ $ 10��$��2q����#e91e63���#f07178���#2a2a2a$ Delete Account��$
��Left=�.���� $ 
Horizontal=�.+�  $ Delete=�$��50��$��50�����#2a2a2a$��0q��� $
 Center=�$��24(�$��32��Delete Account