namespace Entities
{
	public class AwardEvent : IAwardItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int OrderId { get; set; }
		public int AwardsId { get; set; }
		public bool IsCompleted { get; set; }

		public AwardEvent(int id, string title, string description, int orderId, int awardsId, bool isCompleted)
		{
			Id = id;
			Title = title;
			Description = description;
			OrderId = orderId;
			AwardsId = awardsId;
			IsCompleted = isCompleted;
		}

		public bool NeedVote()
		{
			return false;
		}
	}
}
