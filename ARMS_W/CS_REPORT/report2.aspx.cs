using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Web.UI.WebControls;

namespace ARMS_W.CS_REPORT
{
    public partial class report2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rpt1 = new ReportDocument();
            string foldername = Server.MapPath("~/CS_REPORT/rpt/sellinbrand.rpt");
           
            try 
            {
                if (File.Exists(foldername) == true)
                {
                    Label1.Text = "Exist" + foldername;
                }
                else 
                {
                    Label1.Text = "Does not Exist" + foldername;
                }
                /*
                rpt1.Load("U:\\Crystal Reports\\sellinbrand.rpt");

                rpt1.Refresh();

                CrystalReportViewer1.ReportSource = rpt1;
                */
                
            }
            catch (Exception ex) 
            {
                Label1.Text = ex.Message;
            }
            
        }
    }
}