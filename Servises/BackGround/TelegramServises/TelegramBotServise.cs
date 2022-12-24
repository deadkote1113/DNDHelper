using Dal.DbModels;
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
using Telegram.Bot.Types.ReplyMarkups;

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
				await MainCircul(stoppingToken);
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

	private async Task MainCircul(CancellationToken stoppingToken)
	{

	}

	private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
	{
		_logger.LogInformation(JsonConvert.SerializeObject(update));
		switch (update.Type)
		{
			case UpdateType.Message:
				await OnMassage(botClient, update, cancellationToken);
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
		if (text == "/start")
		{
			await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");

			ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
			{
				new KeyboardButton[] { "Help me", "Call me" },
				new KeyboardButton[] { "Help me", "Call me" },
				new KeyboardButton[] { "Help me", "Call me" },
				new KeyboardButton[] { "Help me", "Call me" },
			})
			{
				ResizeKeyboard = true
			};

			var massage = await botClient.SendTextMessageAsync(
				chatId: message.Chat.Id,
				text: "Choose",
				replyMarkup: replyKeyboardMarkup,
				cancellationToken: cancellationToken);
		}
		if (text.StartsWith("/sub"))
		{

		}
		if (text.StartsWith("/vote"))
		{

		}
		if (text == "/cancel_vote")
		{

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