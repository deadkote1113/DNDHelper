using Api.Authentication;
using Api.Enums;
using Api.Extensions;
using Api.Requests;
using Api.Responses;
using BL;
using Common;
using Common.Enums;
using Common.Search;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Clients
{
    [ApiController]
    [Route("v{version:apiVersion}/Game/")]
	[AllowAnonymous]
	public class GameController : ControllerBase
    {
		private readonly ILogger<GameController> logger;

		public GameController(ILogger<GameController> logger)
		{
			this.logger = logger;
		}

		[HttpPost]
		[Route("[action]")]
		[AllowAnonymous]
		public async Task<ResponseWrapper<MakeNextTurnRespons>> MakeNextTurn(RequestWrapper<MakeNextTurnRequest> request) {
			var data = request.RequestData;
			if (!ModelState.IsValid)
			{
				return new ResponseWrapper<MakeNextTurnRespons>(OperationStatus.InvalidRequest);
			}
            try
            {
				
				return new ResponseWrapper<MakeNextTurnRespons>(OperationStatus.Success)
				{
					ResponseData = new MakeNextTurnRespons()
					{
						Fields = data.Fields
					}
				};
            }
            catch (Exception e)
            {
				logger.LogError(e.Message);
				return new ResponseWrapper<MakeNextTurnRespons>(OperationStatus.Failed);
			}
			
		}

	}
}
