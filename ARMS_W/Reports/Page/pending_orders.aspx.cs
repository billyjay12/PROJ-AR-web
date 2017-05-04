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
    public partial class pending_reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                // rpt_pending_orders
                string cardcode = Request.QueryString["cardcode"].ToString();
                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@"
                    select 
	                    a.CardCode, b.ItemCode, b.Quantity, b.LineTotal,
	                    b.Quantity* d.U_GMultiplier Bdft,
	                    a.DocDate,
	                    d.u_brand
                    from SAPSERVER.MATIMCO.dbo.odrf a 
                    inner join SAPSERVER.MATIMCO.dbo.drf1 b on a.DocEntry=b.DocEntry
                    inner join SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd c on c.CardCode=a.CardCode
                    left join SAPSERVER.MATIMCO.dbo.oitm d on d.itemcode = b.itemcode
                    where b.ItemCode IS NOT NULL and a.DocStatus='O' and a.ObjType=17
                    and b.docentry NOT IN (Select distinct Draftkey from SAPSERVER.MATIMCO.dbo.ordr where Draftkey is not null)
                    and Year(a.DocDate) <= Year(getdate())
                    and a.cardcode = '" + cardcode + "'"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_pending_orders", mtable));
                ReportViewer1.LocalReport.Refresh();

            }
        }
    }
}