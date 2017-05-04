<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %><%@ Import Namespace="System.Data.OleDb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% 
    
    DataTable mrktProgram;
    
    DataTable mrktTimeline;

    DataTable mrktProjectTime;

    DataTable mrktAmount;
   
   
    %>


    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/marketingSearchwindow.js" type="text/javascript"></script>


     <script type="text/javascript" language="javascript">


     
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        

        $(function () {

        $("#txt_date").datepicker();
        $("#txt_end_date").datepicker();
          // var acct_type = "";
        $("#txt_brand").attr('disabled', 'disabled');
       $("#txt_date").attr('disabled', 'disabled');
        $("#txt_amount").attr('disabled', 'disabled');
        $("#txt_ProjectType").attr('disabled', 'disabled');
             hide();
             hideAmountbox2();

          $("#byDate").change(function(){EnableDisableButton();});
          $("#byBrand").change(function(){EnableDisableButton();});
          $("#byAmount").change(function(){EnableDisableButton(); });
          $("#byProjectType").change(function(){EnableDisableButton();});
        
    
        });

     

        function EnableDisableButton(){
        
        if($("#byDate").attr("checked")== true){
       
        $("#txt_brand").attr('disabled', 'disabled');
       $("#txt_date").removeAttr('disabled');
        $("#txt_amount").attr('disabled', 'disabled');
        $("#txt_ProjectType").attr('disabled', 'disabled');
        $("#txt_brand").attr("value", "");
        $("#txt_amount").attr("value", "Starting Amount");
        $("#txt_ProjectType").attr("value", "");
        show();
        hideAmountbox2();
       
        
        
        }

         if($("#byBrand").attr("checked")== true){
               
        $("#txt_brand").removeAttr('disabled');
        $("#txt_date").attr('disabled', 'disabled');
        $("#txt_amount").attr('disabled', 'disabled');
        $("#txt_ProjectType").attr('disabled', 'disabled');
         $("#txt_date").attr("value", "Start Date");
        $("#txt_amount").attr("value", "Starting Amount");
        $("#txt_ProjectType").attr("value", "");
            hide();
            hideAmountbox2();
        
        }

        if($("#byAmount").attr("checked")== true){
               
        $("#txt_brand").attr('disabled', 'disabled');
        $("#txt_date").attr('disabled', 'disabled');
        $("#txt_amount").removeAttr('disabled');
        $("#txt_ProjectType").attr('disabled', 'disabled');
        $("#txt_brand").attr("value", "");;
        $("#txt_date").attr("value", "Start Date");
        $("#txt_ProjectType").attr("value", "");
            hide();
            showAmountbox2();
        
        }


        if($("#byProjectType").attr("checked")== true){
               
        $("#txt_brand").attr('disabled', 'disabled');
        $("#txt_date").attr('disabled', 'disabled');
        $("#txt_amount").attr('disabled', 'disable');
        $("#txt_ProjectType").removeAttr('disabled');
        $("#txt_brand").attr("value", "");
        $("#txt_date").attr("value", "Start Date");
        $("#txt_amount").attr("value", "Starting Amount");
            hide();
            hideAmountbox2();
        
        }
        
        }


        function hide(){
$("#txt_end_date").hide();

}
function show(){
$("#txt_end_date").show();

}

       function hideAmountbox2(){
$("#txt_amount2").hide();

}
function showAmountbox2(){
$("#txt_amount2").show();

}


	</script>

    <script type="text/javascript">
        function make_blank() {
            $("#txt_date").attr("value", "");
           
        }
        function make_blank2() {
            $("#txt_end_date").attr("value", "");
        }
        function make_blankAMT() {
            $("#txt_amount").attr("value", "");

        }
        function make_blankAMT2() {
            $("#txt_amount2").attr("value", "");
        }

        

