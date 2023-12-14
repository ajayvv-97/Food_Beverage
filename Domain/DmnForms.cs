using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class DmnForms:DmnGeneralOptions
	{
		public int id { get; set; }
		public int  Form { get; set; }
		public int Write { get; set; }
		public int Read { get; set; }
		public int Status { get; set; }
		public int UserType { get; set; }
		public string Note { get; set; }
        public int CreatedBy { get; set; }
		public int Active { get; set; }
		public DateTime CreatedIn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int FormType { get; set; }
	}
}
