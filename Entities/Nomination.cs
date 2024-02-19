namespace Entities
{
	public class Nomination
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int AwardsId { get; set; }
		public int OrderId { get; set; }
		public int ReaderId { get; set; }

		public Reader Reader { get; set; }

		public Nomination(int id, string title, string description, int awardsId, int orderId, int readerId)
		{
			Id = id;
			Title = title;
			Description = description;
			AwardsId = awardsId;
			OrderId = orderId;
			ReaderId = readerId;
		}
	}
}
