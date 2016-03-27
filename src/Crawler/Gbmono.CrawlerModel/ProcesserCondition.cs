using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gbmono.CrawlerModel
{
    public class ProcesserCondition
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DataType DataType { get; set; }

        public string TypeName { get; set; }

        public string XPathExpression { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ValueType VType { set; get; }

        public bool Multiple { set; get; }
    }


    public enum ValueType
    {
        ImageSrc,
        InnerHtml,
        ImageDsrc,
        Content,
        SplitBrInnerHtml
    }

    public enum DataType
    {
        Image,
        Title,
        Ingredients,
        Custom
    }

}
