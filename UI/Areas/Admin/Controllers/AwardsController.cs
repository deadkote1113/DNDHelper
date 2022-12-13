using System;
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
			await new AwardsBL().AddOrUpdateAsync(AwardModel.ToEntity(model));
			TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
			return RedirectToAction("Index");
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
			return View();
		}

		public async Task<IActionResult> SeeNominations(int awardId, int nominationCount)
		{
			var nominations = await new NominationsBL().GetAsync(new NominationsSearchParams(nominationCount - 1, 1)
			{
				AwardId = awardId,
			});
			if(nominations.Objects.Count == 0)
			{
				return View("AwardEnd");
			}
			var nomination = nominations.Objects.FirstOrDefault();
			var options = await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
			{
				NominationId = nomination.Id
			});
			var nominationModel = NominationModel.FromEntity(nomination);
			var optionModels = NominationsSelectionOptionModel.FromEntitiesList(options.Objects);
			var model = new SeeNominationsViewModel(nominationModel, optionModels);
			return View(model);
		}

		public async Task<IActionResult> AjaxGetAwards(int lastIndex, string q)
		{
			var categories = (await new AwardsBL().GetAsync(new AwardsSearchParams(lastIndex, 5)
			{
				SearchQuery = q,
			})).Objects.Select(item => new Select2ViewModel(item.Id.ToString(), item.Title));

			return Json(categories);
		}

		public async Task<IActionResult> SeeVotes(int awardId, int nominationCount, int nominationId)
		{
			var votes = await new VotesBL().GetAsync(new VotesSearchParams()
			{
				NominationId = nominationId
			});
			var models = VoteModel.FromEntitiesList(votes.Objects);
			return View(models);
		}
	}
}
