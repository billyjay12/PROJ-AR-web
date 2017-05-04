<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

<script type="text/javascript" language="javascript">

    var baseUrl = "<%=ResolveUrl("~/") %>";
    

</script>




<div class="bl_box">

        <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Yearly Calendar</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="<%=ResolveUrl("~/") %>Home/Logout">Logout</a>
                </td>
            </tr>
        </table>

         </div>
        <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <a id="sub_menu_link_1"> Search </a>
                </td>
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

        <table border="1" cellspacing="0" cellpadding="3" width="100%">
        <tr>
       <td><input type="text" id="txt_EventId" readonly="readonly" /></td>
      
        </tr>
        <tr>
        <td>
        <div id="calendar"></div>
        </td>
        </tr>
        
        </table>
        



        </div>
                   
        
       
    
    
    
    </div>

</asp:Content>


               