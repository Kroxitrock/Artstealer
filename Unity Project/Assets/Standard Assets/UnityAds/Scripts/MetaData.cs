using System.Collections.Generic;

namespace UnityEngine.Advertisements
{
	public sealed class MetaData
	{
		readonly IDictionary<string, object> m_MetaData = new Dictionary<string, object>();
		
		public string category { get; private set; }
		
		public MetaData(string category)
		{
			this.category = category;
		}
		
		public void Set(string key, object value)
		{
			m_MetaData[key] = value;
		}
		
		public object Get(string key)
		{
			return m_MetaData[key];
		}
		
		public IDictionary<string, object> Values()
		{
			return m_MetaData;
		}
		
		internal string ToJSON()
		{
			return MiniJSON.Json.Serialize(m_MetaData);
		}
	}
}
