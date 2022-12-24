using Entities;
using Servises.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Servises.BackGround.TelegramServises;

internal class AwardingClientServise : IAwardingClientServise
{
	private readonly ITelegramBotClient _client;

	public AwardingClientServise(ITelegramBotClient client)
	{
		_client = client;
	}

	public async Task<TelegramUser> Start(Update update, CancellationToken cancellationToken)
	{
		var telegramUser = new TelegramUser(update.Message.From);
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

	public async Task SendOptions(List<TelegramUser> user, List<NominationsSelectionOption> options, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}


	public async Task Subscride(TelegramUser user, string code, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task Vote(TelegramUser user, int optionIndex, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}