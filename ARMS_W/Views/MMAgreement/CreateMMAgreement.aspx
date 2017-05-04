<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        _User oUsr = new _User(Session["username"].ToString());
        
        // delete temp files 
        try
        {
            UploadHelper.DeletePrevFiles(UploadHelper.MmaUploadDirectory + Session["username"] + "\\");
        }
        catch (Exception ex)
        {
            Response.Write("\n<!-- ");
            Response.Write(ex.Message);
            Response.Write(" -->\n");
        }
        
        // get the account codes
        
        
    %>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CreateMMAgreement.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/CreateMMAgreement.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
            $("#txt_meeting_date").datepicker();

            $("#txt_prepared_by").attr('value', '<%: oUsr.UserName %>');

            $("#tbl_list_of_actions tr:last td:nth-child(2) input[type=text]").datepicker();
        });

    </script>
    
    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>New Contracts and Agreements</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:15px; font-size:12px;">
        <table width="100%" cellpadding="1" cellspacing="0" border="0" >
            <tr>
                <td>
                    <!-- group 1 START -->
                    <table cellpadding="1" cellspacing="0" border="0">
                        <tr>
                            <td valign="top">Document Type</td>
                            <td valign="top">
                                <input type="text" id="txt_meeting_type" onclick="javascript:LookUpDocumentType('txt_meeting_type', 'ListOfMmaDocType');" readonly="readonly" /> &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Account Code</td>
                            <td>
                                <input type="text" id="txt_acct_code" onclick="javascript:LookUpData('txt_acct_code', 'ListOfAcctNameSoOnly');" readonly="readonly" class="required_fields" />
                            </td>
                        </tr>
                        <tr>
                            <td>Account Name</td>
                            <td><input type="text" id="txt_acct_name" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Account Officer</td>
                            <td><input type="text" id="txt_acct_officer" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Date</td>
                            <td>
                                <input type="text" id="txt_meeting_date" readonly="readonly" class="required_fields" /> 
                            </td>
                        </tr>
                        <!--
                        <tr>
                            <td>Time of Meeting</td>
                            <td><input type="text" id="txt_meeting_start" onclick="javascript:ShowTimeSelector('txt_meeting_start');" readonly="readonly" class="required_fields"  /> to <input type="text" id="txt_meeting_end" onclick="javascript:ShowTimeSelector('txt_meeting_end');" readonly="readonly" class="required_fields"  /></td>
                        </tr>
                        -->
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="simple_box">
                                    <h2>Over-all Objectives: </h2>
                                    <textarea id="txt_meeting_objective" rows="4" style="width:98%; height:50px;"></textarea>
                                </div> <br />
                                <div class="simple_box">
                                    <h2>Attendees </h2>
                                    <table cellpadding="1" cellspacing="0" border="0" id="tbl_list_of_attendee">
                                        <tr>
                                            <td>Names</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td><input type="text" style="width:300px;" /></td>
                                            <td><a href="javascript:AddEntryAttendees();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
                                        </tr>
                                    </table>
                                </div> <br />

                                <div class="simple_box">
                                    <h2>Actions </h2>
                                    <table cellpadding="1" cellspacing="0" border="0" id="tbl_list_of_actions">
                                        <tr>
                                            <td>Action Items</td>
                                            <td>Due Date</td>
                                            <td>Status</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td><input type="text" /></td>
                                            <td><input type="text" /></td>
                                            <td><input type="text" id="txt_meeting_status" onclick="javascript:LookUpDocumentType('txt_meeting_status', 'ListOfMeetingStat');" readonly="readonly" /></td>
                                            <td><a href="javascript:AddEntryActions();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                
                                <table id="tbl_MMA" border="0" cellpadding="1" cellspacing="0" >
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>File Attachment</td>
                                        <td>Description</td>
                                    </tr>
                                    <tr>
                                        <td><input type="text" id="txt_signed_file" onclick="javascript:CreateUploadingBox('txt_signed_file');" readonly="readonly" /></td>
                                        <td><input type="text" id="txt_signed_file_desc" /></td>
                                        <td><a href="javascript:AddMmaAttachments();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>Prepared by:</td>
                                        <td><input type="text" id="txt_prepared_by" class="required_fields" readonly="readonly" /></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                    <!-- group 1 END -->
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <!-- -->
                    <div class="simple_box">
                        <center>
                            <input type="button" value="Save" onclick="javascript:Save_Doc();" />
                        </center>
                    </div>
                    <!-- -->
                </td>
            </tr>
        </table>
    </div>
    </div>

</asp:Content>
