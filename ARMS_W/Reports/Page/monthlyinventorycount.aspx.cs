using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using ARMS_W.Class;
//using System.Device.Location;

namespace ARMS_W.Reports.Page
{
    public partial class monthlyinventorycount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initiializeDropDownList();
                ReportViewer1.Visible = false;
               // GetInfos("", "");
                //if (Request.QueryString["acctCode"] != null && Request.QueryString["month"]!=null)
                //{
                //    if (Request.QueryString["acctCode"].ToString() != "" && Request.QueryString["month"] != null)
                //    {
                //        string pageparam_acctCode = Request.QueryString["batchid"].ToString();
                //        string pageparam_month = Request.QueryString["month"].ToString();

                //        GetInfos(pageparam_acctCode,pageparam_month);
                //    }

                //}

            }
        }

        private void GetInfos(string acctCode,string month,string year)
        {
            DataTable mtable1, mtable2 = null;
            string inventoryCountId="";

            try
            {

                ReportViewer1.Visible = true;
                mtable1 = SqlDbHelper.getDataDT(@"select * from arms2_vw_getInventoryHeader where acctCode='" + acctCode + "' and DATENAME(Month,actualCountDate)='" + month + "' and YEAR(actualCountDate)='" + year + "' ");

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_inventorycounthdr", mtable1));

                foreach (DataRow itm in mtable1.Rows) { inventoryCountId = itm["inventoryCountId"].ToString(); break; }

                if (inventoryCountId == "") throw new Exception("No Result.");

                mtable2 = SqlDbHelper.getDataDT(@"select * from arms2_vw_getInventoryDetail where inventoryCountId = '" + inventoryCountId + "'");

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

                //drop down list for month
                #region DROP DOWN LIST OF MONTH

                month.Items.Add("January");
                month.Items.Add("February");
                month.Items.Add("March");
                month.Items.Add("April");
                month.Items.Add("May");
                month.Items.Add("June");
                month.Items.Add("July");
                month.Items.Add("August");
                month.Items.Add("September");
                month.Items.Add("October");
                month.Items.Add("November");
                month.Items.Add("December");

                #endregion

                // list of account codes
             //   mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) from inventoryCountHdr where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "'");
            //    foreach (DataRow itm in mtable1.Rows)
           //         dropdown_acctcode.Items.Add(itm["acctcode"].ToString());

                 mtable1 = SqlDbHelper.getDataDT(@"select b.acctcode,acctName
                                                  from inventoryCountHdr a inner join arms2_vw_customerheader_lookup b on a.acctcode=b.acctcode
                                                  where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "' group by b.acctcode,acctName");

                 foreach (DataRow itm in mtable1.Rows)
                     dropdown_acctcode.Items.Add(new ListItem(itm["acctName"].ToString(), itm["acctcode"].ToString()));

                 var mtable2 = SqlDbHelper.getDataDT(@"select year(actualCountDate) _year
                                                  from inventoryCountHdr 
                                                  where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "' group by  year(actualCountDate)");


                 

                 foreach (DataRow itm in mtable2.Rows)
                     dropdown_year.Items.Add(new ListItem(itm["_year"].ToString(), itm["_year"].ToString()));
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        protected void month_SelectedIndexChanged(object sender, EventArgs e)
        {

           // getlonglat();
            dropdown_acctcode.Items.Clear();
            // list of account codes
//            var mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) 
//                                                  from inventoryCountHdr 
//                                                  where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "'");
            var mtable1 = SqlDbHelper.getDataDT(@"select b.acctcode,acctName
                                                  from inventoryCountHdr a inner join arms2_vw_customerheader_lookup b on a.acctcode=b.acctcode
                                                  where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "' group by b.acctcode,acctName");
                                                    

           

            foreach (DataRow itm in mtable1.Rows)
                dropdown_acctcode.Items.Add(new ListItem(itm["acctName"].ToString(), itm["acctcode"].ToString()));

            dropdown_year.Items.Clear();
            // list of account codes
            //            var mtable1 = SqlDbHelper.getDataDT(@"select distinct(acctcode) 
            //                                                  from inventoryCountHdr 
            //                                                  where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "'");
            var mtable2 = SqlDbHelper.getDataDT(@"select year(actualCountDate) _year
                                                  from inventoryCountHdr 
                                                  where DATENAME(MONTH,actualCountDate)='" + (month.SelectedValue == "" ? "January" : month.SelectedValue) + "' group by  year(actualCountDate)");



            foreach (DataRow itm in mtable2.Rows)
                dropdown_year.Items.Add(new ListItem(itm["_year"].ToString(), itm["_year"].ToString()));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string acctcode = dropdown_acctcode.SelectedValue; // dropdown_acctcode.SelectedItem.Text;
            string _month = month.SelectedItem.Text;
            string _year = dropdown_year.Text;

            if (acctcode != "" && _month != "") { GetInfos(acctcode, _month,_year); }
        }

        protected void dropdown_year_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void getlonglat()
        //{
        //    GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            

        //    Response.Write("<script> alert(\"" + watcher.Position.Location.Longitude + "\") </script>");
        //    Response.Write("<script> alert(\"" + watcher.Position.Location.Latitude + "\") </script>");

        //    watcher.PositionChanged += (sender, e) =>
        //    {
        //        var coordinate = e.Position.Location;
        //        //Console.WriteLine("Lat: {0}, Long: {1}", coordinate.Latitude,
        //        // coordinate.Longitude);
        //        Response.Write("<script> alert(\"" + coordinate.Longitude + "\") </script>");
        //        Response.Write("<script> alert(\"" + coordinate.Latitude + "\") </script>");
        //        //Uncomment to get only one event. 
        //        watcher.Stop();
        //    };

        //    // Begin listening for location updates.
        //    watcher.Start();

        //}

    }
}