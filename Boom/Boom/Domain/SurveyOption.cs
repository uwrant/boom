using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Boom.Domain
{
    public class SurveyOption : EntityBase
    {
        [JsonIgnore]
        public Survey Survey { get; set; }

        public string Description { get; set; }

        public IList<Vote> Votes { get; set; }
    }
}