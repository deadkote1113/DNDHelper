namespace Entities;

public interface IAwardItem
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public int OrderId { get; set; }
	public int AwardsId { get; set; }

	bool NeedVote();
}
