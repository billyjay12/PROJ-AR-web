<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="<%=ResolveUrl("~/") %>Content/mrktRequest.css" rel="stylesheet" type="text/css" /> 
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>  
    <script src="<%=ResolveUrl("~/") %>Scripts/createMrktngRequest.js" type="text/javascript"></script>     
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

		$(function () {			
			$("#txt_encoded_by").attr('value', '<%: Session["username"] %>');
            $("#txt_request_by").attr('value', '<%: Session["username"] %>');
			$("#txt_setup_date").datepicker();
            $("#txt_avail_deployOn").datepicker();
            $("#txt_actual_deployDate").datepicker();  
            //$("#txt_size").attr('disabled', 'disabled');
            //$("#txt_size").hide();
            //$("#size_field").hide();
       }); 
        
	</script>

   <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Marketing Request</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %>- <a href="<%=ResolveUrl("~/") %>Home/Logout">Logout</a>
                </td>
            </tr>
        </table>
    </div>
     
    <div class="simple_box" style="padding:15px; font-size:12px;">                            
    <div style="padding:15px; font-size:12px;">
         <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td><b>MARKETING REQUEST No.</b></td>
                <td><input type="text" id="Text1" readonly="readonly" /></td>
            </tr>                        
            <tr>
                <td>Encoded By</td>
                <td><input type="text" id="txt_encoded_by" readonly="readonly"/></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td> Account Code </td>
                <td><input type="text" id="txt_acct_code" onclick="javascript:LookUpData('txt_acct_code','ListOfCodeAndName');" class="required_fields" /></td>
            </tr>
            <tr>
                <td> Account Name </td>
                <td><input type="text" id="txt_acct_name" /></td>
            </tr>
            <tr>
                <td > Address</td>
                <td><input type="text" id="txt_address" class="required_fields" /></td>
            </tr>
            <tr>
                <td> Area</td>
                <td><input type="text" id="txt_area" /></td>             
            </tr>
            <tr>
                <td> Account Officer</td>
                <td><input type="text" id="txt_acct_officer" /></td>             
            </tr>
            <tr>
                <td> Requested By</td>
                <td><input type="text" id="txt_request_by" onclick="javascript:LookUpData('txt_request_by','ListOfSo');" /></td>             
            </tr>
            <tr>
                <td> Brand</td>
                <td><input type="text" id="txt_brand" onclick="javascript:LookUpData('txt_brand','ListofMarketingBrand');" class="required_fields" /></td>             
            </tr>
            <tr>
                <td> Category</td>
                <td><input type="text" id="txt_category" onclick="javascript:LookUpData('txt_category','ListOfCategoryType');" class="required_fields" /></td>             
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td>Setup Date</td>
                <td><input type="text" id="txt_setup_date" class="required_fields" /></td>             
            </tr>
            <tr>
                <td id="size_field"> Size</td>
                <td><input type="text" id="txt_size" /></td>             
            </tr>
            <tr>
                <td> Value</td>
                <td><input type="text" id="txt_value" class="required_fields" /></td>             
            </tr>
            <tr>
                <td> Available for Deployment on</td>
                <td><input type="text" id="txt_avail_deployOn" class="required_fields" /></td>             
            </tr>
            <tr>
                <td> Actual Deployment Date</td>
                <td><input type="text" id="txt_actual_deployDate" class="required_fields" /></td>             
            </tr>
        </table>      
    </div>

    <div style="padding:11px; font-size:12px;">
         <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;" id="tbl_other_stipulation">
            <tr>
                <td align="left"><b>Other Stipulations:</b></td>
            </tr>
            <tr>
                <td><input type="text" id="txt_stipulation" style="width:250px;" /></td>
                <td><a href="javascript:;" onclick="javascript:AddStipulation();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
            </tr>
         </table>      
    </div>


    <!--attachment created by hervie--->

    <div style="padding:11px; font-size:12px;">
         <table id="tbl_attachment" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
             <tr><b>Attachments:</b></tr>
             <tr>
                <td>
                   <input type="text" id="txt_attchmnt1"  style="width:150px;" onclick="javascript:CreateUploadingBox('txt_attchmnt1');" readonly="readonly" /> 
                </td>
                <td> <a href="javascript:AddAttachment();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a> </td>
             </tr>
         </table>
    </div>




   <!-- <div style="padding:11px; font-size:12px;">
         <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
             <b>Attachments:</b>
             <tr>
                <td>
                   <input type="text" id="txt_attchmnt1" size="50" onclick="javascript:CreateUploadingBox('txt_attchmnt1');" readonly="readonly" /> 
                </td>
                <td>
                   <a href="javascript:;" onclick="javascript:DeleteFileAttachment('A1');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>                
                </td>
             </tr>
             <tr>
                <td>
                   <input type="text" id="txt_attchmnt2" size="50" onclick="javascript:CreateUploadingBox('txt_attchmnt2');" readonly="readonly" /> 
                </td>
                <td>
                   <a href="javascript:;" onclick="javascript:DeleteFileAttachment('A2');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>                
                </td>
             </tr>
             <tr>
                <td>
                   <input type="text" id="txt_attchmnt3"  size="50" onclick="javascript:CreateUploadingBox('txt_attchmnt3');" readonly="readonly" /> 
                </td>
                <td>
                   <a href="javascript:;" onclick="javascript:DeleteFileAttachment('A3');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>                
                </td>
             </tr>
         </table>
    </div>-->
    <div class="simple_box">
         <center>        
	         <input type="button" value="Save" onclick="javascript:SaveMKRequestDoc();" /> 
	          &nbsp;&nbsp;  &nbsp;&nbsp;
	         <input type="button" value="Cancel" /> 
         </center>         
    </div>

   </div> 
   </div> 
</asp:Content>
