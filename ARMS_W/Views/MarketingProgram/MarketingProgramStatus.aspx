<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>
<script runat="server">

    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%

        
        DataTable mrktProgram;
        mrktProgram = SqlDbHelper.getDataDT("select * from dbo.mrktProgram where programNo='" + Request.QueryString["programNo"].ToString() + "'");

        DataTable mrktResources;
        mrktResources = SqlDbHelper.getDataDT("select * from dbo.mrktResources where programNo='" + Request.QueryString["programNo"].ToString() + "'");

        DataTable mrktTimeline;
        mrktTimeline = SqlDbHelper.getDataDT("select *, convert(varchar(10), startFinish, 101) as 'startFinishFormatted', convert(varchar(10), endDate, 101) as 'endDateFormatted' from dbo.mrktTimeline where programNo='" + Request.QueryString["programNo"].ToString() + "'");

        DataTable mrktAttachments;
        mrktAttachments = SqlDbHelper.getDataDT("select * from dbo.mrktAttachments where programNo='" + Request.QueryString["programNo"].ToString() + "'");

        DataTable mrktActualActivities;
        mrktActualActivities = SqlDbHelper.getDataDT("select *, convert(varchar(10), date, 101) as 'dateFormatted'  from dbo.mrktActualActivities where programNo='" + Request.QueryString["programNo"].ToString() + "'");

       

        DataTable mrktTargetAccounts;
        mrktTargetAccounts = SqlDbHelper.getDataDT("select * from dbo.mrktTargetAccounts where programNo='" + Request.QueryString["programNo"].ToString() + "'");
        
        //
        
       // DataRow TotalResources;
        
        //TotalResources=SqlDbHelper.getDataDT("select totalAmtResources from dbo.mrktProgram where programNo='" + Request.QueryString["programNo"].ToString() + "'");
        
        // select the position of the user
        DataRow userInfo;
        userInfo = SqlDbHelper.getDataDT("select * from userHeader where userName='" + Session["username"] + "'").Rows[0];

       
        DataRow docStatusId;
        docStatusId = SqlDbHelper.getDataDT("select rtrim(ltrim(status)) as 'status' from dbo.mrktProgram where programNo= '" + Request.QueryString["programNo"].ToString() + "'").Rows[0];

        //System.Data.OleDb.OleDbDataReader xcelreader;

        //xcelreader = SqlDbHelper.getExlData("Select * from [sheet1$] ", "C:\\Documents and Settings\\hervieinoc\\Desktop\\sample.xlsx");


        DataTable runningCost;
        runningCost = SqlDbHelper.getDataDT("select * from MTC_RunningCostWithComma  where U_MP_NUM='" + Request.QueryString["programNo"].ToString() + "'");

        //foreach (DataRow item in runningCost.Rows) { 
        //item[1]
        
        //}
        
         
    %>


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



    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/MarketingProg.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";
        var GPreparedBy = "";
        $(function () {
            $("#tabs").tabs();
            $("#sub_tab").tabs();

            LoadDataMktprgm();
            Hide();
        });

        $(function (){
            var rac = $("#txt_RAC").val();
            var n = $.format.number(parseFloat(rac), '#,##0.00#');

            $("#txt_RAC").val(n);

        });


        var MKTPRGaccCode;
        function LoadDataMktprgm() {
            var MPD_mktgPNo;
            var MPD_progName;
            var MPD_projtType;
            var MPD_brand;
            var MPD_targetChan;
            var MPD_targetArea;
            var MPD_targetAccounts;
            var MPD_backgrnd;
            var MPD_objective;
            var MPD_measures;
            var MPD_prepby;
            var MPD_status;
           var  MPD_totalAmtResources;
           var MPD_runninCost;
           


            <%  foreach (DataRow row in mrktProgram.Rows) { %>
               MPD_mktgPNo = '<% Response.Write(row["programNo"].ToString().Trim()); %>';
               MPD_progName = '<% Response.Write(row["progName"].ToString().Trim()); %>';
               MPD_projtType = '<% Response.Write(row["progType"].ToString().Trim()); %>';
               MPD_brand = '<% Response.Write(row["brand"].ToString().Trim()); %>';
               MPD_targetChan = '<% Response.Write(row["targetChannel"].ToString().Trim()); %>';
             //  MPD_targetArea = '<% Response.Write(row["targetArea"].ToString().Trim()); %>';
               MPD_backgrnd = '<% Response.Write(row["backGround"].ToString().Trim()); %>';
               MPD_objective = '<% Response.Write(row["objective"].ToString().Trim()); %>';
               MPD_measures = '<% Response.Write(row["measures"].ToString().Trim()); %>';
               MPD_prepby = '<% Response.Write(row["preparedBy"].ToString().Trim()); %>';
               MPD_status = '<% Response.Write(row["status"].ToString().Trim()); %>';
               MPD_totalAmtResources = '<% Response.Write(row["totalAmtResources"].ToString().Trim()); %>';
              // alert(MPD_mktgPNo);
              GPreparedBy = MPD_prepby;
              
              
            <%  } %>

            $("#MPD_txt_mktgPNo").attr('value', MPD_mktgPNo);
            $("#MPD_txt_progName").attr('value', MPD_progName);
            $("#MPD_txt_projtType").attr('value',  MPD_projtType);
            $("#MPD_txt_brand").attr('value', MPD_brand);
            $("#MPD_txt_targetChan").attr('value', MPD_targetChan);
           // $("#MPD_txt_targetArea").attr('value', MPD_targetArea);
            $("#MPD_txt_backgrnd").attr('value', MPD_backgrnd);
            $("#MPD_txt_objective").attr('value', MPD_objective);
            $("#MPD_txt_measures").attr('value', MPD_measures);
            $("#MPD_txt_prepby").attr('value', MPD_prepby);
            $("#MPD_txt_status").attr('value', MPD_status);
            $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", MPD_totalAmtResources);
            
           
           <% foreach (DataRow item in runningCost.Rows) { %>
               
                 MPD_runninCost=  '<% Response.Write(item[1].ToString().Trim()); %>';

       <% 
       }
        %>
          
          $("#txt_RAC").attr('value', MPD_runninCost);


           // Target Account

           <%  foreach (DataRow row in mrktTargetAccounts.Rows) { 
           
           Response.Write("TarAccountsForDisplay(");
                    Response.Write("'" + row["acctCode"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["acctName"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["area"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["amountAlloc"].ToString().Trim() + "'");  
                    Response.Write(");\n");
             

             } 
             
             %>
           //$("#MPD_txt_targetAccounts").attr('value', MPD_targetAccounts);
           //MKTPRGaccCode=MPD_targetAccounts;




       


         //List of resourcess

//            <%  

//            foreach (DataRow row in mrktResources.Rows) { 
//                    Response.Write("ResourcesData(");
//                    Response.Write("'" + row["item"].ToString().Trim() + "'"); Response.Write(",");
//                    Response.Write("'" + row["dscription"].ToString().Trim() + "'"); Response.Write(",");
//                    Response.Write("'" + row["amount"].ToString().Trim() + "'");  
//                    Response.Write(");\n");
//                } 

//                Sample for query using excel file


//             while (xcelreader.Read())
//            {
//                System.Data.OleDb.OleDbDataReader Accountname;
//                Accountname = SqlDbHelper.getData("Select cardname from SAPSERVER.MATIMCO.dbo.OCRD where cardcode='"+xcelreader.GetValue(0).ToString()+"' ");

//             string nameAccount = "";

//             if (Accountname.Read()) {

//            nameAccount = Accountname.GetValue(0).ToString();

//           
//  
//        
//        }   

//                    Response.Write("ResourcesData(");
//                    Response.Write("'" + xcelreader.GetValue(0).ToString() + "'"); Response.Write(",");
//                    Response.Write("'" +  nameAccount  + "'"); Response.Write(",");
//                    Response.Write("'" + "" + "'");  
//                    Response.Write(");\n");



//            }



//            %>




           // list of Resources.
            <%  foreach (DataRow row in mrktResources.Rows) { 
                    Response.Write("ResourcesData(");
                    Response.Write("'" + row["item"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["dscription"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["amount"].ToString().Trim() + "'");  
                    Response.Write(");\n");
                } 
            %>







            // list of Timeline.
            <%  foreach (DataRow row in mrktTimeline.Rows) { 
                    Response.Write("TimelineData(");
                    Response.Write("'" + row["startFinishFormatted"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["endDateFormatted"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["task"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["responsiblePerson"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["updates"].ToString().Trim() + "'");  
                    Response.Write(");\n");
                } 
            %>



              // list of Attachment .
            <%  foreach (DataRow row in mrktAttachments.Rows) { 

                 
                    Response.Write("AttachmentData(");
                    Response.Write("'" + row["filePath"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["fileDesc"].ToString().Trim() + "'");  
                    Response.Write(");\n");
                } 
            %>



              // list of project Actual Activities .
            <%  foreach (DataRow row in mrktActualActivities.Rows) { 
                    Response.Write("AddProjectActualActivitiesData(");
                    Response.Write("'" + row["dateFormatted"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["activityUpdate"].ToString().Trim() + "'");  
                    Response.Write(");\n");
                } 
            %>

          var doc_stat_msg = '<%:AppHelper.GetMarketingDocStateMsg(docStatusId["status"].ToString()).Trim() %>';
            $("#doc_stat_msg").html(doc_stat_msg);

            }





function txt_projtType_onclick() {

}

    </script>

      <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Marketing Program Status</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
      <!-- <table align="center" >
        <tr>
            <td colspan="5" align="right" valign="top">
                <span id="doc_stat_msg"></span>
            </td>
        </tr>
        
    </table>-->
     
     <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
            <% if (userPosition == "ssg" || userPosition == "csr" || userPosition == "csm" || userPosition == "ssm")
                    { %>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingPog">>>Create New Marketing Program</a>
                </td>
                <% } %> 
            </tr>
        </table>
    </div>

       
   

 

    <div  style="padding:15px; font-size:12px;">

        <table cellpadding="1" id="Tbl_runactcost" cellspacing="2" border="0" style="color:#000000;" align="right">

         <tr>
        <td style="height: 25px">Running Actual Cost</td>
        <td style="height: 25px"><input type="text" id="txt_RAC" size="27"
                style="height: 22px; text-align:right" readonly="readonly" /></td>
        <td style="width:100px;"> <input type="button" id="btn_save" value="View Details" style="color:#000000; width: 75px;" onclick="javascript:displayWindowInfo();" /></td>
        </tr>

        <tr>
        <td></td>
        </tr>

        

       </table>
       </div>


        <div  style="padding:15px; font-size:12px;">

        <table cellpadding="1" id="tbl_mktgProgs" cellspacing="2" border="0" style="color:#000000;">
       
       
        <tr>
        <td style="width: 150px;"> Marketing Program No. </td>
        <td><input type="text" id="MPD_txt_mktgPNo" size="27" /></td>    
        </tr>

        <tr>
        <td></td>
        </tr>


        <tr>
        <td style="width: 150px;"> Program Name </td>
        <td><input type="text" id="MPD_txt_progName" size="27" readonly="readonly" /></td>
        </tr>

        
        <tr>
        <td style="width: 150px;"> Project Type </td>
        <td> <input type="text" id="MPD_txt_projtType" size="27" readonly="readonly" onclick="return txt_projtType_onclick()" /></td>
        </tr>

        <tr>
        <td style="width: 150px;"> Brand </td>
        <td><input type="text" id="MPD_txt_brand" size="27" readonly="readonly" /></td>
        </tr>

        <tr>
        <td style="width: 150px;"> Target Channel </td>
        <td><input type="text" id="MPD_txt_targetChan" size="27" readonly="readonly" /></td>
       

        </tr>


        <tr>
        <td style="width: 150px;"> Background </td>
        <td><textarea rows="2" cols="40" id="MPD_txt_backgrnd" readonly="readonly"></textarea></td>
        </tr>

        <tr>
        <td style="width: 150px;"> Objective </td>
        <td><textarea rows="2" cols="40" id="MPD_txt_objective" readonly="readonly"></textarea></td>
        </tr>

        <tr>
        <td style="width: 150px;"> Measures </td>
        <td><textarea rows="1" cols="40" id="MPD_txt_measures" readonly="readonly"></textarea></td>
        </tr>

        </table>
        </div>



        <div  style="padding:15px; font-size:12px;">
        <table cellpadding="1" id="Tbl_targetAccount" cellspacing="2" border="0" style="color:#000000;">

        <tr>
        <td><b>Target Account</b></td>
        </tr>

        <tr>
        
        <td style="width:100px; ">Account Code</td>
        <td style="width:200px; ">Account Name</td>
        <td style="width:150px; ">Area</td>
        <td style="width:150px; ">Amount Allocation</td>

        </tr>

       

        <tr>
         <td colspan="3" style="height: 26px"><input type="text" id="text4" readonly="readonly" style="width:100%" value="Total" /></td>
         <td style="height: 26px"> <input type="text" id="Total" readonly="readonly" size="40" value="0"/></td>

        </tr>
       

        </table>













   

        <div  style="padding:15px; font-size:12px;">
        <table cellpadding="1" id="Tbl_resources" cellspacing="2" border="0" style="color:#000000;">

        <tr>
        <td><b>Resources</b></td>
        </tr>

        <tr>
        
        <td style="width:100px; ">Item</td>
        <td style="width:200px; ">Description</td>
        <td style="width:150px; ">Amount</td>

        </tr>

        <tr>
        
        <td style="width:100px; height: 26px;"><input type="text" id="MPD_txt_item" size="40" readonly="readonly"/></td>
        <td style="width:200px; height: 26px;"><input type="text" id="MPD_txt_desc" style="width:97%" readonly="readonly"/></td>
        <td style="width:150px; height: 26px;"><input type="text" id="MPD_txt_Amount" size="40" readonly="readonly"/></td>
        

        </tr>

        <tr>
         <td colspan="2"><input type="text" id="text" readonly="readonly" style="width:100%" value="Total" /></td>
         <td> <input type="text" id="MPD_txt_totalAmt" readonly="readonly" size="40" value="0"/></td>

        </tr>
       

        </table>
        </div>


        <div style="padding:15px; font-size:12px;">
        <table cellpadding="1" id="tbl_timeline" cellspacing="2" border="0" style="color:#000000;">
       
       <tr>
       <td><b>Timeline</b></td>
       </tr>


       
        <tr>
        
        <td style="width:150px; ">Start Date</td>
        <td style="width:150px; ">End Date</td>
        <td style="width:150px; ">Task</td>
        <td style="width:150px; ">Responsible Person</td>
        <td style="width:150px; ">Remarks</td>

        </tr>

       <tr>
        
        <td style="width:150px; height: 26px;"><input type="text" id="MPD_txt_start" size="40" readonly="readonly"/></td>
        <td style="width:150px; height: 26px;"><input type="text" id="MPD_txt_endDate" size="40" readonly="readonly"/></td>
        <td style="width:150px; height: 26px;"><input type="text" id="MPD_txt_task" size="40" readonly="readonly"/></td>
        <td style="width:150px; height: 26px;"><input type="text" id="MPD_txt_respPer" size="40" readonly="readonly"/></td>
        <td style="width:150px; height: 26px;"><input type="text" id="MPD_txt-updates" size="40" readonly="readonly"/></td>
       

        </tr>


        </table>
        </div>


         <div  style="padding:15px; font-size:12px;">
         <table cellpadding="1" id="tbl_attachment" cellspacing="2" border="0" style="color:#000000;">

         <tr>
         
         <td><b> Attachments</b></td>
         
         </tr>

         <tr>

         <td style="width:150px;"> File </td>
         <td style="width:150px;"> Brief Discussion </td>

         </tr>




           <tr>
        
        <td style="width:150px; "><input type="text" id="MPD_txt_file" size="40" readonly="readonly"/></td>
        <td style="width:150px; "><input type="text" id="MPD_txt_bdiscussion" size="40" readonly="readonly"/></td>
        <td></td>
          </tr>
        
       

         </table>
         </div>


         <div style="padding:15px; font-size:12px;">
         <table cellpadding="1" id="tbl_PAA" cellspacing="2" border="0" style="color:#000000;">

         <tr> 
         <td style="width: 170px"> <b>Project Actual Activities </b> </td>
         </tr>

         <tr>
         <td style="width:170px;"> Date </td>
         <td style="width:150px;"> Activity/ Update </td>

         </tr>


         <tr>
         <td style="width:170px;"> <input type="text" id="MPD_txt_date" size="28"/> </td>
         <td style="width:150px;"> <input type="text" id="MPD_txt_Act/Upd" size="40"/> </td>


         </tr>

         </table>
         </div>


         <div style="padding:15px; font-size:12px;">
         <table cellpadding="1" id="Table4" cellspacing="2" border="0" style="color:#000000;">

         <tr> 
         
         <td> Prepared By:</td>
         <td></td>
         <td style="width:150px;"><input type="text" id="MPD_txt_prepby" size="40" readonly="readonly"/></td>
         
         </tr>

         </table>
         </div>

         </div>

         


 <% if (userInfo["position"].ToString() == "ssm" && docStatusId["status"].ToString() == AppHelper.MarketingProgramUserPosition("ssm"))
    { %>

    
            
            
<!--
             <div  style="padding:15px; font-size:12px;">
        <table cellpadding="2" cellspacing="0" border="0" align="center">
            <tr>
                <td><input type="button" value="Approved" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: Request.QueryString["programNo"].ToString() %>');"   /></td>
                <td><input type="button" value="Disapproved" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: Request.QueryString["programNo"].ToString() %>');"  /></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
    </div>-->
    <% } %>
       

    </div>
</asp:Content>
