<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
 <script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
 <link href="<%=ResolveUrl("~/") %>Content/DynamicDialogBox.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar.print.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/MonthlyCalendar.js" type="text/javascript"></script>

<link href="<%=ResolveUrl("~/") %>Content/metroUi/MetroJs.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/") %>Scripts/metroscript/MetroJs.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>
 
 
  <script type="text/javascript" language="javascript">

        var baseurl = "<%= ResolveUrl("~/") %>";
  
    </script>
   

   <div class="bl_box">
   
    <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>My Coverage Plan</b>
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
                  <td align="left" valign="middle" class="style2">
                        <a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                 </td>
           

                 <td align="center" valign="middle"><b><span id="doc_stat_msg"></span></b></td>

            </tr>
        </table>
    </div>

     <div class="simple_box" style="padding:10px; font-size:12px;">
     <table style="background-color:#138da5;" cellpadding="0" cellspacing="0" width="100%">
     <tr>
     <td style="height:2px;" class="border_ts2"></td>
     </tr>
        <tr>
        <td style="width:100%; height:30px;" align="center" > <b style="font-size:large; color:white;">My Coverage Plan </b></td>
        </tr>

    <tr>
     <td style="height:2px;" class="border_ts1"></td>
     </tr>
     </table>

     <table style="width:100%; background-color:#eeeeee; border-right-color:#bcbcbc; border-right-style:solid; border-right-width:3px;border-left-color:#bcbcbc; border-left-style:solid; border-left-width:3px;  " align="center">
     <tr>
     <td align="right"><b style="vertical-align:top; font-size:larger;"">Year</b>&nbsp;&nbsp;<input type="text" onclick="javascript:LookUpData('txt_chse_year', 'ListofCoverageyear');" id="txt_chse_year" name="txt_chse_year" readonly="readonly" /></td>
     <%--<td style="text-align:right;">Year &nbsp; <input type="text" id="txt_chse_year" /></td>--%>
     </tr>
     </table>
     
     <table   cellpadding="0" cellspacing="0" width="100%" id="tbl_yearcalendar" height="100%">
                                                                                                                                                                   
    <tr style=" text-align:center; background-color:#eeeeee">
    <td id="tdjan" class="border_jan_hdr">


     <h1>JANUARY</h1>
    
  
   
     </td>

     


     <td id="tdfeb" style="background-color:#eeeeee; height:200px;" class="border_hdr">

  
        <h1>FEBRUARY</h1>
      
  
     
     </td>
     <td id="tdmar" style="background-color:#eeeeee; height:200px;" class="border_hdr" >

   
     <h1>MARCH</h1>


     </td>

     <td id="tdapr" style="background-color:#eeeeee; height:200px;" class="border_hdr">
  
     <h1>APRIL</h1>
    
        
     </td>
     
 </tr>

<tr style="text-align:center;" class="mrow">

      <td id="tdmay" style="background-color:#eeeeee; height:200px;" class="border_ryt_hdr">
   
     <h1>MAY</h1>


      </td>
      
     <td  id="tdjun" style="background-color:#eeeeee; height:200px;" class="border">
     
     <h1>JUNE</h1>
 
        
     </td>

     <td  id="tdjul" style="background-color:#eeeeee; height:200px;" class="border">
  
     <h1>JULY</h1>

     </td>

     <td  id="tdaug" style="background-color:#eeeeee; height:200px;" class="border">
 
     <h1>AUGUST</h1>

        

     </td>
     
     
     </tr>

     <tr style="text-align:center;" class="mrow">
     <td id="tdsep" style="background-color:#eeeeee; height:200px;" class="border_ryt_hdr">
    
     <h1>SEPTEMBER</h1>

        

     </td>
     <td id="tdoct" style="background-color:#eeeeee; height:200px;" class="border">
    
     <h1>OCTOBER</h1>

     

     </td>

     <td id="tdnov" style="background-color:#eeeeee; height:200px;" class="border">
    
     <h1>NOVEMBER</h1>
  

     </td>
     
     <td id="tddec" style="background-color:#eeeeee; height:200px;" class="border">
    
     <h1>DECEMBER</h1>
  
       

     </td>

     </tr>


     </table>
     
     </div>
   
   </div>
    

</asp:Content>
