﻿using Entities;
using Servises.DTO;
using Telegram.Bot.Types;

namespace Servises.BackGround.TelegramServises;

internal interface IAwardingClientServise
{
	Task<TelegramUser> Start(Update update, CancellationToken cancellationToken);

	Task Subscride(TelegramUser user, string code, CancellationToken cancellationToken);

	Task Vote(TelegramUser user, int optionIndex, CancellationToken cancellationToken);

	Task CancelVote(TelegramUser user, CancellationToken cancellationToken);

	Task UpdateAwardSessions(List<AwardSession> newSessions, CancellationToken cancellationToken);
}