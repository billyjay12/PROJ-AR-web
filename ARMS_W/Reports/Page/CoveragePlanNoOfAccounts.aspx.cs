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
    public partial class CoveragePlanNoOfAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportPath = "/arms reports/Coverage Plan No of Accounts";
           // ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("SOID", dropdwnSOName.SelectedValue) });
           // ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Month", dropdwnSOMonth.SelectedValue) });
           // ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Year", dropdwnSOYear.SelectedValue) });
            ReportViewer1.ServerReport.Refresh();
        }
    }
}