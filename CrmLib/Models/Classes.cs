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
    public class Result
    {
        private bool _Success = false;
        private bool _Updated = false;
        private bool _Inserted = false;
        private bool _Deleted = false;
        private int _UpdatedCount = 0;
        private int _InsertedCount = 0;
        private int _DeletedCount = 0;
        private int _TotalCount = 0;
        private string _ErrorMsg = string.Empty;

        public Result()
        {

        }

        public bool Success { get { return _Success; } set { _Success = value; } }

        public bool Updated { get { return _Updated; } set { _Updated = value; } }

        public bool Inserted { get { return _Inserted; } set { _Inserted = value; } }

        public bool Deleted { get { return _Deleted; } set { _Deleted = value; } }

        public int UpdatedCount { get { return _UpdatedCount; } set { _UpdatedCount = value; } }

        public int InsertedCount { get { return _InsertedCount; } set { _InsertedCount = value; } }

        public int DeletedCount { get { return _DeletedCount; } set { _DeletedCount = value; } }

        public int TotalCount { get { return _TotalCount; } set { _TotalCount = value; } }

        public string ErrorMsg { get { return _ErrorMsg; } set { _ErrorMsg = value; } }
    }
    public partial class Main
    {
        [JsonProperty("totalSize")]
        public long TotalSize { get; set; }

        [JsonProperty("done")]
        public bool Done { get; set; }

        [JsonProperty("records")]
        public Record[] Records { get; set; }
    }

    public partial class Record
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("Keys")]
        public Dictionary<string, string> Keys { get; set; }

        [JsonProperty("AccountId")]
        public string AccountId { get; set; }

        [JsonProperty("ContactId")]
        public string ContactId { get; set; }

        [JsonProperty("InquiryId")]
        public string InquiryId { get; set; }

        [JsonProperty("InquiryProgramId")]
        public string InquiryProgramId { get; set; }

        [JsonProperty("ProgramId")]
        public string ProgramId { get; set; }

        [JsonProperty("StudentId")]
        public string StudentId { get; set; }

        [JsonProperty("ServicesInterest")]
        public string ServicesInterest { get; set; }

        [JsonProperty("ProgramInterestId1")]
        public string ProgramInterestId1 { get; set; }

        [JsonProperty("ProgramInterestId2")]
        public string ProgramInterestId2 { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("BothEmail")]
        public bool BothEmail { get; set; }

        [JsonProperty("PreferredEmail")]
        public string PreferredEmail { get; set; }

        [JsonProperty("CollegeEmail")]
        public string CollegeEmail { get; set; }

        [JsonProperty("AlternativeEmail")]
        public string AlternativeEmail { get; set; }

        [JsonProperty("NoCommunication")]
        public bool NoCommunication { get; set; }

        [JsonProperty("Event Name")]
        public string EventName { get; set; }

        [JsonProperty("Event Date")]
        public string EventDate { get; set; }

        [JsonProperty("Event Location")]
        public string EventLocation { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public enum TypeEnum { Contact };

    public partial class Main
    {
        public static Main FromJson(string json) => JsonConvert.DeserializeObject<Main>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Main self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Contact")
            {
                return TypeEnum.Contact;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.Contact)
            {
                serializer.Serialize(writer, "Contact");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}

