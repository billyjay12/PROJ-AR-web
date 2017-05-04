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
    public partial class collections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // rpt_pending_orders
                string cardcode = Request.QueryString["cardcode"].ToString();
                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@"
                    SELECT
                        a.DocNum, 
                        CASE WHEN b.DueDate IS NULL THEN a.DocDueDate ELSE b.DueDate END AS DueDate, 
                        a.CardCode, 
                        ISNULL(a.CardName, '') AS CardName,
                        ISNULL(b.CheckNum, 0) AS CheckNum, 
                        ISNULL(b.BankCode, '') AS BankCode, 
                        CASE WHEN b.BankCode IS NULL THEN a.[DocTotal] ELSE b.[CheckSum] END AS Amount, 
                        a.DocDate,
                        (SELECT AreaName FROM SAPSERVER.MATIMCO.dbo.abmmw_vw_OCRD WHERE (CardCode = a.CardCode)) AS Area,
                        (SELECT SOName FROM SAPSERVER.MATIMCO.dbo.abmmw_vw_OCRD WHERE (CardCode = a.CardCode)) AS SOName  
                    FROM SAPSERVER.MATIMCO.dbo.ORCT a 
                    LEFT OUTER JOIN SAPSERVER.MATIMCO.dbo.RCT1 b ON a.DocNum = b.DocNum 
                    LEFT OUTER JOIN SAPSERVER.MATIMCO.dbo.OCHH c WITH (nolock) ON c.RcptNum = b.DocNum AND c.RcptLineId = b.LineID
                    WHERE a.Canceled <> 'Y'
                    and a.cardcode = '" + cardcode + @"'
                    ORDER BY DueDate"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_collections", mtable));
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}