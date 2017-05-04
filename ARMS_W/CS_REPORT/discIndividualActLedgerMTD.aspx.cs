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
    public partial class discIndividualActLedgerMTD : System.Web.UI.Page
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

            string foldername = Server.MapPath("~/CS_REPORT/rpt/Disc-RunningMTD-IndividualAccount.rpt");

            try
            {
                CrystalDecisions.Web.Parameter customercode = new CrystalDecisions.Web.Parameter();
                string tmp_acctcode = "";
                string cardname = StringHelper.ReCodeCharacters(DropDownList1.Text);
                DataTable user_counterid = SqlDbHelper.getDataDT("SELECT cardcode FROM SAPSERVER.MATIMCO.dbo.arms_vw_discount WHERE cardname='" + cardname + "' GROUP BY cardcode");
                foreach (DataRow acctCode in user_counterid.Rows)
                {
                    tmp_acctcode = acctCode["cardcode"].ToString();
                }
                customercode.Name = "CustomerCode";
                customercode.DefaultValue = tmp_acctcode;


                ReportDocument rpt1 = new ReportDocument();

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

                CrystalReportSource1.Report.Parameters.Add(customercode);
                CrystalReportViewer1.LogOnInfo.Add(tblinfo);

                CrystalReportViewer1.RefreshReport();
                
            }
            catch (Exception ex)
            {
                Label2.Text = "Error-" + ex.Message;
            }
        }
    }
}