<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>
<script runat="server">


    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/AddNewUser.js" type="text/javascript"></script>

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
                    <b>Add New User</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

<div class="simple_box" style="padding:10px; font-size:12px; border: 0px; width: 619px;">
        <table align="right" style="width: 0px" >
            <tr>
                <td colspan="5" align="right" valign="top">
                    <span id=""></span>
                </td>
            </tr>
         </table>
     </div>

     <div class="simple_box" style="padding:15px; font-size:12px; border: 0px; width: 619px;">
		<table cellpadding="1" id="tbl_br1" cellspacing="1" border="0" 
             style="color:#000000; width: 458px;" >
            <tr>
                <td style="width: 129px; height: 30px;"> ID Number</td>
                <td style="width: 143px; height: 30px;"><input type="text" id="txt_idNo" readonly="readonly" onclick="javascript:LookUpData('txt_idNo','ListOfEmployee');" style="width: 250px" /></td>
                <td>
                 <a href="javascript:;" onclick="javascript:CheckUser();">Check</a>
                </td>
            </tr>
            <tr>
                 <td style="width: 129px;"> Last Name </td>
                 <td style="width: 143px">
		            <input type="text" id="txt_lname" readonly="readonly" style="width: 250px"/>
                 </td>
                 
            </tr>
            <tr>
                 <td style="width: 129px;"> First Name </td>
                 <td style="width: 143px">
		            <input type="text" id="txt_fname" readonly="readonly" style="width: 250px" />
                 </td>
                 
            </tr>
             <tr>
                 <td style="width: 129px;"> Position </td>
                 <td style="width: 143px">
		            <input type="text" id="txt_position" style="width: 250px" onclick="javascript:LookUpData('txt_position','ListRoleID');" />
                 </td>
            </tr> 
            <tr> 
             <td colspan="2"><hr /></td>
             </tr>
             <tr>
                <td style="width: 129px;"> Email Address</td>
                <td style="width: 143px">
                    <input type="text" id="txt_email" style="width: 250px" /></td>
            </tr>
            <tr>
                <td style="width:129px;"> Username</td>
                <td style="width:143px;"><input type="text" id="txt_username" size="26" style="width: 250px"/></td>
            </tr>
            <tr>
                 <td style="width: 129px;"> Password </td>
                 <td style="width: 143px">
		            <input type="password" id="txt_password" style="width: 250px"/>
                 </td>
            </tr>
            <tr>
                 <td style="width: 129px;"> Confirm Password </td>
                 <td style="width: 143px">
		            <input type="password" id="txt_confirm_password" style="width: 250px"/>
                 </td>
            </tr>
             
             <tr> 
             <td colspan="2"><hr /></td>
             </tr> 
           
            <tr>
                <td style="width: 129px;"> Area </td>
                <td style="width: 143px">
                    
		            <input type="text" id="txt_area" style="width: 250px" />
                 </td>
            </tr>
            <tr>
                 <td style="width: 129px;"> Territory </td>
                 <td style="width: 143px">
		            <input type="text" id="txt_territory" style="width: 250px" />
                 </td>
            </tr> 
             <tr>
                 <td style="width: 129px;"> Region </td>
                 <td style="width: 143px">
		            <input type="text" id="txt_region" style="width: 250px" />
                 </td>
            </tr> 
          </table>      
        </div>

        <div  style="padding:15px; font-size:12px;">
                            <table cellpadding="2" cellspacing="0" border="0" align="center">
                                <tr>
                                    <td><input type="button" id="btn_add" value="Add" style="color:#000000;" onclick="javascript:AddUser()"/></td>
                                    <td><input type="button" id="btn_cancel" value="Cancel" style="color:#000000;" onclick="javascript:Cancel()"/></td>
                                   
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                        </table>
                </div>
    </div>
</asp:Content>
