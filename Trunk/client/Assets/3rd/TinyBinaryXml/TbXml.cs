using System.Collections.Generic;

namespace TinyBinaryXml
{
	public class TbXml
	{
        public List<TbXmlNodeTemplate> nodeTemplates = null;

        public List<TbXmlNode> nodes = null;

        public List<string> stringPool = null;

        public List<double> valuePool = null;

		public TbXmlNode docNode = null;

		public static TbXml Load(byte[] xmlBytes)
		{
			TbXmlDeserializer deserializer = new TbXmlDeserializer();
			return deserializer.DeserializeXmlBytes(xmlBytes);
		}
	}
}

