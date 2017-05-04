using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ARMS_W.Class;
using System.Data.OleDb;
using System.IO;
using System.Data;


namespace ARMS_W.CS_REPORT
{
    public partial class ListOfCreatedCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/List of Customers.rpt");

            try 
            {
                ReportDocument rpt1 = new ReportDocument();
                CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();
                CrystalDecisions.Web.Parameter par_date = new CrystalDecisions.Web.Parameter();

                par_date.Name = "AsOfDate";
                par_date.DefaultValue = txt_date.Text;

                rpt_web.Parameters.Add(par_date);

                CrystalReportSource1.Report.Parameters.Add(par_date);
                rpt1.Load(foldername);
                rpt_web.FileName = foldername;
                CrystalReportSource1.Report = rpt_web;
                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                TableLogOnInfos tblinfos = new TableLogOnInfos();
                TableLogOnInfo tblinfo = new TableLogOnInfo();
                tblinfo.ConnectionInfo.DatabaseName = "ARMS";
                tblinfo.ConnectionInfo.UserID = "sa";
                tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                tblinfo.ConnectionInfo.ServerName = "192.168.10.13";
                tblinfo.ConnectionInfo.IntegratedSecurity = false;
                tblinfo.ConnectionInfo.AllowCustomConnection = true;

                CrystalReportViewer1.LogOnInfo.Add(tblinfo);

                CrystalReportViewer1.RefreshReport();
            }
            catch (Exception ex) 
            { 
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/List of Customers.rpt");

            try
            {
                ReportDocument rpt1 = new ReportDocument();
                CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();
                CrystalDecisions.Web.Parameter par_date = new CrystalDecisions.Web.Parameter();

                par_date.Name = "AsOfDate";
                par_date.DefaultValue = txt_date.Text;

                rpt_web.Parameters.Add(par_date);

                CrystalReportSource1.Report.Parameters.Add(par_date);
                rpt1.Load(foldername);
                rpt_web.FileName = foldername;
                CrystalReportSource1.Report = rpt_web;
                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                TableLogOnInfos tblinfos = new TableLogOnInfos();
                TableLogOnInfo tblinfo = new TableLogOnInfo();
                tblinfo.ConnectionInfo.DatabaseName = "ARMS";
                tblinfo.ConnectionInfo.UserID = "sa";
                tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                tblinfo.ConnectionInfo.ServerName = "192.168.10.13";
                tblinfo.ConnectionInfo.IntegratedSecurity = false;
                tblinfo.ConnectionInfo.AllowCustomConnection = true;

                CrystalReportViewer1.LogOnInfo.Add(tblinfo);

                CrystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