</script>




     <% 
 // QUERY FOR THE POSITION OF THE USER
        string userPosition="";
        string strquery = "SELECT position FROM userHeader WHERE userName='" + Session["username"] + "'";
        OleDbDataReader greader = SqlDbHelper.getData(strquery);
        if (greader.Read())
        {
            userPosition = greader.GetValue(0).ToString();

        }
         %>



      <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Search Window</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                   <% if (userPosition == "ssg" || userPosition == "csr" || userPosition == "csm" || userPosition == "ssm")
                    { %>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingPog">>>Create Marketing Program</a>
                </td>
                 <% } %> 

            </tr>
        </table>
    </div>






     <div  style="padding:5px; font-size:12px;">
     <table cellpadding="1" cellspacing="3" border="0">
     
                        
     <tr>                
     <td></td>
     <td>Search Value</td>
     
     </tr>

     <tr>                
     <td style="height: 27px"><label><input type="radio" id="byBrand" name="option1" value="Brand"  />Brand</label></td>
     <td style="height: 27px"><input type="text" id="txt_brand"  size="27" onclick="javascript:LookUpData('txt_brand','ListofMarketingBrand');"/></td>
     </tr>

     <tr>                
     <td style="height: 27px"><input type="radio" id="byDate" name="option1" value="Date" />Date 
         Range</td>

    
     <td style="height: 27px"><input type="text" id="txt_date"   size="27" value="Start Date" onclick= "make_blank();"/></td>
     <td style="height: 27px"><input type="text" id="txt_end_date"   size="27" 
             value="End Date" onclick= "make_blank2();"/></td>
      <!--<td style="height: 27px"><input type="text" id="Text2"  size="27" onclick="javascript:LookUpData('txt_date','MktProgramDate');"/></td>-->
     </tr>

     <tr>                
     <td><label><input type="radio" id="byAmount" name="option1" value="Amount"  />Amount</label></td>
     <!--<td><input type="text" id="txt_amount"  size="27" onclick="javascript:LookUpData('txt_amount','MktProgramAmount');" /></td>-->
   <!--  <td><input type="text" id="txt_amount"  size="27" onclick="javascript:LookUpData('txt_amount','MktProgramAmount');" /></td>-->
   <td> <input type="text" id="txt_amount"  size="27" value="Starting Amount" onclick= "make_blankAMT();"  /> </td>
   <td> <input type="text" id="txt_amount2"  size="27" value="Ending Amount"  onclick= "make_blankAMT2();" /> </td>
     </tr>

     <tr>                
     <td style="height: 27px"><label><input type="radio" id="byProjectType" name="option1" value="ProjectType" />Project Type</label></td>
     <td style="height: 27px"><input type="text" id="txt_ProjectType"  size="27"  onclick="javascript:LookUpData('txt_ProjectType','MktProgtype');"/></td>
     </tr>

     </table>
     </div>



     <div  style="padding:5px; font-size:12px;">
     <table cellpadding="1" cellspacing="0" border="0" style="color:#ffffff;">
     
     <tr>
     <td width="60%"></td>
     <td><input type="button" id="search2" value="SEARCH" style="color:#000000; width: 70px;" onclick="javascript:RedirectTosearch();" /></td>
   

     </tr>        
                        
    </table>

    </div>

    <hr style="width: 100%" />


    <div class="simple_box">
		<table id="tbl_lst_eMAT" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">

        <tr>

        <th align="left" style="width: 200px;"> Marketing Program No. </th>
        <th align="left" style="width: 150px;"> Program Name </th>
        <th align="left" style="width: 100px;"> Brand </th>
        <th align="left" style="width: 200px;"> Project Cost </th>
        <th align="left" style="width: 200px;"> Running Cost </th>

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
                                     //Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[0] + "</td>");
                                     Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[1] + "</td>");
                                     Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[2] + "</td>");
                                     Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[3] + "</td>");
                                     Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[4] + "</td>");
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
                                      Response.Write("<td><a href=\"" + ResolveUrl("~/") + "MarketingProgram/MarketingProgramStatus?programNo=" + mrktProgram.Rows[i].ItemArray[0] + "\">" + mrktProgram.Rows[i].ItemArray[0] + "</a></td>");
                                     // Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[0] + "</td>");
                                      Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[1] + "</td>");
                                      Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[2] + "</td>");
                                      Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[3] + "</td>");
                                      Response.Write("<td align=left>" + mrktProgram.Rows[i].ItemArray[4] + "</td>");
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
                         if (Request.QueryString["datetosearch"].ToString() != "Start Date")
                         {
                             if (Request.QueryString["endDate"] != null)
                             {
                                 if (Request.QueryString["endDate"].ToString() != "")
                                 {
                                     if (Request.QueryString["endDate"].ToString() != "END Date")
                                     {

                                         var timeRange = Request.QueryString["datetosearch"].ToString() + " 12:00:00 AM";
                                         var timeDate = Request.QueryString["datetosearch"].ToString();
                                         var endedDate = Request.QueryString["endDate"].ToString();



                                         mrktTimeline = SqlDbHelper.getDataDT("SELECT * FROM MTC_vw_MarktngProgDates WHERE minDate BETWEEN CONVERT(NVARCHAR(15),CAST('" + timeDate + "' AS DATETIME),101) AND CONVERT(NVARCHAR(15),CAST('" + endedDate + "' AS DATETIME),101)");
                                         //mrktTimeline = SqlDbHelper.getDataDT("SELECT a.programNo,a.progName,a.brand ,a.totalAmtResources,(SELECT SUM(jdt1_1.Debit - jdt1_1.Credit) AS Runningcost  FROM SAPSERVER.MATIMCO.dbo.jdt1 AS jdt1_1 INNER JOIN SAPSERVER.MATIMCO.dbo.oprc AS oprc_1 ON oprc_1.PrcCode = jdt1_1.U_PC2 WHERE oprc_1.U_MP_NUM COLLATE DATABASE_DEFAULT = a.programNo COLLATE DATABASE_DEFAULT GROUP BY oprc_1.U_MP_NUM) as Runningcost FROM  mrktProgram a INNER JOIN mrktTimeline b ON a.programNo = b.programNo WHERE b.startFinish= '" + Request.QueryString["datetosearch"].ToString() + "'");
                                         // mrktTimeline = SqlDbHelper.getDataDT("SELECT a.programNo,a.progName,a.brand ,a.totalAmtResources,(SELECT SUM(jdt1_1.Debit - jdt1_1.Credit) AS Runningcost  FROM SAPSERVER.MATIMCO.dbo.jdt1 AS jdt1_1 INNER JOIN SAPSERVER.MATIMCO.dbo.oprc AS oprc_1 ON oprc_1.PrcCode = jdt1_1.U_PC2 WHERE oprc_1.U_MP_NUM COLLATE DATABASE_DEFAULT = a.programNo COLLATE DATABASE_DEFAULT GROUP BY oprc_1.U_MP_NUM) as Runningcost FROM  mrktProgram a INNER JOIN mrktTimeline b ON a.programNo = b.programNo WHERE (b.startFinish<='" + timeRange + "') AND (b.endDate>='" + timeRange + "')");
                                         if (mrktTimeline.Rows.Count > 0)
                                         {
                                             for (int i = 0; i < mrktTimeline.Rows.Count; i++)
                                             {
                                                 Response.Write("<tr>");
                                                 Response.Write("<td><a href=\"" + ResolveUrl("~/") + "MarketingProgram/MarketingProgramStatus?programNo=" + mrktTimeline.Rows[i].ItemArray[1] + "\">" + mrktTimeline.Rows[i].ItemArray[1] + "</a></td>");
                                                 // Response.Write("<td align=left>" + mrktTimeline.Rows[i].ItemArray[0] + "</td>");
                                                 Response.Write("<td align=left>" + mrktTimeline.Rows[i].ItemArray[2] + "</td>");
                                                 Response.Write("<td align=left>" + mrktTimeline.Rows[i].ItemArray[3] + "</td>");
                                                 Response.Write("<td align=left>" + mrktTimeline.Rows[i].ItemArray[4] + "</td>");
                                                 Response.Write("<td align=left>" + mrktTimeline.Rows[i].ItemArray[5] + "</td>");
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

                             }
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
                                 Response.Write("<td><a href=\"" + ResolveUrl("~/") + "MarketingProgram/MarketingProgramStatus?programNo=" + mrktProjectTime.Rows[i].ItemArray[0] + "\">" + mrktProjectTime.Rows[i].ItemArray[0] + "</a></td>");
                                 // Response.Write("<td align=left>" + mrktProjectTime.Rows[i].ItemArray[0] + "</td>");
                                 Response.Write("<td align=left>" + mrktProjectTime.Rows[i].ItemArray[1] + "</td>");
                                 Response.Write("<td align=left>" + mrktProjectTime.Rows[i].ItemArray[2] + "</td>");
                                 Response.Write("<td align=left>" + mrktProjectTime.Rows[i].ItemArray[3] + "</td>");
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


                 else if (Request.QueryString["startingAmtRange"] != null)
                 {

                     if (Request.QueryString["startingAmtRange"].ToString() != "")
                     {
                         if (Request.QueryString["startingAmtRange"].ToString() != "Starting Amount")
                         {

                             if (Request.QueryString["UptoAmtRange"] != null)
                             {
                                 if (Request.QueryString["UptoAmtRange"].ToString() != "")
                                 {
                                     if (Request.QueryString["UptoAmtRange"].ToString() != "Ending Amount")
                                     {
                                         var castedValue = "";
                                         var castedValue2 = "";

                                         castedValue = Request.QueryString["startingAmtRange"];
                                         castedValue2 = Request.QueryString["UptoAmtRange"];

                                         //mrktAmount = SqlDbHelper.getDataDT(" SELECT * FROM    MTC_vw_MarketingProgAmt where totalAmtResources>='" + castedValue + "' and totalAmtResources<='" + castedValue2 + "'");
                                         mrktAmount = SqlDbHelper.getDataDT("exec MTC_sp_MarketingProgAmt " + castedValue + ", " + castedValue2 + "");
                                         
                                         if (mrktAmount.Rows.Count > 0)
                                         {
                                             for (int i = 0; i < mrktAmount.Rows.Count; i++)
                                             {
                                                 Response.Write("<tr>");
                                                 Response.Write("<td><a href=\"" + ResolveUrl("~/") + "MarketingProgram/MarketingProgramStatus?programNo=" + mrktAmount.Rows[i].ItemArray[0] + "\">" + mrktAmount.Rows[i].ItemArray[0] + "</a></td>");
                                                 // Response.Write("<td align=left>" + mrktAmount.Rows[i].ItemArray[0] + "</td>");
                                                 Response.Write("<td align=left>" + mrktAmount.Rows[i].ItemArray[1] + "</td>");
                                                 Response.Write("<td align=left>" + mrktAmount.Rows[i].ItemArray[2] + "</td>");
                                                 Response.Write("<td align=left>" + mrktAmount.Rows[i].ItemArray[3] + "</td>");
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
                             }
                         }
                     }
                 }     
                             
                                         %>

            </table>

           
        </div>
    
    </div>
    
</asp:Content>
