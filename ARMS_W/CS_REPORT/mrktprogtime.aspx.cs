using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ARMS_W.CS_REPORT
{
    public partial class mrktprogtime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/mrktprogtimeline.rpt");

            try
            {
                CrystalDecisions.Web.Parameter myparamss = new CrystalDecisions.Web.Parameter();

                myparamss.DefaultValue = TextBox1.Text;
                myparamss.Name = "Marketing Program No";

                ReportDocument rpt1 = new ReportDocument();
                rpt1.Load(foldername);

                CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();

                rpt_web.FileName = foldername;
                rpt_web.Parameters.Add(myparamss);

                CrystalReportSource1.Report = rpt_web;
                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                //CrystalReportSource1.Report.FileName = foldername;
                //CrystalReportSource1.Report.Parameters.Add(myparamss);
                //CrystalReportSource1.Report.Parameters.Add(myparamss2);
                TableLogOnInfos tblinfos = new TableLogOnInfos();
                TableLogOnInfo tblinfo = new TableLogOnInfo();

                tblinfo.ConnectionInfo.DatabaseName = "MATIMCO";
                tblinfo.ConnectionInfo.UserID = "sa";
                tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                tblinfo.ConnectionInfo.ServerName = "192.168.10.15";
                tblinfo.ConnectionInfo.IntegratedSecurity = false;
                tblinfo.ConnectionInfo.AllowCustomConnection = true;

                CrystalReportViewer1.LogOnInfo.Add(tblinfo);


                CrystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                Label2.Text = "Error-" + ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/mrktprogtimeline.rpt");

            try
            {

                CrystalDecisions.Web.Parameter myparamss = new CrystalDecisions.Web.Parameter();

                myparamss.DefaultValue = TextBox1.Text;
                myparamss.Name = "Marketing Program No";

                ReportDocument rpt1 = new ReportDocument();
                rpt1.Load(foldername);

                CrystalDecisions.Web.Report rpt_web = new CrystalDecisions.Web.Report();

                rpt_web.FileName = foldername;
                rpt_web.Parameters.Add(myparamss);

                CrystalReportSource1.Report = rpt_web;
                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                //CrystalReportSource1.Report.FileName = foldername;
                //CrystalReportSource1.Report.Parameters.Add(myparamss);
                //CrystalReportSource1.Report.Parameters.Add(myparamss2);
                TableLogOnInfos tblinfos = new TableLogOnInfos();
                TableLogOnInfo tblinfo = new TableLogOnInfo();

                tblinfo.ConnectionInfo.DatabaseName = "MATIMCO";
                tblinfo.ConnectionInfo.UserID = "sa";
                tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                tblinfo.ConnectionInfo.ServerName = "192.168.10.15";
                tblinfo.ConnectionInfo.IntegratedSecurity = false;
                tblinfo.ConnectionInfo.AllowCustomConnection = true;

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