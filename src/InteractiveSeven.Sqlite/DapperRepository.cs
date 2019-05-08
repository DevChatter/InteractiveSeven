using Dapper;
using InteractiveSeven.Core.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Sqlite
{
    public class DapperRepository : IRepository
    {
        public const string CONNECTION_STRING = "Data Source=.\\Database.sqlite;";

        public List<Setting> GetAllSettings()
        {
            using (var connection = new SqliteConnection(CONNECTION_STRING))
            {
                const string sql = "SELECT * FROM [Settings]";
                return connection.Query<Setting>(sql).ToList();
            }
        }
    }
}
