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
    public partial class mtdytd_sales : System.Web.UI.Page
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
	                    sum(Amount) as 'amount', 
	                    sum(prev_month) as 'prev_month', 
	                    sum(curr_month) as 'curr_month' ,
	                    case when sum(prev_month) = 0 then 0 else ( sum(curr_month) - sum(prev_month) ) / sum(prev_month) end as 'gr_month',
	                    sum(prevprev_month) as 'prevprev_month', 
	                    case when sum(prev_month) = 0 then 0 else ( sum(prev_month) - sum(prevprev_month) ) / sum(prevprev_month) end as 'gr_prev_month',
	                    sum(prev_year) as 'prev_year', 
	                    sum(curr_year) as 'curr_year', 
	                    case when sum(prev_year) = 0 then 0 else (sum(curr_year) - sum(prev_year) ) / sum(prev_year) end as 'gr_year',
	                    sum(prevprev_year) as 'prevprev_year', 
	                    case when sum(prevprev_year) = 0 then 0 else ( sum(prev_year) - sum(prevprev_year) ) / sum(prevprev_year) end as 'gr_prev_year'
                    from (
                    SELECT 
                    case 
                    when oi.u_brand = 'MW' then 'MATWOOD' 
                    when oi.u_brand = 'GW' then 'GUDWOOD'
                    when oi.u_brand = 'AD' then 'AIRDRIED'
                    when oi.u_brand = 'PW' then 'PCW'
                    when oi.u_brand = 'WW' then 'WEATHERWOOD'
                    when oi.u_brand = 'GM' then 'GMELINA'
                    ELSE '' END AS 'u_brand',
                    --oi.docdate,
                    oi.doctotal Amount ,
                    case when 
	                    oi.docdate between cast(month(dateadd(month, -1, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, -1, getdate())) as nvarchar) and 
	                    dateadd(day, -1, cast(month(dateadd(month, 0, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, 0, getdate())) as nvarchar)) then
	                    oi.doctotal else 0 end as 'prev_month',
                    case when 
	                    oi.docdate between cast(month(dateadd(month, -2, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, -2, getdate())) as nvarchar) and 
	                    dateadd(day, -1, cast(month(dateadd(month, -1, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, -1, getdate())) as nvarchar)) then
	                    oi.doctotal else 0 end as 'prevprev_month',
                    case when 
	                    oi.docdate between cast(month(dateadd(month, 0, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, 0, getdate())) as nvarchar) and 
	                    dateadd(day, -1, cast(month(dateadd(month, 1, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, 1, getdate())) as nvarchar)) then
	                    oi.doctotal else 0 end as 'curr_month',
                    case when 
	                    oi.docdate between '01/01/' + cast((year(getdate()) - 1) as nvarchar) and '12/31/' + cast((year(getdate()) - 1) as nvarchar) 
	                    then oi.doctotal else 0 end as 'prev_year',
                    case when 
	                    oi.docdate between '01/01/' + cast((year(getdate()) - 2) as nvarchar) and '12/31/' + cast((year(getdate()) -2) as nvarchar) 
	                    then oi.doctotal else 0 end as 'prevprev_year',
                    case when 
	                    oi.docdate between '01/01/' + cast((year(getdate())) as nvarchar) and '12/31/' + cast((year(getdate())) as nvarchar) 
	                    then oi.doctotal else 0 end as 'curr_year'
                    FROM SAPSERVER.MATIMCO.dbo.oinv oi
                    INNER JOIN SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd oc ON oi.cardcode=oc.cardcode
                    where oi.cardcode = '" + cardcode + @"'

                    UNION ALL

                    SELECT 
                    case 
                    when ori.u_brand = 'MW' then 'MATWOOD' 
                    when ori.u_brand = 'GW' then 'GUDWOOD'
                    when ori.u_brand = 'AD' then 'AIRDRIED'
                    when ori.u_brand = 'PW' then 'PCW'
                    when ori.u_brand = 'WW' then 'WEATHERWOOD'
                    when ori.u_brand = 'GM' then 'GMELINA'
                    ELSE '' END AS 'u_brand',
                    --ori.docdate,
                    (ori.doctotal)*-1 Amount ,
                    case when 
	                    ori.docdate between cast(cast(month(dateadd(month, -1, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, -1, getdate())) as nvarchar) as datetime) and 
	                    dateadd(day, -1, cast(month(dateadd(month, 0, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, 0, getdate())) as nvarchar)) then
	                    (ori.doctotal)*-1 else 0 end as 'prev_month',
                    case when 
	                    ori.docdate between cast(month(dateadd(month, -2, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, -2, getdate())) as nvarchar) and 
	                    dateadd(day, -1, cast(month(dateadd(month, -1, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, -1, getdate())) as nvarchar)) then
	                    ori.doctotal else 0 end as 'prevprev_month',
                    case when 
	                    ori.docdate between cast(cast(month(dateadd(month, 0, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, 0, getdate())) as nvarchar) as datetime) and 
	                    dateadd(day, -1, cast(month(dateadd(month, 1, getdate())) as nvarchar) + '/01/' + cast(year(dateadd(month, 1, getdate())) as nvarchar)) then
	                    (ori.doctotal)*-1 else 0 end as 'curr_month',
                    case when 
	                    ori.docdate between '01/01/' + cast((year(getdate()) - 1) as nvarchar) and '12/31/' + cast((year(getdate()) - 1) as nvarchar) 
	                    then ori.doctotal else 0 end as 'prev_year',
                    case when 
	                    ori.docdate between '01/01/' + cast((year(getdate()) - 2) as nvarchar) and '12/31/' + cast((year(getdate()) - 2) as nvarchar) 
	                    then ori.doctotal else 0 end as 'prev_year',
                    case when 
	                    ori.docdate between '01/01/' + cast((year(getdate())) as nvarchar) and '12/31/' + cast((year(getdate())) as nvarchar) 
	                    then ori.doctotal else 0 end as 'curr_year'
                    FROM SAPSERVER.MATIMCO.dbo.orin ori
                    INNER JOIN SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd oc ON ori.cardcode=oc.cardcode
                    where ori.cardcode = '" + cardcode + @"'
                    ) mytable"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_mtdytd_sales", mtable));
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}