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
    public partial class arms2_ActivityLogReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTimeFormatInfo mrf = new DateTimeFormatInfo();

            if (!Page.IsPostBack)
            {

                var DATABASE = new ARMS_W.Models.ARMSTestEntities();
                var current_user = new ARMS_W.Class._User(Session["username"].ToString());
                List<int> monthss = new List<int>();
                var qryMonths = (from a in DATABASE.CoverageHdrs
                                 select a.Month).Distinct().ToList();
                foreach (var itm in qryMonths)
                {
                    monthss.Add(Int16.Parse(itm));
                }
                monthss = monthss.OrderBy(o => o).ToList();
                DATABASE.Dispose();

                cmb_empname.Items.Add(new ListItem("ALL", "N/A"));
                foreach (var so in UserDefineFunctions.Application.ListOfSalesOfficer(Session["userid"].ToString()))
                {
                    cmb_empname.Items.Add(new ListItem(so.empFullName, so.empIDNo));
                }

                

                cmb_empname.SelectedValue = Session["userid"].ToString();
               // datefrom.SelectedValue = DateTime.Today.Month.ToString();
                //cmb_year.SelectedValue = DateTime.Today.Year.ToString();
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);

                if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
                {
                    ReportViewer1.ServerReport.ReportPath = "";
                }
                else
                {
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("EmpId", cmb_empname.SelectedValue) });
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("month", txtStartDate.Text) });
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("year", txtEndDate.Text) });
                    ReportViewer1.ServerReport.Refresh();
                }
                DATABASE.Dispose();
            }

        }

        protected void cmb_empname_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void btn_view_Click(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportPath = "/arms reports/activitylogreport";
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("EmpId", cmb_empname.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("datefrom", txtStartDate.Text) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("dateto", txtEndDate.Text) });
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