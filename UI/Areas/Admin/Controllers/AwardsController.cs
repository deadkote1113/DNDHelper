using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Common.Enums;
using Common.Search;
using BL;
using UI.Areas.Admin.Models;
using UI.Areas.Admin.Models.ViewModels;
using UI.Other;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class AwardsController : Controller
	{
		public async Task<IActionResult> Index(int page = 1)
		{
			const int objectsPerPage = 20;
			var searchResult = await new AwardsBL().GetAsync(new AwardsSearchParams
			{
				StartIndex = (page - 1) * objectsPerPage,
				ObjectsCount = objectsPerPage,
			});
			var viewModel = new SearchResultViewModel<AwardModel>(AwardModel.FromEntitiesList(searchResult.Objects),
				searchResult.Total, searchResult.RequestedStartIndex, searchResult.RequestedObjectsCount, 5);
			return View(viewModel);
		}

		public async Task<IActionResult> Update(int? id)
		{
			var model = new AwardModel();
			if (id != null)
			{
				model = AwardModel.FromEntity(await new AwardsBL().GetAsync(id.Value));
				if (model == null)
					return NotFound();
			}
			else
			{
				var user = await new UsersBL().GetAsync(User.Identity.Name);
				model.UserId = user.Id;
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(AwardModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			model.Id = await new AwardsBL().AddOrUpdateAsync(AwardModel.ToEntity(model));
			TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var result = await new AwardsBL().DeleteAsync(id);
			if (result)
				TempData[OperationResultType.Success.ToString()] = "Объект удален";
			else
				TempData[OperationResultType.Error.ToString()] = "Объект не найден";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> StartAwarding()
		{
			var sessions = await new AwardSessionsBL().GetAsync(new AwardSessionsSearchParams() { State = AwardSessionsState.InProgress });
			var sessionsModel = AwardSessionModel.FromEntitiesList(sessions.Objects);
			return View(sessionsModel);
		}

		public async Task<IActionResult> AjaxGetAwards(int lastIndex, string q)
		{
			var categories = (await new AwardsBL().GetAsync(new AwardsSearchParams(lastIndex, 5)
			{
				SearchQuery = q,
			})).Objects.Select(item => new Select2ViewModel(item.Id.ToString(), item.Title));

			return Json(categories);
		}

		public async Task<IActionResult> AjaxCreateSession(int awardId)
		{
			var user = UserModel.FromEntity(await new UsersBL().GetAsync(User.Identity.Name));
			var code = new AwardSessionsBL().GenerateCode();
			var session = new AwardSessionModel()
			{
				UserId = user.Id,
				AwardId = awardId,
				State = AwardSessionsState.Created,
				ConnectionCode = code,
			};
			var id = await new AwardSessionsBL().AddOrUpdateAsync(AwardSessionModel.ToEntity(session));
			return Json(new { code, id });
		}

		public async Task<IActionResult> AjaxStartAwarding(int sessionId)
		{
			var session = await new AwardSessionsBL().GetAsync(sessionId);
			session.State = AwardSessionsState.InProgress;
			await new AwardSessionsBL().AddOrUpdateAsync(session);
			return Json(new { Url = $"SeeNominations?sessionId={session.Id}" });
		}

		public async Task<IActionResult> SeeNominations(int sessionId)
		{
			var session = await new AwardSessionsBL().GetAsync(sessionId);
			var nominations = await new NominationsBL().GetAsync(new NominationsSearchParams
			{
				AwardId = session.AwardId
			});
			var events = await new AwardEventsBL().GetAsync(new AwardEventsSearchParams
			{
				AwardId = session.AwardId
			});
			var result1 = AwardItemViewModel.FromEntities(NominationModel.FromEntitiesList(nominations.Objects), AwardEventModel.FromEntitiesList(events.Objects));

			var nomination = result1.Skip(session.NominationPassed).FirstOrDefault();
			List<NominationsSelectionOptionModel> options = null;
			if (nomination.Type == AwardItemType.Nomination)
			{
				options = NominationsSelectionOptionModel.FromEntitiesList((await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
				{
					NominationId = nomination.Item.Id
				})).Objects);
			}
			var model = new SeeNominationsViewModel(nomination, options);
			return View(model);
		}

		public async Task<IActionResult> SeeVotes(int sessionId)
		{
			var result = new List<VoteViewModel>();
			var session = await new AwardSessionsBL().GetAsync(sessionId);
			var nominations = await new NominationsBL().GetAsync(new NominationsSearchParams
			{
				AwardId = session.AwardId
			});
			var events = await new AwardEventsBL().GetAsync(new AwardEventsSearchParams
			{
				AwardId = session.AwardId
			});
			var result1 = AwardItemViewModel.FromEntities(NominationModel.FromEntitiesList(nominations.Objects), AwardEventModel.FromEntitiesList(events.Objects));

			var nomination = result1.Skip(session.NominationPassed).FirstOrDefault();

			var votes = await new VotesBL().GetAsync(new VotesSearchParams()
			{
				NominationId = nomination.Item.Id
			});
			var nominationOptions = await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
			{
				NominationId = nomination.Item.Id
			});
			foreach (var nominationOption in nominationOptions.Objects)
			{
				var votesModels = votes.Objects.Where(item => item.NominationsSelectionOptionsId == nominationOption.Id);
				var voteModel = new VoteViewModel(NominationsSelectionOptionModel.FromEntity(nominationOption), VoteModel.FromEntitiesList(votesModels));
				result.Add(voteModel);
			}
			return View(result);
		}

		public async Task<IActionResult> GoNext(int sessionId)
		{
			var session = await new AwardSessionsBL().GetAsync(sessionId);
			var nominations = await new NominationsBL().GetAsync(new NominationsSearchParams(0, 0)
			{
				AwardId = session.AwardId
			});
			var events = await new AwardEventsBL().GetAsync(new AwardEventsSearchParams(0,0)
			{
				AwardId = session.AwardId
			});
			if (nominations.Total + events.Total - 1 == session.NominationPassed)
			{
				session.NominationPassed++;
				session.State = AwardSessionsState.End;
				await new AwardSessionsBL().AddOrUpdateAsync(session);
				return RedirectToAction("AwardEnd", new { sessionId });
			}
			else
			{
				session.NominationPassed++;
				await new AwardSessionsBL().AddOrUpdateAsync(session);
				return RedirectToAction("SeeNominations", new { sessionId });
			}
		}

		public async Task<IActionResult> AwardEnd(int sessionId)
		{
			return View();
		}

		public async Task<IActionResult> CountVotes(int sessionId)
		{
			var session = await new AwardSessionsBL().GetAsync(sessionId);
			var nomination = await new AwardsBL().GetCurentNominationItem(session.AwardId, session.NominationPassed);
			var votes = await new VotesBL().GetAsync(new VotesSearchParams()
			{
				NominationId = nomination.Id
			});
			var voteCount = votes.Objects.GroupBy(item => item.TelegramUserName).Where(item => item.Count() == 3).Count();
			return Json(new { voteCount });
		}
	}
}
