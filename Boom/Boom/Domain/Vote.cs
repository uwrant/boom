using System;
using System.Collections.Generic;

namespace Boom.Domain
{
    public class Vote : EntityBase
    {
        public IList<SurveyOptionVote> Options { get; set; }

        public Participant Participant { get; set; }

        public long ParticipantId { get; set; }
    }
}