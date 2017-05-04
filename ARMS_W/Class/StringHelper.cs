using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace ARMS_W.Class
{
    
	public class StringHelper
	{
        // 8 COLUMN ONLY
		public static string ConvertReaderToString (OleDbDataReader _greader) {
            StringBuilder str_res = new StringBuilder(1000);
			int col_count = _greader.VisibleFieldCount;
            
            while (_greader.Read()) {

                switch (col_count) {
                    case 1: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            break;
                    case 2: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            break;
                    case 3: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(2).ToString()); str_res.Append("|");
                            break;
                    case 4: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(2).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(3).ToString()); str_res.Append("|");
                            break;
                    case 5: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(2).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(3).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(4).ToString()); str_res.Append("|");
                            break;
                    case 6: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(2).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(3).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(4).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(5).ToString()); str_res.Append("|");
                            break;
                    case 7: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(2).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(3).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(4).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(5).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(6).ToString()); str_res.Append("|");
                            break;
                    case 8: str_res.Append(_greader.GetValue(0).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(1).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(2).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(3).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(4).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(5).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(6).ToString()); str_res.Append("|");
                            str_res.Append(_greader.GetValue(7).ToString()); str_res.Append("|");
                            break;
                }
                
                str_res.Append("#$");
            }
            
			return str_res.ToString();
		}

        public static string ConvertDataTableToString(DataTable _gtable) 
        { 
            StringBuilder str_res = new StringBuilder(1000);
            int col_count = _gtable.Columns.Count;

            foreach (DataRow _data in _gtable.Rows) 
            {
                switch (col_count)
                {
                    case 1: str_res.Append( _data[0].ToString() ); str_res.Append("|");
                        break;
                    case 2: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        break;
                    case 3: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        str_res.Append(_data[2].ToString()); str_res.Append("|");
                        break;
                    case 4: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        str_res.Append(_data[2].ToString()); str_res.Append("|");
                        str_res.Append(_data[3].ToString()); str_res.Append("|");
                        break;
                    case 5: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        str_res.Append(_data[2].ToString()); str_res.Append("|");
                        str_res.Append(_data[3].ToString()); str_res.Append("|");
                        str_res.Append(_data[4].ToString()); str_res.Append("|");
                        break;
                    case 6: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        str_res.Append(_data[2].ToString()); str_res.Append("|");
                        str_res.Append(_data[3].ToString()); str_res.Append("|");
                        str_res.Append(_data[4].ToString()); str_res.Append("|");
                        str_res.Append(_data[5].ToString()); str_res.Append("|");
                        break;
                    case 7: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        str_res.Append(_data[2].ToString()); str_res.Append("|");
                        str_res.Append(_data[3].ToString()); str_res.Append("|");
                        str_res.Append(_data[4].ToString()); str_res.Append("|");
                        str_res.Append(_data[5].ToString()); str_res.Append("|");
                        str_res.Append(_data[6].ToString()); str_res.Append("|");
                        break;
                    case 8: str_res.Append(_data[0].ToString()); str_res.Append("|");
                        str_res.Append(_data[1].ToString()); str_res.Append("|");
                        str_res.Append(_data[2].ToString()); str_res.Append("|");
                        str_res.Append(_data[3].ToString()); str_res.Append("|");
                        str_res.Append(_data[4].ToString()); str_res.Append("|");
                        str_res.Append(_data[5].ToString()); str_res.Append("|");
                        str_res.Append(_data[6].ToString()); str_res.Append("|");
                        str_res.Append(_data[7].ToString()); str_res.Append("|");
                        break;
                }

                str_res.Append("#$");
            }

            return str_res.ToString();

        }

		/* row splitter */
		public static string[] GetRows (string strToConvert) {
			return strToConvert.Split('$');
		}

		/* column splitter */
		public static string[] GetColumns (string strToConvert) {
			return strToConvert.Split('|');
		}

        /*  */
        public static string InsertSlashes(string strToConvert) {
            int i = 0;
            StringBuilder tmp_strb = new StringBuilder(1000);

            // string tmp_str = "";
            for (i = 0; i < strToConvert.Length; i++) {
                if (strToConvert.Substring(i, 1).ToString() == "\\")
                {
                    // tmp_str = tmp_str + "\\\\";
                    tmp_strb.Append("\\\\");
                }
                else if (strToConvert.Substring(i, 1).ToString() == "\'")
                {
                    // tmp_str = tmp_str + "\\\'";
                    tmp_strb.Append("\\\'");
                }
                else if (strToConvert.Substring(i, 1).ToString() == "\"")
                {
                    // tmp_str = tmp_str + "\\\'";
                    tmp_strb.Append("\\\"");
                }
                else 
                {
                    // tmp_str = tmp_str + strToConvert.Substring(i, 1).ToString();
                    tmp_strb.Append(strToConvert.Substring(i, 1).ToString());
                }
                Application.DoEvents();
            }

            // return tmp_str;
            return tmp_strb.ToString();
        }

        public static string InsertQoutes(string strval) {
            string tmp_str = "";

            for (int i = 0; i < strval.Length; i++) {
                if (strval[i] == '\'')
                {
                    tmp_str = tmp_str + '\'' + strval[i];
                }
                else 
                {
                    tmp_str = tmp_str + strval[i];
                }
                Application.DoEvents();
            }

            return tmp_str;
        }

        public static string GetFileName(string strToConvert) {
            return System.IO.Path.GetFileName(strToConvert);
        }

        public static string UrlDecode(string strToConvert) {
            string tmp_str = "";
            tmp_str = strToConvert.Replace("%26", "&");

            return tmp_str;
        }

        public static string GetRegion(string strRegion) {
            if (strRegion.ToUpper().IndexOf("LUZON") > -1) { return "LUZON"; }
            else { return "VISMIN"; }
        }

        public static string ReCodeCharacters(string str_data) 
        {
            string tmp_str = "";

            // AMPERSAND
            tmp_str = str_data.Replace("$AG$", "&");

            // SINGLE QOUTES
            tmp_str = tmp_str.Replace("'", "''");



            return tmp_str;
        }

        public static string DtToJson(DataTable data_table)
        {

            StringBuilder tmp_res = new StringBuilder(1000);

            foreach (DataRow itm in data_table.Rows) 
            {
                if (tmp_res.Length > 0) tmp_res.Append(" ,");

                // starting row
                tmp_res.Append("{");

                // get column name
                foreach (DataColumn dc in data_table.Columns) 
                {
                    // column name
                    tmp_res.Append("\""); tmp_res.Append(dc.ColumnName); tmp_res.Append("\"");
                    // separator
                    tmp_res.Append(":");
                    // column value
                    tmp_res.Append("\""); tmp_res.Append(itm[dc.ColumnName]); tmp_res.Append("\"");
                }

                // ending row
                tmp_res.Append("}");
            }

            return tmp_res.ToString();

        }

        public static string CTJ(string val) 
        {
            StringBuilder s_b = new StringBuilder(1000);

            string tmp_val = val.Replace("\r", "");
            tmp_val = tmp_val.Replace("\n", "\\n");
            tmp_val = tmp_val.Replace("\"", "\\\"");
            tmp_val = tmp_val.Replace("\'", "\\'");

            return tmp_val;
        }
        //code added by Billy Jay Delima
        public static string LogChangesToStr(List<SkelClass.Globals.db_changes> data)
        {
            StringBuilder d_data = new StringBuilder(1000);

            int i = 0;
            foreach (var itm in data)
            {
                if (i > 0) d_data.Append("^");
                d_data.Append(itm.FieldName); d_data.Append("|");
                d_data.Append(itm.PrevValue); d_data.Append("|");
                d_data.Append(itm.NewValue);
                i++;
            }

            return d_data.ToString();
        }

	}
}
