<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
     <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/OutletAssign.js" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/complete.min.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

     <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Content/demo_table_jui.css"  type="text/css">
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.blockUI.js" type="text/javascript"></script>

        
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>
    
    <style type="text/css">
        .readonly
        {
            background-color:#ededed;
        }
    </style>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                          <b>Inventory Count Schedule </b>
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
                    <td align"left" valign="middle">
                        <a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                    </td>
               
                </tr>
            </table>
        </div>

        <div class="simple_box">
             <table id="tbl_list" style="width:100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CCANum</th>
                        <th>Account ID</th>
                        <th>Account Name</th>
                        <th>Account Location</th>
                        <th>Previous Count Date</th>
                        <th>Next Count Date</th>
                        <th>Sales Officer Name</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>

            <div style="display:none">
                <table id="tbl_header">
                    <tr>
                        <td>Account ID</td>
                        <td><input type="text" id="txt_outletId" readonly="readonly" title="Account ID" /></td>
                        <td>Sales Officer ID</td>
                        <td><input type="text" id="txt_empId" readonly="readonly" title="Sales Officer ID"/></td>
                 
                    </tr>
                    <tr>
                        <td>Account Name</td>
                        <td><input type="text" id="txt_outletName" readonly="readonly" title="Account Name" /></td>
                        <td>Sales Officer Name </td>
                        <td><input type="text" id="txt_empName" readonly="readonly" title="Sales Officer Name" /></td>
                
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align="right">  
                                <input type="button" value="Update" id="btn_save"/>
                                <input type="button" value="Cancel" id="btn_cancel" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
     <div id="div_last_element"></div>

</asp:Content>
