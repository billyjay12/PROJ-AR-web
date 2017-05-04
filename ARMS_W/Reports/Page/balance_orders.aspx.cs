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
    public partial class balance_orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // rpt_pending_orders
                string cardcode = Request.QueryString["cardcode"].ToString();
                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@"
                    select a.CardCode, b.ItemCode, (b.OpenQty-b.ReleasQtty) Pcs, (b.OpenQty-b.ReleasQtty)*b.Price Amount,
                        (b.OpenQty-b.ReleasQtty)*d.U_GMultiplier Bdft,
                        a.DocDate,
                        case when left(c.Channel,3)='LIS' then 'ISL'
                        when left(c.Channel,3)='VIS' then 'ISV'
                        when left(c.Channel,3)='LTS' then 'GTL'
                        when left(c.Channel,3)='VTS' then 'GTV'
                        when left(c.Channel,3)='LDI' then 'MTL'
                        when left(c.Channel,3)='VDI' then 'MTV'
                        when left(c.Channel,3)='VWC' then 'WCV'
                        when left(c.Channel,3)='LWC' then 'WCL'
                        when left(c.Channel,3)='VTR' then 'TRV'
                        when left(c.Channel,3)='LTR' then 'TRL'
                        when left(c.Channel,3)='VTW' then 'TWL'
                        when left(c.Channel,3)='LTW' then 'TWV' end Channel,
                        LEFT(c.AreaName,5) AreaName,
                        case when left(c.Channel,3) in ('LIS','VIS') then 'IS'
                        when left(c.Channel,3) in ('LTS','VTS') then 'GT'
                        when left(c.Channel,3) in ('VDI','LDI') then 'MT'
                        when left(c.Channel,3) in ('VWC','LWC') then 'WC'
                        when left(c.Channel,3) in ('VTR','LTR') then 'TR'
                        when left(c.Channel,3) in ('VTW','LTW') then 'TW' end MotherChannel
                        ,d.u_brand as 'brand'
                    from SAPSERVER.MATIMCO.dbo.ordr a
                    inner join SAPSERVER.MATIMCO.dbo.rdr1 b on a.DocEntry=b.DocEntry
                    inner join SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd c on c.CardCode=a.CardCode
                    inner join SAPSERVER.MATIMCO.dbo.oitm d on d.itemcode=b.itemcode
                    where d.ItmsGrpCod='104' and a.Canceled='N' and b.LineStatus<>'C' and a.DocStatus='O'
                    and (b.OpenQty-b.ReleasQtty)>0 and Year(a.DocDate)<=2012
                    and (left(c.TerritoryName,1)<>'D' or a.CardCode='CLWILCON' or a.CardCode='CVWILCON' 
                    or a.CardCode=(select CardCode from SAPSERVER.MATIMCO.dbo.OCRD where isnull(OCRD.ChannlBP,'')='CLWILCON' and CardCode=a.CardCode))
                    and a.cardcode = '" + cardcode + "'"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_balance_orders", mtable));
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}