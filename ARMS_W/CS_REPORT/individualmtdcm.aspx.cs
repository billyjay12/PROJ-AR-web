using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.OleDb;
using System.IO;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.CS_REPORT
{
    public partial class individualmtdcm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        
        private void LoadReport()
        {

            string foldername = MapPath("~/CS_REPORT/rpt/individual-mtd-cm-report.rpt");

            CrystalDecisions.Web.Parameter myparamss2 = new CrystalDecisions.Web.Parameter();

            string tmp_acctcode = "";
            string cardname = StringHelper.ReCodeCharacters(DropDownList1.Text);
            DataTable user_counterid = SqlDbHelper.getDataDT("SELECT cardcode FROM SAPSERVER.MATIMCO.dbo.arms_vw_individualmtdcmreport WHERE cardname='" + cardname + "' GROUP BY cardcode");
            foreach (DataRow acctCode in user_counterid.Rows)
            {
                tmp_acctcode = acctCode["cardcode"].ToString();
            }

            myparamss2.DefaultValue = tmp_acctcode;
            myparamss2.Name = "Customer Code";

            ReportDocument rpt1 = new ReportDocument();

            rpt1.Load(foldername);

            CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();

            rpt_web.FileName = foldername;
            rpt_web.Parameters.Add(myparamss2);

            CrystalReportSource1.Report = rpt_web;
            CrystalReportSource1.Report.FileName = foldername;
            CrystalReportViewer1.ReportSource = CrystalReportSource1;

            TableLogOnInfos tblinfos = new TableLogOnInfos();
            TableLogOnInfo tblinfo = new TableLogOnInfo();

            tblinfo.ConnectionInfo.DatabaseName = "MATIMCO";
            tblinfo.ConnectionInfo.UserID = "sa";
            tblinfo.ConnectionInfo.Password = "p@ssw0rd";
            tblinfo.ConnectionInfo.ServerName = "192.168.10.15";
            tblinfo.ConnectionInfo.IntegratedSecurity = false;
            tblinfo.ConnectionInfo.AllowCustomConnection = true;
            CrystalReportSource1.Report.Parameters.Add(myparamss2);

            CrystalReportViewer1.LogOnInfo.Add(tblinfo);


            CrystalReportViewer1.RefreshReport();
            Label2.Visible = false;

        }
    }
}