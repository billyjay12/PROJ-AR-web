<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">



   





    <title>RunningCostDetails</title>
</head>
<body>

<% 
    
    DataTable mrktProgram;
    
   
    %>


     <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/MarketingProg.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


   var baseUrl = "<%= ResolveUrl("~/") %>";
   
   
   </script>
    <div class="simple_box">

        <table cellpadding="1" id="searchWindow" cellspacing="0" border="1" align="center">

        <tr>

        <th align="center"><font face="calibri"> Reference Date </font> </th>
        <th align="center" style="width: 400px;"><font face="calibri"> Line Memo </font> </th>
        <th align="center" style="width: 100px;"><font face="calibri"> Break Down </font> </th>

        </tr>

        <%
            
           // mrktProgram = SqlDbHelper.getDataDT("select  * FROM TOTALrunningCost where U_MP_NUM='" + Request.QueryString["programnumbertosearch"].ToString() + "' order by cast(RefDateFormatted as datetime)  ");
            //mrktProgram = SqlDbHelper.getDataDT("select  * FROM runningCost");
           // mrktProgram = SqlDbHelper.getDataDT("select  convert(varchar(10), RefDate, 101) as 'RefDateFormatted', LineMemo, CONVERT(varchar(10), CONVERT(money, Debit-Credit)) from SAPSERVER.MATIMCO.dbo.jdt1 inner join SAPSERVER.MATIMCO.dbo.oprc on oprc.prccode=jdt1.U_PC2 where U_MP_NUM='" + Request.QueryString["programnumbertosearch"].ToString() + "' order by refDate");
           // mrktProgram = SqlDbHelper.getDataDT("select programNo, progName, brand, totalAmtResources  from mrktProgram where brand='" + Request.QueryString["brandtosearch"].ToString() + "'");

            if (Request.QueryString["programnumbertosearch"] != null)
            {
                if (Request.QueryString["programnumbertosearch"].ToString() != "")
                {

                    mrktProgram = SqlDbHelper.getDataDT("select  * FROM TOTALrunningCost where U_MP_NUM='" + Request.QueryString["programnumbertosearch"].ToString() + "' order by cast(RefDateFormatted as datetime)  ");

                    if (mrktProgram.Rows.Count > 0)
                    {
                        for (int i = 0; i < mrktProgram.Rows.Count; i++)
                        {
                            Response.Write("<tr>");
                            Response.Write("<td align=left><font face=calibri>" + mrktProgram.Rows[i].ItemArray[0] + "</font></td>");
                            Response.Write("<td align=left><font face=calibri>" + mrktProgram.Rows[i].ItemArray[1] + "</font></td>");
                            Response.Write("<td align=right><font face=calibri> " + mrktProgram.Rows[i].ItemArray[2] + "</font></td>");
                            Response.Write("</tr>");
                        }

                    }
                }
            }
            else {

                Response.Write("<tr>");
                Response.Write("</tr>");
                Response.Write("<tr>");
                Response.Write("</tr>");
                Response.Write("<tr>");
                Response.Write("<td colspan=5 align=center> <b> No RECORDS FOUND </b> </td>");
                Response.Write("</tr>");
            
            }
            
            
            
            
            
            
            
            
             %>



        </table>
    </div>
</body>
</html>
