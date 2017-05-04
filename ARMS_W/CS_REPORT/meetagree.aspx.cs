using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ARMS_W.Class;
namespace ARMS_W.CS_REPORT
{
    public partial class meetagree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string foldername = Server.MapPath("~/CS_REPORT/rpt/summarymeetingsagreements.rpt");

            try
            {
                string foldername = Server.MapPath("~/CS_REPORT/rpt/summarymeetingsagreements.rpt");
                //string strpath = Server.MapPath("~/CS_REPORT/rpt/ftmandytdsellinperareareport.rpt");
                _User Ousr = new _User(Session["username"].ToString());
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

                PFIELDS["Region"].CurrentValues.Clear();
                PFIELDS["Area"].CurrentValues.Clear();
                PFIELDS["Channel"].CurrentValues.Clear();
                PFIELDS["SO"].CurrentValues.Clear();

                ReportHelper.GetRegions(Session["username"].ToString(), "Region", ref PFIELDS, ref param_values);
                foreach (ParameterDiscreteValue pd in PFIELDS["Region"].CurrentValues)
                {
                    if (pd.Value.ToString() == "LUZON") pd.Value = "CL";
                    if (pd.Value.ToString() == "VISMIN") pd.Value = "CV";
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

                CrystalReportViewer1.ReportSource = ReportDoc;
            }
            catch (Exception ex)
            {
                Label2.Text = "Error-" + ex.Message;
            }
        }
    }
}