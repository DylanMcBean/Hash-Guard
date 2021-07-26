using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public string loggedInUser;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            textBlockWarning.Visibility = Visibility.Hidden;

            UserClass newUser = new UserClass();
            newUser.username = usernameField.Text; // get username from username field
            newUser.passwordHash = HashPassword(passwordField.Password); // get password from password field


            UserClass checkingUser = SQLiteDataAccess.LoadUser(newUser.username);
            bool validNewUser = checkingUser.username == null ? true : CompairPassword(passwordField.Password, checkingUser.passwordHash);

            if (checkingUser.username == null)
            {
                textBlockWarning.Text = "User does not exsist.";
                textBlockWarning.Visibility = Visibility.Visible;
            }
            else if (!validNewUser)
            {
                textBlockWarning.Text = "Password does not match";
                textBlockWarning.Visibility = Visibility.Visible;
            }
            else
            {
                MainWindow main = new MainWindow(checkingUser, passwordField.Password);
                main.Show();
                this.Close();
            }

        }

        private void passwordField_Changed(object sender, RoutedEventArgs e)
        {
            passwordFieldWarning.Visibility = CheckIfPasswordSecure(passwordField.Password) ? Visibility.Hidden : Visibility.Visible;
        }

        private void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            textBlockWarning.Visibility = Visibility.Hidden;

            UserClass newUser = new UserClass();
            newUser.username = usernameField.Text; // get username from username field
            newUser.passwordHash = HashPassword(passwordField.Password); // get password from password field

            newUser.encryptedData = null;

            UserClass checkingUser = SQLiteDataAccess.LoadUser(newUser.username);
            bool validNewUser = checkingUser.username == null ? true : CompairPassword(passwordField.Password, checkingUser.passwordHash);

            if (validNewUser && newUser.username.Length != 0)
                try
                {
                    SQLiteDataAccess.SaveUser(newUser, false);
                    MainWindow main = new MainWindow(newUser, passwordField.Password);
                    main.Show();
                    this.Close();
                }
                catch (Exception)
                {
                    textBlockWarning.Text = "User Already Exists";
                    textBlockWarning.Visibility = Visibility.Visible;
                }
            else if (newUser.username.Length > 0)
            {
                textBlockWarning.Text = "User Already Exists";
                textBlockWarning.Visibility = Visibility.Visible;
            }
            else
            {
                textBlockWarning.Text = "Invalid Username";
                textBlockWarning.Visibility = Visibility.Visible;
            }
        }

        private string HashPassword(string password)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //get hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            //combine hash and salt for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            //turn into string
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            //return saved password
            return savedPasswordHash;
        }

        private bool CompairPassword(string newPassword, string storedPassword)
        {
            //convert stored password back to bytes
            byte[] hashBytes = Convert.FromBase64String(storedPassword);
            //get salt from stored password
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            //compute hash of new password
            var pbkdf2 = new Rfc2898DeriveBytes(newPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            //check if they match
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }

        private bool CheckIfPasswordSecure(string password)
        {
            //Password Requirements: length: 12+, lowercase: 2+, uppercase: 2+, numbers: 2+, symbols: 2+

            //check length of the password
            if (password.Length < 12) return false;
            //check for minimum 2 lowercase characters
            if (new Regex(@"[a-z]").Matches(password).Count < 2) return false;
            //check for minimum 2 uppercase characters
            if (new Regex(@"[A-Z]").Matches(password).Count < 2) return false;
            //check for minimum 2 numbers
            if (new Regex(@"[0-9]").Matches(password).Count < 2) return false;
            //check for minimum 2 symbols
            if (new Regex(@"([\x20-\x2F]|[\x3A-\x40]|[\x5B-\x60]|[\x7B-\x7E])").Matches(password).Count < 2) return false;

            return true;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}