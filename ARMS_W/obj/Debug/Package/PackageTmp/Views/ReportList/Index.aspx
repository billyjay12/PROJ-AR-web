<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Reports</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <h2>Index</h2>

    <table cellpadding="1" cellspacing="0" border="0" >
    <tr>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinperchannelreport.aspx');">Report1</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/sellinbrand.aspx');">test</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/sellinftmledger.aspx')">test1</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/sellinytdledger.aspx')">test2</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/cmperaccount.aspx')">test3</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/cmperbrand.aspx')">test4</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinperareareport.aspx')">test5</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinperbrandreport.aspx')">test6</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/ftmandytdsellinpercustomerreport.aspx')">test7</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/individualcmperarea.aspx')">test8</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/individualcmperbrand.aspx')">test9</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/individualmtdcmreport.aspx')">test10</a>
        </td>
        <td>
            <a href="javascript:LoadReport('', 'CS_REPORT/individualytdcmreport.aspx')">test11</a>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    </table>

    </div>
</asp:Content>