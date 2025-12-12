using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWebUI.Dtos.SliderDtos
{
	public class UpdateSliderDto
	{
		public int SliderId { get; set; }
		public String Title1 { get; set; }
		public String Title2 { get; set; }
		public String Title3 { get; set; }
		public String Description1 { get; set; }
		public String Description2 { get; set; }
		public String Description3 { get; set; }
	}
}
