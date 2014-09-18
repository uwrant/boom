using Newtonsoft.Json;
using System;

namespace Boom.Domain
{

    public class BacklogOption : EntityBase
    {
        [JsonIgnore]
        public Backlog Backlog { get; set; }

        public long BacklogId { get; set; }

	    public string Description { get; set; }
    }
}