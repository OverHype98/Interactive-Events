using Application.BoredApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[AllowAnonymous]
	public class BoredApiController : BaseApiController
	{

		public async Task<IActionResult> GetBored()
		{
			return HandleResult(await Mediator.Send(new Get.Query()));
		}
	}
}
