<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%=ResolveUrl("~/") %>Content/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/fullcalendar.print.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>
   <%-- <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.draggable.js" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/uploadmonthlycoveragepreview_details.js"></script>
    <div class="div_title_bar">
        <b>Upload Monthly Coverage Preview</b>
    </div>
    <div class="div_content">
        <div id="calendar"></div>
    </div>
    



</asp:Content>
