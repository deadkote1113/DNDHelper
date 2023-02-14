using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Servises.DTO;

public class TelegramUser
{
	[JsonProperty("id")]
	public long Id { get; set; }
	[JsonProperty("is_bot")]
	public bool IsBot { get; set; }
	[JsonProperty("first_name")]
	public string FirstName { get; set; }
	[JsonProperty("last_name")]
	public string? LastName { get; set; }
	[JsonProperty("last_name")]
	public long ChatId { get; set; }

	public TelegramUser(long id, bool isBot, string firstName, string? lastName)
	{
		Id = id;
		IsBot = isBot;
		FirstName = firstName;
		LastName = lastName;
	}

	public TelegramUser(Message massege)
	{
		User user = massege.From;
		Id = user.Id;
		IsBot = user.IsBot;
		FirstName = user.FirstName;
		LastName = user.LastName;
		ChatId = massege.Chat.Id;
	}
}