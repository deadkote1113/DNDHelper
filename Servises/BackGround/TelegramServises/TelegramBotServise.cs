using BL;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Servises.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Common.Search;

namespace Servises.BackGround.TelegramServises;

internal class TelegramBotServise : BackgroundService
{
	private readonly ILogger<TelegramBotServise> _logger;
	private readonly ITelegramBotClient _bot;
	private readonly IAwardingClientServise _awardingClientServise;

	public TelegramBotServise(ILogger<TelegramBotServise> logger)
	{
		_logger = logger;
		_bot = new TelegramBotClient("5655087992:AAG2o94rAcsSFURAUuc6xMzp7INdGgzzxJE");
		_awardingClientServise = new AwardingClientServise(_bot);
	}

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await CreateBot(stoppingToken);
		try
		{
			while (stoppingToken.IsCancellationRequested == false)
			{
				await Task.Delay(1000);
				var sessions = await new AwardSessionsBL().GetAsync(new AwardSessionsSearchParams());
				await _awardingClientServise.UpdateAwardSessions(sessions.Objects.ToList(), stoppingToken);
			}
		}
		catch (Exception)
		{

			throw;
		}
	}

	private async Task CreateBot(CancellationToken stoppingToken)
	{
		var receiverOptions = new ReceiverOptions
		{
			AllowedUpdates = new UpdateType[] { UpdateType.Message }, // receive all update types
		};
		_bot.StartReceiving(
			HandleUpdateAsync,
			HandleErrorAsync,
			receiverOptions,
			stoppingToken
		);
	}

	private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
	{
		_logger.LogInformation(JsonConvert.SerializeObject(update));
		switch (update.Type)
		{
			case UpdateType.Message:
				try
				{
					await OnMassage(botClient, update, cancellationToken);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "{0}", ex);
				}
				break;
			default:
				await GenericAnswer(botClient, update, cancellationToken);
				break;
		}
	}

	private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
	{
		_logger.LogInformation(JsonConvert.SerializeObject(exception));
	}

	private async Task OnMassage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
	{
		var message = update.Message;
		var text = message.Text.ToLower();

		var telegramUser = new TelegramUser(update.Message);
		if (text == "/start")
		{
			await _awardingClientServise.Start(update, cancellationToken);
		}
		if (text.StartsWith("/sub"))
		{
			var code = text.Replace("/sub", "").Trim();
			await _awardingClientServise.Subscride(telegramUser, code, cancellationToken);
		}
		if (text.StartsWith("/vote"))
		{
			var option = int.Parse(text.Split(" ")[1]);
			await _awardingClientServise.Vote(telegramUser, option, cancellationToken);
		}
		if (text == "/cancel_vote")
		{
			await _awardingClientServise.CancelVote(telegramUser, cancellationToken);
		}
	}

	private async Task GenericAnswer(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
	{
		var message = update.Message;
		await botClient.SendTextMessageAsync(message.Chat, "пиши сука текстом"); ;
	}
}

/*

/start - основная инфа по сервису + добавление пользователя в бд
/sub <code> - подписка на рассылку голосование
/vote <number> - голосование за варианты
/cancel_vote - отмена голоса

всякие мемные штуки мб придумаю потом
 
 */