<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


                                                                                                 
<script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
<link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar.print.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.min.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/gcal.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/Mycalendar.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

<link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-buttons.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-thumbs.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/") %>Scripts/jquery.fancybox.js" type="text/javascript"></script>



<script type="text/javascript" language="javascript">

    var baseUrl = "<%=ResolveUrl("~/") %>";
    
 
    
 

</script>

<style type="text/css">
    .fc-button.fc-button-month.fc-state-default.fc-state-active,
    .fc-button.fc-button-basicWeek.fc-state-default.fc-corner-right.fc-state-active
    {
        z-index:-1;
    }
    .style2
    {
        width: 77px;
    }
</style>

<div class="bl_box">

        <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Yearly Calendar</b>
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

              <%-- <td align="right">--%>
                    
                   <%--<span><a id="sub_menu_link_2"> Save and Send </a></span>--%><%--|
                    <a id="sub_menu_link_3"> Return </a>|
                    <a id="sub_menu_link_1"> Search </a>--%>
                    
               <%-- </td>--%>
            </tr>
        </table>
    </div>

        <div style="padding:10px; font-size:12px;" <%--id="calendar"--%>>
        <%--<table border="1" cellspacing="0" cellpadding="3" width="100%" id="MyCalendar">
          <tr class="first_row">
          </tr>
           <tr class="second_row">
           </tr>                                               
           <tr class="third_row">
           </tr>
           <tr class="fourth_row">
           </tr>


        
        </table>--%>
        
        <table border="0" cellspacing="0" cellpadding="3" width="100%" id="tbl_calendarholder">
        <tr>
<%--       <td><input type="text" id="txt_EventId" readonly="readonly" /><input type="button" id="btn_test" value="Test" /></td>
--%>      
        <td class="hiddenTD">
                <table>
                <tr>
                 <td></td>
                 <td><input type="hidden" id="txt_EventId"/></td>
                 <td></td>
                 <td></td>
                 <td><input type="hidden" id="txt_so_id"/></td>
                 <td><input type="hidden" id="txt_hidden_id" /></td>
                 <%--<td><b><span id="doc_stat_msg"></span></b></td>--%>
                </tr>
                </table>
        </td>
       
        </tr>
        <tr>
        <td>
        <div id="calendar"></div>
        </td>
        </tr>
        <tr><td align="center"> <input type="button" id="btn_SaveAndSend" value="Save and Send" /> </td></tr>
        <tr>
        <td  valign="top">
            
                <table>
                <tr>
                <td><b>Legend:</b></td>
                <td style="width:1px;"></td>
                <td style="background-color:#5C9DDE;" ><b><font color="black">Approved</font></b></td>
                <td style="width:1px;"></td>
                <td style="background-color:#59DEBC;" ><b><font color="black">Changes Approved</font></b></td>
                <td style="width:1px;"></td>
                <td style="background-color:#B1CACA;" ><b><font color="black">Changes Disapproved</font></b></td>
                <td style="width:1px;"></td>
                <td style="background-color:#FF5F5F;"><b><font color="black">Deleted</font></b></td>
                <td style="width:1px;"></td>
                <td style="background-color:#E8E819;"><b>Edited</b></td>
                <td style="width:1px;"></td>
                 </tr>
                </table>
           </td>

         
                
         </tr>
         </table>

        </td>
        

        
        
        </tr>
        
        </table>
        



        </div>
                   
        
       
    
    
    
    </div>

</asp:Content>


               