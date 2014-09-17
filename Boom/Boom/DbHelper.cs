using System;
using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;

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
    }
}