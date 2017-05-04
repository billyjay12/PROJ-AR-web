<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>
<script runat="server">

   
    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% DataTable userAccessTable;  %>

<link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/usserAccess.js" type="text/javascript"></script>

     <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

//        $(function () {
//            $("#tabs").tabs();
//            $("#sub_tab").tabs();
//             });

 </script>


     <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>User's Access</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>


    <div class="simple_box" 
             style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px; width: 645px;">
        <table align="right" style="width: 0px" >
            <tr>
                <td colspan="5" align="right" valign="top">
                    <span id=""></span>
                </td>
            </tr>
         </table>
     </div>


     <div class="simple_box" style="padding:15px; font-size:12px; border: 0px; width: 619px;">
		<table cellpadding="1" id="tbl_br1" cellspacing="1" border="0" style="color:#000000; width: 624px;" align="center" >
            <tr>
                       <td style="width:70px; height: 29px;"> Role ID </td>
                       <td style="width: 243px; height: 29px;"><input type="text" id="txt_roleID" readonly="readonly" style="width: 150px"  /> </td>
                       <td style="width: 70px; height: 29px;"></td>
                       <td style="width: 70px; height: 29px;"> Module</td>
                       <td style="height: 29px"><input type="text" id="txt_module" readonly="readonly" style="width: 150px" onclick="javascript:LookUpData('txt_module','docdesc');" /></td>
               </tr>
               <tr>
                       <td style="width:70px">User Name</td>
                       <td style="width: 243px"><input type="text" id="txt_Username" readonly="readonly" onclick="javascript:LookUpData('txt_Username','UsserID');" style="width: 150px" /></td>
                       <td></td>
                       <td>Acess</td>
                        <td><select id="txt_Acess" style="width: 158px">
                            <option>Select Access Rights</option>
                            <option>No Access</option>
                            <option>Read Only</option>
                            <option>Full Access</option>
                            </select></td>
              </tr>

               <tr>
               <td style="height:30px"></td>
               </tr>
               

           </table>  
           
        <table cellpadding="1" id="Table1" cellspacing="0" border="0" style="color:#000000;" align="center">
        <tr>
        
        <td style="width:50px"></td>
        <td><input type="button" id="btn_save" value="Save" style="color:#000000; width: 70px;" onclick="javascript:DocSave();" /></td>
        <td></td>
        <td><input type="button" value="Cancel" id="btn_cancel" style="color:#000000; width: 70px; " onclick="javascript:cancel();" /></td>
        </tr>
        <tr>
        <td style="height:10px"></td>
        </tr>
        
        </table>
            </div>
                       
           <hr style="width: 100%"/>  
        

          <div class="simple_box" style="padding:15px; font-size:12px; border: 0px; width: 619px;">    
		<table cellpadding="1" id="Tbl_usedtls" cellspacing="1" border="1" style="color:#000000; width: 624px;" align="center">

        <tr>
				<th style="width: 100px;">Role ID</th>
				<th style="width: 150px;">Role</th>
				<th style="width: 200px;">Module</th>
                <th style="width: 150px;">Acess Rights</th>
                
			</tr>
             <% 
                 userAccessTable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfUserAcess());
                if (userAccessTable.Rows.Count > 0)
                {
                    for (int i = 0; i < userAccessTable.Rows.Count; i++)
                    {
                    Response.Write("<tr>");
                    Response.Write("<td>" + userAccessTable.Rows[i].ItemArray[0] + "</td>");
                    Response.Write("<td>" + userAccessTable.Rows[i].ItemArray[1] + "</td>");
                    Response.Write("<td>" + userAccessTable.Rows[i].ItemArray[2] + "</td>");
                    Response.Write("<td>" + userAccessTable.Rows[i].ItemArray[3] + "</td>");
                    Response.Write("</tr>");
                    }
                }
            %>

           

        </table>
             </div>
             </div>
</asp:Content>
