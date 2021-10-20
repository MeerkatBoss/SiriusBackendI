using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiriusBackendI.Models;
using SiriusBackendI.Services;
using System.Threading.Tasks;

namespace SiriusBackendI.Controllers
{
	[Route("")]
	[ApiController]
	public class ShortenerController : ControllerBase
	{
		private readonly ShortenerService _service;
		public ShortenerController(ShortenerService shortenerService)
		{
			_service = shortenerService;
		}

		[HttpPost("shorten")]
		public async Task<IActionResult> Create(ShortenRequest request)
		{
			var response = await _service.Create(request.UrlToShorten);
			response = "https://" + HttpContext.Request.Host + "/" + response;
			return CreatedAtAction(
				nameof(Create),
				new { url = response },
				new { status = "Created", shortenedUrl = response });
		}

		[HttpGet("{url}")]
		public async Task<IActionResult> GetFullUrl(string url)
		{
			var result = await _service.GetFullUrl(url);
			if (result == null)
				return NotFound();
			return RedirectPermanent(result);
		}

		[HttpGet("{url}/views")]
		public async Task<IActionResult> GetUrlViews(string url)
		{
			var result = await _service.GetUrlViewsCount(url);
			if (result == null)
				return NotFound();
			return Ok(new { viewCount = result });
		}
	}
}
