namespace Password_Manager
{
    public class UserClass
    {
        public string username { get; set; }
        public string passwordHash { get; set; }
        public byte[] encryptedData { get; set; }
    }
}
