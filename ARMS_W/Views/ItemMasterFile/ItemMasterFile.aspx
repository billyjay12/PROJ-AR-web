<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <%-- <b>Item Master File</b>--%>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
     <script src="<%=ResolveUrl("~/") %>Scripts/ItemMasterFile.js" type="text/javascript"></script>
     <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>


   <script src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js" type="text/javascript"></script>
 <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/complete.min.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.blockUI.js" type="text/javascript"></script>
     <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Content/demo_table_jui.css"  type="text/css">

    <script type="text/javascript" language="javascript">

         var baseurl = "<%= ResolveUrl("~/") %>";
    </script>

    <div class="bl_box">
    
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                          <b>Item Master File</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>

           <div class="page_header_y">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                    </td>
               
                </tr>
            </table>
        </div>

        <div class="simple_box">
                <%--<table>
                <tr>
                  <td><b>Brand</b></td>
                  <td><input type="text" id="txt_brand" /></td>
                </tr>
                <tr>
                  <td><b>Product Group</b></td>
                  <td><input type="text" id="txt_prodgrp" /></td>
                </tr>
                </table>

                <hr />--%>

                <table id="tbl_list" style="width:100%" cellspacing="0">
                     <thead>
                     <tr>
                         <th>Item Code</th>
                         <th>Description</th>
                         <th>Multiplier</th>
                         <th>T</th>
                         <th>W</th>
                         <th>L</th>
                         <th>Group</th>
                         <th>Brand</th>
                         <th>Applicable Channel</th>
                         <%--<th>Class</th>
                         <th>SRP bdft</th>
                         <th>Applicable Channel</th>
                         <th>Price List Name</th>--%>
                         <%--<th>Discount Rate(SRP)</th>--%>
                     </tr>
                     </thead>
                     <tbody>
                     </tbody>
                </table>
        
        </div>
    
    </div>

</asp:Content>
