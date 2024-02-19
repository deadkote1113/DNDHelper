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
using Entities;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class PicturesController : Controller
	{
		public async Task<IActionResult> Index(int page = 1)
		{
			const int objectsPerPage = 20;
			var searchResult = await new PicturesBL().GetAsync(new PicturesSearchParams
			{
				StartIndex = (page - 1) * objectsPerPage,
				ObjectsCount = objectsPerPage,
			});
			var viewModel = new SearchResultViewModel<PictureModel>(PictureModel.FromEntitiesList(searchResult.Objects), 
				searchResult.Total, searchResult.RequestedStartIndex, searchResult.RequestedObjectsCount, 5);
			return View(viewModel);
		}

		public async Task<IActionResult> Update(int? id)
		{
			var model = new PictureModel();
			if (id != null)
			{
				model = PictureModel.FromEntity(await new PicturesBL().GetAsync(id.Value));
				if (model == null)
					return NotFound();
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(PictureModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			await new PicturesBL().AddOrUpdateAsync(PictureModel.ToEntity(model));
			TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			var result = await new PicturesBL().DeleteAsync(id);
			if (result)
				TempData[OperationResultType.Success.ToString()] = "Объект удален";
			else
				TempData[OperationResultType.Error.ToString()] = "Объект не найден";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> AjaxGetPathes(InnerPictureFilterModel filterModel)
		{
			var searchParams = InnerPictureFilterModel.ConvertToSearchParams(filterModel);
			var picturesToOters = await new PicturesToOtherBL().GetAsync(searchParams);
			var model = picturesToOters.Objects.Select(item => new PictureViewModel(PictureModel.FromEntity(item.Picture), item.Id));
			var viewName = filterModel.ViewName ?? "Partials/_Items";
			return PartialView(viewName, model);
		}

		public async Task<IActionResult> AjaxGetPictures(int lastIndex, string q)
		{
			var categories = (await new PicturesBL().GetAsync(new PicturesSearchParams
			{
				ObjectsCount = 15,
				StartIndex = lastIndex,
				SearchQuery = q,
			})).Objects.Select(item => new Select2ViewModel(item.Id.ToString(), item.Title));

			return Json(categories);
		}

		public async Task<IActionResult> AjaxAddPictures(int PictureId, InnerPictureFilterModel filterModel)
		{
			var pictureLink = new PicturesToOther()
			{
				PictureId = PictureId,
			};

			switch (filterModel.Type)
			{
				case PictureToOtherType.Award:
					{
						pictureLink.AwardId = filterModel.Id;
						break;
					}
				case PictureToOtherType.Nomination:
					{
						pictureLink.NominationId = filterModel.Id;
						break;
					}
				case PictureToOtherType.NominationSelectionOption:
					{
						pictureLink.NominationsSelectionOptionId = filterModel.Id;
						break;
					}
			}
			var linkId = await new PicturesToOtherBL().AddOrUpdateAsync(pictureLink);
			var picture = await new PicturesBL().GetAsync(PictureId);

			var model = new PictureViewModel(PictureModel.FromEntity(picture), linkId);

			return PartialView("Partials/_Item", model);
		}

		public async Task DeleteLink(int LinkId)
		{
			await new PicturesToOtherBL().DeleteAsync(LinkId);
		}
	}
}
