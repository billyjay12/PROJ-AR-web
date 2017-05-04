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
    public partial class CustomerCreditLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dropdwnRegion.Items.Add(new ListItem("Luzon", "L"));
                dropdwnRegion.Items.Add(new ListItem("Vismin", "V"));


                dropdwnChannel.Items.Add(new ListItem("IS", "IS"));
                dropdwnChannel.Items.Add(new ListItem("MT", "MT"));
                dropdwnChannel.Items.Add(new ListItem("RT", "RT"));
                dropdwnChannel.Items.Add(new ListItem("GT", "GT"));
                dropdwnChannel.Items.Add(new ListItem("All", "All"));


                dropdwnRegion.SelectedValue = "L";
                dropdwnChannel.SelectedValue = "All";
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);

                ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Region", dropdwnRegion.SelectedValue) });
                ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Channel", dropdwnChannel.SelectedValue) });

                ReportViewer1.ServerReport.Refresh();

            }
        }


        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportPath = "/arms reports/Customer_Credit_Line_Data";
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Region", dropdwnRegion.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Channel", dropdwnChannel.SelectedValue) });
            ReportViewer1.ServerReport.Refresh();
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
    }

}