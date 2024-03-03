namespace Entities
{
	public class Nomination : IAwardItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int AwardsId { get; set; }
		public int OrderId { get; set; }
		public bool IsCompleted { get; set; }

		public Nomination(int id, string title, string description, int awardsId, int orderId, bool isCompleted)
		{
			Id = id;
			Title = title;
			Description = description;
			AwardsId = awardsId;
			OrderId = orderId;
			IsCompleted = isCompleted;
		}

		public bool NeedVote()
		{
			return true;
		}
	}
}
