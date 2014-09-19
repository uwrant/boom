using System.Data.Entity;
using Boom.Domain;
using Microsoft.Framework.ConfigurationModel;

namespace Boom
{
    /// <summary>
    /// DbContext for Boom.Domain
    /// </summary>
    public class BoomContext : DbContext
    {
        public BoomContext(Configuration configuration) : base(configuration.Get("Data:DefaultConnection:ConnectionString"))
        {
        }

        public DbSet<Backlog> Backlogs { get; set; }
        public DbSet<BacklogOption> BacklogOptions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<SurveyOptionVote> SurveyOptionVotes { get; set; }
    }
}