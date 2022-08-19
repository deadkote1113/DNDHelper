using System.Text.Json.Serialization;
using Api.Enums;

namespace Api.Authentication
{
	public class ApiUserModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ApiUserRole Role { get; set; }

		[JsonIgnore]
		public bool IsBlocked { get; set; }
	}

	public class ApiUserModel<TEntity> : ApiUserModel where TEntity: class
	{
		[JsonIgnore]
		public TEntity Entity { get; set; }
	}
}
