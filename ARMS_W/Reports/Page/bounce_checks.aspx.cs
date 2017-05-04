using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.Reports.Page
{
    public partial class bounce_checks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // rpt_bounce_checks
                string cardcode = Request.QueryString["cardcode"].ToString();
                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@"
                    select 
	                    notes,
	                    CntctDate,
	                    CntctTime,
	                    tel,
	                    fax,
	                    details,
	                    street,
	                    city,
	                    country,
	                    state,
	                    room
                    from SAPSERVER.MATIMCO.dbo.oclg where CntctType='1'
                    and cardcode = '" + cardcode + "'"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_bounce_checks", mtable));
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}