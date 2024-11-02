namespace TimePass.Models
{
    public class UserLogin
    {
        public int Userid {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Active { get; set; }
    }
}
