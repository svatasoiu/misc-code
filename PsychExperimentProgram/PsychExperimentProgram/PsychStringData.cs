using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MassMed.Data;
using System.Data;

namespace PsychExperimentProgram
{
    class PsychStringData
    {
        //connect to local database
        SqlConnection ChessRankings = new SqlConnection("Data Source=HP-DV6T;Initial Catalog=Chess_Rankings;Integrated Security=True");

        //add a result to the database
        public void AddEntry(string standardString, string inputString, int correct, string experimentalGroup)
        {
            SqlCommand cmd = ChessRankings.CreateCommand();
            cmd.CommandText = "EXEC AddPsychString @standardString, @inputString, @correct, @experimentalGroup"; //SQL statement
            cmd.Parameters.Add("@standardString", System.Data.SqlDbType.VarChar, 50).Value = standardString;
            cmd.Parameters.Add("@inputString", System.Data.SqlDbType.VarChar, 50).Value = inputString;
            cmd.Parameters.Add("@correct", System.Data.SqlDbType.Int).Value = correct;
            cmd.Parameters.Add("@experimentalGroup", System.Data.SqlDbType.VarChar, 50).Value = experimentalGroup;

            ChessRankings.Open();
            cmd.ExecuteNonQuery();
            ChessRankings.Close();
        }
        //return a datatable that can be displayed in the grid
        public DataTable displayTable()
        {
            return MassMed.Data.DataAccess.ExecuteDatatable("Data Source=HP-DV6T;Initial Catalog=Chess_Rankings;Integrated Security=True",
                    CommandType.StoredProcedure,
                    "dbo.GetAllPsych",
                    null);
        }
    }
}
