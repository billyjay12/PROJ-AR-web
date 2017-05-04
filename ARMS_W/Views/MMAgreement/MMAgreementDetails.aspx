<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        string CurDocId = Request.QueryString["agreeno"].ToString();
        
        const int IS_NOT_FOUND = -1;
        _User oUsr = new _User(Session["username"].ToString());
        
        _Document oDocument = new _Document("MMA", CurDocId);
        
        DataTable mtgMinutesAgreement;
        mtgMinutesAgreement = SqlDbHelper.getDataDT("select *, convert(varchar(10),MtgDate,101) as 'FormattedMtgDate',convert(varchar(10),MtgTimeStart,101) as 'FormattedMtgTimeStart',convert(varchar(10),MtgTimeEnd,101) as 'FormattedMtgTimeEnd'" +
            ", (select soname from SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd where cardcode=mtgMinutesAgreement.acctcode collate SQL_Latin1_General_CP1_CI_AS) as 'soname' " +
            ", case when right(left(acctcode,2),1)='L' then 'Luzon' when right(left(acctcode,2),1)='V' then 'Vismin' end as 'docregion' " +
            " from mtgMinutesAgreement where agreeno=" + CurDocId + "");

        DataRow mtgMinutesAgreement_row = mtgMinutesAgreement.Rows[0];

        // string doc_region = StringHelper.GetRegion(mtgMinutesAgreement_row["docregion"].ToString().Trim());
        
        DataTable mtgAttendees;
        mtgAttendees = SqlDbHelper.getDataDT("select * from mtgAttendees where agreeno=" + CurDocId + "");

        DataTable mtgActionItems;
        mtgActionItems = SqlDbHelper.getDataDT("select * from mtgActionItems where agreeno=" + CurDocId + "");

        DataTable mtgAttachment;
        mtgAttachment = SqlDbHelper.getDataDT("select * from mtgAttachment where agreeno=" + CurDocId + "");
        
        
    %>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/MMAgreementsDetails.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/MMAgreementDetails.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
        var m_attachments="";
        var MMAreq_id="";

        $(function () {
            $("#txt_meeting_date").datepicker("option", "disabled");

            LoadData();
            disable_controls();

        });

        function LoadData()
        {
            var m_request_id = "";
            var m_acct_code = "";
            var m_acct_name = "";
            var m_acct_so_officer = "";
            var m_meeting_type = "";
            var m_meeting_date = "";
            var m_meeting_objective = "";
            var m_meeting_prepared_by = "";
            var m_mma_status = "";

            m_request_id = "<%: CurDocId %>";

            <%  foreach (DataRow row in mtgMinutesAgreement.Rows) { %>
                m_acct_code = "<% Response.Write(row["acctCode"].ToString().Trim()); %>";
                m_acct_name = "<% Response.Write(StringHelper.CTJ(row["acctName"].ToString().Trim())); %>";
                m_acct_so_officer = "<% Response.Write(row["soname"].ToString().Trim()); %>";
                m_meeting_type = "<% Response.Write(row["mtgType"].ToString().Trim()); %>";
                m_meeting_date = "<% Response.Write(row["FormattedMtgDate"].ToString().Trim()); %>";
                m_meeting_objective = "<% Response.Write(StringHelper.CTJ(row["mtgObjective"].ToString().Trim())); %>";
                m_meeting_prepared_by = "<% Response.Write(row["preparedby"].ToString().Trim()); %>";
                m_mma_status = "<% Response.Write(AppHelper.GetMMaDocStatusMessage(row["status"].ToString().Trim())); %>";
            <%  } %>


            $("#txt_acct_code").attr("value", m_acct_code);
            $("#txt_acct_name").attr("value", m_acct_name);
            $("#txt_acct_officer").attr("value", m_acct_so_officer);
            $("#txt_meeting_type").attr("value", m_meeting_type);
            $("#txt_meeting_date").attr("value", m_meeting_date);
            
            $("#txt_meeting_objective").val(m_meeting_objective);
            $("#txt_prepared_by").attr("value", m_meeting_prepared_by);
            $("#txt_doc_status").html(m_mma_status);
            
            var m_meeting_signed_minutes = "";
            var m_meeting_signed_minutes_desc = "";
           // var m_attachment;
       
            MMAreq_id=m_request_id;
            <%  foreach (DataRow row in mtgAttachment.Rows) {%>
                <% 
                    string url_link = "";
                    url_link = "<a href='" + ResolveUrl("~/") + "SQL/DownloadFile?doctype=MMA&fileName=" + HttpUtility.UrlEncode(row["filePath"].ToString().Trim()) + "&id=" + row["agreeno"].ToString().Trim() + "'><img src='" + ResolveUrl("~/") + "Images/page_white_get.png' style='border:0;' /></a>";
                    Response.Write("AttachmentData(");
                    Response.Write("'" + StringHelper.CTJ(row["filePath"].ToString().Trim()) + "'"); Response.Write(",");
                    Response.Write("'" + row["dscription"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("\"" + url_link + "\"");
                    Response.Write(");\n");
                %> 
            <%  } %>

            //$("#txt_signed_file").attr("value", m_meeting_signed_minutes);
            
            // attendees
            <%  foreach (DataRow row in mtgAttendees.Rows) { %>
                <% 
                    Response.Write("AddAttendees(");
                    Response.Write("'" + row["attendName"].ToString().Trim() + "'"); 
                    Response.Write(");\n");
                %>
            <%  } %>

            // actionitems
            <%  foreach (DataRow row in mtgActionItems.Rows) { %>
                <% 
                    Response.Write("AddActionItems(");
                    Response.Write("'" + StringHelper.CTJ(row["actionitm"].ToString().Trim()) + "'"); Response.Write(",");
                    Response.Write("'" + row["proposedtime"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["status"].ToString().Trim() + "'");
                    Response.Write(");\n");
                %>
            <%  } %>

            // attachments
            //var m_attachment;
            <% foreach (DataRow row in mtgAttachment.Rows) { %>
                m_attachment = "<%: row["filePath"].ToString() %>";
                m_attachments=m_attachment;
                if (m_attachment != "") {
                    //$("#txt_signed_file").after("<a href=\"<%=ResolveUrl("~/") %>SQL/DownloadFile?doctype=MMA&fileName=" + UrlEncode(m_attachment) + "&id=" + m_request_id + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page_white_get.png\" style=\"border:0;\" /></a>");
                }
                

            <% } %>

            $("#txt_doc_status").html(m_mma_status);

        }

    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Contracts and Agreements Details</b>
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
                    <table cellpadding="1" cellspacing="0" border="0" width="100%" >
                        <tr>
                            <td colspan="2" align="right">
                                <span id="txt_doc_status"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>Document Type</td>
                            <td><input type="text" id="txt_meeting_type" /></td>
                        </tr>
                        <tr>
                            <td>Account Code</td>
                            <td><input type="text" id="txt_acct_code" onclick="javascript:LookUpData('txt_acct_code', 'ListOfAcctNameSo');" readonly="readonly" /></td>
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
                            <td><input type="text" id="txt_meeting_date" readonly="readonly" /></td>
                        </tr>
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
                                            <td><input type="text" /></td>
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
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>File Attachment</td>
                                    <td>Description</td>
                                </tr>
                                    <!--
                                    <tr>
                                        <td>
                                            <input type="text" id="txt_signed_file" onclick="javascript:CreateUploadingBox('txt_signed_file');" readonly="readonly" />
                                            <input type="hidden" id="txt_signed_file_forupload" />
                                        </td>
                                        <td>
                                            <input type="text" id="txt_signed_file_desc" />
                                        </td>
                                    </tr>
                                    -->
                                </table>
                                <table>
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
                            <% 
                                // mtgMinutesAgreement_row["docregion"].ToString() == Session["userregion"].ToString() &&
                            %>
                            <% if (
                                    oUsr.HasPositionOf("asm") != IS_NOT_FOUND &&
                                    oUsr.HasAreaOf("asm", oDocument.Area) == true && 
                                    mtgMinutesAgreement_row["status"].ToString() == AppHelper.GetUserPositionId("asm")
                                   ) { 
                                   %>
                                   <!-- ASM -->
                                   <input type="button" value="Approve" onclick="javascript:MarkDocument('APPROVE', '<%: CurDocId %>');" /> / 
                                   <input type="button" value="Disapprove" onclick="javascript:MarkDocument('DISAPPROVE', '<%: CurDocId %>');" />
                                   <%
                               }
                            %>

                            <% if (
                                    oUsr.HasPositionOf("chm") != IS_NOT_FOUND &&
                                    oUsr.HasChannelOf("chm", oDocument.Channel) == true &&
                                    mtgMinutesAgreement_row["status"].ToString() == AppHelper.GetUserPositionId("chm")
                                   ) { 
                                   %>
                                   <!-- CHM -->
                                   <input type="button" value="Approve" onclick="javascript:MarkDocument('APPROVE', '<%: CurDocId %>');" /> / 
                                   <input type="button" value="Disapprove" onclick="javascript:MarkDocument('DISAPPROVE', '<%: CurDocId %>');" />
                                   <%
                               }
                            %>

                        </center>
                    </div>
                    <!-- -->
                </td>
            </tr>
        </table>
    </div>
    </div>

</asp:Content>
