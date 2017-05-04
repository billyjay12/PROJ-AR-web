<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%:ResolveUrl("~/") %>Scripts/utilities_salesorderstatusupload.js" type="text/javascript"></script>

    <style type="text/css">
        
        #tbl_excel_contents { font-family:Arial; font-size:11px; }
        
    </style>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Sales Order Status Uploading</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:0;" >
        <center>
            <span id="decoy_span">&nbsp;</span>
        </center>
        <br /><br />
        

        <div style="display:block;">
        <center>
            <input type="hidden" id="txt_filename" /> 
            <table border="0" cellspacing="0" cellpadding="3">
            <tr>
                <td colspan="2">
                    <b>
                    <a style="text-decoration:none; color:blue;" href="<%:ResolveUrl("~/") %>Template/Template.xlsx">Download</a> Template
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;<br /></td>
            </tr>
            <tr>
                <td align="left">Date:</td>
                <td align="left"><input id="txt_upload_date" type="text" readonly="readonly" /></td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <input type="button" value="Upload Data" id="btn_upload" />
                </td>
            </tr>
            </table>
            
        </center>     
        </div>
        <br /><br />
        

    </div>
    </div>
    
</asp:Content>
