using BL;
using Common.Search;
using Entities;
using Servises.DTO;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;

namespace Servises.BackGround.TelegramServises;

internal class AwardingClientServise : IAwardingClientServise
{
	private readonly ITelegramBotClient _client;
	private readonly List<AwardSession> _curentSessions;
	private readonly Dictionary<long, List<Vote>> _votes;
	private readonly Dictionary<string, List<TelegramUser>> _usersToAwards;

	public AwardingClientServise(ITelegramBotClient client)
	{
		_client = client;
		_curentSessions = new();
		_votes = new();
		_usersToAwards = new();
	}

	public async Task UpdateAwardSessions(List<AwardSession> newSessions, CancellationToken cancellationToken)
	{
		foreach (var newSession in newSessions)
		{
			var previosSession = _curentSessions.Where(item => item.Id == newSession.Id).FirstOrDefault();
			if (previosSession == null)
			{
				_curentSessions.Add(newSession);
			}
			else
			{
				if (newSession.State == Common.Enums.AwardSessionsState.End)
				{
					var index = _curentSessions.Select(item => item.Id).ToList().IndexOf(newSession.Id);
					_curentSessions.RemoveAt(index);
					await new AwardSessionsBL().DeleteAsync(newSession.Id);
				}
				else
				{
					if (previosSession.NominationPassed != newSession.NominationPassed || previosSession.State != newSession.State)
					{
						previosSession.NominationPassed = newSession.NominationPassed;
						previosSession.State = newSession.State;

						var nomination = await new AwardsBL().GetCurentNominationItem(newSession.AwardId, newSession.NominationPassed);

						if (nomination.NeedVote())
						{
							var options = await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
							{
								NominationId = nomination.Id
							});
							await SendOptions(newSession.ConnectionCode.ToLower(), options.Objects.ToList(), cancellationToken);
						}
					}
				}
			}
		}
	}

	public async Task<TelegramUser> Start(Update update, CancellationToken cancellationToken)
	{
		var telegramUser = new TelegramUser(update.Message);
		//save
		var message = update.Message;
		var text = "Приветствую в боте для проведения Награждений.\nДля начала работы введите /sub CODE\nи вместо слова CODE сам код";
		var massage = await _client.SendTextMessageAsync(chatId: message.Chat.Id, text: text, cancellationToken: cancellationToken);
		return telegramUser;
	}

	public async Task CancelVote(TelegramUser user, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task Subscride(TelegramUser user, string code, CancellationToken cancellationToken)
	{
		string text;
		if (_usersToAwards.Select(item => item.Value).SelectMany(item => item).Select(item => item.ChatId).Contains(user.ChatId))
		{
			text = "нельзя участвовать в двух голосаваниях одновременно";
		}
		else
		{
			if (_curentSessions.Select(item => item.ConnectionCode.ToLower()).Contains(code))
			{
				text = "вы успешно подписались";
				bool isExict = _usersToAwards.TryGetValue(code, out List<TelegramUser> users);
				if (isExict)
				{
					users.Add(user);
				}
				else
				{
					_usersToAwards.Add(code, new() { user });
				}
			}
			else
			{
				text = "не валидный код";
			}
		}
		var massage = await _client.SendTextMessageAsync(chatId: user.ChatId, text: text, cancellationToken: cancellationToken);
	}

	public async Task Vote(TelegramUser user, int nominationOptionId, CancellationToken cancellationToken)
	{
		var code = _usersToAwards.Select(item => item).Where(item => item.Value.Select(item => item.Id).Contains(user.Id)).FirstOrDefault().Key ?? "";
		if (string.IsNullOrEmpty(code))
		{
			var text = "вы не участвуете ни в одном голосовании";
			var massage = await _client.SendTextMessageAsync(chatId: user.ChatId, text: text, cancellationToken: cancellationToken);
		}
		else
		{
			var isExisted = _votes.TryGetValue(user.Id, out List<Vote> votes);
			votes = isExisted ? votes : new();


			if (votes.Count() <= 2)
			{
				var awardSession = _curentSessions.Where(item => item.ConnectionCode.ToLower() == code).FirstOrDefault();
				var nomination = await new AwardsBL().GetCurentNominationItem(awardSession.AwardId, awardSession.NominationPassed);
				var options = await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
				{
					NominationId = nomination.Id
				});
				var vote = new Vote(0, null, options.Objects.Where(item => item.Id == nominationOptionId).FirstOrDefault().Id, false, $"{user.LastName} {user.FirstName}", "", Common.Enums.VoteTir.Gold);

				var badIndexes = new List<int>()
				{
					nominationOptionId
				};
				foreach (var item in votes)
				{
					badIndexes.Add(item.NominationsSelectionOptionsId);
				}
				var lastNominations = options.Objects.Where(item => badIndexes.Contains(item.Id) == false).ToList();
				if (votes.Count == 0)
				{
					ReplyKeyboardMarkup replyKeyboardMarkup = CreateButtons(lastNominations);

					vote.VoteTir = Common.Enums.VoteTir.Gold;
					await _client.SendTextMessageAsync(
						chatId: user.ChatId,
						text: "выберайте серебренного призёра",
						replyMarkup: replyKeyboardMarkup,
						cancellationToken: cancellationToken);
				}
				if (votes.Count == 1)
				{
					ReplyKeyboardMarkup replyKeyboardMarkup = CreateButtons(lastNominations);

					vote.VoteTir = Common.Enums.VoteTir.Silver;
					await _client.SendTextMessageAsync(
						chatId: user.ChatId,
						text: "выберайте бронзового призёра",
						replyMarkup: replyKeyboardMarkup,
						cancellationToken: cancellationToken);
				}
				if (votes.Count == 2)
				{
					vote.VoteTir = Common.Enums.VoteTir.Bronze;
					await _client.SendTextMessageAsync(
						chatId: user.ChatId,
						text: "вы проголосовали 3 раза",
						replyMarkup: new ReplyKeyboardRemove(),
						cancellationToken: cancellationToken);
					_votes.Remove(user.Id);
				}
				await new VotesBL().AddOrUpdateAsync(vote);

				if (votes.Count == 0)
				{
					_votes.Add(user.Id, new() { vote });
				}
				else
				{
					votes.Add(vote);
				}
			}
		}
	}

	private async Task SendOptions(string code, List<NominationsSelectionOption> options, CancellationToken cancellationToken)
	{
		var replyKeyboardMarkup = CreateButtons(options);

		foreach (var user in _usersToAwards.Where(item => item.Key == code).FirstOrDefault().Value)
		{
			var massage = await _client.SendTextMessageAsync(
			chatId: user.ChatId,
			text: "выберайте золотого призёра",
			replyMarkup: replyKeyboardMarkup,
			cancellationToken: cancellationToken);
		}
	}

	private ReplyKeyboardMarkup CreateButtons(List<NominationsSelectionOption> options)
	{
		List<List<KeyboardButton>> keaboardOptions = new();
		for (int i = 0; i < options.Count; i++)
		{
			KeyboardButton button = new($"/vote {options[i].Id} {options[i].Title}");
			if (i % 2 == 0)
			{
				var buttons = new List<KeyboardButton>() { button };
				keaboardOptions.Add(buttons);
			}
			else
			{
				keaboardOptions[i / 2].Add(button);
			}
		}

		return new ReplyKeyboardMarkup(keaboardOptions)
		{
			ResizeKeyboard = true
		};
	}
}