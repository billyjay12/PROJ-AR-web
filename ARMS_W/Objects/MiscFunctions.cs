using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.Objects
{
    public class MiscFunctions
    {

        public static string GetRoleCode(int? DocTypeId, int? DocumentStatusId,int roleid=-1)
        {
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();

            string rolecode = "";

            if (DocTypeId == null || DocumentStatusId == null)
            {
                return "";
            }
            try
            {
                if (roleid >= 0)
                {
                    var qry =
                        from appvr_state in DATABASE.approvalStates
                        from appvr_role in DATABASE.apprvrRoles
                        where
                            appvr_state.docType == DocTypeId &&
                            appvr_state.stateID == DocumentStatusId &&
                            appvr_role.roleID == appvr_state.roleID &&
                            appvr_role.roleID == roleid
                        select new
                        {
                            appvr_role.roleCode
                        };
                    foreach (var itm in qry)
                    {
                        rolecode = itm.roleCode;
                    }
                }
                else
                {
                    var qry1 =
                        from appvr_state in DATABASE.approvalStates
                        from appvr_role in DATABASE.apprvrRoles
                        where
                            appvr_state.docType == DocTypeId &&
                            appvr_state.stateID == DocumentStatusId &&
                            appvr_role.roleID == appvr_state.roleID
                        select new
                        {
                            appvr_role.roleCode
                        };
                    foreach (var itm in qry1)
                    {
                        rolecode = itm.roleCode;
                    }
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return rolecode;
        }

        public class GetEmailAddressesTMP
        {
            public string EmailAddress { get; set; }
        }

        public static List<GetEmailAddressesTMP> GetEmailAddresses_New(int nextid, int roleid, string empidno)
        {
            List<string> branches = new List<string>();
            var DATABASE  = new Models.ARMSTestEntities();
            List<GetEmailAddressesTMP> res = new List<GetEmailAddressesTMP>();

            if (roleid != 8) // roleid - 8 [CHM/RSM]
            {
                var qry_branches = (from a in DATABASE.apprvrDesigs
                                    from b in DATABASE.userHeaders
                                    where a.counterId == b.counterId
                                        && b.empIdNo == empidno
                                        && a.roleID == roleid
                                    select a.branch);

                //DBHelper.getData(dbtype, "select a.branch from apprvrdesig a inner join userheader b on a.counterid=b.counterid 
                //where empidno='" + Globals.EmpId + "' and roleid=" + roleid).Rows;

                foreach (var branch in qry_branches.Distinct())
                {
                    branches.Add(branch);
                }
            }

            foreach (var itm in branches)
            {
                var emails = (from a in DATABASE.approvalStates
                              from b in DATABASE.apprvrDesigs
                              where a.roleID==b.roleID
                                && a.docType==14
                                && a.stateID == nextid
                                && branches.Contains(b.branch) 
                                && b.channel.Contains("TRADE")
                              select b.email);
                    
                //DBHelper.getData(dbtype, "SELECT distinct b.email FROM (approvalstate a inner join apprvrdesig b on a.roleid=b.roleid) 
                //where branch='" + itm + "' and doctype=14 and stateid=" + next_apprverstate.stateid + "").Rows;

                foreach (var email in emails.Distinct())
                {
                    res.Add(new GetEmailAddressesTMP() { EmailAddress = email });
                }
            }

            return res;
        }


        public static List<GetEmailAddressesTMP> GetEmailAddresses(int DocTypeId, int DocumentStatusId, string area = "", bool isForChannelManager = false, string channel = "")
        {
            List<GetEmailAddressesTMP> res = new List<GetEmailAddressesTMP>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            try
            {
                if (isForChannelManager)
                {
                    string roleCode = (from a in DATABASE.approvalStates
                                       from b in DATABASE.apprvrRoles
                                       where a.docType == DocTypeId &&
                                        a.stateID == DocumentStatusId &&
                                        a.roleID == b.roleID
                                       select b.roleCode).FirstOrDefault();

                    res =
                        (from a in DATABASE.apprvrDesigs
                         from c in DATABASE.userHeaders
                         where a.counterId==c.counterId && (from b in DATABASE.apprvrRoles
                                where b.roleCode == roleCode
                                select b.roleID).Contains(a.roleID) && a.channel == channel
                                && c.status.ToUpper()=="ACTIVE"

                         select new GetEmailAddressesTMP()
                         {
                             EmailAddress = a.email
                         }).ToList();

                    /* DISTINCT EMAIL ADDRESS */
                    res = res.GroupBy(o => o.EmailAddress).Select(grp => grp.First()).ToList();
                }
                else
                {
                    string roleCode = (from a in DATABASE.approvalStates
                                       from b in DATABASE.apprvrRoles
                                       where a.docType == DocTypeId &&
                                        a.stateID == DocumentStatusId &&
                                        a.roleID == b.roleID
                                       select b.roleCode).FirstOrDefault();



                    res =
                        (from a in DATABASE.apprvrDesigs
                         from c in DATABASE.userHeaders
                         where a.counterId == c.counterId && (from b in DATABASE.apprvrRoles
                                                              where b.roleCode == roleCode
                                                              select b.roleID).Contains(a.roleID) 
                                //&& a.area == area
                                && !(a.channel.StartsWith("LDI") || a.channel.StartsWith("VDI") || a.channel.StartsWith("LWC"))
                                && c.status.ToUpper() == "ACTIVE"
                         select new GetEmailAddressesTMP()
                         {
                             EmailAddress = a.email
                         }).ToList();

                    /* DISTINCT EMAIL ADDRESS */
                    res = res.GroupBy(o => o.EmailAddress).Select(grp => grp.First()).ToList();
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return res;
        }

        public static List<GetEmailAddressesTMP> GetEmailAddresses__(int DocTypeId, int DocumentStatusId, string area = "", bool isForChannelManager = false, string channel = "")
        {
            List<GetEmailAddressesTMP> res = new List<GetEmailAddressesTMP>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            try
            {
                if (isForChannelManager)
                {
                    string roleCode = (from a in DATABASE.approvalStates
                                       from b in DATABASE.apprvrRoles
                                       where a.docType == DocTypeId &&
                                        a.stateID == DocumentStatusId && 
                                        a.roleID == b.roleID && (b.roleID == 2 || b.roleID==17)
                                       select b.roleCode).FirstOrDefault();



                    res =
                        (from a in DATABASE.apprvrDesigs
                         from b in DATABASE.userHeaders
                         where a.counterId == b.counterId && a.roleID == 2 && a.channel == channel
                            && b.status.ToUpper()=="ACTIVE"
                            
                         select new GetEmailAddressesTMP()
                         {
                             EmailAddress = a.email
                         }).ToList();

                    /* DISTINCT EMAIL ADDRESS */
                    res = res.GroupBy(o => o.EmailAddress).Select(grp => grp.First()).ToList();
                }
                else
                {
                    string roleCode = (from a in DATABASE.approvalStates
                                       from b in DATABASE.apprvrRoles
                                       where a.docType == DocTypeId &&
                                        a.stateID == DocumentStatusId &&
                                        a.roleID == b.roleID
                                       select b.roleCode).FirstOrDefault();



                    res =
                        (from a in DATABASE.apprvrDesigs
                         from c in DATABASE.userHeaders
                         where a.counterId == c.counterId && (from b in DATABASE.apprvrRoles
                                                              where b.roleCode == roleCode
                                                              select b.roleID).Contains(a.roleID)
                               && a.area == area && c.status.ToUpper()=="ACTIVE"


                         select new GetEmailAddressesTMP()
                         {
                             EmailAddress = a.email
                         }).ToList();

                    /* DISTINCT EMAIL ADDRESS */
                    res = res.GroupBy(o => o.EmailAddress).Select(grp => grp.First()).ToList();
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return res;
        }

        public static List<GetEmailAddressesTMP> GetEmailAddresses(string empId)
        {
            List<GetEmailAddressesTMP> res = new List<GetEmailAddressesTMP>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            try
            {
                res =
                    (from a in DATABASE.userHeaders
                     where a.empIdNo == empId && a.status.ToUpper()=="ACTIVE"

                     select new GetEmailAddressesTMP()
                     {
                         EmailAddress = a.emailAdd
                     }).ToList();

                /* DISTINCT EMAIL ADDRESS */

                res = res.GroupBy(o => o.EmailAddress).Select(grp => grp.First()).ToList();
            }
            finally
            {
                DATABASE.Dispose();
            }
            return res;
        }

        public static void sendMail(string email, string subj, string body, string Type = "EMAIL")
        {
            // place to arms.dbo.scheduler
            ARMS_W.SkelClass.cls_email res = new SkelClass.cls_email();

            try
            {
                res.email_from = "ARMS@matimco.com";
                res.email_to = email;
                res.email_subject = subj;
                res.email_content = body;

                AppHelper.InsertToTable("Scheduler", new Dictionary<string, object>() { 
                        {"Type", Type}
                        ,{"Data", Parser.toJson(res) }
                    });
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        public static void sendEmail1(string docLink, string title, string recipient, string mailServer = "mail2.matimco.com")
        {

            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(recipient);
                message.Subject = title;
                message.From = new System.Net.Mail.MailAddress("arms@matimco.com");
                message.Body = "To view the details, please click this link -->  " + docLink;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("billyjaydelima@matimco.com");
                message.Subject = "Error in Email Sending from Routing Controller.";
                message.From = new System.Net.Mail.MailAddress("ARMS@matimco.com");
                message.Body = "Error message: " + title + " " + ex.Message + ". " + recipient;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);
            }

        }

        public static DocumentStatus getDocumentStatusMessage(int doctypeid, int documentstatusid)
        {
            var res = new DocumentStatus();
            var DATABASE = new Models.ARMSTestEntities();
            try
            {
                var qry = (from a in DATABASE.approvalStates
                           where a.docType == doctypeid && a.stateID == documentstatusid
                           select a);

                foreach (var itm in qry)
                {
                    res.stateID = itm.stateID;
                    res.stateDesc = itm.stateDesc;
                    res.roleID = itm.roleID;
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return res;
        }

        public static void saveRouteChanges(int doctypeid, Globals.DocActionType act_type, int? docstatid, string UserName, string remarks, string DocId, int? prevStatusId=null)
        {

            AppHelper.InsertToTable("RouteChanges", new Dictionary<string, object>() { 
                    {"DocTypeId", doctypeid}
                    , {"ActionType", (int)act_type}
                    , {"DocStatusId", docstatid}
                    , {"PrevDocStatusId", prevStatusId}
                    , {"UserName", UserName}
                    , {"Remarks", remarks}
                    , {"TimeStamp", DateTime.Now}
                    , {"DocId", DocId}
                    , {"RoleCode", GetRoleCode(doctypeid,docstatid) }
                    });
        }

        public static List<Globals.db_route_changes> GetRouteChanges(Globals.InfoType info_type, string DocumentId = null)
        {
            List<Globals.db_route_changes> data = new List<Globals.db_route_changes>();
            Models.ARMSTestEntities DATABASE = new Models.ARMSTestEntities();
            try
            {
                int i_doctype = (int)info_type;
                if (DocumentId != null)
                {
                    var qry =
                    from afcndoroutechanges in DATABASE.RouteChanges
                    from appvrrole in DATABASE.apprvrRoles
                    where
                        afcndoroutechanges.RoleCode == appvrrole.roleCode &&
                        afcndoroutechanges.DocTypeId == i_doctype &&
                        afcndoroutechanges.DocId == DocumentId
                    select new
                    {
                        Actiontype = afcndoroutechanges.ActionType,
                        CurDocStatus = afcndoroutechanges.DocStatusId,
                        PrevDocStatus = afcndoroutechanges.PrevDocStatusId,
                        DateTimeStamp = (DateTime)afcndoroutechanges.TimeStamp,
                        UserName = afcndoroutechanges.UserName,
                        PositionName = appvrrole.roleName,
                        afcndoroutechanges.Remarks
                    };

                    foreach (var itm in qry)
                    {
                        data.Add(new Globals.db_route_changes()
                        {
                            Action = (Globals.DocActionType)Convert.ToInt32(itm.Actiontype),
                            CurDocStatus = itm.CurDocStatus.ToString(),
                            DateTimeStamp = itm.DateTimeStamp,
                            PositionName = itm.PositionName,
                            PrevDocStatus = itm.PrevDocStatus.ToString(),
                            Remarks = itm.Remarks,
                            UserName = itm.UserName
                        });
                    }
                }
                else
                {
                    var qry =
                    from afcndoroutechanges in DATABASE.RouteChanges
                    from appvrrole in DATABASE.apprvrRoles
                    where
                        afcndoroutechanges.RoleCode == appvrrole.roleCode &&
                        afcndoroutechanges.DocTypeId == i_doctype
                    select new
                    {
                        Actiontype = afcndoroutechanges.ActionType,
                        CurDocStatus = afcndoroutechanges.DocStatusId,
                        PrevDocStatus = afcndoroutechanges.PrevDocStatusId,
                        DateTimeStamp = (DateTime)afcndoroutechanges.TimeStamp,
                        UserName = afcndoroutechanges.UserName,
                        PositionName = appvrrole.roleName,
                        afcndoroutechanges.Remarks
                    };

                    foreach (var itm in qry)
                    {
                        data.Add(new Globals.db_route_changes()
                        {
                            Action = (Globals.DocActionType)Convert.ToInt32(itm.Actiontype),
                            CurDocStatus = itm.CurDocStatus.ToString(),
                            DateTimeStamp = itm.DateTimeStamp,
                            PositionName = itm.PositionName,
                            PrevDocStatus = itm.PrevDocStatus.ToString(),
                            Remarks = itm.Remarks,
                            UserName = itm.UserName
                        });
                    }
                }
            }
            finally
            {
                DATABASE.Dispose();
            }
            return data;
        }


        public static List<Globals.db_visit_logs> GetVisitLogs(string LineNum)
        {
            var data = new List<Globals.db_visit_logs>();
            var DATABASE = new Models.ARMSTestEntities();


            var qry_visits = (from a in DATABASE.VisitLogs
                              where a.LineNum == LineNum
                              select a);

            foreach (var itm in qry_visits)
            {
                data.Add(new Globals.db_visit_logs()
                {
                    address = itm.Address,
                    inout = itm.INOUT,
                    latitude = itm.Latitude,
                    longitude = itm.Longitude,
                    datetime = itm.Time.Value,
                    isPlanned = itm.isPlanned == "T" ? "Planned" : "Unplanned"
                });
            }

            return data;
        }


    }
}