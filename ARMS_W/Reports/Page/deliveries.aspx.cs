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
    public partial class deliveries : System.Web.UI.Page
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
	                    a.DocNum as TRFNum,
	                    isnull(b.BaseRef,'') as SONum,
	                    (select DocDate from SAPSERVER.MATIMCO.dbo.ORDR where DocNum=b.BaseRef) as SODate,
	                    a.DocDate as TRFDate,
	                    a.CardCode as BPCode,
	                    isnull(a.U_MAX_NUMBER,'') as MAXTRFNum,
	                    isnull(a.U_INVOICE_NUMBER,'') as ChargeInvoiceNum,
	                    case a.U_BRAND when 'MW' then 'MATWOOD'
	                     when 'GW' then 'GUDWOOD'
	                     when 'AD' then 'AIRDRIED'
	                     when 'PW' then 'PCW'
	                     when 'WW' then 'WEATHERWOOD'
	                     when 'GM' then 'GMELINA'
	                     when 'MH' then 'MAHOGANY'
	                     when NULL then 'XMISC'
	                     ELSE 'XMISC'
	                    end as Brand,
	                    isnull(a.U_Brand,'XX') as BrandShort,
	                    b.LineTotal*(100-a.DiscPrcnt)*.01 LineTotal,
	                    a.DiscSum DocDiscount,
	                    a.DocTotal as DocTotal,
	                    isnull(b.OpenQty,0) PCS,
	                    isnull(b.OpenQty*c.U_GMultiplier,0) GBDFT,
	                    isnull(b.OpenQty*c.U_NMultiplier,0) NBDFT,
	                    a.U_INVOICE_DATE CI_Date,
	                    d.areaname,
	                    d.soname
                    from SAPSERVER.MATIMCO.dbo.ODLN a inner join SAPSERVER.MATIMCO.dbo.DLN1 b on a.DocEntry=b.DocEntry
                    left outer join SAPSERVER.MATIMCO.dbo.OITM c on c.ItemCode=b.ItemCode
                    inner join SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd d on d.cardcode = a.cardcode
                    where b.LineStatus='O'
                    and a.cardcode = '" + cardcode + "'"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_deliveries", mtable));
                ReportViewer1.LocalReport.Refresh();

            }
        }
    }
}