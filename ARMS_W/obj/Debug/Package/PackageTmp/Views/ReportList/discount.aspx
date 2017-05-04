<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <% 
        _User Ousr = new _User(Session["username"].ToString());
        const int IS_NOT_FOUND = -1;
        
    %>
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
                    <b>DISCOUNTS</b>
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
        <a href="javascript:LoadReport('', 'CS_REPORT/discIndividualActLedgerMTD.aspx');">Individual Discount - Ledger Type (MTD)</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/discIndividualActLedgerYTD.aspx');">Individual Discount - Ledger Type (YTD)</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/discIndividualActBrand.aspx');">Individual Discount per Brand</a>
    </div>
    <% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND || Ousr.HasPositionOf("asm") != IS_NOT_FOUND || 
       Ousr.HasPositionOf("brd") != IS_NOT_FOUND || Ousr.HasPositionOf("brm") != IS_NOT_FOUND || Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("csr") != IS_NOT_FOUND || Ousr.HasPositionOf("csm") != IS_NOT_FOUND || Ousr.HasPositionOf("vptfi") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("ceo") != IS_NOT_FOUND || Ousr.HasPositionOf("vw1") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("sstm") != IS_NOT_FOUND || Ousr.HasPositionOf("tmg") != IS_NOT_FOUND || Ousr.HasPositionOf("admin") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND)
       {%>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/discArea.aspx');">Discount per Area</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/discBrand.aspx');">Discount per Brand</a>
    </div>
     <%} %>

      <% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("brd") != IS_NOT_FOUND || Ousr.HasPositionOf("brm") != IS_NOT_FOUND || Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("csr") != IS_NOT_FOUND || Ousr.HasPositionOf("csm") != IS_NOT_FOUND || Ousr.HasPositionOf("vptfi") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("ceo") != IS_NOT_FOUND || Ousr.HasPositionOf("vw1") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("sstm") != IS_NOT_FOUND || Ousr.HasPositionOf("tmg") != IS_NOT_FOUND || Ousr.HasPositionOf("admin") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND)
       {%>
     <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/discChannel.aspx');">Discount per Channel</a>
    </div>
     <%} %>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/discAccount.aspx');">Discount per Account</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/dscReport.aspx');">Account Discount Report</a>
    </div>
    
</div>
</div>

</asp:Content>
