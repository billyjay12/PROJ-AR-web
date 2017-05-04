using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;


namespace ARMS_W.Class
{
    public class ReportMaker
    {

        private int num_of_columns;

        private List<string> _columns;
        private List<List<string>> _rows;
        private List<string> _column_names;

        public ReportMaker() 
        {
            _columns = null;
            _rows = null;
            _column_names = new List<string>();
        }

        public void DataSource(DataTable rows_to_load, string[] grouping_fields ) 
        {
            // column names
            foreach (DataColumn itm in rows_to_load.Columns) 
            {
                _column_names.Add(itm.ColumnName);
            }

            foreach (DataRow itm in rows_to_load.Rows) 
            {
                // rows
                foreach (object data in itm.ItemArray) 
                { 
                    // columns
                    _columns.Add((string)data);
                }
                _rows.Add(_columns);
            }
        }

        public string GenerateHtml() 
        {
            StringBuilder html_output = new StringBuilder(2000);

            html_output.Append("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" >");

            html_output.Append("<tr>");
            foreach (string itm in _column_names) 
            {
                html_output.Append("<td align=\"center\" >");
                html_output.Append("<b>" + itm + "</b>");
                html_output.Append("</td>");
            }
            html_output.Append("</tr>");

            if (_rows != null) {
                foreach (List<string> itm in _rows)
                {
                    html_output.Append("<tr>");
                    foreach (string itm_contents in itm) 
                    {
                        html_output.Append("<td >");
                        html_output.Append("<b>" + itm_contents + "</b>");
                        html_output.Append("</td>");
                    }
                    html_output.Append("</tr>");
                }
            }
            html_output.Append("</table>");

            return html_output.ToString();
        }

    }
}