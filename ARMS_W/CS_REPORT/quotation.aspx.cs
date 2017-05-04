using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace ARMS_W.CS_REPORT
{
    public partial class quotation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string foldername = Server.MapPath("~/CS_REPORT/rpt/quotation.rpt");

            try
            {
                CrystalDecisions.Web.Parameter CashOnDelivery = new CrystalDecisions.Web.Parameter();
                CrystalDecisions.Web.Parameter SalesQuotationNumber = new CrystalDecisions.Web.Parameter();
                CashOnDelivery.Name = "Cash On Delivery";
                SalesQuotationNumber.Name = "Sales Quotation Number";
                SalesQuotationNumber.DefaultValue = "0";
                CashOnDelivery.DefaultValue = "Yes";


                ReportDocument rpt1 = new ReportDocument();

                CrystalReportSource1.Report.FileName = foldername;

                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                TableLogOnInfos tblinfos = new TableLogOnInfos();
                TableLogOnInfo tblinfo = new TableLogOnInfo();

                tblinfo.ConnectionInfo.DatabaseName = "MATIMCO";
                tblinfo.ConnectionInfo.UserID = "sa";
                tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                tblinfo.ConnectionInfo.ServerName = "192.168.10.15";
                tblinfo.ConnectionInfo.IntegratedSecurity = false;
                tblinfo.ConnectionInfo.AllowCustomConnection = true;
                CrystalReportSource1.Report.Parameters.Add(SalesQuotationNumber);
                CrystalReportSource1.Report.Parameters.Add(CashOnDelivery);
                CrystalReportViewer1.LogOnInfo.Add(tblinfo);

                CrystalReportViewer1.RefreshReport();
                Label2.Text = CrystalReportViewer1.ParameterFieldInfo[0].Name + "-" + CrystalReportViewer1.ParameterFieldInfo[1].Name;
            }
            catch (Exception ex)
            {
                //Label2.Text = "Error-" + ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/quotation.rpt");

            try
            {
                CrystalDecisions.Web.Parameter CashOnDelivery = new CrystalDecisions.Web.Parameter();
                CrystalDecisions.Web.Parameter SalesQuotationNumber = new CrystalDecisions.Web.Parameter();
                CashOnDelivery.Name = "Cash On Delivery";
                SalesQuotationNumber.Name = "Sales Quotation Number";
                SalesQuotationNumber.DefaultValue = TextBox1.Text;
                CashOnDelivery.DefaultValue = DropDownList1.Text;


                ReportDocument rpt1 = new ReportDocument();

                CrystalReportSource1.Report.FileName = foldername;

                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                TableLogOnInfos tblinfos = new TableLogOnInfos();
                TableLogOnInfo tblinfo = new TableLogOnInfo();

                tblinfo.ConnectionInfo.DatabaseName = "MATIMCO";
                tblinfo.ConnectionInfo.UserID = "sa";
                tblinfo.ConnectionInfo.Password = "p@ssw0rd";
                tblinfo.ConnectionInfo.ServerName = "192.168.10.15";
                tblinfo.ConnectionInfo.IntegratedSecurity = false;
                tblinfo.ConnectionInfo.AllowCustomConnection = true;
                CrystalReportSource1.Report.Parameters.Add(SalesQuotationNumber);
                CrystalReportSource1.Report.Parameters.Add(CashOnDelivery);
                CrystalReportViewer1.LogOnInfo.Add(tblinfo);

                CrystalReportViewer1.RefreshReport();
                Label2.Text = CrystalReportViewer1.ParameterFieldInfo[0].Name + "-" + CrystalReportViewer1.ParameterFieldInfo[1].Name;
            }
            catch (Exception ex)
            {
                //Label2.Text = "Error-" + ex.Message;
            }
        }


    }
}