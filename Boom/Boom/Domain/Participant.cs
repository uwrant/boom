using Newtonsoft.Json;
using System;

namespace Boom.Domain
{
    public class Participant : EntityBase
    {
        [JsonIgnore]
        public Survey Survey { get; set; }

        public string Name { get; set; }
    }
}