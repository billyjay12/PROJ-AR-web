using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ARMS_W.SkelClass
{
    public class Utils
    {
        public static object JsonToObject<Type>(string strval)
        {
            Type mtype;
            mtype = Newtonsoft.Json.JsonConvert.DeserializeObject<Type>(strval);
            return mtype;
        }

        public static List<Globals.db_changes> LogChangesToObj(string value)
        {
            List<Globals.db_changes> res = new List<Globals.db_changes>();

            // split to array using ^
            string[] rows = value.Split('^');

            // loop throw the rows
            foreach (string cols in rows)
            {
                // split to array using |
                string[] col = cols.Split('|');

                // col[0] = field name
                // col[1] = prev value
                // col[2] = new value
                res.Add(new Globals.db_changes()
                {
                    FieldName = col[0],
                    PrevValue = col[1],
                    NewValue = col[2]
                });

            }

            return res;
        }
    }
}