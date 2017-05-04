<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%=ResolveUrl("~/") %>Content/ReportList.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlistindex.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
 <div class="bl_box">

    <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>SELL-OUT REPORTS</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
             </tr>
          </table>
   </div>

   <div class="page_header_y">
       
      </div>

<div class="simple_box">    
    <div class="report_menu">
        <a>Individual Sell-out per Brand</a>
    </div>
    <div class="report_menu">
        <a>Sell-out per Area</a>
    </div>
    <div class="report_menu">
        <a>Sell-out per Channel</a>
    </div>
    <div class="report_menu">
        <a>Sell-out per Brand</a>
    </div>
    <div class="report_menu">
        <a>Sell-out per Account</a>
    </div>
    
</div>
</div>

</asp:Content>
