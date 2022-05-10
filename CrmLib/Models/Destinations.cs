using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CrmLcLib
{
    public class Destinations
    {
        private string _ToDestination = string.Empty;
        private Dictionary<string, string> _ReplacementTemplateData = null;

        public string ToDestination { get { return _ToDestination; } set { _ToDestination = value; } }
        public Dictionary<string, string> ReplacementTemplateData { get { return _ReplacementTemplateData; } set { _ReplacementTemplateData = value; } }
    }
}
