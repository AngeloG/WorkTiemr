using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkTiemr
{
	public partial class FormMain : Form
	{
		private DateTime m_tmLastTick;

		public FormMain()
		{
			InitializeComponent();

			TimerEntryFactory.Initialize();

			var entries = TimerEntryFactory.GetEntries();
			foreach (var entry in entries) {
				list.Items.Add(entry.Value);
			}

			m_tmLastTick = DateTime.Now;
		}

		private void list_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) {
				return;
			}

			ContextMenuStrip cms = new ContextMenuStrip();

			var itemAdd = cms.Items.Add("Add item");
			itemAdd.Click += (o, ee) => {
			};

			var itemDelete = cms.Items.Add("Delete");
			if (list.SelectedItems.Count > 0) {
				itemDelete.Enabled = true;
				itemDelete.Click += (o, ee) => {
				};
			} else {
				itemDelete.Enabled = false;
			}

			cms.Show(Cursor.Position);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			var tmDelta = DateTime.Now - m_tmLastTick;
			m_tmLastTick = DateTime.Now;

			var entries = TimerEntryFactory.GetEntries();
			foreach (var entry in entries.Values) {
				if (!entry.Checked) {
					continue;
				}
				entry.m_tmToday += tmDelta;
				entry.m_tmAllTime += tmDelta;
				entry.UpdateText();
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			TimerEntryFactory.Save();
		}
	}
}
