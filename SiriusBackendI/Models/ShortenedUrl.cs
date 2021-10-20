using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiriusBackendI.Models
{
	public class ShortenedUrl
	{
		public string Original { get; set; }

		[Key]
		public string Shortened { get; set; }

		public int Views { get; set; }
	}
}
