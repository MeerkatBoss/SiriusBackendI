using Microsoft.EntityFrameworkCore;
using SiriusBackendI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusBackendI.Services
{
	public class ShortenerService
	{
		private readonly ShortenedUrlContext _database;

		public ShortenerService(ShortenedUrlContext context)
		{
			_database = context;
		}

		public async Task<string> Create(string originalUrl)
		{
			var shortened = GenerateStringId();
			_database.Add(
				new ShortenedUrl
				{
					Original = originalUrl,
					Shortened = shortened,
					Views = 0
				});
			await _database.SaveChangesAsync();
			return shortened;
		}

		public async Task<string> GetFullUrl(string shortened)
		{
			var result = await _database
				.ShortenedUrls
				.FirstOrDefaultAsync(x => x.Shortened == shortened);
			if (result == null)
				return null;
			result.Views++;
			_database.Update(result);
			await _database.SaveChangesAsync();
			return result.Original;
		}

		public async Task<int?> GetUrlViewsCount(string url)
		{
			var result = await _database.ShortenedUrls
				.FirstOrDefaultAsync(x => x.Shortened == url);
			return result?.Views;
		}

		private static string GenerateStringId()
		{
			var guid = Guid.NewGuid();
			var id = new StringBuilder(Convert.ToBase64String(guid.ToByteArray()));
			id.Replace("+", "A");
			id.Replace("=", "B");
			id.Replace("-", "C");
			id.Replace("/", "D");
			id.Remove(0, id.Length - 8);
			return id.ToString();
		}
	}
}
