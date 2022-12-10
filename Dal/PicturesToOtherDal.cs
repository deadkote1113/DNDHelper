using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Enums;
using Common.Search;
using Dal.DbModels;

namespace Dal
{
	public class PicturesToOtherDal : BaseDal<DefaultDbContext, PicturesToOther, Entities.PicturesToOther, int, PicturesToOtherSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public PicturesToOtherDal()
		{
		}

		protected internal PicturesToOtherDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.PicturesToOther entity, PicturesToOther dbObject, bool exists)
		{
			dbObject.PictureId = entity.PictureId;
			dbObject.ItemId = entity.ItemId;
			dbObject.CreatureId = entity.CreatureId;
			dbObject.StructureId = entity.StructureId;
			dbObject.AwardId = entity.AwardId;
			dbObject.NominationId = entity.NominationId;
			dbObject.NominationsSelectionOptionId = entity.NominationsSelectionOptionId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<PicturesToOther>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<PicturesToOther> dbObjects, PicturesToOtherSearchParams searchParams)
		{
			if(searchParams.ItemId != null)
			{
				dbObjects = dbObjects.Where(item => item.ItemId == searchParams.ItemId);
			}
			if (searchParams.StructureId != null)
			{
				dbObjects = dbObjects.Where(item => item.StructureId == searchParams.StructureId);
			}
			if (searchParams.CreatureId != null)
			{
				dbObjects = dbObjects.Where(item => item.CreatureId == searchParams.CreatureId);
			}
			if (searchParams.AwardId != null)
			{
				dbObjects = dbObjects.Where(item => item.AwardId == searchParams.AwardId);
			}
			if (searchParams.NominationId != null)
			{
				dbObjects = dbObjects.Where(item => item.NominationId == searchParams.NominationId);
			}
			if (searchParams.NominationsSelectionOptionId != null)
			{
				dbObjects = dbObjects.Where(item => item.NominationsSelectionOptionId == searchParams.NominationsSelectionOptionId);
			}
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.PicturesToOther>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<PicturesToOther> dbObjects, object convertParams, bool isFull)
		{
			dbObjects = dbObjects.Include(p => p.Picture);
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<PicturesToOther, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.PicturesToOther, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.PicturesToOther ConvertDbObjectToEntity(PicturesToOther dbObject)
		{
			return dbObject == null ? null : new Entities.PicturesToOther(dbObject.Id, dbObject.PictureId,
				dbObject.ItemId, dbObject.CreatureId, dbObject.StructureId, dbObject.AwardId, dbObject.NominationId,
				dbObject.NominationsSelectionOptionId)
			{
				Picture = PicturesDal.ConvertDbObjectToEntity(dbObject.Picture),
			};
		}
	}
}
