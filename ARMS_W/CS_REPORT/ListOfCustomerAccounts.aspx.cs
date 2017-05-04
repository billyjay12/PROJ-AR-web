using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ARMS_W.Class;
using System.IO;
using System.Data;

namespace ARMS_W.CS_REPORT
{
    public partial class ListOfCustomerAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FilterReport("page_load");
        }

        private void FilterReport(string action) 
        {

            string foldername = Server.MapPath("~/CS_REPORT/rpt/ListOfCustomerAccounts.rpt");
            _User Ousr = new _User(Session["username"].ToString());

            string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
            string[] AreaUsers = { "asm" };
            string[] ChannelUsers = { "chm", "cmg", "ca", "cmm" };
            string[] OtherUsers = { "vpbsm", "vptfi", "ceo", "ssm", "ssgm", "brd", "admin" };

            ReportDocument ReportDoc = new ReportDocument();
            ReportDoc.Load(foldername);
            ReportDoc.SetDatabaseLogon("sa", "p@ssw0rd");

            CrystalReportViewer1.ReportSource = ReportDoc;
        }
    }
}