namespace TimePass.Models
{
	public class TransactionModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int? Userid { get; set; }

		public int? Coins { get; set; }

		public DateTime? TTime { get; set; }
	}
}
