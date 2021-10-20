using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiriusBackendI.Models
{
	public class ShortenedUrlContext : DbContext
	{
		public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
		public ShortenedUrlContext(DbContextOptions<ShortenedUrlContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
