using System.Threading.Tasks;
using Dal;
using Common.Search;
using Picture = Entities.Picture;

namespace BL
{
	public class PicturesBL
	{
		public async Task<int> AddOrUpdateAsync(Picture entity)
		{
			entity.Id = await new PicturesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new PicturesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(PicturesSearchParams searchParams)
		{
			return new PicturesDal().ExistsAsync(searchParams);
		}

		public Task<Picture> GetAsync(int id)
		{
			return new PicturesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new PicturesDal().DeleteAsync(id);
		}

		public Task<SearchResult<Picture>> GetAsync(PicturesSearchParams searchParams)
		{
			return new PicturesDal().GetAsync(searchParams);
		}
	}
}

