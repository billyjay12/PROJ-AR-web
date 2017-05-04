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
    
    public partial class SOMonthlyWorkPlan : System.Web.UI.Page
    {
        DateTimeFormatInfo mrf = new DateTimeFormatInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var current_user = new ARMS_W.Class._User(Session["username"].ToString());
                List<ARMS_W.Models.Arms_vw_SOLookUps> LiSoName = new List<Models.Arms_vw_SOLookUps>();
                ARMS_W.Models.ARMSTestEntities armsEntities = new Models.ARMSTestEntities();
                List<int> monthss = new List<int>();
                var qryMonths = (from a in armsEntities.CoverageHdrs
                                   select a.Month).Distinct().ToList();
                foreach (var itm in qryMonths)
                {
                    monthss.Add(Int16.Parse(itm));
                }
                monthss = monthss.OrderBy(o => o).ToList();


                LiSoName = armsEntities.Arms_vw_SOLookUps.OrderBy(p => p.Name).ToList();
                //foreach (ARMS_W.Models.Arms_vw_SOLookUps soName in new ARMS_W.Models.ARMSTestEntities().Arms_vw_SOLookUps.OrderBy(p => p.Name).ToList())
                foreach (var so in UserDefineFunctions.Application.ListOfSalesOfficer(Session["userid"].ToString()))
                {
                    dropdwnSOName.Items.Add(new ListItem(so.empFullName, so.empIDNo));
                }
              //  foreach (string month in new ARMS_W.Models.ARMSTestEntities().CoverageHdrs.Select(p => p.Month).Distinct().ToList())
                foreach (int month in monthss)
                {
                    dropdwnSOMonth.Items.Add(new ListItem(mrf.GetMonthName(month == 0 ? 1 : month),  month.ToString()));
                }
                foreach (int year in new ARMS_W.Models.ARMSTestEntities().CoverageHdrs.Select(p => p.Year).Distinct().ToList())
                {
                    dropdwnSOYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                dropdwnSOName.SelectedValue = Session["userid"].ToString();
                dropdwnSOMonth.SelectedValue = DateTime.Today.Month.ToString();
                dropdwnSOYear.SelectedValue = DateTime.Today.Year.ToString();
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);
                    

                if (current_user.Roles.Any(p => p.Position == "SPRUSER"))
                {
                    ReportViewer1.ServerReport.ReportPath = "";
                }
                else
                {
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("SOID", dropdwnSOName.SelectedValue) });
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Month", dropdwnSOMonth.SelectedValue) });
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Year", dropdwnSOYear.SelectedValue) });

                    ReportViewer1.ServerReport.Refresh();
                }
            }


            

        }


        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportPath = "/arms reports/somonthlyworkplan";
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("SOID", dropdwnSOName.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Month", dropdwnSOMonth.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("Year", dropdwnSOYear.SelectedValue) });
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