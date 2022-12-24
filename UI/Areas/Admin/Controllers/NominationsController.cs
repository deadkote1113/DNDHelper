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
using UI.Areas.Admin.Models.ViewModels.FilterModels;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class NominationsController : Controller
	{
		public async Task<IActionResult> Index(NominationFilterModel filterModel, int page = 1)
		{
			const int objectsPerPage = 20;
			var searchResult = await new NominationsBL().GetAsync(new NominationsSearchParams
			{
				StartIndex = (page - 1) * objectsPerPage,
				ObjectsCount = objectsPerPage,
				AwardId = filterModel.AwardId
			});
			var viewModel = new SearchResultViewModel<NominationModel, NominationFilterModel>(NominationModel.FromEntitiesList(searchResult.Objects),
				filterModel, searchResult.Total, searchResult.RequestedStartIndex, searchResult.RequestedObjectsCount, 5);
			return View(viewModel);
		}

		public async Task<IActionResult> Update(int? id, int awardId)
		{
			var model = new NominationModel();
			if (id != null)
			{
				model = NominationModel.FromEntity(await new NominationsBL().GetAsync(id.Value));
				if (model == null)
					return NotFound();
			}
			else
			{
				model.AwardsId = awardId;
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(NominationModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			model.Id = await new NominationsBL().AddOrUpdateAsync(NominationModel.ToEntity(model));
			TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var result = await new NominationsBL().DeleteAsync(id);
			if (result)
				TempData[OperationResultType.Success.ToString()] = "Объект удален";
			else
				TempData[OperationResultType.Error.ToString()] = "Объект не найден";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> UpdateOption(NominationsSelectionOptionModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var id = await new NominationsSelectionOptionsBL().AddOrUpdateAsync(NominationsSelectionOptionModel.ToEntity(model));
			model.Id = id;
			return PartialView("Partials/_Option", model);
		}

		public async Task<IActionResult> GetOptions(int nominationId)
		{
			var options = await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
			{
				NominationId = nominationId
			});
			var models = NominationsSelectionOptionModel.FromEntitiesList(options.Objects);
			return PartialView("Partials/_GetOptions", models);
		}
		public async Task DeleteOption(int id)
		{
			await new NominationsSelectionOptionsBL().DeleteAsync(id);
		}
	}
}
