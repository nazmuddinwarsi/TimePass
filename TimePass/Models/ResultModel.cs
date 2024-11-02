using System.ComponentModel.DataAnnotations;

namespace TimePass.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public DateTime? ResultTime { get; set; }
		public DateTime? NextPlayTime { get; set; }
        [Required(ErrorMessage = "Please Enter Coins")]
        [Range(0, 9, ErrorMessage = "Number should be between 0 and 9")]
        public string? ResultNumber { get; set; }
        public int? ResultWinner { get; set; }
        public int? zero {  get; set; }
        public int? one {  get; set; }
        public int? two { get; set; }
        public int? three { get; set; }  
        public int? four { get; set; }
        public int? five { get; set; }
        public int? six { get; set; }
        public int? seven { get; set; }
        public int? eight { get; set; }
        public int? nine { get; set; }
        public List<ResultModel> ResultsList { get; set;}
    }
}
