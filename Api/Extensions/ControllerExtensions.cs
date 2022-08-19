using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
	public static class ControllerExtensions
	{
		public static TEntity GetApiUser<TEntity>(this ControllerBase controller) where TEntity: class
		{
			return ((controller?.User?.Identity as ApiUserIdentity)?.UserData as ApiUserModel<TEntity>)?.Entity;
		}
	}
}
