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

	public TelegramUser(long id, bool isBot, string firstName, string? lastName)
	{
		Id = id;
		IsBot = isBot;
		FirstName = firstName;
		LastName = lastName;
	}

	public TelegramUser(User user)
	{
		Id = user.Id;
		IsBot = user.IsBot;
		FirstName = user.FirstName;
		LastName = user.LastName;
	}
}