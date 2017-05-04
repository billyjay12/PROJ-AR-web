<%@ Page Title="ARMS-Maintenance" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master"  Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>




      

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">       
   <% DataTable approverRoleTable;  %>
  <% DataTable desigApprvrTable;  %>
  <% DataTable routingPerModule;  %>

    
	<link href="<%=ResolveUrl("~/") %>Content/routing.css" rel="stylesheet" type="text/css" />
    <link href="/Css/Themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/Routing.js" type="text/javascript"></script>   	
 

    <script type="text/javascript" language="javascript">        
        var baseUrl = "<%= ResolveUrl("~/") %>";

    </script>  
  
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Routing and User Roles</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px; width: 645px;">
        <table align="right" style="width: 0px" >
            <tr>
                <td colspan="5" align="right" valign="top">
                    <span id=""></span>
                </td>
            </tr>
         </table>
     </div>

     <div class="user_roles" style="padding:15px; font-size:12px; border: 0px; width: 619px;">
       <p><b> User Roles </b></p>
		<table cellpadding="1" id="tbl_br1" cellspacing="1" border="0" style="color:#000000; width: 624px;" align="center" >
           <tr>
                       
                       <td style="width: 70px; height: 29px;"> Role Name</td>
                       <td style="height: 29px"><input type="text" id="txt_UserRole_name"  style="width: 150px" /></td>
               </tr>
        </table>
        <table cellpadding="1" id="Table1" cellspacing="0" border="0" style="color:#000000;" align="center">
          <tr>        
            <td style="width:50px"></td>
            <td><input type="button" id="btn_save" value="Add" style="color:#000000; width: 70px;" onclick="javascript:addUserRole();" /></td>
            <td></td>
            <td><input type="button" value="Cancel" id="btn_cancel" style="color:#000000; width: 70px; " onclick="javascript:cancel();" /></td>
          </tr>
          <tr>
            <td style="height:10px"></td>
          </tr>        
        </table>
     
   
    <div> 
      <table cellpadding="1" id="Tbl_usedtls" cellspacing="1" border="1" style="color:#000000; width: 624px;" align="center">

        <tr>
		   <th style="width: 10px;">Role ID</th>
		   <th style="width: 20px;">Role Name</th>		               
		</tr>
             <% 
                 approverRoleTable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfRoles());
                 if (approverRoleTable.Rows.Count > 0)
                {
                    for (int i = 0; i < approverRoleTable.Rows.Count; i++)
                    {
                      Response.Write("<tr>");
                      Response.Write("<td>" + approverRoleTable.Rows[i].ItemArray[0] + "</td>");
                      Response.Write("<td>" + approverRoleTable.Rows[i].ItemArray[1] + "</td>");                    
                      Response.Write("</tr>");
                    }
                }
            %>     

         </table>  
     
   </div>  
   
   <br />
   <br />
     <hr style="width: 100%"/>       

  </div>       
  

  <div class="approval_Desig" style="padding:15px; font-size:12px; border: 0px; width: 619px;">
    <p><b> Designated Approvers </b></p>
		<table cellpadding="1" id="Table2" cellspacing="1" border="0" style="color:#000000; width: 624px;" align="center" >
           <tr>
             <td style="width:90px; height: 29px;"> Role ID </td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Txt_desig_roleID" readonly="readonly" style="width: 150px"  /> </td>
             <td style="width: 70px; height: 29px;"></td>
             <td style="width: 70px; height: 29px;">Name</td>
             <td style="height: 29px"><input type="text" id="Txt_desig_Name" readonly="readonly" style="width: 150px" onclick="javascript:LookUpData('Txt_desig_Name','routingEmp');" /></td>
             <td style="width: 70px; height: 29px;">Id</td>
             <td style="height: 29px"><input type="text" id="Txt_desig_IdNo" readonly="readonly" style="width: 150px" /></td>
           <tr/>
           <tr>
             <td style="width:90px; height: 29px;"> Role Name </td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Txt_desig_RoleName" readonly="readonly" style="width: 150px" onclick="javascript:LookUpData('Txt_desig_RoleName','roleName');" /> </td>
             <td style="width: 70px; height: 29px;"></td>
             <td style="width: 70px; height: 29px;"> E-Mail</td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Txt_desig_eMail" readonly="readonly" style="width: 150px"  /> </td>
             <td style="width: 70px; height: 29px;"> Channel</td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Txt_channel" readonly="readonly" style="width: 150px" onclick="javascript:LookUpData('Txt_channel','ListOfChannels');" /> </td>
           </tr>
           <tr>
             <td style="width:90px; height: 29px;"> Branch </td>
             <td><select id="Txt_desig_branch" style="width: 158px" onclick="return Txt_desig_branch_onclick()">
                            <option>VM</option>
                            <option>LZ</option>
                            </select> </td>
             <td style="width: 70px; height: 29px;"></td>

             <td style="width:90px; height: 29px;"> Brand </td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Txt_desig_brand"  style="width: 150px" onclick="javascript:LookUpData('Txt_desig_brand','ListOfRoutingBrand');" /> </td>
             <td style="width:90px; height: 29px;"> Area </td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Txt_area"  style="width: 150px" onclick="javascript:LookUpData('Txt_area','ListOfArea');" /> </td>
           </tr>
        </table>

        <table cellpadding="1" id="Table3" cellspacing="0" border="0" style="color:#000000;" align="center">
          <tr>        
            <td style="width:50px"></td>
            <td><input type="button" id="Button1" value="Save" style="color:#000000; width: 70px;" onclick="javascript:addDesigApprvr();" /></td>
            <td></td>
            <td><input type="button" value="Cancel" id="Button2" style="color:#000000; width: 70px; " onclick="javascript:cancel();" /></td>
          </tr>
          <tr>
            <td style="height:10px"></td>
          </tr>        
        </table>

    
    <div> 
      <table cellpadding="1" id="Table4" cellspacing="1" border="1" style="color:#000000; width: 624px;" align="center">

        <tr>
		   <th style="width: 10px;">Role ID</th>
		   <th style="width: 30px;">Role Name</th>	
           <th style="width: 75px;">Employee Name</th>
           <th style="width: 40px;">Employee E-Mail</th>
           <th style="width: 15px;">Branch</th>	 
           <th style="width: 15px;">Brand</th>	              
		</tr>
             <% 
                 desigApprvrTable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfDesigApprvr());
                 if (desigApprvrTable.Rows.Count > 0)
                {
                    for (int i = 0; i < desigApprvrTable.Rows.Count; i++)
                    {
                      Response.Write("<tr>");
                      Response.Write("<td>" + desigApprvrTable.Rows[i].ItemArray[0] + "</td>");
                      Response.Write("<td>" + desigApprvrTable.Rows[i].ItemArray[1] + "</td>");
                      Response.Write("<td>" + desigApprvrTable.Rows[i].ItemArray[2] + "</td>");
                      Response.Write("<td>" + desigApprvrTable.Rows[i].ItemArray[3] + "</td>");
                      Response.Write("<td>" + desigApprvrTable.Rows[i].ItemArray[4] + "</td>");
                      Response.Write("<td>" + desigApprvrTable.Rows[i].ItemArray[5] + "</td>");                      
                      Response.Write("</tr>");
                    }
                }
            %>     

         </table>  
     
   </div>  
    
   <br />
   <br />
     <hr style="width: 100%"/>       
  
  </div>                    

  <div class="routing" style="padding:15px; font-size:12px; border: 0px; width: 619px;">
     <p><b> Routing </b></p>
       <table cellpadding="1" id="Table5" cellspacing="1" border="0" style="color:#000000; width: 624px;" align="center" >
           <tr>
             <td style="width:130px; height: 29px;"> Routed To</td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Text1" readonly="readonly" style="width: 150px"  /> </td>
             <td style="width: 70px; height: 29px;"></td>
             <td style="width: 70px; height: 29px;">Module</td>
             <td style="height: 29px"><input type="text" id="Text2" readonly="readonly" style="width: 150px" onclick="javascript:LookUpData('txt_module','docdesc');" /></td>
           <tr>
             <td style="width:90px; height: 29px;"> State Id </td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Text3" readonly="readonly" style="width: 150px"  /> </td>
             <td style="width: 70px; height: 29px;"></td>
             <td style="width: 150px; height: 29px;"> State Description</td>
             <td style="width: 243px; height: 29px;"><input type="text" id="Text4" readonly="readonly" style="width: 150px"  /> </td>
           </tr>
        </table>
        <table cellpadding="1" id="Table6" cellspacing="0" border="0" style="color:#000000;" align="center">
          <tr>        
            <td style="width:50px"></td>
            <td><input type="button" id="Button3" value="Save" style="color:#000000; width: 70px;" onclick="javascript:DocSave();" /></td>
            <td></td>
            <td><input type="button" value="Cancel" id="Button4" style="color:#000000; width: 70px; " onclick="javascript:cancel();" /></td>
          </tr>
          <tr>
            <td style="height:10px"></td>
          </tr>        
        </table>

     <div> 
      <table cellpadding="1" id="Table7" cellspacing="1" border="1" style="color:#000000; width: 624px;" align="center">

        <tr>
		   <th style="width: 10px;">Module Name</th>
		   <th style="width: 30px;">Routed To</th>	
           <th style="width: 20px;">State Id</th>
           <th style="width: 40px;">State Description</th>                    
		</tr>
             <% 
                 routingPerModule = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfRoutingPerModule());
                 if (routingPerModule.Rows.Count > 0)
                {
                    for (int i = 0; i < desigApprvrTable.Rows.Count; i++)
                    {
                      Response.Write("<tr>");
                      Response.Write("<td>" + routingPerModule.Rows[i].ItemArray[0] + "</td>");
                      Response.Write("<td>" + routingPerModule.Rows[i].ItemArray[1] + "</td>");
                      Response.Write("<td>" + routingPerModule.Rows[i].ItemArray[2] + "</td>");
                      Response.Write("<td>" + routingPerModule.Rows[i].ItemArray[3] + "</td>");                                                            
                      Response.Write("</tr>");
                    }
                }
            %>     

         </table>  
     
   </div>  

  </div>
       
 </asp:Content>



