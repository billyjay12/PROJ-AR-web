using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ARMS_W.Class;

using Microsoft.Reporting.WebForms;

namespace ARMS_W.Reports.Page
{
    public partial class quarterlyinventorycount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initiializeDropDownList();
                ReportViewer1.Visible = false;
            }
        }

        private void GetInfos(string acctCode)
        {
            DataTable mtable1, mtable2 = null;
            string inventoryCountIds = "";
            int counter = 1;

            try
            {

                ReportViewer1.Visible = true;

                mtable1 = SqlDbHelper.getDataDT(@"select * from arms2_vw_getInventoryHeader where acctCode='" + acctCode + "' and DATENAME(Month,actualCountDate) in (" + getQuarterMonths() + ")");

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_inventorycounthdr", mtable1));

                foreach (DataRow itm in mtable1.Rows)
                {
                    inventoryCountIds += "'" + itm["inventoryCountId"].ToString() + "'";
                    if (counter < mtable1.Rows.Count)
                        inventoryCountIds += ",";

                    counter++;
                }
                

                if (inventoryCountIds == "") throw new Exception("No Result.");


                mtable2 = SqlDbHelper.getDataDT(@"select * from arms2_vw_rptInventoryCount where inventorycountId in(" + inventoryCountIds + ") order by month");

                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_inventorycountdtl", mtable2));

                ReportViewer1.LocalReport.Refresh();

            }
            catch (Exception ex)
            {
                ReportViewer1.Visible = false; Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        private void initiializeDropDownList()
        {
            DataTable mtable1 = null;

            try
            {
                //drop down list for quarter
                #region DROP DOWN LIST OF QUARTER

                month.Items.Add("1st quarter");
                month.Items.Add("2nd quarter");
                month.Items.Add("3rd quarter");
                month.Items.Add("4th quarter");

                #endregion

                // list of account codes
                mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) from inventoryCountHdr where DATENAME(Month,actualCountDate) in (" + getQuarterMonths() + ")");
                foreach (DataRow itm in mtable1.Rows)
                    dropdown_acctcode.Items.Add(itm["acctcode"].ToString());


                


            }
            catch (Exception)
            {

                throw;
            }

        }

        private string getQuarterMonths()
        {
            if (month.SelectedItem.Text == "1st quarter") { return "'January','February','March'"; }
            else if (month.SelectedItem.Text == "2nd quarter") { return "'April','May','June'"; }
            else if (month.SelectedItem.Text == "3rd quarter") { return "'July','August','September'"; }
            else { return "'October','November','December'"; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string acctcode = dropdown_acctcode.SelectedItem.Text;
            string _quarter = month.SelectedItem.Text;

            if (acctcode != "" && _quarter != "") { GetInfos(acctcode); }
        }

        protected void month_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropdown_acctcode.Items.Clear();
            // list of account codes
            var mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) from inventoryCountHdr where  DATENAME(Month,actualCountDate) in (" + getQuarterMonths() + ")");
            foreach (DataRow itm in mtable1.Rows)
                dropdown_acctcode.Items.Add(itm["acctcode"].ToString());
        }


    }
}