using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ARMS_W.Class
{
    public class ExcelReader
    {
        OleDbConnection _conn;
        OleDbDataReader _reader;
        OleDbCommand _comm;

        public OleDbDataReader getExlData(string strQuery, string filename)
        {
            string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
            _conn = new OleDbConnection(_connectionString);
            _reader = null;
            OleDbCommand _comm = new OleDbCommand();

            _comm.Connection = _conn;
            _comm.CommandText = strQuery;
            _comm.CommandTimeout = 36000;

            _conn.Open();
            _reader = _comm.ExecuteReader();

            return _reader;
        }


        public static DataTable getExclData12(string strQuery, string filename)
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

        public void Close()
        {
            _conn.Close();
        }

    }
}