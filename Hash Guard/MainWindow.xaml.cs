using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserClass user;
        string userPass;
        AccountManager accountManager = new AccountManager();
        public MainWindow(UserClass _user, string userpass)
        {
            user = _user;
            userPass = userpass;
            InitializeComponent();
            welcomeText.Text = user.username + "'s Accounts";
            if (user.encryptedData != null)
                RefreshAccounts(true);
            else
                totalAccountsText.Text = $"you have {accountManager.accounts.Count} account{(accountManager.accounts.Count == 1 ? "" : 's')}";
        }
        public void RefreshAccounts(bool fromStart)
        {
            accountsStackPanel.Children.Clear();

            if (fromStart)
            {
                BinaryFormatter bf = new BinaryFormatter();

                EncryptionHandler encryption = new EncryptionHandler();

                byte[] decryptedStream = encryption.mainLoop(false, userPass, 8, user.encryptedData);

                MemoryStream stream = new MemoryStream(decryptedStream);
                stream.Position = 0;

                accountManager = (AccountManager)bf.Deserialize(stream);
            }
            accountManager.ProcessAccounts();
            foreach (KeyValuePair<string, List<int>> item in accountManager.accountsAmounts)
            {
                LoadAccount(item.Key, item.Value.Count);
            }
            totalAccountsText.Text = $"you have {accountManager.accounts.Count} account{(accountManager.accounts.Count == 1 ? "" : 's')}";
        }
        public void LoadAccount(string accountName, int amount)
        {

            var newAccountButton = new Button();
            var newAccountGrid = new Grid();
            var newAccountIcon = new MaterialDesignThemes.Wpf.PackIcon();
            var newAccountName = new TextBlock();
            var newAccountAmount = new TextBlock();

            //Set Account Grid
            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(90);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(455);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(455);
            newAccountGrid.ColumnDefinitions.Add(gridCol1);
            newAccountGrid.ColumnDefinitions.Add(gridCol2);
            newAccountGrid.ColumnDefinitions.Add(gridCol3);

            //Set Account Amount
            newAccountAmount.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
            newAccountAmount.SetValue(TextBlock.FontSizeProperty, 48.0);
            newAccountAmount.SetValue(TextBlock.HeightProperty, 64.0);
            Grid.SetColumn(newAccountAmount, 2);
            newAccountAmount.Text = $"{amount} account{(amount > 1 ? "s" : "")}";

            //Set Account Name
            newAccountName.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
            newAccountName.SetValue(TextBlock.FontSizeProperty, 48.0);
            newAccountName.SetValue(TextBlock.HeightProperty, 64.0);
            Grid.SetColumn(newAccountName, 1);
            newAccountName.Text = accountName;

            //Set Account Icon
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, GetAccountIcon(accountName));
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.WidthProperty, 64.0);
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.HeightProperty, 64.0);
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.ForegroundProperty, new BrushConverter().ConvertFromString("#c3c3c3"));
            Grid.SetColumn(newAccountIcon, 0);

            //Set Account Button
            newAccountButton.Width = 1100;
            newAccountButton.Height = 100;
            newAccountButton.Margin = new Thickness(15, 5, 15, 5);
            newAccountButton.Style = buttonAddAccount.Style;
            newAccountButton.SetValue(MaterialDesignThemes.Wpf.ButtonAssist.CornerRadiusProperty, new CornerRadius(15));
            newAccountButton.SetValue(Button.BorderThicknessProperty, new Thickness(2));
            newAccountButton.SetValue(Button.BorderBrushProperty, new BrushConverter().ConvertFromString("#191823"));
            newAccountButton.SetValue(Button.BackgroundProperty, new BrushConverter().ConvertFromString("#292d36"));
            newAccountButton.SetValue(Button.ForegroundProperty, new BrushConverter().ConvertFromString("#a8aebd"));
            newAccountButton.Tag = accountName;
            newAccountButton.Click += showInnderAccounts_Click;

            //Join Childrens
            newAccountGrid.Children.Add(newAccountIcon);
            newAccountGrid.Children.Add(newAccountName);
            newAccountGrid.Children.Add(newAccountAmount);
            newAccountButton.Content = newAccountGrid;

            accountsStackPanel.Children.Add(newAccountButton);

        }
        private MaterialDesignThemes.Wpf.PackIconKind GetAccountIcon(string Name)
        {
            switch (Name)
            {
                case "Amazon": return MaterialDesignThemes.Wpf.PackIconKind.Amazon;
                case "Android": return MaterialDesignThemes.Wpf.PackIconKind.Android;
                case "Apple": return MaterialDesignThemes.Wpf.PackIconKind.Apple;
                case "Icloud": return MaterialDesignThemes.Wpf.PackIconKind.AppleIcloud;
                case "Arch": return MaterialDesignThemes.Wpf.PackIconKind.Arch;
                case "Artstation": return MaterialDesignThemes.Wpf.PackIconKind.Artstation;
                case "Aws": return MaterialDesignThemes.Wpf.PackIconKind.Aws;
                case "Babel": return MaterialDesignThemes.Wpf.PackIconKind.Babel;
                case "Bandcamp": return MaterialDesignThemes.Wpf.PackIconKind.Bandcamp;
                case "Battlenet": return MaterialDesignThemes.Wpf.PackIconKind.Battlenet;
                case "Bitbucket": return MaterialDesignThemes.Wpf.PackIconKind.Bitbucket;
                case "Bitcoin": return MaterialDesignThemes.Wpf.PackIconKind.Bitcoin;
                case "Blackmesa": return MaterialDesignThemes.Wpf.PackIconKind.BlackMesa;
                case "Blend Swap": return MaterialDesignThemes.Wpf.PackIconKind.BlenderSoftware;
                case "Blender": return MaterialDesignThemes.Wpf.PackIconKind.BlenderSoftware;
                case "Blogger": return MaterialDesignThemes.Wpf.PackIconKind.Blogger;
                case "Bootstrap": return MaterialDesignThemes.Wpf.PackIconKind.Bootstrap;
                case "Box": return MaterialDesignThemes.Wpf.PackIconKind.Box;
                case "Buffer": return MaterialDesignThemes.Wpf.PackIconKind.Buffer;
                case "Bulma": return MaterialDesignThemes.Wpf.PackIconKind.Bulma;
                case "Codepen": return MaterialDesignThemes.Wpf.PackIconKind.Codepen;
                case "Cordova": return MaterialDesignThemes.Wpf.PackIconKind.Cordova;
                case "Cryengine": return MaterialDesignThemes.Wpf.PackIconKind.Cryengine;
                case "Debian": return MaterialDesignThemes.Wpf.PackIconKind.Debian;
                case "Deviantart": return MaterialDesignThemes.Wpf.PackIconKind.Deviantart;
                case "Discord": return MaterialDesignThemes.Wpf.PackIconKind.Discord;
                case "Disqus": return MaterialDesignThemes.Wpf.PackIconKind.Disqus;
                case "Dlna": return MaterialDesignThemes.Wpf.PackIconKind.Dlna;
                case "Docker": return MaterialDesignThemes.Wpf.PackIconKind.Docker;
                case "Dolby": return MaterialDesignThemes.Wpf.PackIconKind.Dolby;
                case "Douban": return MaterialDesignThemes.Wpf.PackIconKind.Douban;
                case "Dropbox": return MaterialDesignThemes.Wpf.PackIconKind.Dropbox;
                case "Drupal": return MaterialDesignThemes.Wpf.PackIconKind.Drupal;
                case "Electron": return MaterialDesignThemes.Wpf.PackIconKind.ElectronFramework;
                case "Ember": return MaterialDesignThemes.Wpf.PackIconKind.Ember;
                case "Emby": return MaterialDesignThemes.Wpf.PackIconKind.Emby;
                case "Eslint": return MaterialDesignThemes.Wpf.PackIconKind.Eslint;
                case "Etherium": return MaterialDesignThemes.Wpf.PackIconKind.Ethereum;
                case "Evernote": return MaterialDesignThemes.Wpf.PackIconKind.Evernote;
                case "Facebook": return MaterialDesignThemes.Wpf.PackIconKind.Facebook;
                case "Fedora": return MaterialDesignThemes.Wpf.PackIconKind.Fedora;
                case "Firebase": return MaterialDesignThemes.Wpf.PackIconKind.Firebase;
                case "Firefox": return MaterialDesignThemes.Wpf.PackIconKind.Firefox;
                case "Font Awesome": return MaterialDesignThemes.Wpf.PackIconKind.FontAwesome;
                case "Freebsd": return MaterialDesignThemes.Wpf.PackIconKind.Freebsd;
                case "Gatsby": return MaterialDesignThemes.Wpf.PackIconKind.Gatsby;
                case "Gentoo": return MaterialDesignThemes.Wpf.PackIconKind.Gentoo;
                case "Git": return MaterialDesignThemes.Wpf.PackIconKind.Git;
                case "Github": return MaterialDesignThemes.Wpf.PackIconKind.Github;
                case "Gmail": return MaterialDesignThemes.Wpf.PackIconKind.Gmail;
                case "Gnome": return MaterialDesignThemes.Wpf.PackIconKind.Gnome;
                case "Gog": return MaterialDesignThemes.Wpf.PackIconKind.Gog;
                case "Google": return MaterialDesignThemes.Wpf.PackIconKind.Google;
                case "Graphql": return MaterialDesignThemes.Wpf.PackIconKind.Graphql;
                case "Hulu": return MaterialDesignThemes.Wpf.PackIconKind.Hulu;
                case "Humble Bundle": return MaterialDesignThemes.Wpf.PackIconKind.HumbleBundle;
                case "Instagram": return MaterialDesignThemes.Wpf.PackIconKind.Instagram;
                case "Ifunny": return MaterialDesignThemes.Wpf.PackIconKind.Smiley;
                case "Iobroker": return MaterialDesignThemes.Wpf.PackIconKind.Iobroker;
                case "Jabber": return MaterialDesignThemes.Wpf.PackIconKind.Jabber;
                case "Jira": return MaterialDesignThemes.Wpf.PackIconKind.Jira;
                case "Kickstarter": return MaterialDesignThemes.Wpf.PackIconKind.Kickstarter;
                case "Kodi": return MaterialDesignThemes.Wpf.PackIconKind.Kodi;
                case "Kubernetes": return MaterialDesignThemes.Wpf.PackIconKind.Kubernetes;
                case "Lastpass": return MaterialDesignThemes.Wpf.PackIconKind.Lastpass;
                case "Linked In": return MaterialDesignThemes.Wpf.PackIconKind.Linkedin;
                case "LinkedIn": return MaterialDesignThemes.Wpf.PackIconKind.Linkedin;
                case "Linux": return MaterialDesignThemes.Wpf.PackIconKind.Linux;
                case "Litecoin": return MaterialDesignThemes.Wpf.PackIconKind.Litecoin;
                case "Lumx": return MaterialDesignThemes.Wpf.PackIconKind.Lumx;
                case "Manjaro": return MaterialDesignThemes.Wpf.PackIconKind.Manjaro;
                case "Mastodon": return MaterialDesignThemes.Wpf.PackIconKind.Mastodon;
                case "Meteor": return MaterialDesignThemes.Wpf.PackIconKind.Meteor;
                case "Microsoft": return MaterialDesignThemes.Wpf.PackIconKind.Microsoft;
                case "Minecraft": return MaterialDesignThemes.Wpf.PackIconKind.Minecraft;
                case "Netflix": return MaterialDesignThemes.Wpf.PackIconKind.Netflix;
                case "Nix": return MaterialDesignThemes.Wpf.PackIconKind.Nix;
                case "Nuxt": return MaterialDesignThemes.Wpf.PackIconKind.Nuxt;
                case "Oci": return MaterialDesignThemes.Wpf.PackIconKind.Oci;
                case "Onepassword": return MaterialDesignThemes.Wpf.PackIconKind.Onepassword;
                case "Openid": return MaterialDesignThemes.Wpf.PackIconKind.Openid;
                case "Opera": return MaterialDesignThemes.Wpf.PackIconKind.Opera;
                case "Origin": return MaterialDesignThemes.Wpf.PackIconKind.Origin;
                case "Pandora": return MaterialDesignThemes.Wpf.PackIconKind.Pandora;
                case "Patreon": return MaterialDesignThemes.Wpf.PackIconKind.Patreon;
                case "Pinterest": return MaterialDesignThemes.Wpf.PackIconKind.Pinterest;
                case "Plex": return MaterialDesignThemes.Wpf.PackIconKind.Plex;
                case "Polymer": return MaterialDesignThemes.Wpf.PackIconKind.Polymer;
                case "Qqchat": return MaterialDesignThemes.Wpf.PackIconKind.Qqchat;
                case "Reddit": return MaterialDesignThemes.Wpf.PackIconKind.Reddit;
                case "Salesforce": return MaterialDesignThemes.Wpf.PackIconKind.Salesforce;
                case "Sass": return MaterialDesignThemes.Wpf.PackIconKind.Sass;
                case "SimpleIcons": return MaterialDesignThemes.Wpf.PackIconKind.SimpleIcons;
                case "SinaWiebo": return MaterialDesignThemes.Wpf.PackIconKind.SinaWeibo;
                case "Skype": return MaterialDesignThemes.Wpf.PackIconKind.Skype;
                case "Slack": return MaterialDesignThemes.Wpf.PackIconKind.Slack;
                case "Snapchat": return MaterialDesignThemes.Wpf.PackIconKind.Snapchat;
                case "Playstation": return MaterialDesignThemes.Wpf.PackIconKind.SonyPlaystation;
                case "Soundcloud": return MaterialDesignThemes.Wpf.PackIconKind.Soundcloud;
                case "Spotify": return MaterialDesignThemes.Wpf.PackIconKind.Spotify;
                case "Stack Exchange": return MaterialDesignThemes.Wpf.PackIconKind.StackExchange;
                case "Stack Overflow": return MaterialDesignThemes.Wpf.PackIconKind.StackOverflow;
                case "Stack Path": return MaterialDesignThemes.Wpf.PackIconKind.Stackpath;
                case "Steam": return MaterialDesignThemes.Wpf.PackIconKind.Steam;
                case "Team Viewer": return MaterialDesignThemes.Wpf.PackIconKind.Teamviewer;
                case "Terraform": return MaterialDesignThemes.Wpf.PackIconKind.Terraform;
                case "Trello": return MaterialDesignThemes.Wpf.PackIconKind.Trello;
                case "Twitch": return MaterialDesignThemes.Wpf.PackIconKind.Twitch;
                case "Twitter": return MaterialDesignThemes.Wpf.PackIconKind.Twitter;
                case "Ubisoft": return MaterialDesignThemes.Wpf.PackIconKind.Ubisoft;
                case "Ubuntu": return MaterialDesignThemes.Wpf.PackIconKind.Ubuntu;
                case "Umbraco": return MaterialDesignThemes.Wpf.PackIconKind.Umbraco;
                case "Unity": return MaterialDesignThemes.Wpf.PackIconKind.Unity;
                case "Unreal": return MaterialDesignThemes.Wpf.PackIconKind.Unreal;
                case "Untappd": return MaterialDesignThemes.Wpf.PackIconKind.Untappd;
                case "Vimeo": return MaterialDesignThemes.Wpf.PackIconKind.Vimeo;
                case "Vk": return MaterialDesignThemes.Wpf.PackIconKind.Vk;
                case "Wechat": return MaterialDesignThemes.Wpf.PackIconKind.Wechat;
                case "Whatsapp": return MaterialDesignThemes.Wpf.PackIconKind.Whatsapp;
                case "Wikipedia": return MaterialDesignThemes.Wpf.PackIconKind.Wikipedia;
                case "Wordpress": return MaterialDesignThemes.Wpf.PackIconKind.Wordpress;
                case "Xamarin": return MaterialDesignThemes.Wpf.PackIconKind.Xamarin;
                case "Xbox": return MaterialDesignThemes.Wpf.PackIconKind.MicrosoftXbox;
                case "Xing": return MaterialDesignThemes.Wpf.PackIconKind.Xing;
                case "Yahoo": return MaterialDesignThemes.Wpf.PackIconKind.Yahoo;
                case "Youtube": return MaterialDesignThemes.Wpf.PackIconKind.Youtube;
            }
            return MaterialDesignThemes.Wpf.PackIconKind.Account;
        }
        private void buttonAddAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountsListStackPanel.Visibility = Visibility.Hidden;
            AccountDetailsStackPanel.Visibility = Visibility.Visible;

            accountManager.currentAcount = new Account("", "", "", "", "", "");

            accountManager.currentAcount.uuid = CreateUUID();

            while (true)
            {
                bool unique = true;
                foreach (Account acc in accountManager.accounts)
                    if (acc.uuid == accountManager.currentAcount.uuid)
                    {
                        unique = false;
                        break;
                    }
                if (!unique)
                    accountManager.currentAcount.uuid = CreateUUID();
                else
                    break;
            }

            accountDetailsPasswordCheckbox.IsChecked = false;
            accountDetailsAccountName.Text = accountManager.currentAcount.name;
            accountDetailsUrl.Text = accountManager.currentAcount.url;
            accountDetailsEmail.Text = accountManager.currentAcount.email;
            accountDetailsUsername.Text = accountManager.currentAcount.username;
            accountDetailsPassword.Password = accountManager.currentAcount.password;
            accountDetailsNotes.Text = accountManager.currentAcount.notes;
            accountDetailsPassword.PasswordChar = '•';
            accountDetailsPasswordText.Visibility = Visibility.Hidden;

            if (!accountManager.currentAcount.favorite)
            {
                favoriteIcon.Tag = "unckecked";
                favoriteIcon.Foreground = (Brush)new BrushConverter().ConvertFromString("#440011");
            }
            else
            {
                favoriteIcon.Tag = "checked";
                favoriteIcon.Foreground = (Brush)new BrushConverter().ConvertFromString("#ff4466");
            }

        }
        public string CreateUUID()
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();
            return myuuidAsString;
        }
        private void buttonSaveBackup_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Title = "Save Backup";
            dlg.Filter = "db files (*.db)|*.db|All files (*.*)|*.*";
            dlg.FileName = "Backup.db";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    File.Copy("user-data.db", dlg.FileName, true);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }
            }
        }
        private void buttonLoadBackup_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Load Backup";
            dlg.Filter = "db files (*.db)|*.db|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    File.Copy(dlg.FileName, "user-data.db", true);

                    UserClass checkingUser = SQLiteDataAccess.LoadUser(user.username);

                    if (checkingUser.username == null)
                    {
                        LoginScreen login = new LoginScreen();
                        login.Show();
                        this.Close();
                    }
                    else
                    {
                        user = checkingUser;
                        if (user.encryptedData != null)
                            RefreshAccounts(true);
                        else
                            totalAccountsText.Text = $"you have {accountManager.accounts.Count} account{(accountManager.accounts.Count == 1 ? "" : 's')}";
                    }
                    
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }
            }
        }
        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }
        private void accountSave_Click(object sender, RoutedEventArgs e)
        {
            bool saved = false;
            foreach (Account acc in accountManager.accounts)
            {
                if (acc.uuid == accountManager.currentAcount.uuid)
                {
                    acc.name = accountDetailsAccountName.Text;
                    acc.url = accountDetailsUrl.Text;
                    acc.email = accountDetailsEmail.Text;
                    acc.username = accountDetailsUsername.Text;
                    acc.password = accountDetailsPassword.Password;
                    acc.notes = accountDetailsNotes.Text;
                    acc.favorite = ((string)favoriteIcon.Tag == "checked");
                    accountManager.currentAcount = acc;
                    saved = true;
                }
            }
            if (!saved)
            {
                accountManager.currentAcount.name = accountDetailsAccountName.Text;
                accountManager.currentAcount.url = accountDetailsUrl.Text;
                accountManager.currentAcount.email = accountDetailsEmail.Text;
                accountManager.currentAcount.username = accountDetailsUsername.Text;
                accountManager.currentAcount.password = accountDetailsPassword.Password;
                accountManager.currentAcount.notes = accountDetailsNotes.Text;
                accountManager.currentAcount.favorite = ((string)favoriteIcon.Tag == "checked");
                accountManager.accounts.Add(accountManager.currentAcount);
            }
            updateSaveIconLocal();
            SaveAccounts();
        }
        private void accountDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Account acc in accountManager.accounts)
            {
                if (acc.uuid == accountManager.currentAcount.uuid)
                {
                    accountManager.accounts.Remove(acc);
                    break;
                }
            }
            RefreshAccounts(false);
            AccountsListStackPanel.Visibility = Visibility.Visible;
            AccountDetailsStackPanel.Visibility = Visibility.Hidden;
            SaveAccounts();
        }
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void showInnderAccounts_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            accountsStackPanel.Children.Clear();
            int[] indexs = accountManager.accountsAmounts[button.Tag.ToString()].ToArray();
            foreach (int i in indexs)
            {
                LoadInnerAccount(button.Tag.ToString(), i);
            }
        }
        private void ShowAccountDetails(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AccountsListStackPanel.Visibility = Visibility.Hidden;
            AccountDetailsStackPanel.Visibility = Visibility.Visible;

            accountManager.currentAcount = accountManager.accounts[(int)button.Tag];

            accountDetailsPasswordCheckbox.IsChecked = false;
            accountDetailsAccountName.Text = accountManager.currentAcount.name;
            accountDetailsUrl.Text = accountManager.currentAcount.url;
            accountDetailsEmail.Text = accountManager.currentAcount.email;
            accountDetailsUsername.Text = accountManager.currentAcount.username;
            accountDetailsPassword.Password = accountManager.currentAcount.password;
            accountDetailsNotes.Text = accountManager.currentAcount.notes;
            accountDetailsPassword.PasswordChar = '•';
            accountDetailsPasswordText.Visibility = Visibility.Hidden;

            if (!accountManager.currentAcount.favorite)
            {
                favoriteIcon.Tag = "unckecked";
                favoriteIcon.Foreground = (Brush)new BrushConverter().ConvertFromString("#440011");
            }
            else
            {
                favoriteIcon.Tag = "checked";
                favoriteIcon.Foreground = (Brush)new BrushConverter().ConvertFromString("#ff4466");
            }

        }
        private void passwordCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            accountDetailsPassword.Visibility = Visibility.Hidden;
            accountDetailsPasswordText.Visibility = Visibility.Visible;
            accountDetailsPasswordText.Text = accountDetailsPassword.Password;
            accountDetailsPasswordText.Focus();
        }
        private void passwordCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            accountDetailsPassword.Visibility = Visibility.Visible;
            accountDetailsPasswordText.Visibility = Visibility.Hidden;
            accountDetailsPassword.Password = accountDetailsPasswordText.Text;
            accountDetailsPassword.Focus();
        }
        private void buttonPasswordReset_Click(object sender, RoutedEventArgs e)
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            int seed = BitConverter.ToInt32(randomBytes, 0);
            Random r = new Random(seed);

            StringBuilder passwordBuilder = new StringBuilder((int)30);
            char[] allowableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"£$%^&*()_+-=[]{}'#@~./>?,<\\€".ToCharArray();

            for (int i = 0; i < 30; i++)
            {
                int nextInt = r.Next(allowableChars.Length);
                char c = allowableChars[nextInt];
                passwordBuilder.Append(c);
            }

            string newPassword = passwordBuilder.ToString();
            accountDetailsPassword.Password = newPassword;
            accountDetailsPasswordText.Text = newPassword;
        }
        private void buttonPasswordCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(accountDetailsPassword.Password);
        }
        public void LoadInnerAccount(string accountName, int index)
        {
            Account account = accountManager.accounts[index];

            var newAccountButton = new Button();
            var newAccountGrid = new Grid();
            var newAccountIcon = new MaterialDesignThemes.Wpf.PackIcon();
            var newAccountName = new TextBlock();
            var newAccountAmount = new TextBlock();

            //Set Account Grid
            ColumnDefinition gridCol1 = new ColumnDefinition();
            gridCol1.Width = new GridLength(90);
            ColumnDefinition gridCol2 = new ColumnDefinition();
            gridCol2.Width = new GridLength(455);
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol3.Width = new GridLength(455);
            newAccountGrid.ColumnDefinitions.Add(gridCol1);
            newAccountGrid.ColumnDefinitions.Add(gridCol2);
            newAccountGrid.ColumnDefinitions.Add(gridCol3);

            //Set Account Amount
            newAccountAmount.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
            newAccountAmount.SetValue(TextBlock.FontSizeProperty, 28.0);
            newAccountAmount.SetValue(TextBlock.HeightProperty, 36.0);
            Grid.SetColumn(newAccountAmount, 2);
            newAccountAmount.Text = account.username == "" ? account.email == "" ? "Account" : account.email : account.username;

            //Set Account Name
            newAccountName.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
            newAccountName.SetValue(TextBlock.FontSizeProperty, 28.0);
            newAccountName.SetValue(TextBlock.HeightProperty, 36.0);
            Grid.SetColumn(newAccountName, 1);
            newAccountName.Text = account.name;

            //Set Account Icon
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.KindProperty, GetAccountIcon(accountName));
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.WidthProperty, 64.0);
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.HeightProperty, 64.0);
            newAccountIcon.SetValue(MaterialDesignThemes.Wpf.PackIcon.ForegroundProperty, new BrushConverter().ConvertFromString("#c3c3c3"));
            Grid.SetColumn(newAccountIcon, 0);

            //Set Account Button
            newAccountButton.Width = 1100;
            newAccountButton.Height = 100;
            newAccountButton.Margin = new Thickness(15, 5, 15, 5);
            newAccountButton.Style = buttonAddAccount.Style;
            newAccountButton.SetValue(MaterialDesignThemes.Wpf.ButtonAssist.CornerRadiusProperty, new CornerRadius(15));
            newAccountButton.SetValue(Button.BorderThicknessProperty, new Thickness(2));
            newAccountButton.SetValue(Button.BorderBrushProperty, new BrushConverter().ConvertFromString("#191823"));
            newAccountButton.SetValue(Button.BackgroundProperty, new BrushConverter().ConvertFromString("#292d36"));
            newAccountButton.SetValue(Button.ForegroundProperty, new BrushConverter().ConvertFromString("#a8aebd"));
            newAccountButton.Tag = index;
            newAccountButton.Click += ShowAccountDetails;

            //Join Childrens
            newAccountGrid.Children.Add(newAccountIcon);
            newAccountGrid.Children.Add(newAccountName);
            newAccountGrid.Children.Add(newAccountAmount);
            newAccountButton.Content = newAccountGrid;

            accountsStackPanel.Children.Add(newAccountButton);

        }
        private void updateSaveIcon(object sender, RoutedEventArgs e)
        {
            updateSaveIconLocal();
        }
        private void updateSaveIconLocal()
        {
            saveWarning.Visibility = CheckIfChanged() ? Visibility.Hidden : Visibility.Visible;
        }
        private bool CheckIfChanged()
        {
            if (accountDetailsAccountName.Text != accountManager.currentAcount.name) return false;
            if (accountDetailsUrl.Text != accountManager.currentAcount.url) return false;
            if (accountDetailsEmail.Text != accountManager.currentAcount.email) return false;
            if (accountDetailsUsername.Text != accountManager.currentAcount.username) return false;
            if (accountDetailsPassword.Password != accountManager.currentAcount.password) return false;
            if (accountDetailsNotes.Text != accountManager.currentAcount.notes) return false;
            if (((string)favoriteIcon.Tag == "checked") != accountManager.currentAcount.favorite) return false;
            return true;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }
        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            RefreshAccounts(false);
            AccountsListStackPanel.Visibility = Visibility.Visible;
            AccountDetailsStackPanel.Visibility = Visibility.Hidden;
        }
        private void SaveAccounts()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            bf.Serialize(stream, accountManager);
            byte[] data = stream.ToArray();

            EncryptionHandler encryption = new EncryptionHandler();

            byte[] encryptedStream = encryption.mainLoop(true, userPass, 8, data);

            user.encryptedData = encryptedStream;

            SQLiteDataAccess.SaveUser(user, true);
        }
        private void switchFavorited(object sender, RoutedEventArgs e)
        {
            bool favorited = ((string)favoriteIcon.Tag == "checked");
            if (favorited)
            {
                favoriteIcon.Tag = "unckecked";
                favoriteIcon.Foreground = (Brush)new BrushConverter().ConvertFromString("#440011");
            }
            else
            {
                favoriteIcon.Tag = "checked";
                favoriteIcon.Foreground = (Brush)new BrushConverter().ConvertFromString("#ff4466");
            }
            updateSaveIconLocal();
        }
    }
}
