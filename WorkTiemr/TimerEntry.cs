using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nimble.JSON;

namespace WorkTiemr
{
	public class TimerEntry : ListViewItem
	{
		public string m_id;

		private string m_name;
		public string ItemName
		{
			get { return m_name; }
			set
			{
				m_name = value;
				Text = value;
			}
		}

		public TimeSpan m_tmToday;
		public TimeSpan m_tmAllTime;

		private ListViewSubItem m_subToday;
		private ListViewSubItem m_subAllTime;

		public TimerEntry(string id)
		{
			m_id = id;

			m_subToday = SubItems.Add("-");
			m_subAllTime = SubItems.Add("-");
		}

		private string HumanTime(TimeSpan tm)
		{
			string ret = "";
			if (tm.TotalDays >= 1) { ret += tm.Days + "d "; }
			if (tm.TotalHours >= 1) { ret += tm.Hours + "h "; }
			if (tm.TotalMinutes >= 1) { ret += tm.Minutes + "m "; }
			ret += tm.Seconds + "s";
			return ret;
		}

		public void UpdateText()
		{
			m_subToday.Text = HumanTime(m_tmToday);
			m_subAllTime.Text = HumanTime(m_tmAllTime);
		}
	}
}
