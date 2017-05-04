using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using ARMS_W;

using Microsoft.Reporting.WebForms;
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
    public partial class CustomerCreationLeadTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmb_region.Items.Add(new ListItem("VISMIN","V"));
                cmb_region.Items.Add(new ListItem("LUZON", "L"));

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);

                //ReportViewer1.ServerReport.Refresh();
                
            }
        }

        protected void btn_view_Click(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportPath = "/arms reports/ARMS Code Creation LeadTime";
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("region", cmb_region.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("datefrom", txtStartDate.Text) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("dateto", txtEndDate.Text) });
            ReportViewer1.ServerReport.Refresh();
        }

        protected void cmb_region_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}