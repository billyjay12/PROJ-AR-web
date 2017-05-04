<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--<h2>mrktRequest</h2>-->
    <link href="<%=ResolveUrl("~/") %>Content/mrktRequest.css" rel="stylesheet" type="text/css" />
 
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

   <!-- <h2>mrktRequestCreate</h2> -->

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Create New Marketing Request</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %>- <a href="<%=ResolveUrl("~/") %>Home/Logout">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <a href="<%=ResolveUrl("~/") %>MrktngRequest/mrktRequestCreate">>> New Marketing Request</a>
                </td>
            </tr>
        </table>
    </div>
    </div>


</asp:Content>
