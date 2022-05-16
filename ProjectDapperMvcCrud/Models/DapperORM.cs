using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace DapperMVCCRUD.Models
{
    public static class DapperORM
    {
        private static string connectionstring = @"Data Source=DESKTOP-15RODQR;Initial Catalog=DapperDB;Integrated Security=True;";

        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        //DapperORM.ExcuteReturnScalar<int>(_,_);
        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType:
                    CommandType.StoredProcedure), typeof(T));
            }
        }
        //DapperORM.ReturnList<EmpolyeeLModel> <=  IEnumerable<EmployeeModel>
        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}