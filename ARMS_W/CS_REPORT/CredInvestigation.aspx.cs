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
    public partial class CredInvestigation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/Credit Investigation Tab Info.rpt");

            try
            {

                CrystalDecisions.Web.Parameter acctcode = new CrystalDecisions.Web.Parameter();
                string tmp_acctcode = "";
                string cardcode = StringHelper.ReCodeCharacters(DropDownList1.Text);
                DataTable user_counterid = SqlDbHelper.getDataDT("SELECT acctcode FROM customerheader WHERE acctcode='" + cardcode + "' GROUP BY acctcode");
                foreach (DataRow acctCode in user_counterid.Rows)
                {
                    tmp_acctcode = acctCode["acctcode"].ToString();
                }
                acctcode.Name = "Account Code";
                acctcode.DefaultValue = tmp_acctcode;
                ReportDocument rpt1 = new ReportDocument();
                rpt1.Load(foldername);

                CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();

                rpt_web.FileName = foldername;
                rpt_web.Parameters.Add(acctcode);


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
                CrystalReportSource1.Report.Parameters.Add(acctcode);

                CrystalReportViewer1.LogOnInfo.Add(tblinfo);


                CrystalReportViewer1.RefreshReport();
                Label2.Visible = false;
            }
            catch (Exception ex)
            {
                Label2.Visible = true;
                Label2.Text = "Error: " + ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string foldername = Server.MapPath("~/CS_REPORT/rpt/Credit Investigation Tab Info.rpt");
            try
            {
                string tmp_acctcode = "";
                string cardcode = StringHelper.ReCodeCharacters(DropDownList1.Text);
                DataTable user_counterid = SqlDbHelper.getDataDT("SELECT acctcode FROM customerheader WHERE acctcode='" + cardcode + "' GROUP BY acctcode");
                foreach (DataRow acctCode in user_counterid.Rows)
                {
                    tmp_acctcode = acctCode["acctcode"].ToString();
                }
                    Label2.Visible = false;
                   
                    CrystalDecisions.Web.Parameter acctcode = new CrystalDecisions.Web.Parameter();
                    acctcode.Name = "Account Code";
                    acctcode.DefaultValue = tmp_acctcode;

                    ReportDocument rpt1 = new ReportDocument();

                    CrystalReportSource1.Report.FileName = foldername;

                    CrystalReportViewer1.ReportSource = CrystalReportSource1;
                    TableLogOnInfos tblinfos = new TableLogOnInfos();
                    TableLogOnInfo tblinfo = new TableLogOnInfo();

                    tblinfo.ConnectionInfo.DatabaseName = "ARMS";
                    tblinfo.ConnectionInfo.UserID = "sa";
                    tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                    tblinfo.ConnectionInfo.ServerName = "192.168.10.13";
                    tblinfo.ConnectionInfo.IntegratedSecurity = false;
                    tblinfo.ConnectionInfo.AllowCustomConnection = true;
                    CrystalReportSource1.Report.Parameters.Add(acctcode);

                    CrystalReportViewer1.LogOnInfo.Add(tblinfo);

                    CrystalReportViewer1.RefreshReport();
                   
                
            }
            catch (Exception ex)
            {
                Label2.Visible = true;
                Label2.Text = "Error: " + ex.Message;
            }
        }
    }
}
