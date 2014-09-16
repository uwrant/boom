using System;
using System.Collections.Generic;

namespace Boom.Domain
{
    public class Survey : EntityBase
    {
        public DateTime CreationDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<SurveyOption> Options { get; set; }
    }
}
