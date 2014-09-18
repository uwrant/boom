using System;
using System.Collections.Generic;

namespace Boom.Domain
{
    public class Survey : EntityBase
    {
        public Survey()
        {
            this.Options = new List<SurveyOption>();
            this.Participants = new List<Participant>();
        }

        public string Name { get;set; } 

        public DateTime CreationDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IList<SurveyOption> Options { get; set; }

        public IList<Participant> Participants { get; set; }
    }
}
