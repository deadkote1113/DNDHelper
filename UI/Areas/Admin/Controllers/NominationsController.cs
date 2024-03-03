using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Common.Enums;
using Common.Search;
using BL;
using UI.Areas.Admin.Models;
using UI.Areas.Admin.Models.ViewModels;
using UI.Other;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class NominationsController : Controller
	{
		public async Task<IActionResult> Index(int awardId)
		{
			var searchResult1 = await new NominationsBL().GetAsync(new NominationsSearchParams
			{
				AwardId = awardId
			});
			var searchResult2 = await new AwardEventsBL().GetAsync(new AwardEventsSearchParams
			{
				AwardId = awardId
			});
			var result = AwardItemViewModel.FromEntities(NominationModel.FromEntitiesList(searchResult1.Objects), AwardEventModel.FromEntitiesList(searchResult2.Objects));
			ViewBag.AwardId = awardId;
			return View(result);
		}

		public async Task<IActionResult> Update(int? id, int awardId)
		{
			var model = new NominationModel();
			ViewBag.AwardId = awardId;
			if (id != null)
			{
				model = NominationModel.FromEntity(await new NominationsBL().GetAsync(id.Value));
				if (model == null)
				{
					return NotFound();
				}
			}
			else
			{
				model.AwardsId = awardId;
				model.OrderId = await GetOrderById(awardId);
			}
			await ConfigureViewBag();
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
			var isNew = model.Id == 0;
			model.Id = await new NominationsBL().AddOrUpdateAsync(NominationModel.ToEntity(model));
			if (isNew)
			{
				return RedirectToAction("Update", new { id = model.Id, awardId = model.AwardsId });
			}
			await ConfigureViewBag();
			TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
			ViewBag.AwardId = model.AwardsId;
			return View(model);
		}

		public async Task<IActionResult> UpdateAwardEvent(int? id, int awardId)
		{
			var model = new AwardEventModel();
			ViewBag.AwardId = awardId;
			if (id != null)
			{
				model = AwardEventModel.FromEntity(await new AwardEventsBL().GetAsync(id.Value));
				if (model == null)
				{
					return NotFound();
				}
			}
			else
			{
				model.AwardsId = awardId;
				model.OrderId = await GetOrderById(awardId);
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAwardEvent(AwardEventModel model)
		{
			ViewBag.AwardId = model.AwardsId;
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var isNew = model.Id == 0;
			model.Id = await new AwardEventsBL().AddOrUpdateAsync(AwardEventModel.ToEntity(model));
			if (isNew)
			{
				return RedirectToAction("UpdateAwardEvent", new { id = model.Id, awardId = model.AwardsId });
			}
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
			await ConfigureViewBag();
			return PartialView("Partials/_Option", model);
		}

		public async Task<IActionResult> GetOptions(int nominationId)
		{
			var options = await new NominationsSelectionOptionsBL().GetAsync(new NominationsSelectionOptionsSearchParams()
			{
				NominationId = nominationId
			});
			var models = NominationsSelectionOptionModel.FromEntitiesList(options.Objects);
			await ConfigureViewBag();
			return PartialView("Partials/_GetOptions", models);
		}
		public async Task DeleteOption(int id)
		{
			await new NominationsSelectionOptionsBL().DeleteAsync(id);
		}

		private async Task ConfigureViewBag()
		{
			var readers = await new ReadersBL().GetAsync(new ReadersSearchParams());

			ViewBag.Readers = readers.Objects.Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToList();
		}

		private async Task<int> GetOrderById(int awardId)
		{
			var searchResult1 = await new NominationsBL().GetAsync(new NominationsSearchParams(0,0)
			{
				AwardId = awardId
			});
			var searchResult2 = await new AwardEventsBL().GetAsync(new AwardEventsSearchParams(0, 0)
			{
				AwardId = awardId
			});

			return searchResult1.Total + searchResult2.Total + 1;
		}
	}
}
