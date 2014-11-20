using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	class JsonConverter<ConvertType>
	{
		public string Object2Json(ConvertType obj)
		{
			//DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ConvertType));
			//MemoryStream mem = new MemoryStream();
			//ser.WriteObject(mem, obj);
			//mem.Position = 0;

			//StreamReader reader = new StreamReader(mem);

			//return reader.ReadToEnd();
			return JsonConvert.SerializeObject(obj);
		}

		public ConvertType Json2Object(string json)
		{
			//DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ConvertType));
			//MemoryStream mem = new MemoryStream();

			//StreamWriter writer = new StreamWriter(mem);
			//writer.Write(json);
			//writer.Flush();

			//mem.Position = 0;
			//return (ConvertType)ser.ReadObject(mem);

			return (ConvertType)JsonConvert.DeserializeObject<ConvertType>(json);
		}
	}
}
