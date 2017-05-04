using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.Class
{
    public class _Document
    {

        private string _Channel;
        public string Channel { get { return _Channel; } }

        private string _Area;
        public string Area { get { return _Area; } }

        private string _Region;
        public string Region { get { return _Region; } }

        private string _DocStatus;
        public string DocStatus { get { return _DocStatus; } }

        private string _AccountCode;
        public string AccountCode { get { return _AccountCode; } }

        //private string _AcctOfficer;
        //public string _AcctOfficer { get { return _AcctOfficer; } }

        private string _DOCID;

        public _Document(string doctype, string docid) 
        {

            _DOCID = docid;

            try 
            {
                // CUSTOMER ACCOUNT CREATION 
                if (doctype == "CCA")
                {
                    string strQuery = @"
                        select a.acctcode, a.status, 
                        case when bb.area != '' and bb.area is not null then bb.area else a.area end as 'area', a.region, 
                        case when bb.area != '' and bb.area is not null then 
	                        isnull((select c.descript from SAPSERVER.MATIMCO.dbo.oter b , SAPSERVER.MATIMCO.dbo.oter c where b.parent=c.territryid and b.descript=bb.area collate SQL_Latin1_General_CP850_CI_AS ), '')
	                        else
	                        isnull((select c.descript from SAPSERVER.MATIMCO.dbo.oter b , SAPSERVER.MATIMCO.dbo.oter c where b.parent=c.territryid and b.descript=a.area collate SQL_Latin1_General_CP850_CI_AS ), '')
                        end as 'channel' 
                        from customerheader a left join proposedchangesca bb on a.ccanum=bb.ccanum
                        where a.ccanum='" + docid + "'";
                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows) 
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                    }
                }

                // LEAD CREATION - customerLeadI
                if (doctype == "LDI")
                {
                    string strQuery = "" +
                        "select a.requestid, a.status, c.area, c.channel, c.grp_name, " +
                        "case when right(a.ProposedChannel,1) = 'L' then 'LUZON' else 'VISMIN' end as 'region', sapleadcode as 'acctcode' " +
                        "from customerleadi a left join  " +
                        "SAPSERVER.MATIMCO.dbo.oslp b on a.AssignTo_empId=b.slpcode  " +
                        "left join ChannelGroup c on c.area collate SQL_Latin1_General_CP850_CI_AS = (b.u_area + ' - ' + b.u_area) " +
                        "where a.requestid=" + docid + " " +
                        "";
                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows)
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                    }
                }

                // CONTRACTS AND AGREEMENTS
                if (doctype == "MMA") 
                {
                    string strQuery = "" +
                        "select a.agreeno, a.acctcode, a.status, " +
                        "case when charindex('LUZON',c.channel) > 0 then 'LUZON' when charindex('VISMIN',c.channel) > 0 then 'VISMIN' else '' end as 'region', " +
                        "isnull(c.channel,'') as 'channel', isnull(c.area,'') as 'area' " +
                        "from mtgMinutesAgreement a inner join customerHeader b on a.acctcode=b.acctcode  " +
                        "left join ChannelGroup c on c.area=b.area where a.agreeno='" + docid + "'" +
                        "";
                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows)
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                    }
                }

                // FOR BUSINESS REVIEW
                if (doctype == "BR")
                {
                    string strQuery = "" +
                    "SELECT d.busReviewNo, a.acctcode, a.status, a.area, a.region,isnull((select c.descript from SAPSERVER.MATIMCO.dbo.oter b , SAPSERVER.MATIMCO.dbo.oter c " +
                    "where b.parent=c.territryid and b.descript=a.area collate SQL_Latin1_General_CP850_CI_AS),'') as 'Channel' FROM dbo.busReview d inner join customerHeader a " +
                    "on a.ccanum = d.ccanum and d.busReviewNo='" + docid + "'";

                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows)
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                      
                    }
                }

                // FOR EMAT
                if (doctype == "EM")
                {
                    string strQuery = "" +
                      "SELECT a.ematno, a.acctcode, a.status, d.area, d.region, isnull((select c.descript " +
                      "from SAPSERVER.MATIMCO.dbo.oter b , SAPSERVER.MATIMCO.dbo.oter c " +
                      "where b.parent=c.territryid and b.descript=d.area  collate SQL_Latin1_General_CP850_CI_AS), '') as 'channel' " +
                      "FROM emat a inner join customerheader d on a.acctcode = d.acctcode where a.ematno='" + docid + "'" +
                        "";
                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows)
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                    }
                }

                // LEAD DATABASE - customerLeadDB
                if (doctype == "LDB")
                {
                    string strQuery = "" +
                        "select a.requestid, z.status, c.area, c.channel, c.grp_name, " +
                        "case when right(a.ProposedChannel,1) = 'L' then 'LUZON' else 'VISMIN' end as 'region', a.sapleadcode as 'acctcode' " +
                        "from customerleadi a inner join customerLeadDb z on a.requestid=z.requestid left join " +
                        "SAPSERVER.MATIMCO.dbo.oslp b on a.AssignTo_empId=b.slpcode " +
                        "left join ChannelGroup c on c.area collate SQL_Latin1_General_CP850_CI_AS = (b.u_area + ' - ' + b.u_area) " +
                        "where z.requestid=" + docid + " " +
                        "";
                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows)
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                    }
                }

                // MARKETING REQUEST
                if (doctype == "MKR")
                {
                    string strQuery = "" +
                        "select a.acctcode, b.status, a.area, a.region, isnull((select c.descript " +
                        "from SAPSERVER.MATIMCO.dbo.oter b , SAPSERVER.MATIMCO.dbo.oter c " +
                        "where b.parent=c.territryid and b.descript=a.area collate SQL_Latin1_General_CP850_CI_AS " +
                        "), '') as 'channel' from customerheader a inner join marktingRequest b on " +
                        "a.ccanum=b.ccanum " +
                        "where b.reqid='" + docid + "'" +
                        "";
                    DataTable _docinfos = SqlDbHelper.getDataDT(strQuery);

                    foreach (DataRow itm in _docinfos.Rows)
                    {
                        this._Channel = itm["channel"].ToString().Trim();
                        this._Area = itm["area"].ToString().Trim();
                        this._Region = itm["region"].ToString().Trim();
                        this._DocStatus = itm["status"].ToString().Trim();
                        this._AccountCode = itm["acctcode"].ToString().Trim();
                    }
                }


            }
            catch (Exception ex) 
            { 
                // empty
                this._Channel = "";
                this._Area = "";
                this._Region = "";
                this._DocStatus = "";
                this._AccountCode = "";
            }
            
        }

        /*
         FOR CUSTOMERLEADI USE ONLY
         */
        public bool IsInDocGroupChannel(string usr_channel) 
        {
            
            string strQuery = "" +
                "select a.channel " +
                "from ChannelGroup a, customerleadi b " +
                "where a.channel='" + usr_channel + "' and a.grp_name=b.ProposedChannel " +
                "and b.requestid='" + this._DOCID + "' " +
                "";
            DataTable grpchannel = SqlDbHelper.getDataDT(strQuery);

            foreach (DataRow itm in grpchannel.Rows) 
            {
                return true;
            }

            return false;
        }
    }
}
