<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%=ResolveUrl("~/") %>Content/ReportList.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlist_inventorycount.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

     <div class="bl_box">

        <div class="page_header">
             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>INVENTORY COUNT</b>
                    </td>
                    <td align="right" valign="middle" >
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                 </tr>
              </table>
       </div>
    <div class="simple_box">    
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('monthlyinventorycount');">Monthly Inventory Count Report</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('quarterlyinventorycount');">Quarterly Inventory Count Report</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('annualinventorycount');">Annual Inventory Count Report</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('inventorycountreport');">Inventory Count Report</a>
        </div>
    </div>
    </div>

</asp:Content>
