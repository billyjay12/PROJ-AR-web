<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<script runat="server">

   
</script>
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
                    <b>SELL-IN REPORTS</b>
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
<% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND || Ousr.HasPositionOf("asm") != IS_NOT_FOUND || 
       Ousr.HasPositionOf("brd") != IS_NOT_FOUND || Ousr.HasPositionOf("brm") != IS_NOT_FOUND || Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("csr") != IS_NOT_FOUND || Ousr.HasPositionOf("csm") != IS_NOT_FOUND || Ousr.HasPositionOf("vptfi") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("ceo") != IS_NOT_FOUND || Ousr.HasPositionOf("vw1") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("sstm") != IS_NOT_FOUND || Ousr.HasPositionOf("tmg") != IS_NOT_FOUND || Ousr.HasPositionOf("admin") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND)
       {%>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinperareareport.aspx');">Sell-in per Area</a>
    </div>
     <%} %>

     <% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND ||Ousr.HasPositionOf("brd") != IS_NOT_FOUND ||
            Ousr.HasPositionOf("brm") != IS_NOT_FOUND || Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND ||Ousr.HasPositionOf("csr") != IS_NOT_FOUND ||
            Ousr.HasPositionOf("csm") != IS_NOT_FOUND || Ousr.HasPositionOf("vptfi") != IS_NOT_FOUND ||Ousr.HasPositionOf("ceo") != IS_NOT_FOUND || 
            Ousr.HasPositionOf("vw1") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND || Ousr.HasPositionOf("sstm") != IS_NOT_FOUND ||
            Ousr.HasPositionOf("tmg") != IS_NOT_FOUND || Ousr.HasPositionOf("admin") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND)
       {%>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinperchannelreport.aspx');">Sell-in per Channel</a>
    </div>
     <%} %>
     
     <% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND || Ousr.HasPositionOf("asm") != IS_NOT_FOUND || 
       Ousr.HasPositionOf("brd") != IS_NOT_FOUND || Ousr.HasPositionOf("brm") != IS_NOT_FOUND || Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("csr") != IS_NOT_FOUND || Ousr.HasPositionOf("csm") != IS_NOT_FOUND || Ousr.HasPositionOf("vptfi") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("ceo") != IS_NOT_FOUND || Ousr.HasPositionOf("vw1") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("sstm") != IS_NOT_FOUND || Ousr.HasPositionOf("tmg") != IS_NOT_FOUND || Ousr.HasPositionOf("admin") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND)
       {%>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinperbrandreport.aspx');">Sell-in per Brand</a>
    </div>
     <%} %>

    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinpercustomer.aspx');">Sell-in per Account</a>
    </div>

    <% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND || Ousr.HasPositionOf("asm") != IS_NOT_FOUND || 
       Ousr.HasPositionOf("brd") != IS_NOT_FOUND || Ousr.HasPositionOf("brm") != IS_NOT_FOUND || Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("csr") != IS_NOT_FOUND || Ousr.HasPositionOf("csm") != IS_NOT_FOUND || Ousr.HasPositionOf("vptfi") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("ceo") != IS_NOT_FOUND || Ousr.HasPositionOf("vw1") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND || 
       Ousr.HasPositionOf("sstm") != IS_NOT_FOUND || Ousr.HasPositionOf("tmg") != IS_NOT_FOUND || Ousr.HasPositionOf("admin") != IS_NOT_FOUND ||
       Ousr.HasPositionOf("so") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND)
       {%>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/sellinftmledger.aspx');">Individual Sell-in (FTM Ledger Type)</a>
    </div>
     
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/sellinytdledger.aspx');">Individual Sell-in (YTD Ledger Type)</a>
    </div>
   
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/sellinbrand.aspx');">Individual Sell-in per Brand</a>
    </div>
     <%} %>
    <!--<div class="report_menu">
        <a>Posted Transaction with Discount Info</a>
    </div>-->
    
</div>
</div>

</asp:Content>
