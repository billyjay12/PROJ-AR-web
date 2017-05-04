using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ARMS_W.Class;
using System.Data.OleDb;
using System.IO;
using System.Data;


namespace ARMS_W.CS_REPORT
{
    public partial class List_Of_Approved_Accounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string start_date = "2011-09-05";
            string end_date = DateTime.Now.Date.ToString("d");
            FilterReport(start_date, end_date,"");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string val_start_date = date_from.Text;
            string val_end_date = date_to.Text;
            if (val_start_date != "" && val_end_date != "")
            {
                FilterReport(val_start_date, val_end_date, "button_click");
            }
            else {
                CrystalReportViewer1.RefreshReport();
            }
        }


        private void FilterReport(string start_date, string end_date, string action)
        {
            string foldername = Server.MapPath("~/CS_REPORT/rpt/List-Of-Approved-Accounts.rpt");
            string val_region = TextBox1.Text.ToUpper();
            _User Ousr = new _User(Session["username"].ToString());
            string[] No_Filter_Users = { "ceo", "vpbsm", "vptfi", "ssgm", "ssm", "brd","admin" };
            string[] User_more_roles = { "6", "14", "20", "21", "23", "31", "36", "52", "62", "64", "65", "71", "72", "77", "79", "90" };
            string orig_ids = string.Join(",", User_more_roles);
            string tmp_ids="";
           
            DataTable user_counterid = SqlDbHelper.getDataDT("SELECT counterid FROM userheader WHERE username='" + Session["username"].ToString() + "'");
            foreach (DataRow ids in user_counterid.Rows)
            {
                tmp_ids = ids["counterid"].ToString();
            }
            
            if (val_region != "")
            {
                ReportDocument ReportDoc = new ReportDocument();
                ReportDoc.Load(foldername);
                ReportDoc.SetDatabaseLogon("sa", "p@ssw0rd");

                ParameterFields PFIELDS = ReportDoc.ParameterFields;

                ParameterDiscreteValue[] param_values = new ParameterDiscreteValue[] { };

                ParameterDiscreteValue region_default_val = new ParameterDiscreteValue();
                region_default_val.Value = "C";
                ParameterDiscreteValue channel_default_val = new ParameterDiscreteValue();
                channel_default_val.Value = "All";
                ParameterDiscreteValue area_default_val = new ParameterDiscreteValue();
                area_default_val.Value = "All";
                ParameterDiscreteValue so_default_val = new ParameterDiscreteValue();
                so_default_val.Value = "All";
                ParameterDiscreteValue start_date_default_val = new ParameterDiscreteValue();
                start_date_default_val.Value = start_date;
                ParameterDiscreteValue end_date_default_val = new ParameterDiscreteValue();
                end_date_default_val.Value = end_date;

                PFIELDS["Region"].CurrentValues.Clear();
                PFIELDS["Area"].CurrentValues.Clear();
                PFIELDS["Channel"].CurrentValues.Clear();
                PFIELDS["SO"].CurrentValues.Clear();
                PFIELDS["Start Date"].CurrentValues.Clear();
                PFIELDS["End Date"].CurrentValues.Clear();

                if (action == "button_click" && (Ousr.HasPositionsOf(No_Filter_Users) || orig_ids.IndexOf(tmp_ids) != -1))
                {   
                    if (val_region == "LUZON") region_default_val.Value = "CL";
                        else if (val_region == "VISMIN") region_default_val.Value = "CV";
                        else region_default_val.Value = "C";
                   
                    }
                else {

                    ReportHelper.GetRegions(Session["username"].ToString(), "Region", ref PFIELDS, ref param_values);
                    foreach (ParameterDiscreteValue pd in PFIELDS["Region"].CurrentValues)
                    {
                        if (pd.Value.ToString() == "LUZON") pd.Value = "CL";
                        if (pd.Value.ToString() == "VISMIN") pd.Value = "CV";
                    }
                
                }
               
                ReportHelper.GetChannels(Session["username"].ToString(), "Channel", ref PFIELDS, ref param_values);
                ReportHelper.GetAreas(Session["username"].ToString(), "Area", ref PFIELDS, ref param_values);

                if (Ousr.HasPositionOf("so") != -1) so_default_val.Value = Ousr.SlpName;

                bool has_region = false, has_channel = false, has_area = false, has_so = false;
                foreach (ParameterField pd in PFIELDS)
                {
                    bool has_val = pd.CurrentValues.Count > 0 ? true : false;

                    if (has_val)
                        switch (pd.Name.ToUpper())
                        {
                            case "REGION": has_region = true; break;
                            case "CHANNEL": has_channel = true; break;
                            case "AREA": has_area = true; break;
                            case "SO": has_so = true; break;
                        }
                }

                if (!has_region) ReportDoc.ParameterFields["Region"].CurrentValues.Add(region_default_val);
                if (!has_channel) ReportDoc.ParameterFields["Channel"].CurrentValues.Add(channel_default_val);
                if (!has_area) ReportDoc.ParameterFields["Area"].CurrentValues.Add(area_default_val);
                if (!has_so) ReportDoc.ParameterFields["SO"].CurrentValues.Add(so_default_val);
                ReportDoc.ParameterFields["Start Date"].CurrentValues.Add(start_date_default_val);
                ReportDoc.ParameterFields["End Date"].CurrentValues.Add(end_date_default_val);
                
                CrystalReportViewer1.ReportSource = ReportDoc;
            }
            else
            {
                CrystalReportViewer1.RefreshReport();
            }

        }
    }
}
