using System;

namespace Boom.Domain
{

    public class BacklogOption : EntityBase
    {
        public Backlog Backlog { get; set; }

        public long BacklogId { get; set; }

	    public string Description { get; set; }
    }
}