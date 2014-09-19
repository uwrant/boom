using System;
using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;
using System.Data.SqlClient;
using Boom.Domain;

namespace Boom
{
    public class DbHelper
    {
        public static void EnsureDbCreated(IBuilder app)
        {
            using (var db = app.ApplicationServices.GetService<BoomContext>())
            {
                var sqlServerDataStore = db.Configuration.DataStore as SqlServerDataStore;
                if (sqlServerDataStore != null)
                {
                    db.Database.EnsureCreated();
                }
            }
        }

        public static void InitDatabase(IBuilder app)
        {
            using (var db = app.ApplicationServices.GetService<BoomContext>())
            {
                var TeamPresentationSurvey = new Survey()
                {
                    Name = "TeamPressentation",
                    CreationDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1)
                };

                db.Add(TeamPresentationSurvey);

                db.SaveChanges();
            }
        }

        public static void DropDatabase(string databaseName)
        {
            try
            {
                Console.WriteLine("Trying to drop database '{0}'", databaseName);
                using (var conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;"))
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(@"IF EXISTS (SELECT * FROM sys.databases WHERE name = N'{0}')
                                          BEGIN
                                               ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                               DROP DATABASE [{0}];
                                          END", databaseName);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Successfully dropped database {0}", databaseName);
                }
            }
            catch (Exception exception)
            {
                //Ignore if there is failure in cleanup.
                Console.WriteLine("Error occured while dropping database {0}. Exception : {1}", databaseName, exception);
            }
        }
    }
}