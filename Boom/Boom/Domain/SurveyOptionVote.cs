namespace Boom.Domain
{
    public class SurveyOptionVote : EntityBase
    {
        public SurveyOption Option { get; set; }

        public int Weight { get; set; }
    }
}