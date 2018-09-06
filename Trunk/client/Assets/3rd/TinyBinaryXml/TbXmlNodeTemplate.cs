using System.Collections.Generic;

namespace TinyBinaryXml
{
	public class TbXmlNodeTemplate
	{
        public ushort id = 0;

        public string name = string.Empty;

        public List<string> attributeNames = null;

        public Dictionary<string, int> attributeNameIndexMapping = null;

        public List<TB_XML_ATTRIBUTE_TYPE> attributeTypes = null;
	}
}

