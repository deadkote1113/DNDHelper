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
	public class NominationsSelectionOptionsDal : BaseDal<DefaultDbContext, NominationsSelectionOption, Entities.NominationsSelectionOption, int, NominationsSelectionOptionsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public NominationsSelectionOptionsDal()
		{
		}

		protected internal NominationsSelectionOptionsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.NominationsSelectionOption entity, NominationsSelectionOption dbObject, bool exists)
		{
			dbObject.UserId = entity.UserId;
			dbObject.Title = entity.Title;
			dbObject.Description = entity.Description;
			dbObject.NominationId = entity.NominationId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<NominationsSelectionOption>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<NominationsSelectionOption> dbObjects, NominationsSelectionOptionsSearchParams searchParams)
		{
			if (searchParams.NominationId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.NominationId == searchParams.NominationId);
			}
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.NominationsSelectionOption>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<NominationsSelectionOption> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<NominationsSelectionOption, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.NominationsSelectionOption, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.NominationsSelectionOption ConvertDbObjectToEntity(NominationsSelectionOption dbObject)
		{
			return dbObject == null ? null : new Entities.NominationsSelectionOption(dbObject.Id, dbObject.UserId,
				dbObject.Title, dbObject.Description, dbObject.NominationId);
		}
	}
}
