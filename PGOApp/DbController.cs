using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PGOApp
{
    public class DbController
    {
        public List<DbModel> SearchAccount(string userName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlConnecHelper.ConnecVal("PokemonGoDatabase")))
            {
                var output = connection.Query<DbModel>($"SELECT * FROM PokemonGO.dbo.PGoTable where USERNAME = '{userName}'").ToList();
                return output;
            }
        }

        public string CompareUsernames(string userName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlConnecHelper.ConnecVal("PokemonGoDatabase")))
            {
                var output = connection.Query<DbModel>($"SELECT USERNAME FROM PokemonGO.dbo.PGoTable where USERNAME = '{userName}'").ToList();
                if (output.Count > 0)
                {
                    string str = output[0].Username;
                    return str;
                }
                else
                    return "";
            }
        }

        public List<DbModel> FindAllAccounts()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlConnecHelper.ConnecVal("PokemonGoDatabase")))
            {
                var output = connection.Query<DbModel>($"SELECT * FROM PokemonGO.dbo.PGoTable").ToList();
                return output;
            }
        }

        public void AddAccount(string username, string pass, string email, string emailpass, string details, int sold, int listed)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlConnecHelper.ConnecVal("PokemonGoDatabase")))
            {
                connection.Query<DbModel>($"INSERT INTO PokemonGO.dbo.PGoTable VALUES ('{username}','{pass}','{email}','{emailpass}','{details}',{sold},{listed});").ToList();
            }

        }

        public void UpdateAccount(string username, string pass, string email, string emailpass, string details, int sold, int listed)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlConnecHelper.ConnecVal("PokemonGoDatabase")))
            {
                connection.Query<DbModel>($"UPDATE PokemonGO.dbo.PGoTable SET PASSWORD = '{pass}', EMAIL = '{email}', EMAIL_PASS = '{emailpass}', DETAILS = '{details}', SOLD = {sold}, LISTED = {listed} WHERE USERNAME = '{username}';");
            }

        }

        public void DeleteAccount(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlConnecHelper.ConnecVal("PokemonGoDatabase")))
            {
                connection.Query<DbModel>($"DELETE FROM PokemonGO.dbo.PGoTable WHERE USERNAME = '{username}'");
            }
        }
    }
}
