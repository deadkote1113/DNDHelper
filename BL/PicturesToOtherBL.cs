using System.Threading.Tasks;
using Dal;
using Common.Search;
using PicturesToOther = Entities.PicturesToOther;

namespace BL
{
	public class PicturesToOtherBL
	{
		public async Task<int> AddOrUpdateAsync(PicturesToOther entity)
		{
			entity.Id = await new PicturesToOtherDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new PicturesToOtherDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(PicturesToOtherSearchParams searchParams)
		{
			return new PicturesToOtherDal().ExistsAsync(searchParams);
		}

		public Task<PicturesToOther> GetAsync(int id)
		{
			return new PicturesToOtherDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new PicturesToOtherDal().DeleteAsync(id);
		}

		public Task<SearchResult<PicturesToOther>> GetAsync(PicturesToOtherSearchParams searchParams)
		{
			return new PicturesToOtherDal().GetAsync(searchParams);
		}
	}
}

