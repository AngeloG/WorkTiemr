using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimble.JSON;

namespace WorkTiemr
{
	public static class TimerEntryFactory
	{
		private static Dictionary<string, TimerEntry> m_entries = new Dictionary<string, TimerEntry>();

		public static void Initialize(string fnm = "data.json")
		{
			if (!File.Exists(fnm)) {
				return;
			}
			dynamic obj = Json.JsonDecode(File.ReadAllText(fnm));
			foreach (string key in obj.Keys) {
				TimerEntry entry = new TimerEntry(key);
				entry.ItemName = obj[key]["name"];
				entry.m_tmAllTime = new TimeSpan((long)obj[key]["alltime"]);
				entry.UpdateText();
				m_entries.Add(key, entry);
			}
		}

		public static void Save(string fnm = "data.json")
		{
			if (File.Exists(fnm)) {
				if (File.Exists(fnm + ".bak")) {
					File.Delete(fnm + ".bak");
				}
				File.Move(fnm, fnm + ".bak");
			}
			var ht = new Hashtable();
			foreach (var entry in m_entries) {
				var eht = new Hashtable();
				eht["name"] = entry.Value.ItemName;
				eht["alltime"] = entry.Value.m_tmAllTime.Ticks;
				ht[entry.Key] = eht;
			}
			File.WriteAllText(fnm, Json.JsonEncode(ht));
		}

		public static Dictionary<string, TimerEntry> GetEntries()
		{
			return m_entries;
		}

		public static TimerEntry GetEntry(string id)
		{
			TimerEntry ret = null;
			m_entries.TryGetValue(id, out ret);
			return ret;
		}

		public static TimerEntry MakeEntry(string id, string name)
		{
			TimerEntry ret = new TimerEntry(id);
			ret.ItemName = name;
			m_entries.Add(id, ret);
			return ret;
		}
	}
}
