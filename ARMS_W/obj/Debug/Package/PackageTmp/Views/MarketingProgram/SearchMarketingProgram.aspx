<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


 <link href="/Css/Themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script src="/Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="/Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="/Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="/Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="/Scripts/marketingSearchwindow.js" type="text/javascript"></script>
   
   

   
    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

	</script>



    <h2>Search Marketing Program</h2>



    <div  style="padding:15px; font-size:12px;">

        <table cellpadding="1" id="searchWindow" cellspacing="2" border="0" style="color:#000000;" >

        <tr>

        <td> By Brand </td>
        <td> <input type="text" id="txt_searchByBrand" size="27" onclick="javascript:LookUpData('txt_searchByBrand','ListofMarketingBrand');"/> </td>

        </tr>
        
        <tr>

        <td> <input type="button" value="Search" style="color:#000000; width: 70px;" onclick="javascript:getSearch();" /></td>

        </tr>




        </table>

    </div>








</asp:Content>
