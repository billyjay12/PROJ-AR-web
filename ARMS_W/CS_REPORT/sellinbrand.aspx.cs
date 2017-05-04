using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ARMS_W.Class;
using System.Data.OleDb;
using System.Data;


namespace ARMS_W.CS_REPORT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport() {
            
            string foldername = Server.MapPath("~/CS_REPORT/rpt/sellinbrand.rpt");

            try
            {
                CrystalDecisions.Web.Parameter acctcode = new CrystalDecisions.Web.Parameter();


                ReportDocument rpt1 = new ReportDocument();
                CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();
                string tmp_acctcode = "";
                string cardname = StringHelper.ReCodeCharacters(DropDownList1.Text);
                DataTable user_counterid = SqlDbHelper.getDataDT("SELECT cardcode FROM SAPSERVER.MATIMCO.dbo.arms_vw_individualsellinperbrand WHERE cardname='" + cardname + "' GROUP BY cardcode");
                foreach (DataRow acctCode in user_counterid.Rows)
                {
                    tmp_acctcode = acctCode["cardcode"].ToString();
                }
                acctcode.Name = "Customer Code";
                acctcode.DefaultValue = tmp_acctcode;
                rpt_web.Parameters.Add(acctcode);


                CrystalReportSource1.Report.Parameters.Add(acctcode);
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
                Label2.Visible = false;

            }
            catch (Exception ex)
            {

            }
        
        }
    }
}


