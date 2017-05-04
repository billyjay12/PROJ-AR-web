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
<script src="<%=ResolveUrl("~/") %>Scripts/weeklycalendar.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">

    var baseUrl = "<%=ResolveUrl("~/") %>";
    

</script>

    <style type="text/css">
        #calendar {
		width: 900px;
		margin: 0 auto;
		}
        .fc-event.fc-event-skin.fc-event-hori.fc-corner-left.fc-corner-right
        {
            background-color: green; /* background color */
            border-color: green;     /* border color */
            color: #FFFFFF;           /* text color */
        }
        .fc-event.fc-event-skin.fc-event-hori.fc-corner-left.fc-corner-right.newtype
        {
            background-color: #DD004A; /* background color */
            border-color: #DD004A;     /* border color */
            color: #FFFFFF;           /* text color */
         
        }
    </style>


    <div class="bl_box">


        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                          <b>Weekly Calendar</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>

        <%--<div style="padding:10px; font-size:12px;"> <%--id="calendar">--%>
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
              <%--<div id="calendar"></div>--%>
      <%--  <table border="1" cellspacing="0" cellpadding="3" width="100%">
        <tr>
        <td>
        <div id="calendar"></div>
        </td>
        </tr>
        
        </table>--
        </div>--%>
        



                   
        <div id="calendar_holder">
            <div id="calendar"></div>
        </div>
       
    
    
    
    </div>
</asp:Content>
