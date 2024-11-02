using System.ComponentModel.DataAnnotations;

namespace TimePass.Models
{
    public class UserDashboard
    {
       public int Id {  get; set; }
        public string Name { get; set; }
        public int? Coins { get; set; }
        public string PlayTime { get; set; }
		public DateTime NextPlayTime { get; set; }
        [Required(ErrorMessage = "Please Select Number")]
        public string PlayNumber { get; set; }
        [Required(ErrorMessage ="Please Enter Coins")]
        [Range(10,1000, ErrorMessage ="Coins should be between 10 and 1000")]
        
        public int? PlayCoins { get; set; }
        public string? photo {  get; set; }
       
        
        public List<ResultModel> PlayResult { get; set; }
    }
    
}
