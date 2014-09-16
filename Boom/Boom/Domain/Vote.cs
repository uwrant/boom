using System;
using System.Collections.Generic;

namespace Boom.Domain
{
    public class Vote : EntityBase
    {
        public IList<SurveyOption> Options { get; set; }

        public Participant Participant { get; set; }
    }
}