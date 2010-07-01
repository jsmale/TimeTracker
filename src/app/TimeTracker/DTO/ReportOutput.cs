using System;
using System.Collections.Generic;

namespace TimeTracker.DTO
{
	public class ReportOutput
	{
		public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
		public IEnumerable<ReportDetail> ReportDetails { get; set; }
	}

	public class ReportDetail
	{
		public string Project { get; set; }

		public string Task { get; set; }

		public string Type { get; set; }
		public double[] Hours { get; set; }
	}
}