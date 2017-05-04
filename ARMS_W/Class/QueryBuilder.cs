using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ARMS_W.Class
{
    public class QueryBuilder
    {

        public static string InsertTo(string TableName, Dictionary<string, object> data_to_insert)
        {
            StringBuilder qry = new StringBuilder(1000);

            qry.Append("insert into ");
            qry.Append(TableName);

            /* COLUMN NAMES */
            qry.Append(" (");
            bool has_field = false;
            foreach (string FieldName in data_to_insert.Keys)
            {
                if (has_field) qry.Append(", ");
                qry.Append(FieldName);
                has_field = true;
            }
            qry.Append(" )");

            /* VALUES */
            qry.Append(" VALUES (");
            bool has_values = false;
            foreach (object FieldValue in data_to_insert.Values)
            {
                if (has_values) qry.Append(", ");

                // check data type
                if (FieldValue != null)
                {
                    if (FieldValue.GetType().Equals(typeof(string)))
                    {
                        // if string
                        qry.Append("'");
                        qry.Append(FieldValue.ToString().Replace("'", "''"));
                        qry.Append("'");
                    }
                    else if (FieldValue.GetType().Equals(typeof(DateTime)))
                    {
                        // if datetime
                        qry.Append("'");
                        qry.Append(Convert.ToDateTime(FieldValue).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry.Append("'");
                    }
                    else
                    {
                        // mostly integers
                        qry.Append("" + FieldValue + "");
                    }
                }
                else
                {
                    qry.Append("NULL");
                }

                has_values = true;
            }
            qry.Append(" )");

            return qry.ToString();
        }

        public static string UpdateTo(string TableName, Dictionary<string, object> data_to_update, Dictionary<string, object> condition)
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

            return qry.ToString();
        }

        public static string DeleteFrom(string TableName, Dictionary<string, object> condition)
        {
            StringBuilder qry = new StringBuilder(1000);

            qry.Append("delete from ");
            qry.Append(TableName);

            if (condition.Count > 0) qry.Append(" where ");

            // conditions
            bool has_conditions = false;
            foreach (string FieldName in condition.Keys)
            {
                if (has_conditions) qry.Append(" and ");

                qry.Append(FieldName);

                if (condition[FieldName] != null)
                {
                    if (condition[FieldName].GetType().Equals(typeof(string)))
                    {
                        qry.Append("=");

                        // if string
                        qry.Append(" '");
                        qry.Append(condition[FieldName].ToString().Replace("'", "''"));
                        qry.Append("' ");
                    }
                    else if (condition[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        qry.Append("=");

                        qry.Append(" '");
                        qry.Append(Convert.ToDateTime(condition[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry.Append("' ");
                    }
                    else
                    {
                        qry.Append("=");
                        qry.Append(condition[FieldName].ToString());
                    }
                }
                else
                {
                    qry.Append(" is ");
                    qry.Append("NULL");
                }

                has_conditions = true;
            }

            return qry.ToString();
        }

        public static string TryUpdateInsert(string TableName, Dictionary<string, object> data_to_update, Dictionary<string, object> condition)
        {
            StringBuilder qry = new StringBuilder(1000);

            StringBuilder condition_for_seach = new StringBuilder(1000);
            #region CONDITION FOR SEARCHING
            if (condition.Keys.Count > 0)
            {
                condition_for_seach.Append(" where ");
            }
            int condition_for_search_counter = 0;
            foreach (string FieldName in condition.Keys)
            {
                if (condition_for_search_counter > 0) qry.Append(" and ");
                condition_for_seach.Append(FieldName);

                if (condition[FieldName] != null)
                {
                    if (condition[FieldName].GetType().Equals(typeof(string)))
                    {
                        condition_for_seach.Append(" = '");
                        condition_for_seach.Append(condition[FieldName].ToString().Replace("'", "''"));
                        condition_for_seach.Append("' ");
                    }
                    else if (condition[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        condition_for_seach.Append(" = '");
                        condition_for_seach.Append(Convert.ToDateTime(condition[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        condition_for_seach.Append("' ");
                    }
                    else
                    {
                        condition_for_seach.Append(" = ");
                        condition_for_seach.Append(condition[FieldName].ToString());
                    }
                }
                else
                {
                    condition_for_seach.Append(" is NULL ");
                }

                condition_for_search_counter++;
            }
            #endregion

            StringBuilder qry_for_insert = new StringBuilder(1000);
            #region INSERT QUERY
            if (data_to_update.Keys.Count > 0 || condition.Keys.Count > 0)
            {
                qry_for_insert.Append(" insert into " + TableName);
            }

            qry_for_insert.Append(" (");
            int icounter1 = 0;
            foreach (string FieldName in data_to_update.Keys)
            {
                if (icounter1 > 0) qry_for_insert.Append(",");
                qry_for_insert.Append(FieldName);
                icounter1++;
            }
            qry_for_insert.Append(" ) ");

            // the values
            qry_for_insert.Append(" values (");
            int icounter2 = 0;
            foreach (string FieldName in data_to_update.Keys)
            {
                if (icounter2 > 0) qry_for_insert.Append(",");
                if (data_to_update[FieldName] != null)
                {
                    if (data_to_update[FieldName].GetType().Equals(typeof(string)))
                    {
                        qry_for_insert.Append("'");
                        qry_for_insert.Append(data_to_update[FieldName].ToString().Replace("'", "''"));
                        qry_for_insert.Append("'");
                    }
                    else if (data_to_update[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        qry_for_insert.Append("'");
                        qry_for_insert.Append(Convert.ToDateTime(data_to_update[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry_for_insert.Append("'");
                    }
                    else
                    {
                        qry_for_insert.Append(data_to_update[FieldName].ToString());
                    }
                }
                else
                {
                    // null goes here
                    qry_for_insert.Append("NULL");
                }
                icounter2++;
            }
            qry_for_insert.Append(")");
            #endregion

            StringBuilder qry_for_update = new StringBuilder(1000);
            #region UPDATE QUERY

            qry_for_update.Append("update " + TableName + " set ");

            int icounter3 = 0;
            foreach (string FieldName in data_to_update.Keys)
            {
                if (icounter3 > 0) qry_for_update.Append(", ");
                if (data_to_update[FieldName] != null)
                {
                    if (data_to_update[FieldName].GetType().Equals(typeof(string)))
                    {
                        qry_for_update.Append(FieldName + " = '");
                        qry_for_update.Append(data_to_update[FieldName].ToString().Replace("'", "''"));
                        qry_for_update.Append("'");
                    }
                    else if (data_to_update[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        qry_for_update.Append(FieldName + " = '");
                        qry_for_update.Append(Convert.ToDateTime(data_to_update[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry_for_update.Append("'");
                    }
                    else
                    {
                        qry_for_update.Append(FieldName + " = ");
                        qry_for_update.Append(data_to_update[FieldName].ToString());
                    }
                }
                else
                {
                    qry_for_update.Append(FieldName + " = ");
                    qry_for_update.Append("NULL");
                }
                icounter3++;
            }

            // condition
            if (condition.Keys.Count > 0)
            {
                qry_for_update.Append(" where ");
            }
            int icounter4 = 0;
            foreach (string FieldName in condition.Keys)
            {
                if (condition[FieldName] != null)
                {
                    if (icounter4 > 0) qry_for_update.Append(" and ");

                    if (data_to_update[FieldName].GetType().Equals(typeof(string)))
                    {
                        qry_for_update.Append(FieldName + " = '");
                        qry_for_update.Append(data_to_update[FieldName].ToString().Replace("'", "''"));
                        qry_for_update.Append("'");
                    }
                    else if (data_to_update[FieldName].GetType().Equals(typeof(DateTime)))
                    {
                        qry_for_update.Append(FieldName + " = '");
                        qry_for_update.Append(Convert.ToDateTime(data_to_update[FieldName].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                        qry_for_update.Append("'");
                    }
                    else
                    {
                        qry_for_update.Append(FieldName + " = ");
                        qry_for_update.Append(data_to_update[FieldName].ToString());
                    }

                }
                else
                {
                    qry_for_update.Append(" is ");
                    qry_for_update.Append(" NULL ");
                }
                icounter4++;
            }

            #endregion

            qry.Append(" IF not EXISTS(select * from " + TableName + " " + condition_for_seach.ToString() + " ) \n");
            qry.Append(" BEGIN \n");
            // update
            qry.Append(qry_for_insert.ToString());
            qry.Append("\n");

            qry.Append(" END \n");
            qry.Append(" ELSE \n");
            qry.Append(" BEGIN \n");
            // insert
            qry.Append(qry_for_update.ToString());
            qry.Append("\n");

            qry.Append(" END \n");

            return qry.ToString();
        }



    }
}