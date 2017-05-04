using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.OleDb;

namespace ARMS_W.Class
{
    public class SQLTransaction
    {
        private string _username;
        private string _password;
        private string _host;
        private string _database;

        private OleDbConnection _connection;
        private OleDbCommand _command;
        private OleDbTransaction _transaction;

        private string _command_text;

        const string ArmsDatabase = "ARMS";

        public SQLTransaction(string uid = "sa", string pwd = "p@ssw0rd", string host = "reportserver", string db = ArmsDatabase)
        {
            _username = uid;
            _password = pwd;
            _host = host;
            _database = db;
            Connect();
        }

        private void Connect()
        {
            _connection = new OleDbConnection("Provider=SQLOLEDB;Server=" + _host + ";Database=" + _database + ";User Id=" + _username + ";Password=" + _password + ";");
        }

        public string CommandText
        {
            get { return _command_text; }
            set
            {
                _command_text = value;
                _command.CommandText = _command_text;
                _command.ExecuteNonQuery();
            }
        }

        public void StartTransaction()
        {
            _command = new OleDbCommand();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _command.Connection = _connection;
            _command.Transaction = _transaction;
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        public void Committransaction()
        {
            _transaction.Commit();
            _connection.Close();
        }

        public void InsertTo(string tablename, Dictionary<string, object> fieldNameNValue)
        {
            StringBuilder w = new StringBuilder(1000);

            StringBuilder w_field_names = new StringBuilder(1000);
            StringBuilder w_field_values = new StringBuilder(1000);

            w.Append("insert into " + tablename + " ");
            // fieldname
            foreach (string fname in fieldNameNValue.Keys)
            {
                if (w_field_names.ToString() == "") { w_field_names.Append("("); }
                else { w_field_names.Append(","); }
                w_field_names.Append(fname);
            }
            if (w_field_names.ToString() != "") w_field_names.Append(") ");
            w.Append(w_field_names);

            //fieldvalues
            w.Append(" values (");
            foreach (object fvalue in fieldNameNValue.Values)
            {
                if (w_field_values.ToString() != "")
                {
                    w_field_values.Append(",");
                }

                if (fvalue != null)
                {
                    if (fvalue.GetType().Equals(typeof(string)))
                    {
                        w_field_values.Append("'" + fvalue.ToString().Replace("'", "''") + "'");
                    }
                    else if (fvalue.GetType().Equals(typeof(DateTime)))
                    {
                        w_field_values = w_field_values.Append("'" + Convert.ToDateTime(fvalue).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                    }
                    else
                    {
                        // mostly numbers will go here
                        w_field_values.Append("" + fvalue + "");
                    }
                }
                else
                {
                    w_field_values.Append("NULL");
                }
            }

            w_field_values.Append(")");
            w.Append(w_field_values);

            this.CommandText = w.ToString();
        }

        public void UpdateTo(string tablename, Dictionary<string, object> fieldDataNValue, Dictionary<string, object> conditionDataNValue)
        {
            StringBuilder w = new StringBuilder(1000);
            StringBuilder fieldnamevalue = new StringBuilder(1000);

            w.Append("update " + tablename + " ");
            w.Append("set ");

            foreach (string fname in fieldDataNValue.Keys)
            {
                if (fieldnamevalue.ToString() != "") fieldnamevalue.Append(", ");
                fieldnamevalue.Append(fname + "=");

                if (fieldDataNValue[fname] != null)
                {
                    if (fieldDataNValue[fname].GetType().Equals(typeof(string)))
                    {
                        fieldnamevalue.Append("'" + fieldDataNValue[fname].ToString().Replace("'", "''") + "'");
                    }
                    else if (fieldDataNValue[fname].GetType().Equals(typeof(DateTime)))
                    {
                        fieldnamevalue.Append("'" + Convert.ToDateTime(fieldDataNValue[fname].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                    }
                    else
                    {
                        fieldnamevalue.Append(fieldDataNValue[fname].ToString());
                    }
                }
                else
                {
                    fieldnamevalue.Append("NULL");
                }
            }
            w.Append(fieldnamevalue.ToString());

            // condition
            if (conditionDataNValue != null)
            {
                if (conditionDataNValue.Count > 0)
                {
                    w.Append(" where ");

                    foreach (string fname in conditionDataNValue.Keys)
                    {
                        w.Append(fname + "=");

                        if (conditionDataNValue[fname] != null)
                        {
                            if (conditionDataNValue[fname].GetType().Equals(typeof(string)))
                            {
                                w.Append("'" + conditionDataNValue[fname].ToString().Replace("'", "''") + "'");
                            }
                            else if (conditionDataNValue[fname].GetType().Equals(typeof(DateTime)))
                            {
                                w.Append("'" + Convert.ToDateTime(conditionDataNValue[fname].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                            }
                            else
                            {
                                w.Append(conditionDataNValue[fname].ToString());
                            }
                        }
                        else
                        {
                            w.Append("NULL");
                        }
                    }
                }
            }

            this.CommandText = w.ToString();
        }

        public void UpdateTo1(string TableName, Dictionary<string, object> data_to_update, Dictionary<string, object> condition)
        {
            StringBuilder qry = new StringBuilder(1000);

            qry.Append("update ");
            qry.Append(TableName);
            qry.Append(" set ");

            // values to update
            #region VALUES FOR UPDATING
            bool has_values = false;
            foreach (string FieldName in data_to_update.Keys)
            {
                if (has_values) qry.Append(", ");

                // field name
                qry.Append(FieldName);
                qry.Append("=");

                // field values
                if (data_to_update[FieldName] != null)
                {
                    if (data_to_update[FieldName].GetType().Equals(typeof(string)))
                    {
                        // if string
                        qry.Append(" '");
                        qry.Append(data_to_update[FieldName].ToString().Replace("'", "''"));
                        qry.Append("' ");
                    }
                    else if (data_to_update[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        // if datetime
                        qry.Append(" '");
                        qry.Append(Convert.ToDateTime(data_to_update[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry.Append("' ");
                    }
                    else
                    {
                        qry.Append(data_to_update[FieldName].ToString());
                    }
                }
                else
                {
                    qry.Append("NULL");
                }

                has_values = true;
            }
            #endregion

            #region CONDITIONS
            if (condition.Keys.Count > 0)
            {
                qry.Append(" where ");

                bool has_condition = false;
                foreach (string FieldName in condition.Keys)
                {
                    if (has_condition) qry.Append(" and ");

                    // fieldname
                    qry.Append(FieldName);

                    // value
                    if (condition[FieldName] != null)
                    {
                        if (condition[FieldName].GetType().Equals(typeof(string)))
                        {
                            qry.Append(" = ");
                            qry.Append(" '");
                            qry.Append(condition[FieldName].ToString().Replace("'", "''"));
                            qry.Append("' ");
                        }
                        else if (condition[FieldName].GetType().Equals(typeof(DateTime)))
                        {
                            qry.Append(" = ");
                            qry.Append(" '");
                            qry.Append(Convert.ToDateTime(condition[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                            qry.Append("' ");
                        }
                        else
                        {
                            qry.Append(" = ");
                            qry.Append(condition[FieldName].ToString());
                        }
                    }
                    else
                    {
                        qry.Append(" is ");
                        qry.Append(" NULL ");
                    }

                    has_condition = true;
                }
            }

            #endregion
            this.CommandText = qry.ToString();

        }

        public void DeleteFromWithIsNull(string TableName, Dictionary<string, object> data_to_update, Dictionary<string, object> condition)
        {
            StringBuilder qry = new StringBuilder(1000);

            qry.Append("delete from ");
            qry.Append(TableName);
            qry.Append(" where ");

            // values to update
            #region VALUES FOR UPDATING
            bool has_values = false;
            foreach (string FieldName in data_to_update.Keys)
            {
                if (has_values) qry.Append(" and  ");

                // field name
                qry.Append(FieldName);
                qry.Append("=");

                // field values
                if (data_to_update[FieldName] != null)
                {
                    if (data_to_update[FieldName].GetType().Equals(typeof(string)))
                    {
                        // if string
                        qry.Append(" '");
                        qry.Append(data_to_update[FieldName].ToString().Replace("'", "''"));
                        qry.Append("' ");
                    }
                    else if (data_to_update[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        // if datetime
                        qry.Append(" '");
                        qry.Append(Convert.ToDateTime(data_to_update[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry.Append("' ");
                    }
                    else
                    {
                        qry.Append(data_to_update[FieldName].ToString());
                    }
                }
                else
                {
                    qry.Append("NULL");
                }

                has_values = true;
            }
            #endregion

            #region CONDITIONS
            if (condition.Keys.Count > 0)
            {
                qry.Append(" and ");

                bool has_condition = false;
                foreach (string FieldName in condition.Keys)
                {
                    if (has_condition) qry.Append(" and ");

                    // fieldname
                    qry.Append(FieldName);

                    // value
                    if (condition[FieldName] != null)
                    {
                        if (condition[FieldName].GetType().Equals(typeof(string)))
                        {
                            qry.Append(" = ");
                            qry.Append(" '");
                            qry.Append(condition[FieldName].ToString().Replace("'", "''"));
                            qry.Append("' ");
                        }
                        else if (condition[FieldName].GetType().Equals(typeof(DateTime)))
                        {
                            qry.Append(" = ");
                            qry.Append(" '");
                            qry.Append(Convert.ToDateTime(condition[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                            qry.Append("' ");
                        }
                        else
                        {
                            qry.Append(" = ");
                            qry.Append(condition[FieldName].ToString());
                        }
                    }
                    else
                    {
                        qry.Append(" is ");
                        qry.Append(" NULL ");
                    }

                    has_condition = true;
                }
            }

            #endregion
            this.CommandText = qry.ToString();

        }


        public void DeleteFrom(string tablename, Dictionary<string, object> conditionDataNValue)
        {
            StringBuilder w = new StringBuilder(1000);
            StringBuilder condition = new StringBuilder(1000);

            w.Append("delete from " + tablename + " ");

            // condition
            if (conditionDataNValue != null)
            {
                if (conditionDataNValue.Count > 0)
                {
                    w.Append(" where ");

                    foreach (string fname in conditionDataNValue.Keys)
                    {
                        if (condition.ToString().Length > 0) condition.Append(" and ");
                            condition.Append(fname + "=");

                        if (conditionDataNValue[fname] != null)
                        {
                            if (conditionDataNValue[fname].GetType().Equals(typeof(string)))
                            {
                                condition.Append("'" + conditionDataNValue[fname].ToString().Replace("'", "''") + "'");
                            }
                            else if (conditionDataNValue[fname].GetType().Equals(typeof(DateTime)))
                            {
                                condition.Append("'" + Convert.ToDateTime(conditionDataNValue[fname].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                            }
                            else
                            {
                                condition.Append(conditionDataNValue[fname].ToString());
                            }
                        }
                        else
                        {
                            condition.Append("NULL");
                        }
                    }
                }
            }
            w.Append(condition.ToString());
            this.CommandText = w.ToString();
        }

        #region TryInsertUpdate

        public void TryInsertUpdate(string tablename, Dictionary<string, object> data_to_insert, Dictionary<string, object> conditionDataNValue)
        {
            StringBuilder w = new StringBuilder(1000);
            StringBuilder fields_n = new StringBuilder(1000);
            StringBuilder condition_f = new StringBuilder(1000);
            Dictionary<string, object> merge_dictionary = new Dictionary<string, object>();

            foreach (string key in conditionDataNValue.Keys)
            {
                if (condition_f.ToString() != "")
                {
                    condition_f.Append(" and ");
                }
                else
                {
                    condition_f.Append(" where ");
                }
                condition_f.Append(key + "=");
                if (conditionDataNValue[key] != null)
                {
                    if (conditionDataNValue[key].GetType().Equals(typeof(string)))
                    {
                        condition_f.Append("'" + conditionDataNValue[key].ToString().Replace("'", "''") + "'");
                    }
                    else if (conditionDataNValue[key].GetType().Equals(typeof(DateTime)))
                    {
                        condition_f.Append("'" + Convert.ToDateTime(conditionDataNValue[key].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                    }
                    else
                    {
                        condition_f.Append(conditionDataNValue[key].ToString());
                    }
                }
                else
                {
                    condition_f.Append("NULL");
                }
            }

            w.Append("IF exists(select top 1 * from " + tablename + " " + condition_f.ToString() + ") \n");
            w.Append("begin\n");
            // UPDATE
            w.Append(_UpdateTo(tablename, data_to_insert, conditionDataNValue));

            w.Append("\n\n");

            w.Append("end\n");
            w.Append("ELSE\n");
            w.Append("begin\n");
            // INSERT
            foreach (string key in data_to_insert.Keys)
            {
                if (!merge_dictionary.ContainsKey(key))
                    merge_dictionary.Add(key, data_to_insert[key]);
            }

            foreach (string key in conditionDataNValue.Keys)
            {
                if (!merge_dictionary.ContainsKey(key))
                    merge_dictionary.Add(key, conditionDataNValue[key]);
            }

            w.Append(_InsertTo(tablename, merge_dictionary));
            w.Append("end\n");

            this.CommandText = w.ToString();
        }

        private string _UpdateTo(string tablename, Dictionary<string, object> fieldDataNValue, Dictionary<string, object> conditionDataNValue)
        {
            StringBuilder w = new StringBuilder(1000);
            StringBuilder fieldnamevalue = new StringBuilder(1000);
            StringBuilder conditionvalue = new StringBuilder(1000);

            w.Append("update " + tablename + " ");
            w.Append("set ");

            foreach (string fname in fieldDataNValue.Keys)
            {
                if (fieldnamevalue.ToString() != "") fieldnamevalue.Append(", ");
                fieldnamevalue.Append(fname + "=");

                if (fieldDataNValue[fname] != null)
                {
                    if (fieldDataNValue[fname].GetType().Equals(typeof(string)))
                    {
                        fieldnamevalue.Append("'" + fieldDataNValue[fname].ToString().Replace("'", "''") + "'");
                    }
                    else if (fieldDataNValue[fname].GetType().Equals(typeof(DateTime)))
                    {
                        fieldnamevalue.Append("'" + Convert.ToDateTime(fieldDataNValue[fname].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                    }
                    else
                    {
                        fieldnamevalue.Append(fieldDataNValue[fname].ToString());
                    }
                }
                else
                {
                    fieldnamevalue.Append("NULL");
                }
            }
            w.Append(fieldnamevalue.ToString());

            // condition
            if (conditionDataNValue != null)
            {
                if (conditionDataNValue.Count > 0)
                {
                    w.Append(" where ");

                    foreach (string fname in conditionDataNValue.Keys)
                    {
                        if (conditionvalue.ToString() != "") conditionvalue.Append(" and ");
                        conditionvalue.Append(fname + "=");

                        if (conditionDataNValue[fname] != null)
                        {
                            if (conditionDataNValue[fname].GetType().Equals(typeof(string)))
                            {
                                conditionvalue.Append("'" + conditionDataNValue[fname].ToString().Replace("'", "''") + "'");
                            }
                            else if (conditionDataNValue[fname].GetType().Equals(typeof(DateTime)))
                            {
                                conditionvalue.Append("'" + Convert.ToDateTime(conditionDataNValue[fname].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                            }
                            else
                            {
                                conditionvalue.Append(conditionDataNValue[fname].ToString());
                            }
                        }
                        else
                        {
                            conditionvalue.Append("NULL");
                        }
                    }
                }
            }
            w.Append(conditionvalue.ToString());
            return w.ToString();
        }

        private string _InsertTo(string tablename, Dictionary<string, object> fieldNameNValue)
        {
            StringBuilder w = new StringBuilder(1000);

            StringBuilder w_field_names = new StringBuilder(1000);
            StringBuilder w_field_values = new StringBuilder(1000);

            w.Append("insert into " + tablename + " ");
            // fieldname
            foreach (string fname in fieldNameNValue.Keys)
            {
                if (w_field_names.ToString() == "") { w_field_names.Append("("); }
                else { w_field_names.Append(","); }
                w_field_names.Append(fname);
            }
            if (w_field_names.ToString() != "") w_field_names.Append(") ");
            w.Append(w_field_names);

            //fieldvalues
            w.Append(" values (");
            foreach (object fvalue in fieldNameNValue.Values)
            {
                if (w_field_values.ToString() != "")
                {
                    w_field_values.Append(",");
                }

                if (fvalue != null)
                {
                    if (fvalue.GetType().Equals(typeof(string)))
                    {
                        w_field_values.Append("'" + fvalue.ToString().Replace("'", "''") + "'");
                    }
                    else if (fvalue.GetType().Equals(typeof(DateTime)))
                    {
                        w_field_values = w_field_values.Append("'" + Convert.ToDateTime(fvalue).ToString("MM/dd/yyyy HH:mm:ss") + "'");
                    }
                    else
                    {
                        // mostly numbers will go here
                        w_field_values.Append("" + fvalue + "");
                    }
                }
                else
                {
                    w_field_values.Append("NULL");
                }
            }

            w_field_values.Append(")");
            w.Append(w_field_values);

            return w.ToString();
        }

        #endregion

    }
}
