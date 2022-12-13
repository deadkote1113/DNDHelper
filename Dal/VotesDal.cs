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
	public class VotesDal : BaseDal<DefaultDbContext, Vote, Entities.Vote, int, VotesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public VotesDal()
		{
		}

		protected internal VotesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Vote entity, Vote dbObject, bool exists)
		{
			dbObject.UserId = entity.UserId;
			dbObject.NominationsSelectionOptionsId = entity.NominationsSelectionOptionsId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Vote>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Vote> dbObjects, VotesSearchParams searchParams)
		{
			if (searchParams.NominationId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.NominationsSelectionOptions.NominationId == searchParams.NominationId);
			}
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Vote>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Vote> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Vote, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Vote, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Vote ConvertDbObjectToEntity(Vote dbObject)
		{
			return dbObject == null ? null : new Entities.Vote(dbObject.Id, dbObject.UserId,
				dbObject.NominationsSelectionOptionsId);
		}
	}
}
