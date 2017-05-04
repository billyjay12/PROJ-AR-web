using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ARMS_W.Class;
using System.Data;


namespace ARMS_W.Reports.Page
{
    public partial class annualinventorycount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initiializeDropDownList();
                ReportViewer1.Visible = false;
            }
        }

        private void GetInfos(string acctCode,string selected_year)
        {
            DataTable mtable1, mtable2 = null;
            string inventoryCountIds = "";
            int counter = 1;

            try
            {
                ReportViewer1.Visible = true;

                mtable1 = SqlDbHelper.getDataDT(@"select * from arms2_vw_getInventoryHeader where acctCode='" + acctCode + "' and year(actualCountDate) in (" + selected_year + ")");

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
                mtable1 = SqlDbHelper.getDataDT(@"select distinct(year(actualCountDate)) year from inventoryCountHdr");
                foreach (DataRow inventoryCountHdr in mtable1.Rows)
                    year.Items.Add(inventoryCountHdr["year"].ToString());

                mtable1 = null;

                // list of account codes
                mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) from inventoryCountHdr");
                foreach (DataRow itm in mtable1.Rows)
                    dropdown_acctcode.Items.Add(itm["acctcode"].ToString());
                
            }
            catch (Exception)
            {
                throw;
            }

        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string acctcode = dropdown_acctcode.SelectedItem.Text;
            string _year = year.SelectedItem.Text;

            if (acctcode != "" && _year != "") { GetInfos(acctcode, _year); }
        }

        protected void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropdown_acctcode.Items.Clear();
            // list of account codes
            var mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) from inventoryCountHdr where year(actualCountDate) in (" + year.SelectedValue + ")");
            foreach (DataRow itm in mtable1.Rows)
                dropdown_acctcode.Items.Add(itm["acctcode"].ToString());
        }



    
    }
}