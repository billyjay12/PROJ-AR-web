﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>
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
                    <b>E-MAT REPORTS</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
             </tr>
          </table>
    </div>

    <div class="page_header_y">
       &nbsp;
    </div>

<div class="simple_box">    
     <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/E-MatTrans.aspx');">E-MAT TRANSACTIONS</a>
    </div>
</div>
</div>
</asp:Content>
