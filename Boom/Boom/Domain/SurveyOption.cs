using Newtonsoft.Json;
using System;

namespace Boom.Domain
{
    public class SurveyOption : EntityBase
    {
        [JsonIgnore]
        public Survey Survey { get; set; }

        public long SurveyId { get; set; }

        public string Description { get; set; }
    }
}