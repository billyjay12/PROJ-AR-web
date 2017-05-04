using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace ARMS_W.CS_REPORT
{
    public partial class cmperacc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string foldername = Server.MapPath("~/CS_REPORT/rpt/cmperaccount.rpt");

            try
            {
                string strpath = MapPath("~/CS_REPORT/rpt/cmperaccount.rpt");
                //string strpath = Server.MapPath("~/CS_REPORT/rpt/ftmandytdsellinperareareport.rpt");

                ReportDocument report = new ReportDocument();

                CrystalDecisions.Shared.TableLogOnInfos crtableLogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtableLogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();
                CrystalDecisions.Shared.ConnectionInfo crConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
                Tables CrTables;

                CrystalReportSource1.Report.FileName = strpath;
                report.Load(strpath);

                crConnectionInfo.ServerName = "192.168.10.15";
                crConnectionInfo.DatabaseName = "MATIMCO";
                crConnectionInfo.UserID = "sa";
                crConnectionInfo.Password = "p@ssw0rd";


                CrTables = report.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.RefreshReport();
                Label2.Text = CrystalReportViewer1.ParameterFieldInfo[0].Name + "-" + CrystalReportViewer1.ParameterFieldInfo[1].Name;
            }
            catch (Exception ex)
            {
                Label2.Text = "Error-" + ex.Message;
            }
        }
    }
}