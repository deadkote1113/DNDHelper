using Common.Enums;

namespace Entities
{
	public class Vote
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public int NominationsSelectionOptionsId { get; set; }
		public bool IsCanseld { get; set; }
		public string TelegramUserName { get; set; }

		public string TelegramAvatar { get; set; }
		public VoteTir VoteTir { get; set; }

		public Vote(int id, int? userId, int nominationsSelectionOptionsId, bool isCanseld, string telegramUserName, string telegramAvatar, VoteTir voteTir)
		{
			Id = id;
			UserId = userId;
			NominationsSelectionOptionsId = nominationsSelectionOptionsId;
			IsCanseld = isCanseld;
			TelegramUserName = telegramUserName;
			TelegramAvatar = telegramAvatar;
			VoteTir = voteTir;
		}
	}
}
