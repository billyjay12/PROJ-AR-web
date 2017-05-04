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
                    <b>SALES OPPORTUNITIES</b>
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
        <a href="javascript:LoadReport('', 'CS_REPORT/Leads-Endorsement.aspx');">Leads Endorsement Report</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/Leads-Forecast.aspx');">Leads Forecast</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/Pipeline-Per-SO-Per-Account.aspx');">Pipeline per SO per Account</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/Pipeline-Forecast.aspx');">Pipeline Forecast</a>
    </div>
     <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/Sales-Quotation-Aging.aspx');">Aging of Pipeline</a>
    </div>
    
</div>
</div>

</asp:Content>
