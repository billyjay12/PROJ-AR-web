using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ARMS_W.SkelClass;

namespace ARMS_W.Class
{
    public class Parser
    {

        public static Object toObject(string val, string SCHED_TYPE)
        {
            JavaScriptSerializer j_serializer = new JavaScriptSerializer();

            cls_email email_data_iobject = new cls_email();

            if (SCHED_TYPE == "UPDATE_TO_SAP")
            {
                sap_businesspartner c_obj = j_serializer.Deserialize<sap_businesspartner>(val);
                return c_obj;
            }

            if (SCHED_TYPE == "EMAIL")
            {
                cls_email c_obj = j_serializer.Deserialize<cls_email>(val);
                return c_obj;
            }

            return null;
        }

        public static string toJson(Object val)
        {
            JavaScriptSerializer j_serializer = new JavaScriptSerializer();
            return j_serializer.Serialize(val);
        }

    }
}