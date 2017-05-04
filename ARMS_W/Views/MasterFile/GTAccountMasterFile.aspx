<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/GTAccountMasterFile.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Content/demo_table_jui.css"  type="text/css">
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/complete.min.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js"></script>
    
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.blockUI.js" type="text/javascript"></script>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                          <b>GT Account Master File</b>
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
                        <a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" alt="" /> MENU</a>
                    </td>
               
                </tr>
            </table>
        </div>
        <div class="simple_box">
            <div>
            <table id="tbl_header">
                <tr>
                    <td>Account Code</td>
                    <td><input type="text" id="txt_acctCode" readonly="readonly" style="background:#ededed"/></td>
                    <td>Pareto: </td>
                    <td><%--<input type="text" id="txt_pareto" readonly="readonly" style="text-align:center"/>--%>
                        <input type="radio" id="btnrad_yes" name="pareto" value="true" onclick="handleClick(this)"/> Yes
                        <input type="radio" id="btnrad_no" name="pareto" value="false" onclick="handleClick(this)" /> No
                    </td>
                </tr>
                <tr>
                    <td>Account Name</td>
                    <td colspan="2"><input type="text" id="txt_acctName" readonly="readonly" style="background:#ededed" /></td>
                    <td align="right">  
                        <input type="button" value="Update" id="btn_save"/>
                        <input type="button" value="Cancel" id="btn_cancel" />
                    </td>
                </tr>
            </table>
            </div>
        </div>
        <div>
            <table id="tbl_list" style="width:100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CCA Num</th>
                        <th>Account Code</th>
                        <th>Account Name</th>
                        <th>SO Name</th>
                        <th>Area</th>
                        <th>Pareto</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
