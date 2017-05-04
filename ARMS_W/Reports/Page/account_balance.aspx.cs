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
    public partial class account_balance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // rpt_account_balance
                string cardcode = Request.QueryString["cardcode"].ToString();
                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@"
                    SELECT 
                        transid, shortname, cardname, PymntGroup, TransType TransTypeNo,
                        CASE WHEN transtype='13' THEN (SELECT DocNum from SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid)
                                WHEN transtype='14' THEN (SELECT DocNum from SAPSERVER.MATIMCO.dbo.ORIN WHERE Transid=a.Transid)
                                WHEN transtype='24' THEN (SELECT DocNum from SAPSERVER.MATIMCO.dbo.ORCT WHERE Transid=a.Transid)
                                ELSE transid END DocNum,
                        CASE WHEN transtype='13' THEN (SELECT U_ActDelDate from SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid)
                                ELSE '' END ActDelDate,
                        CASE WHEN transtype='13' THEN 'INV'
                                WHEN transtype='14' THEN 'CM'
                                WHEN transtype='24' THEN 'PMT'
                                ELSE 'ADJ' END TransType,
                        CASE WHEN transtype='13' THEN (SELECT isnull(U_INVOICE_NUMBER,'') from SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid)
                                ELSE '' END CI_Num,
                        CASE WHEN transtype='13' THEN (SELECT DocDate from SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid)
                                WHEN transtype='14' THEN (SELECT DocDate from SAPSERVER.MATIMCO.dbo.ORIN WHERE Transid=a.Transid)
                                WHEN transtype='24' THEN (SELECT DocDate from SAPSERVER.MATIMCO.dbo.ORCT WHERE Transid=a.Transid)
                                ELSE RefDate END TRFDate,
                        CASE WHEN transtype='13' THEN (CASE WHEN (SELECT ISNULL(U_ActDelDate,'') FROM SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid) IS NULL THEN '' ELSE (SELECT DATEADD(DAY,c.ExtraDays,U_ActDelDate) FROM SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid) END)
                                WHEN transtype='14' THEN (SELECT DocDate from SAPSERVER.MATIMCO.dbo.ORIN WHERE Transid=a.Transid)
                                WHEN transtype='24' THEN (SELECT DocDate from SAPSERVER.MATIMCO.dbo.ORCT WHERE Transid=a.Transid)
                                ELSE RefDate END DueDate,
                        CASE WHEN transtype='13' THEN (SELECT U_BRAND from SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid)
                                WHEN transtype='14' THEN (SELECT ISNULL(U_BRAND,'') from SAPSERVER.MATIMCO.dbo.ORIN WHERE Transid=a.Transid)
                                ELSE '' END Brand,
                        CASE WHEN transtype='13' THEN (SELECT DocTotal from SAPSERVER.MATIMCO.dbo.OINV WHERE Transid=a.Transid)
                                WHEN transtype='14' THEN (SELECT DocTotal*-1 from SAPSERVER.MATIMCO.dbo.ORIN WHERE Transid=a.Transid)
                                WHEN transtype='24' THEN (SELECT DocTotal*-1 from SAPSERVER.MATIMCO.dbo.ORCT WHERE Transid=a.Transid)
                                ELSE Debit-Credit END Orig_Amt,
                        (select sum(balduedeb-balduecred) from SAPSERVER.MATIMCO.dbo.jdt1 c where c.transid=a.transid and left(shortname,1)<>'_') Balance
                    FROM SAPSERVER.MATIMCO.dbo.jdt1 a
                    INNER JOIN SAPSERVER.MATIMCO.dbo.ocrd b on a.shortname=b.cardcode
                    INNER JOIN SAPSERVER.MATIMCO.dbo.octg c on c.GroupNum=b.GroupNum
                    WHERE (select sum(balduedeb-balduecred) from SAPSERVER.MATIMCO.dbo.jdt1 c where c.transid=a.transid and left(shortname,1)<>'_')<>0 
                    and intrnmatch=0 
                    and b.cardcode = '" + cardcode + @"'
                    GROUP BY transid, shortname, transtype, refdate, debit, credit,transid, shortname, cardname, c.groupnum, PymntGroup, c.extradays"
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_account_balance", mtable));
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}