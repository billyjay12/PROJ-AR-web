﻿using System;
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
    public partial class InventoryCountReport : System.Web.UI.Page
    {
        DateTimeFormatInfo mrf = new DateTimeFormatInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var DATABASE = new Models.ARMSTestEntities();
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
                //foreach (Models.Arms_vw_SOLookUps so in new ARMSTestEntities().Arms_vw_SOLookUps.OrderBy(p => p.Name).OrderBy(p => p.Name).ToList())
                comboSO.Items.Add(new ListItem("ALL","all"));
                foreach (var so in UserDefineFunctions.Application.ListOfSalesOfficer(Session["userid"].ToString()))
                {
                    comboSO.Items.Add(new ListItem(so.empFullName, so.empIDNo));
                }
                foreach (int month in monthss)
                {
                    comboMonth.Items.Add(new ListItem(mrf.GetMonthName(month == 0 ? 1 : month), month.ToString()));
                }
                foreach (int year in new ARMSTestEntities().CoverageHdrs.Select(p => p.Year).Distinct().ToList())
                {
                    comboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                comboSO.SelectedValue = Session["userid"].ToString();
                comboMonth.SelectedValue = DateTime.Today.Month.ToString();
                comboYear.SelectedValue = DateTime.Today.Year.ToString();
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);

                if (current_user.Roles.Any(o => o.Position == "SPRUSER"))
                {
                    ReportViewer1.ServerReport.ReportPath = "";
                }
                else
                {
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("month", comboMonth.SelectedValue) });
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("year", comboYear.SelectedValue) });
                    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("empid", comboSO.SelectedValue) });
                    ReportViewer1.ServerReport.Refresh();
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportPath = "/arms reports/inventorycountreport";
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("month", comboMonth.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("year", comboYear.SelectedValue) });
            ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>() { new ReportParameter("empid", comboSO.SelectedValue) });
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