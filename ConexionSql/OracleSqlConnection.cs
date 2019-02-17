namespace ConexionSql
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OracleSqlConnection
    {
        OracleConnection con;
        OracleTransaction tra;
        OracleCommand comma;
        OracleDataAdapter ad;

        public void Conectar()
        {
            con = new OracleConnection("ConnectionString");
            con.Open();
            comma = con.CreateCommand();
            tra = con.BeginTransaction(System.Data.IsolationLevel.Serializable);
            comma.Connection = con;
            comma.Transaction = tra;
        }

        public void Desconectar()
        {
            tra.Commit();
            con.Close();
        }

        public System.Data.DataSet seleccionar(string sql)
        {
            DataSet ds = new DataSet();
            ad = new OracleDataAdapter(sql, con);
            ad.SelectCommand.Transaction = tra;
            ad.Fill(ds);
            return ds;
        }

        public void insomod(string query)
        {
            comma.CommandText = query;
            comma.ExecuteNonQuery();
        }
        public void InsImagen(string strSQLInsertar, Byte[] MapaContent)
        {
            comma.CommandText = strSQLInsertar;
            comma.CommandType = CommandType.Text;

            OracleParameter paramImagen;
            paramImagen = new OracleParameter("@FOTO", SqlDbType.Image);

            paramImagen.Value = MapaContent;
            paramImagen.Direction = ParameterDirection.Input;

            comma.Parameters.Add(paramImagen);
            comma.ExecuteNonQuery();
        }
    }
}
