using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace _0605_01
{
    class SQLHelper
    {
        public SqlConnection GetConnection()
        {

            SqlConnection strConnection = new SqlConnection();

            try
            {

                string strConn = "Data Source=.;Initial Catalog=Final_Exam;uid=sa;pwd=1;";

                strConnection.ConnectionString = strConn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //连接对象非空且数据库处于打开状态，用finally语句可以保证一定关闭数据库连接
                if (strConnection != null && strConnection.State == ConnectionState.Open)
                    strConnection.Close();
            }

            return strConnection;
        }

        public DataSet GetDataSet(string strQuery)
        {
            try
            {

                SqlConnection sqlConn = GetConnection();

                DataSet ds = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter(strQuery, sqlConn);

                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable getDataTable(string strQuery)
        {
            try
            {
                DataSet ds = GetDataSet(strQuery);
                DataTable dt = ds.Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int operateTable(string strOperate)
        {
            int intResult = 0;

            SqlConnection conn = GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strOperate;

                conn.Open();

                intResult = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return intResult;
        }
    }
}

