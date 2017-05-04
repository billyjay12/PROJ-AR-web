using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.Class
{
    public class DataChanges
    {

        public static int LogChanges(ref SQLTransaction sql_trans, List<SkelClass.Globals.db_changes> data, int DocTypeId , string UserName, string DocumentId = null)
        {
            string serialized_data = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            sql_trans.InsertTo("LogChanges",
                new Dictionary<string, object>() { 
                    {"DocTypeId", DocTypeId }
                    , {"DocId", DocumentId }
                    , {"TimeStmp", DateTime.Now }
                    , {"UserName", UserName.ToUpper() }
                    , {"Data", StringHelper.LogChangesToStr(data) }
                });

            return 0;
        }

        public static List<Globals.db_changes_hdr> GetLogChanges(Globals.InfoType info_type)
        {
            List<Globals.db_changes_hdr> log_changes = new List<Globals.db_changes_hdr>();
            var DATABASE = new Models.ARMSTestEntities();
            try
            {
                var qry = (from logchanges in DATABASE.LogChanges
                           where logchanges.DocTypeId == (int)info_type
                           select new { logchanges.TimeStmp, logchanges.UserName, logchanges.Data }).ToList();

                foreach (var itm in qry)
                {
                    log_changes.Add(new Globals.db_changes_hdr()
                    {
                        log_datetime = (DateTime)itm.TimeStmp,
                        username = itm.UserName,
                        logs = Utils.LogChangesToObj(itm.Data)
                    });
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return log_changes;
        }

        public static List<Globals.db_changes_hdr> GetLogChanges(Globals.InfoType info_type, string DocId = null)
        {
            List<Globals.db_changes_hdr> log_changes = new List<Globals.db_changes_hdr>();
            var DATABASE = new Models.ARMSTestEntities();

            try
            {
                var qry = (from afcndologchanges in DATABASE.LogChanges
                           where afcndologchanges.DocTypeId == (int)info_type && afcndologchanges.DocId == DocId
                           select new { afcndologchanges.TimeStmp, afcndologchanges.UserName, afcndologchanges.Data }).ToList();
                foreach (var itm in qry)
                {
                    log_changes.Add(new Globals.db_changes_hdr()
                    {
                        log_datetime = (DateTime)itm.TimeStmp,
                        username = itm.UserName,
                        logs = Utils.LogChangesToObj(itm.Data)
                      

                    });
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return log_changes;
        }

        public static List<Globals.db_route_changes> GetRouteChanges(Globals.InfoType info_type, string DocId = null)
        {
            List<Globals.db_route_changes> result = new List<Globals.db_route_changes>();

            var DATABASE = new Models.ARMSTestEntities();
            try
            {
                var routechanges = from a in DATABASE.RouteChanges
                                   from b in DATABASE.apprvrRoles
                                   where a.RoleCode == b.roleCode && a.DocTypeId == (int)info_type && a.DocId == DocId
                                   select new
                                   {
                                      ActionType = a.ActionType,
                                      CurDocStatus =  a.DocStatusId,
                                      PrevDocStatus = a.PrevDocStatusId,
                                      DateTimeStamp = (DateTime)a.TimeStamp,
                                      Username = a.UserName,
                                      PositionName = b.roleName,
                                      a.Remarks,
                                   };

                foreach (var item in routechanges)
                {
                    result.Add(new Globals.db_route_changes()
                    {
                        Action = (Globals.DocActionType) Int16.Parse(item.ActionType),
                        CurDocStatus = item.CurDocStatus.ToString(),
                        DateTimeStamp = item.DateTimeStamp,
                        PositionName = item.PositionName,
                        PrevDocStatus = item.PrevDocStatus.ToString(),
                        Remarks = item.Remarks,
                        UserName = item.Username
                    });
                }
                
            }
            finally
            {
                DATABASE.Dispose();
            }

            return result;
        }

        public static object JsonToObject<Type>(string strval)
        {
           // Type mtype;
          //  mtype = Newtonsoft.Json.JsonConvert.DeserializeObject<Type>(strval);
          //  return mtype;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Type>(strval);
        }


        public static List<Globals.db_route_changes> GetRouteChangesbyAccount(Globals.InfoType info_type, string DocId = null)
        {
            List<Globals.db_route_changes> result = new List<Globals.db_route_changes>();

            var DATABASE = new Models.ARMSTestEntities();

            try {

                var routechanges = from a in DATABASE.RouteChanges
                                   from b in DATABASE.apprvrRoles
                                   where a.RoleCode == b.roleCode && a.DocTypeId == (int)info_type && a.DocId == DocId
                                   select new
                                   {
                                       ActionType = a.ActionType,
                                       CurDocStatus = a.DocStatusId,
                                       PrevDocStatus = a.PrevDocStatusId,
                                       DateTimeStamp = (DateTime)a.TimeStamp,
                                       Username = a.UserName,
                                       PositionName = b.roleName,
                                       a.Remarks,
                                   };


                foreach (var item in routechanges)
                {
                    result.Add(new Globals.db_route_changes()
                    {
                        Action = (Globals.DocActionType)Int16.Parse(item.ActionType),
                        CurDocStatus = item.CurDocStatus.ToString(),
                        DateTimeStamp = item.DateTimeStamp,
                        PositionName = item.PositionName,
                        PrevDocStatus = item.PrevDocStatus.ToString(),
                        Remarks = item.Remarks,
                        UserName = item.Username
                    });
                }

            }
            finally
            {
                DATABASE.Dispose();
            }

            return result;
        
        }
    }
}