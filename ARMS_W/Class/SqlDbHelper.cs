using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace ARMS_W.Class
{
	public class SqlDbHelper
	{
		public const string ArmsDatabase = "ARMS";

		public static void ExecNQuery(string strQuery)
		{
			string _connectionString = "Provider=sqloledb;Server=reportserver;Database=" + ArmsDatabase + ";Uid=sa; Pwd=p@ssw0rd;";
			OleDbConnection _conn = new OleDbConnection(_connectionString);
			OleDbCommand _comm = new OleDbCommand();

			_comm.Connection = _conn;
			_comm.CommandText = strQuery;
			_comm.CommandTimeout = 36000;

			_conn.Open();
			_comm.ExecuteNonQuery();
			_conn.Close();
		}

		public static DataTable getDataDT(string strquery) {
			string _connectionString = "Provider=sqloledb;Server=reportserver;Database=" + ArmsDatabase + ";Uid=sa; Pwd=p@ssw0rd;";
			OleDbConnection _connection = new OleDbConnection(_connectionString);
			OleDbCommand _command = new OleDbCommand(strquery, _connection); _command.CommandTimeout = 3600;
			OleDbDataAdapter _data_adapter = new OleDbDataAdapter(_command);
			System.Data.DataSet _result = new System.Data.DataSet();
			
			_data_adapter.Fill(_result, "tmptable");
			
			// close the connection and empty variables
			_connection.Close();
			_command = null;
			_connection = null;
			_data_adapter = null;
			
			return _result.Tables["tmptable"];
		}

		public static void getDataTable(string strquery, ref DataTable _dt)
		{
			string _connectionString = "Provider=sqloledb;Server=reportserver;Database=" + ArmsDatabase + ";Uid=sa; Pwd=p@ssw0rd;";
			OleDbConnection _connection = new OleDbConnection(_connectionString);
			OleDbCommand _command = new OleDbCommand(strquery, _connection); _command.CommandTimeout = 3600;
			OleDbDataAdapter _data_adapter = new OleDbDataAdapter(_command);
			System.Data.DataSet _result = new System.Data.DataSet();

			_data_adapter.Fill(_result, "tmptable");
			_dt = _result.Tables["tmptable"];
			
			// clean up everything
			_connection.Close();
			_command = null;
			_connection = null;
			_data_adapter = null;
			_result = null;
		}

		public static DataTable getExclDataDt(string strQuery, string filename) 
		{
			string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
			OleDbConnection _connection = new OleDbConnection(_connectionString);
			OleDbCommand _command = new OleDbCommand(strQuery, _connection); _command.CommandTimeout = 3600;
			OleDbDataAdapter _data_adapter = new OleDbDataAdapter(_command);
			System.Data.DataSet _result = new System.Data.DataSet();

			_data_adapter.Fill(_result, "tmptable");

			// close the connection and empty variables
			_connection.Close();
			_command = null;
			_connection = null;
			_data_adapter = null;

			return _result.Tables["tmptable"];
		}

		#region TO_REMOVE

		public static OleDbDataReader getData(string strQuery)
		{
			string _connectionString = "Provider=sqloledb;Server=reportserver;Database=" + ArmsDatabase + ";Uid=sa; Pwd=p@ssw0rd;";
			OleDbConnection _conn = new OleDbConnection(_connectionString);
			OleDbDataReader _reader = null;
			OleDbCommand _comm = new OleDbCommand();

			_comm.Connection = _conn;
			_comm.CommandText = strQuery;
			_comm.CommandTimeout = 36000;

			_conn.Open();
			_reader = _comm.ExecuteReader();

			return _reader;

		}

		public static OleDbDataReader getExlData(string strQuery, string filename)
		{
			string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
			OleDbConnection _conn = new OleDbConnection(_connectionString);
			OleDbDataReader _reader = null;
			OleDbCommand _comm = new OleDbCommand();

			_comm.Connection = _conn;
			_comm.CommandText = strQuery;
			_comm.CommandTimeout = 36000;

			_conn.Open();
			_reader = _comm.ExecuteReader();

			return _reader;
		}

		public static DataRowCollection GetData_dr(string str_table, string str_condition) {
			string _connectionString = "Provider=sqloledb;Server=reportserver;Database=" + ArmsDatabase + ";Uid=sa; Pwd=p@ssw0rd;";
			OleDbConnection _connection = new OleDbConnection(_connectionString);
			string strquery = "";
			if (str_condition != "") 
			{
				str_condition = "where " + str_condition;
			}

			strquery = "select * from " + str_table + " " + str_condition;

			OleDbCommand _command = new OleDbCommand(strquery, _connection); _command.CommandTimeout = 3600;
			OleDbDataAdapter _data_adapter = new OleDbDataAdapter(_command);
			System.Data.DataSet _result = new System.Data.DataSet();

			_data_adapter.Fill(_result, "tmptable");

			return _result.Tables["tmptable"].Rows;
		}

		#endregion

	}
}
