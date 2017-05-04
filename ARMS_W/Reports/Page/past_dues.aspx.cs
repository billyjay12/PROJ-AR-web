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
    public partial class past_dues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // rpt_account_balance
                string cardcode = Request.QueryString["cardcode"].ToString();
                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@"
                    select * from (
                        SELECT 
	                        a.shortname, f.cardname, 
	                        CASE WHEN a.transtype='13' THEN 'INV' WHEN a.transtype='14' THEN 'CM' WHEN a.transtype='24' and a.balance>0 THEN 'PMT' WHEN a.transtype='24' and a.balance<0 THEN 'PMT' ELSE 'ADJ' END TransType,
	                        round(a.Balance,2) Balance,
	                        left(f.AreaName,5) Area,
	                        f.SOName SOName,
	                        f.CreditLine CreditLimit,
	                        f.PymntGroup Terms,
	                        CASE WHEN c.QryGroup1='Y' THEN 'NON-PAYING ACCOUNTS' ELSE ' REGULAR ACCOUNTS' END NonPaying,
	                        CASE WHEN a.transtype='13' THEN DATEDIFF(DAY,getdate(),(SELECT DATEADD(DAY,d.ExtraDays,ISNULL(U_ActDelDate,DocDate)) FROM SAPSERVER.MATIMCO.dbo.OINV OINV WHERE OINV.Transid=a.Transid)) ELSE 0 END Days,
	                        0 pdc_amt,
	                        f.TerritoryName
                        from (
		                        SELECT 
			                        x.shortname, 
			                        x.transid,
			                        x.transtype, 
			                        sum(x.balance) balance
		                        FROM (
			
			                        select 
				                        shortname, Line_ID, transid, transtype, debit-credit balance
			                        from SAPSERVER.MATIMCO.dbo.jdt1 jdt1 inner join SAPSERVER.MATIMCO.dbo.ocrd ocrd on ocrd.cardcode=jdt1.shortname
			                        where ocrd.cardcode = '" + cardcode + @"'
			
			                        UNION ALL

			                        select 
				                        shortname, transrowid, transid,srcobjtyp,  case when IsCredit='D' then ReconSum*-1 else ReconSum END balance
			                        from SAPSERVER.MATIMCO.dbo.itr1 itr1 inner join SAPSERVER.MATIMCO.dbo.oitr oitr on itr1.reconnum=oitr.reconnum
			                        where left(shortname,4)<>'_SYS' and srcobjtyp<>'-5' and shortname = '" + cardcode + @"'
			
			
			                        UNION ALL

			                        select 
				                        shortname, transrowid, 
				                        CASE 
					                        WHEN itr1.IsCredit='D' THEN 
						                        (Select transid from SAPSERVER.MATIMCO.dbo.OINV OINV where DocNum in (SELECT Ref1 from SAPSERVER.MATIMCO.dbo.JDT1 JDT1 where transid=itr1.transid and TransRowId=Line_ID)) 
					                        else
						                        (Select transid from SAPSERVER.MATIMCO.dbo.ORIN ORIN where DocNum in (SELECT Ref1 from SAPSERVER.MATIMCO.dbo.JDT1 JDT1 where  transid=itr1.transid and TransRowId=Line_ID)) 
				                        END transid,
				                        CASE WHEN itr1.IsCredit='D' THEN '13' ELSE '14'  END transtype,
				                        case when IsCredit='D' then ReconSum*-1 else ReconSum END balance 
			                        from SAPSERVER.MATIMCO.dbo.itr1 itr1 
			                        inner join SAPSERVER.MATIMCO.dbo.oitr on itr1.reconnum=oitr.reconnum 
			                        where left(shortname,4)<>'_SYS' and srcobjtyp='-5' and shortname = '" + cardcode + @"'
		                        ) x
		                        where shortname = '" + cardcode + @"'
		                        group by x.shortname, x.transid, x.transtype
		                        having sum(x.balance)<>0
                        ) a
                        inner join SAPSERVER.MATIMCO.dbo.ocrd c on c.CardCode=a.Shortname
                        inner join SAPSERVER.MATIMCO.dbo.octg d on d.GroupNum=c.GroupNum
                        inner join SAPSERVER.MATIMCO.dbo.abmmw_vw_OCRD f on f.CardCode=a.ShortName
                        where f.cardcode = '" + cardcode + @"'

                        UNION ALL

                        select 
	                        orct.cardcode,
	                        ocrd.cardname,
	                        'PDC',
	                        0,
	                        left(f.AreaName,5) Area,
	                        f.SOName SOName,
	                        f.CreditLine CreditLimit,
	                        f.PymntGroup Terms,
	                        CASE WHEN OCRD.QryGroup1='Y' THEN 'NON-PAYING ACCOUNTS' ELSE ' REGULAR ACCOUNTS' END NonPaying,
	                        DATEDIFF(DAY,GETDATE(),rct1.duedate),
	                        rct1.checksum,
	                        f.territoryname
                        FROM SAPSERVER.MATIMCO.dbo.rct1 rct1
                        inner join SAPSERVER.MATIMCO.dbo.orct orct on rct1.docnum=orct.docentry
                        inner join SAPSERVER.MATIMCO.dbo.ocrd ocrd on ocrd.cardcode=orct.cardcode
                        inner join SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd f on f.cardcode=orct.cardcode
                        where orct.Canceled='N'
                        and DueDate > cast(convert(nvarchar,getdate(),101) as datetime)
                        and orct.CardCode = '" + cardcode + @"'
                        ) mytable "
                );

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_past_dues", mtable));
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}