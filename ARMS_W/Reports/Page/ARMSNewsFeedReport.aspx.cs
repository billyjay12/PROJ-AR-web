using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARMS_W.Models;
using Microsoft.Reporting.WebForms;
using System.Globalization;
//
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Web.Configuration;

namespace ARMS_W.Reports.Page
{
    public partial class ARMSNewsFeedReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dropdownRegion.Items.Add(new ListItem("LUZON", "LZ"));
                dropdownRegion.Items.Add(new ListItem("VISMIN", "VM"));

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);
                
                ReportViewer1.ServerReport.ReportPath = dropdownRegion.SelectedValue == "LZ" ? "/arms reports/ARMS News Feed (LUZON)" : "/arms reports/ARMS News Feed (VISMIN)";

                //if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
               // {
                //    ReportViewer1.ServerReport.ReportPath = "";
               // }
               // else
              //  {
             //       ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("EmpId", comboSO.SelectedValue) });
             //       ReportViewer1.ServerReport.Refresh();
             //   }
            }
        }

        protected void dropdownRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

            ReportViewer1.ServerReport.ReportPath = dropdownRegion.SelectedValue == "LZ" ? "/arms reports/ARMS News Feed (LUZON)" : "/arms reports/ARMS News Feed (VISMIN)";
            ReportViewer1.ServerReport.Refresh();
        }
    }
}