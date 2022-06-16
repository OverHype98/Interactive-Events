using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BoredApi
{
	public class BoredApiDto
	{
		public string Activity { get; set; }
		public string Type { get; set; }
		public int Participants { get; set; }
		public float Price { get; set; }
		public string Link { get; set; }
		public string Key { get; set; }
		public float Accessibility { get; set; }
	}
}
