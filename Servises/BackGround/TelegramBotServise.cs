using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

namespace Servises.BackGround
{
	internal class TelegramBotServise : BackgroundService
	{
		private readonly ILogger<TelegramBotServise> _logger;
		private readonly ITelegramBotClient _bot;

		public TelegramBotServise(ILogger<TelegramBotServise> logger)
		{
			_logger = logger;
			_bot = new TelegramBotClient("5655087992:AAG2o94rAcsSFURAUuc6xMzp7INdGgzzxJE");
		}

		protected async override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				await MainCircul(stoppingToken);
			}
			catch (Exception)
			{

				throw;
			}
		}

		private async Task MainCircul(CancellationToken stoppingToken)
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
				case UpdateType.Unknown:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.Message:
					await OnMassage(botClient, update, cancellationToken);
					break;
				case UpdateType.InlineQuery:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.ChosenInlineResult:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.CallbackQuery:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.EditedMessage:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.ChannelPost:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.EditedChannelPost:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.ShippingQuery:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.PreCheckoutQuery:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.Poll:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.PollAnswer:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.MyChatMember:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.ChatMember:
					await GenericAnswer(botClient, update, cancellationToken);
					break;
				case UpdateType.ChatJoinRequest:
					await GenericAnswer(botClient, update, cancellationToken);
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
			if (message.Text.ToLower() == "/start")
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
		}

		private async Task GenericAnswer(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			var message = update.Message;
			await botClient.SendTextMessageAsync(message.Chat, "пиши сука текстом"); ;		
		}
	}
}
