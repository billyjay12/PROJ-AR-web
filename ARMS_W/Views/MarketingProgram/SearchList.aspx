<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<% 
    
    DataTable mrktProgram;
    
    DataTable mrktTimeline;

    DataTable mrktProjectTime;

    DataTable mrktAmount;
   
   
    %>

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/marketingSearchwindow.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


   var baseUrl = "<%= ResolveUrl("~/") %>";
   
   
   </script>


   <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Search List</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <hr />
                </td>
            </tr>
           
        </table>
    </div>

   

    


      <div class="simple_box">

        <table cellpadding="1" id="searchWindow" cellspacing="0" border="0" align="center">

        <tr>

        <th align="center"> Marketing Program No. </th>
        <th align="center" style="width: 150px;"> Program Name </th>
        <th align="center" style="width: 100px;"> Brand </th>
        <th align="center" style="width: 100px;"> Project Cost </th>
        <th align="center" style="width: 100px;"> Running Cost </th>

        </tr>

             <%   

                 if (Request.QueryString["brandtosearch"] != null)
                 {
                     if (Request.QueryString["brandtosearch"].ToString() != "")
                     {
                         if (Request.QueryString["brandtosearch"].ToString() != "ALL BRANDS")
                         {
                             //mrktProgram = SqlDbHelper.getDataDT("select programNo, progName, brand, totalAmtResources  from mrktProgram where brand='" + Request.QueryString["brandtosearch"].ToString() + "'");
                             mrktProgram = SqlDbHelper.getDataDT("SELECT * FROM dply_RunningCost   WHERE brand='" + Request.QueryString["brandtosearch"].ToString() + "'");

                             if (mrktProgram.Rows.Count > 0)
                             {
                                 for (int i = 0; i < mrktProgram.Rows.Count; i++)
                                 {
                                     Response.Write("<tr>");
                                     Response.Write("<td><a href=\"" + ResolveUrl("~/") + "MarketingProgram/MarketingProgramStatus?programNo=" + mrktProgram.Rows[i].ItemArray[0] + "\">" + mrktProgram.Rows[i].ItemArray[0] + "</a></td>");
                                    // Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[0] + "</td>");
                                     Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[1] + "</td>");
                                     Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[2] + "</td>");
                                     Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[3] + "</td>");
                                     Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[4] + "</td>");
                                     Response.Write("</tr>");
                                 }
                             }

                             else
                             {
                                 Response.Write("<tr>");
                                 Response.Write("</tr>");
                                 Response.Write("<tr>");
                                 Response.Write("</tr>");
                                 Response.Write("<tr>");
                                 Response.Write("<td colspan=5 align=center> <b> No RECORDS FOUND </b> </td>");
                                 Response.Write("</tr>");
                             }
                         }
                         
                          else if(Request.QueryString["brandtosearch"].ToString() == "ALL BRANDS"){


                              mrktProgram = SqlDbHelper.getDataDT("SELECT * FROM dply_RunningCost ");

                              if (mrktProgram.Rows.Count > 0)
                              {
                                  for (int i = 0; i < mrktProgram.Rows.Count; i++)
                                  {
                                      Response.Write("<tr>");
                                      Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[0] + "</td>");
                                      Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[1] + "</td>");
                                      Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[2] + "</td>");
                                      Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[3] + "</td>");
                                      Response.Write("<td align=center>" + mrktProgram.Rows[i].ItemArray[4] + "</td>");
                                      Response.Write("</tr>");
                                  }

                              }

                         }

                     }
                 }

                 else if (Request.QueryString["datetosearch"] != null)
                 {
                     if (Request.QueryString["datetosearch"].ToString() != "")
                     

                         {

 mrktTimeline = SqlDbHelper.getDataDT("SELECT a.programNo,a.progName,a.brand ,a.totalAmtResources,(SELECT SUM(jdt1_1.Debit - jdt1_1.Credit) AS Runningcost  FROM SAPSERVER.MATIMCO.dbo.jdt1 AS jdt1_1 INNER JOIN SAPSERVER.MATIMCO.dbo.oprc AS oprc_1 ON oprc_1.PrcCode = jdt1_1.U_PC2 WHERE oprc_1.U_MP_NUM COLLATE DATABASE_DEFAULT = a.programNo COLLATE DATABASE_DEFAULT GROUP BY oprc_1.U_MP_NUM) as Runningcost FROM  mrktProgram a INNER JOIN mrktTimeline b ON a.programNo = b.programNo WHERE b.startFinish= '" + Request.QueryString["datetosearch"].ToString() + "'");
                             if (mrktTimeline.Rows.Count > 0)
                             {
                                 for (int i = 0; i < mrktTimeline.Rows.Count; i++)
                                 {
                                     Response.Write("<tr>");
                                     Response.Write("<td align=center>" + mrktTimeline.Rows[i].ItemArray[0] + "</td>");
                                     Response.Write("<td align=center>" + mrktTimeline.Rows[i].ItemArray[1] + "</td>");
                                     Response.Write("<td align=center>" + mrktTimeline.Rows[i].ItemArray[2] + "</td>");
                                     Response.Write("<td align=center>" + mrktTimeline.Rows[i].ItemArray[3] + "</td>");
                                     Response.Write("<td align=center>" + mrktTimeline.Rows[i].ItemArray[4] + "</td>");
                                     Response.Write("</tr>");
                                 }
                             }

                             else
                             {

                                 Response.Write("<tr>");
                                 Response.Write("</tr>");
                                 Response.Write("<tr>");
                                 Response.Write("</tr>");
                                 Response.Write("<tr>");
                                 Response.Write("<td colspan=5 align=center> <b> No RECORDS FOUND </b> </td>");

                                 Response.Write("</tr>");

                             }
                         }
                     }

                 else if (Request.QueryString["projecttypetosearch"] != null)
                     {

                         if (Request.QueryString["projecttypetosearch"].ToString() != "")
                         {
                             mrktProjectTime = SqlDbHelper.getDataDT("SELECT a.programNo,a.progName,a.brand ,a.totalAmtResources,(SELECT SUM(jdt1_1.Debit - jdt1_1.Credit) AS Runningcost  FROM SAPSERVER.MATIMCO.dbo.jdt1 AS jdt1_1 INNER JOIN SAPSERVER.MATIMCO.dbo.oprc AS oprc_1 ON oprc_1.PrcCode = jdt1_1.U_PC2 WHERE oprc_1.U_MP_NUM COLLATE DATABASE_DEFAULT = a.programNo COLLATE DATABASE_DEFAULT GROUP BY oprc_1.U_MP_NUM) as Runningcost FROM  mrktProgram a INNER JOIN mrktTimeline b ON a.programNo = b.programNo where progType='" + Request.QueryString["projecttypetosearch"].ToString() + "'");
                            // mrktProjectTime = SqlDbHelper.getDataDT("select programNo, progName, brand, totalAmtResources  from mrktProgram where progType='" + Request.QueryString["projecttypetosearch"].ToString() + "'");
                             if (mrktProjectTime.Rows.Count > 0)
                             {
                                 for (int i = 0; i < mrktProjectTime.Rows.Count; i++)
                                 {
                                     Response.Write("<tr>");
                                     Response.Write("<td align=center>" + mrktProjectTime.Rows[i].ItemArray[0] + "</td>");
                                     Response.Write("<td align=center>" + mrktProjectTime.Rows[i].ItemArray[1] + "</td>");
                                     Response.Write("<td align=center>" + mrktProjectTime.Rows[i].ItemArray[2] + "</td>");
                                     Response.Write("<td align=center>" + mrktProjectTime.Rows[i].ItemArray[3] + "</td>");
                                     Response.Write("</tr>");
                                 }
                             }

                             else

                             {
                                 Response.Write("<tr>");
                                 Response.Write("</tr>");
                                 Response.Write("<tr>");
                                 Response.Write("</tr>");
                                 Response.Write("<tr>");
                                 Response.Write("<td colspan=5 align=center> <b> No RECORDS FOUND </b> </td>");
                                 Response.Write("</tr>");

                             }
                         }

                     }


                 else if (Request.QueryString["amounttosearch"] != null)
                 {

                     if (Request.QueryString["amounttosearch"].ToString() != "")
                     {
                         var castedValue="";
                      
                         castedValue = Request.QueryString["amounttosearch"];
                                                  //mrktAmount = SqlDbHelper.getDataDT("select programNo, progName, brand, totalAmtResources  from mrktProgram where totalAmtResources='" + castedValue + "'");
                         mrktAmount = SqlDbHelper.getDataDT("SELECT a.programNo,a.progName,a.brand ,a.totalAmtResources,(SELECT SUM(jdt1_1.Debit - jdt1_1.Credit) AS Runningcost  FROM SAPSERVER.MATIMCO.dbo.jdt1 AS jdt1_1 INNER JOIN SAPSERVER.MATIMCO.dbo.oprc AS oprc_1 ON oprc_1.PrcCode = jdt1_1.U_PC2 WHERE oprc_1.U_MP_NUM COLLATE DATABASE_DEFAULT = a.programNo COLLATE DATABASE_DEFAULT GROUP BY oprc_1.U_MP_NUM) as Runningcost FROM  mrktProgram a where a.totalAmtResources='" + castedValue + "'");

                         
                         if (mrktAmount.Rows.Count > 0)
                         {
                             for (int i = 0; i < mrktAmount.Rows.Count; i++)
                             {
                                 Response.Write("<tr>");
                                 Response.Write("<td align=center>" + mrktAmount.Rows[i].ItemArray[0] + "</td>");
                                 Response.Write("<td align=center>" + mrktAmount.Rows[i].ItemArray[1] + "</td>");
                                 Response.Write("<td align=center>" + mrktAmount.Rows[i].ItemArray[2] + "</td>");
                                 Response.Write("<td align=center>" + mrktAmount.Rows[i].ItemArray[3] + "</td>");
                                 Response.Write("</tr>");
                             }
                         }

                         else
                         {

                             Response.Write("<tr>");
                             Response.Write("</tr>");
                             Response.Write("<tr>");
                             Response.Write("</tr>");
                             Response.Write("<tr>");
                             Response.Write("<td colspan=5 align=center> <b> No RECORDS FOUND </b> </td>");
                             Response.Write("</tr>");
                         }
                     }
                 }

            %>

            </table>

            <table align="center">

            <tr><td></td></tr>
            <tr><td></td></tr>
            <tr><td></td></tr>
            <tr><td></td></tr>
            <tr><td></td></tr>
            <tr><td></td></tr>
            <tr><td></td></tr>
            <tr><td></td></tr>

             <tr>
        
        <td colspan="5" align="center"> <input type="button" id="search2" value="SEARCH AGAIN" style="color:#000000; width: 125px;"  onclick="javascript:backtosearch();"/> </td>
        <td></td>
        <td><input type="button" id="Button1" value="ViewList" style="color:#000000; width: 125px;"  onclick="javascript:viewListdetail();"/></td>
        </tr>

  

        </table>
          

    </div>
</asp:Content>
