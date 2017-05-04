<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
       // Request.QueryString["Business Review"]
       
        DataTable busReview;
        busReview = SqlDbHelper.getDataDT("SELECT * FROM dbo.CustomerDetails WHERE busReviewNo ='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable busRevStrategicPlan;
        busRevStrategicPlan = SqlDbHelper.getDataDT("SELECT * FROM dbo.busRevStrategicPlan WHERE busReviewNo='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable busRevPropCreditChange;
        busRevPropCreditChange = SqlDbHelper.getDataDT("SELECT credLimitReco, creditTermReco FROM dbo.busRevPropCreditChange WHERE busReviewNo='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable busRevFinanceUse;
        busRevFinanceUse = SqlDbHelper.getDataDT("SELECT lenPayment, remarksCredTerm, dishonCheck, remarksDisCheck FROM busRevFinanceUse WHERE busReviewNo='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable SSM_Remarks = SqlDbHelper.getDataDT("SELECT SSM_Remarks, SSM_Approver FROM mtc_vw_Business_Review_Details WHERE busReviewNo ='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable FNM_Remarks = SqlDbHelper.getDataDT("SELECT FNM_Remarks, FNM_Approver FROM mtc_vw_Business_Review_Details WHERE busReviewNo ='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable VPTFI_Remarks = SqlDbHelper.getDataDT("SELECT VPTFI_Remarks, VPTFI_Approver FROM mtc_vw_Business_Review_Details WHERE busReviewNo ='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable VPBSM_Remarks = SqlDbHelper.getDataDT("SELECT VPBSM_Remarks, VPBSM_Approver FROM mtc_vw_Business_Review_Details WHERE busReviewNo ='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable CEO_Remarks = SqlDbHelper.getDataDT("SELECT CEO_Remarks, CEO_Approver FROM mtc_vw_Business_Review_Details WHERE busReviewNo ='" + Request.QueryString["busReviewNo"].ToString() + "'");

        DataTable Prev_Doc_status = SqlDbHelper.getDataDT("SELECT TOP 1 PrevDocStatus FROM document_history WHERE DocId ='" + Request.QueryString["busReviewNo"].ToString() + "' ORDER BY id DESC");

        DataTable Prev_Action_Type = SqlDbHelper.getDataDT("SELECT TOP 1 mType FROM document_history WHERE DocId ='" + Request.QueryString["busReviewNo"].ToString() + "' ORDER BY id DESC");

        List<String> UserRoleList = new List<String>();
        _User CurrentUser = new _User(Session["username"].ToString());
        UserRoleList = CurrentUser.GetRoleList();
      
        
        bool done_SSM = false;
        bool done_FNM = false;
        bool done_VPTFI = false;
        bool done_VPBSM = false;
        bool done_CEO = false;
        string prev_status = "";
        string previous_action_type = "";
        
        foreach (DataRow status in Prev_Doc_status.Rows) {
            prev_status = status["PrevDocStatus"].ToString();
        }
        
        foreach(DataRow action_type in Prev_Action_Type.Rows)
        {
            previous_action_type = action_type["mType"].ToString();
        }
       
        // QUERY FOR DOCUMENT STATUS
        string docStatusId="";
        string strquery1 = "SELECT status FROM dbo.busReview WHERE busReviewNo= '" + Request.QueryString["busReviewNo"].ToString() + "'";
        OleDbDataReader _greader = SqlDbHelper.getData(strquery1);
        if (_greader.Read())
            {
                docStatusId = _greader.GetValue(0).ToString();
            }
       
        /** OLD CODE **/
        /**
         * 
            const int IS_NOT_FOUND = -1;
            string roles = CurrentUser.Roles.ToString();
            string roles = "";
            if (CurrentUser.HasPositionOf("fnm") != IS_NOT_FOUND)
            {
                roles = "fnm";
            }
            if (CurrentUser.HasPositionOf("vpbsm") != IS_NOT_FOUND)
            {
                roles = "vpbsm";
            }

            if (CurrentUser.HasPositionOf("vptfi") != IS_NOT_FOUND)
            {
                roles = "vptfi";
            }
            if (CurrentUser.HasPositionOf("csr") != IS_NOT_FOUND)
            {
                roles = "csr";
            }
            if (CurrentUser.HasPositionOf("ceo") != IS_NOT_FOUND)
            {
                roles = "ceo";
            }
            if (CurrentUser.HasPositionOf("ssm") != IS_NOT_FOUND)
            {
                roles = "ssm";
            }

            if (CurrentUser.HasPositionOf("ssgm") != IS_NOT_FOUND)
            {
                roles = "ssgm";
            }

            if (CurrentUser.HasPositionOf("ca") != IS_NOT_FOUND)
            {
                roles = "ca";
            }
        **/
               
    %>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/BusinessReviewDetails.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
            
            <% if(docStatusId=="0" && UserRoleList.Contains("CSR")){ %>
                $("#txt_encoded_by").attr('value', '<%: Session["InputedUname"] %>');
            <% } %>

            <% if (docStatusId=="3" && UserRoleList.Contains("FNM")){%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM();
                DisableEditing(); 
                HideFinanceTable();
                HideTbl_improvements();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();

            <% } else if (docStatusId=="5" && UserRoleList.Contains("FNM")){%>
                LoadDataBusReview();
                LoadDataBusReviewForSSMFNM();
                LoadDataBusReviewForOtherUsers();
                HideTbl_improvements();
                DisableEditing();
                HideCEOAreaRemarks();
                HideAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                        
            <% } else if (docStatusId=="5" && UserRoleList.Contains("VPTFI") && previous_action_type !="SEND_BACK_TO_REQUESTER"){%>
                LoadDataBusReview();
                LoadDataBusReviewForSSMFNM();
                LoadDataBusReviewForOtherUsers();
                DisableEditingAll();
                DisableRemarkVPTFI();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
             
             <% } else if (docStatusId=="5" && UserRoleList.Contains("VPTFI") && previous_action_type =="SEND_BACK_TO_REQUESTER"){%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingForCEO()
                DisableEditingFF();
                DisableEditingRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();

            <% } else if (docStatusId=="9" && prev_status == "8"){%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingForCEO()
                DisableEditingFF();
                DisableEditingRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
            <% } else if (docStatusId=="7" && UserRoleList.Contains("VPTFI") && previous_action_type !="SEND_BACK_TO_REQUESTER"){%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM();
                DisableEditingAll();
                DisableEditingRemarks();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();

            <% } else if (docStatusId=="7" && UserRoleList.Contains("VPTFI") && previous_action_type =="SEND_BACK_TO_REQUESTER"){%>
                 LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingForCEO()
                DisableEditingFF();
                DisableEditingRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();

            <% } else if (docStatusId=="7" && UserRoleList.Contains("VPBSM")){%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM();
                DisableEditingAll();
                DisableRemarkVPBSM();
                HideCEOAreaRemarks();
                DisableEditingFF();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                          
            <% } else if (docStatusId=="8" && UserRoleList.Contains("VPBSM")){%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM();
                DisableEditingRemarks();
                DisableEditingAll();
                HideCEOAreaRemarks();
                DisableEditingFF();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
                        
            <% } else if (docStatusId=="8" && UserRoleList.Contains("CEO")){%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingForCEO()
                DisableEditingFF();
                DisableEditingRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
                        
            <% } else if (docStatusId=="1" && UserRoleList.Contains("MAD")) {%>
                LoadDataBusReviewForSSMFNM();
                DisableEditingFF();
                HideFinanceTable();
                HideTbl_improvements();
                LoadDataBusReview();
                DisableEditingAll();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                HideRemarksFromFNM();
            <% } else if (docStatusId=="0" && UserRoleList.Contains("CSR")) {%>
                LoadDataBusReview();
                HideFinanceTable();
                LoadDataBusReviewForSSMFNM(); 
                HideTbl_improvements();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                HideRemarksFromFNM();
                HideRemarksFromSSM();
                        
            <% } else if (docStatusId=="10" && UserRoleList.Contains("CSR")) {%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingAll();
                DisableEditingFF();
                DisableField();
                DisableEditingRemarks();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
                        
            <% } else if (docStatusId=="0" && UserRoleList.Contains("CA")) {%>
                LoadDataBusReview();
                HideFinanceTable();
                LoadDataBusReviewForSSMFNM(); 
                HideTbl_improvements();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                HideRemarksFromFNM();
                HideRemarksFromSSM();
                         
            <% } else if (docStatusId=="10" && UserRoleList.Contains("CA")) {%>
                LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingAll();
                DisableEditingFF();
                DisableField();
                DisableEditingRemarks();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
                      
            <% } else if (docStatusId=="0") {%>
                LoadDataBusReview();
                DisableEditingAll(); 
                HideFinanceTable();
                HideTbl_improvements();
                HideBorders();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                HideRemarksFromSSM();
                HideRemarksFromFNM();
            <% } else if (docStatusId=="1") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingAll();
                HideTbl_improvements();
                HideFinanceTable();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                HideRemarksFromFNM();
                HideRemarksFromSSM();
                        
            <% } else if (docStatusId=="2") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingAll();
                HideTbl_improvements();
                HideFinanceTable();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                HideRemarksFromFNM();
                DisableEditingRemarksfromSSM();
                         
            <% } else if (docStatusId=="3") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingAll();
                HideTbl_improvements();
                HideFinanceTable();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                HideRemarksFromFNM();
                DisableEditingRemarksfromSSM();
                        
            <% } else if (docStatusId=="4") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingAll();
                HideFinanceTable();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                        
            <% } else if (docStatusId=="5" && previous_action_type !="SEND_BACK_TO_REQUESTER") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM();
                LoadDataBusReviewForOtherUsers() 
                DisableEditingAll();
                HideTbl_improvements();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                        
            <% } else if (docStatusId=="6" || docStatusId=="7") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM();
                LoadDataBusReviewForOtherUsers(); 
                DisableEditingAll();
                HideAreaRemarks();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarks();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();

            <% } else if (docStatusId=="8") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                LoadDataBusReviewForOtherUsers()
                DisableEditingAll();
                DisableField();
                Hide();
                DisableEditingRemarks();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
                        
            <% } else if (docStatusId=="9") {%>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                LoadDataBusReviewForOtherUsers()
                DisableEditingAll();
                HideCEOAreaRemarks();
                DisableField();
                Hide();
                DisableEditingRemarks();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();

                <% } else if (docStatusId=="5" && previous_action_type=="SEND_BACK_TO_REQUESTER") {%>
                 LoadDataBusReview();
                LoadDataBusReviewForOtherUsers();
                LoadDataBusReviewForSSMFNM(); 
                DisableEditingForCEO()
                DisableEditingFF();
                DisableEditingRemarks();
                DisableField();
                Hide();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();
                      
            <% } else { %>
                LoadDataBusReview(); 
                LoadDataBusReviewForSSMFNM(); 
                LoadDataBusReviewForOtherUsers();
                DisableEditingAll();
                DisableEditingFF();
                DisableField();
                Hide();
                DisableEditingRemarks();
                DisableEditingRemarksfromSSM();
                DisableEditingRemarksfromFNM();
                DisableRemarkVPBSM();
                DisableRemarkVPTFI();

            <% } %>

            // DOCUMENT STATUS 
            var doc_stat_msg = '<%:AppHelper.BusReviewDocStateMsg(docStatusId.ToString()) %>';
            $("#doc_stat_msg").html(doc_stat_msg);

            // REQUIRED FIELDS
			$("#txt_acctCode").addClass("required_fields");
            $("#txt_comExAgr").addClass("required_fields");
			$("#txt_comAcctPer").addClass("required_fields");
            $("#txt_STOrigAnn").addClass("required_fields");
			$("#txt_STRevAnn").addClass("required_fields");
			$("#txt_plan").addClass("required_fields");
			$("#txt_support").addClass("required_fields");
            $("#txt_ceoRemarks").addClass("required_fields");
            $("#txt_ReccrdLimit").addClass("required_fields");
			$("#txt_STRevAnn").addClass("required_fields");
			$("#txt_ReccrdTerm").addClass("required_fields");
            $("#txt_br_date").addClass("required_fields");
            $("#txt_length_of_payment").addClass("required_fields");
            $("#txt_disChecks").addClass("required_fields");
			$("#txt_area_field").addClass("required_fields");
            $("#txt_brn").addClass("required_fields");
            $("#txt_STReason").addClass("required_fields");
			$("#txt_remarks").addClass("required_fields");
            $("#txt_disremarks").addClass("required_fields");
            $("#txt_areaRemarks").addClass("required_fields");
            $("#txt_remarksfromSSM").addClass("required_fields");
            $("#txt_remarksfromFNM").addClass("required_fields");
            $("#txt_other_info").addClass("required_fields");

            BindToTextFormatting("txt_ReccrdLimit");
            BindToTextFormatting("txt_STOrigAnn");
            BindToTextFormatting("txt_STRevAnn");
        });

        
        var BusRevaccCode;
        

        // LOAD DETAILS FOR ASM
        function LoadDataBusReview() 
        {
            var ED_CardCode = "";
            var ED_CardName = "";
            var ED_SOName = "";
            var ED_U_ASM = "";
            var ED_U_CHMGR = "";
            var ED_busReviewNo = "";
            var ED_newBusRevDate = "";
            var ED_comExistingAgree = "";
            var ED_comCurrentAcctPerf = "";
            var ED_areasForImprovements = "";
            var area_remarks = "";
            var credLimit = "";
            var credTerm = "";
            var encodedBy = "";
            var comments = "";
            var remarksFromSSM = "";
            var remarksFromFNM = "";

            //Business Review table
            <%  foreach (DataRow row in busReview.Rows) { %>
                ED_busReviewNo = '<% Response.Write(row["busReviewNo"].ToString().Trim()); %>';
                ED_newBusRevDate = '<% Response.Write(row["newBusRevDate"].ToString().Trim()); %>';
                ED_CardCode = '<% Response.Write(row["CardCode"].ToString().Trim()); %>';
                ED_CardName = '<%Response.Write(StringHelper.InsertSlashes(row["CardName"].ToString().Trim())); %>';
                ED_SOName = '<% Response.Write(row["SOName"].ToString().Trim()); %>';
                ED_U_ASM = '<% Response.Write(row["U_ASM"].ToString().Trim()); %>';
                ED_U_CHMGR = '<% Response.Write(row["U_CHMGR"].ToString().Trim()); %>';
                ED_comExistingAgree = '<% Response.Write(StringHelper.InsertSlashes(row["comExistingAgree"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                ED_comCurrentAcctPerf = '<% Response.Write(StringHelper.InsertSlashes(row["comCurrentAcctPerf"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                credLimit = '<% Response.Write(row["CreditLine"].ToString().Trim()); %>';
                credTerm = '<% Response.Write(row["CredTerm"].ToString().Trim()); %>';
                encodedBy = '<% Response.Write(row["encodedBy"].ToString().Trim()); %>';
                       
                BusRevaccCode = ED_CardCode;
               
            <%  } %>

            $("#txt_brn").attr('value', ED_busReviewNo);
            $("#txt_br_date").attr('value', ED_newBusRevDate);
            $("#txt_acctCode").attr('value', BusRevaccCode);
            $("#txt_acctName").attr('value', ED_CardName);
            $("#txt_acctOfficer").attr('value', ED_SOName);
            $("#txt_salesManager").attr('value', ED_U_ASM);
            $("#txt_channelManager").attr('value', ED_U_CHMGR);
             
            $("#txt_ExstcrdLimit").attr('value', credLimit);
            $("#txt_ExstcrdTerm").attr('value', credTerm);
            $("#txt_exst_credit_term").attr('value', credTerm);
            $("#txt_encoded").attr('value', encodedBy);

            $("#txt_comExAgr").val(ED_comExistingAgree.replace(/&#10;/g, "\n"));
            $("#txt_comAcctPer").val(ED_comCurrentAcctPerf.replace(/&#10;/g, "\n"));

            // SAVE ORIGINAL VALUES
            $("#txt_comExAgr").attr('orig_value', ED_comExistingAgree);
            $("#txt_comAcctPer").attr('orig_value', ED_comCurrentAcctPerf);
        }

            // Load Details For SSM & FNM
            function LoadDataBusReviewForSSMFNM()
            {
                   
                var STorigAnnual = "";
                var STrevAnnual = "";
                var STreason;
                var IncentivePlan = "";
                var Support = "";
                var fieldName = "";
                var existingVal = "";
                var revisedVal = "";
                var credLimitExist = "";
                var credLimitReco = "";
                var creditTermExist = "";
                var creditTermReco = "";
                var OtherInfo = "";
           
                //Strategic Plan Table
                <%  foreach (DataRow row in busRevStrategicPlan.Rows) { %>
                    STorigAnnual = '<% Response.Write(row["STorigAnnual"].ToString().Trim()); %>';
                    STrevAnnual = '<% Response.Write(row["STrevAnnual"].ToString().Trim()); %>';
                    STreason = '<% Response.Write(StringHelper.CTJ(row["STreason"].ToString().Trim())); %>';
                    IncentivePlan = '<% Response.Write(StringHelper.CTJ(row["IncentivePlan"].ToString().Trim())); %>';
                    Support = '<% Response.Write(StringHelper.CTJ(row["Support"].ToString().Trim())); %>';
                    OtherInfo = '<% Response.Write(StringHelper.CTJ(row["OtherInfo"].ToString().Trim())); %>';
                <%  } %>

                $("#txt_STOrigAnn").attr('value', STorigAnnual);
                $("#txt_STRevAnn").attr('value', STrevAnnual);
                $("#txt_STReason").attr('value', STreason);
                
                $("#txt_plan").val(IncentivePlan);
                $("#txt_support").val(Support);
                $("#txt_other_info").val(OtherInfo);
                
                // SAVE ORIGINAL VALUES
                $("#txt_plan").attr('orig_value', IncentivePlan);
                $("#txt_support").attr('orig_value', Support);
                $("#txt_other_info").attr('orig_value', OtherInfo);
  
                //Proposed Credit Change Table
                <%  foreach (DataRow row in busRevPropCreditChange.Rows) { %>
                    credLimitReco = '<% Response.Write(row["credLimitReco"].ToString().Trim()); %>';
                    creditTermReco = '<% Response.Write(row["creditTermReco"].ToString().Trim()); %>';
                <% } %>

                $("#txt_ReccrdLimit").attr('value', credLimitReco);
                $("#txt_ReccrdTerm").attr('value', creditTermReco);

            }

            // LOAD DETAILS FOR VP-FINANCE, VP-SALES & CEO
            function LoadDataBusReviewForOtherUsers()
            {
                    var STorigAnnual = "";
                    var STrevAnnual = "";
                    var STreason = "";
                    var IncentivePlan = "";
                    var Support = "";
                    var existingVal = "";
                    var revisedVal = "";
                    var creditTermExist = "";
                    var creditTermReco = "";
                    var lenPayment = "";
                    var existCreditTerm = "";
                    var remarksCredTerm = "";
                    var dishonCheck = "";
                    var remarksDisCheck = "";
                    var credLimitReco = "";

      
                   //Strategic Plan Table
                    <%  foreach (DataRow row in busRevStrategicPlan.Rows) { %>
                        STorigAnnual = '<% Response.Write(row["STorigAnnual"].ToString().Trim()); %>';
                        STrevAnnual = '<% Response.Write(row["STrevAnnual"].ToString().Trim()); %>';
                        STreason = '<% Response.Write(StringHelper.InsertSlashes(row["STreason"].ToString().Trim())); %>';
                        IncentivePlan = '<% Response.Write(StringHelper.InsertSlashes(row["IncentivePlan"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                        Support = '<% Response.Write(StringHelper.InsertSlashes(row["Support"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                        OtherInfo = '<% Response.Write(StringHelper.InsertSlashes(row["OtherInfo"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                    <%  } %>

                    $("#txt_STOrigAnn").attr('value', STorigAnnual);
                    $("#txt_STRevAnn").attr('value', STrevAnnual);
                    $("#txt_STReason").attr('value', STreason);
                    // tmp_str.replace(/^\s*|\s*$/gi, "");
                    $("#txt_plan").val(IncentivePlan.replace(/&#10;/g, "\n"));
                    $("#txt_support").val(Support.replace(/&#10;/g, "\n"));
                    $("#txt_other_info").val(OtherInfo.replace(/&#10;/g, "\n"));
              
                    // SAVE ORIGINAL VALUES
                    $("#txt_plan").attr('orig_value', IncentivePlan);
                    $("#txt_support").attr('orig_value', Support);
                    $("#txt_other_info").attr('orig_value', OtherInfo);
                    
                    //Proposed Credit Change Table
                    <% foreach (DataRow row in busRevPropCreditChange.Rows) { %>
                        credLimitReco = '<% Response.Write(row["credLimitReco"].ToString().Trim()); %>';
                        creditTermReco = '<% Response.Write(row["creditTermReco"].ToString().Trim()); %>';
                    <% } %>

                    $("#txt_ReccrdLimit").attr('value', credLimitReco);
                    $("#txt_ReccrdTerm").attr('value', creditTermReco);

                    //For Finance Table
                    <% foreach (DataRow row in busRevFinanceUse.Rows) { %>
                        lenPayment = '<% Response.Write(row["lenPayment"].ToString().Trim()); %>';
                        remarksCredTerm = '<% Response.Write(StringHelper.InsertSlashes(row["remarksCredTerm"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                        dishonCheck = '<% Response.Write(row["dishonCheck"].ToString().Trim()); %>';
                        remarksDisCheck = '<% Response.Write(StringHelper.InsertSlashes(row["remarksDisCheck"].ToString().Replace("\n","&#10;").Replace("\r","&#10;").Trim())); %>';
                    <% } %>

                    $("#txt_length_of_payment").attr('value', lenPayment);
                    $("#txt_remarks").val(remarksCredTerm.replace(/&#10;/g, "\n"));
                    $("#txt_disChecks").attr('value', dishonCheck);
                    $("#txt_disremarks").val(remarksDisCheck.replace(/&#10;/g, "\n"));

            // SAVE ORIGINAL VALUES
            $("#txt_remarks").attr('orig_value', remarksCredTerm);
            $("#txt_disremarks").attr('orig_value', remarksDisCheck);

            }

    </script>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                        <b>Business Review</b>
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
                    <td align="right">
                        <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>BusinessReview/BusinessReviewList">>> List of Business Review</a>
                    </td>
                </tr>
            </table>
        </div>

        <div class="simple_box" style="padding:10px; font-size:12px; border-top:0; border-bottom:0;">
            <span id="doc_stat_msg"></span>
        </div>
         
    <!--<div class="simple_box" style="height:650px; overflow: scroll; width:99%" >-->
        <div class="simple_box" style="border-style: none; border-color: inherit; border-width: 0px; padding: 15px; font-size:12px; width: 674px;">
		    <table cellpadding="1" id="tbl_br1" cellspacing="1" border="0" style="color:#000000;" >
            <tr>
                <td style="width: 170px;"> Business Review Number</td>
                <td><input type="text" id="txt_brn" readonly="readonly" style="width: 250px" /></td>
            </tr>
            <tr>
                 <td style="width: 170px;"> Business Review Date </td>
                 <td>
		            <input type="text" id="txt_br_date" readonly="readonly" style="width: 250px" />
                 </td>
            </tr>
            <tr> 
             <td colspan="2" ><hr /></td>
             </tr>
            <tr>
                <td style="width:172px;"> Account Code</td>
                <td style="width:100px;"><input type="text" id="txt_acctCode" readonly="readonly" size="26" style="width: 250px"/></td>
            </tr>
            <tr>
                 <td style="width: 172px;"> Account Name </td>
                 <td>
		            <input type="text" id="txt_acctName" readonly="readonly" style="width: 250px"/>
                 </td>
            </tr>
            <tr> 
                <td colspan="2"><hr /></td>
            </tr> 
            <tr>
                <td style="width: 170px;"> Account Officer</td>
                <td><input type="text" id="txt_acctOfficer" readonly="readonly" style="width: 250px" /></td>
            </tr>
            <tr>
                 <td style="width: 170px;"> Area Sales Manager </td>
                 <td>
		            <input type="text" id="txt_salesManager" readonly="readonly" style="width: 250px" />
                 </td>
            </tr> 
             <tr>
                 <td style="width: 170px;"> Channel Manager </td>
                 <td>
		            <input type="text" id="txt_channelManager" readonly="readonly" style="width: 250px" />
                 </td>
            </tr> 
            <tr>
                <% if(docStatusId=="0" && !UserRoleList.Contains("CSR")) { %>
                    <td style="width:170px;"> Scheduled By:</td>
                <% } else { %>
                    <td style="width:170px;"> Encoded By:</td>
                <% } %>
                <% if(docStatusId=="0" && UserRoleList.Contains("CSR")){%>
                <td><input type="text" id="txt_encoded_by" readonly="readonly" style="width: 250px"/></td>
                <% } %>
                <% if ((!UserRoleList.Contains("CSR") || (UserRoleList.Contains("CSR") && docStatusId != "0"))){%>
                <td><input type="text" id="txt_encoded" readonly="readonly" style="width: 250px"/></td>
                <% } %>
                
            </tr>
            </table>      
        </div>

        <div class="simple_box" id="comExAgr" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
            <h2>Comments on Existing Agreements</h2>
            <textarea rows="6" id="txt_comExAgr" style="width:570px; height:100px;"></textarea>
        </div>

        <div class="simple_box" id="comAcctPer" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
            <h2>Comments on Current Account Performance</h2>
            <textarea rows="8" id="txt_comAcctPer" style="width:570px; height:100px;"></textarea>
        </div>

       <div class="simple_box" id="strat_plans" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
           <h2>Strategic Plans</h2>
                <table cellpadding="1" id="tbl_br4" cellspacing="1" border="0" style="color:#000000;" >
                    <tr>
                        <td style="width:105px;"></td>
                        <td align="center">Original Annual Amount</td>
                        <td align="center">Revised Annual Amount</td>
                        <td align="center">Reason</td>
                    </tr>
                    <tr>
                        <td style="width:105px;">Sales Target</td>
                        <td style="width:0px;"><input type="text" style="width:150px;" onkeypress="return isNumberKey(event,'txt_STOrigAnn')" id="txt_STOrigAnn"/></td>
                        <td style="width:0px;"><input type="text" style="width:150px;" onkeypress="return isNumberKey(event,'txt_STRevAnn')" id="txt_STRevAnn"/></td>
                        <td style="width:0px;"><input type="text" style="width:150px;" id="txt_STReason"/></td>
                    </tr>
                  </table>
            </div>
             
            <div class="simple_box" id="plans" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
                <table cellpadding="5" id="tbl_br5" cellspacing="0" border="0" style="color:#000000;" >
                    <tr>
                        <td style="width: 105px">Incentive Planning</td>
                        <td>
                            <textarea rows="2" id="txt_plan" style="width: 471px; margin-left: 0px; height:70px;" ></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px">Support</td>
                        <td>
                            <textarea rows="2" id="txt_support" style="width: 471px; height:70px;" ></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px">Other Information</td>
                        <td>
                            <textarea rows="2" id="txt_other_info" style="width: 471px; height:70px;" ></textarea>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="simple_box" id="proposed_credit_change" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
                <h2>Proposed Credit Change</h2>
                    <table cellpadding="1" id="tbl_br8" cellspacing="0" border="0" style="color:#000000;">
                        <tr>
                            <td></td>
                            <td align="center">Existing</td>
                            <td align="center">Recommended</td>
                        </tr>
                        <tr>
                            <td style="width: 80px">Credit Limit</td>
                            <td><input type="text" readonly="readonly" onkeypress="return isNumberKey(event,'txt_ExstcrdLimit')" id="txt_ExstcrdLimit" style="width:200px;"/></td>
                            <td><input type="text" onkeypress="return isNumberKey(event,'txt_ReccrdLimit')" id="txt_ReccrdLimit" style="margin-left: 0px; width:200px;" /></td>
                        </tr>
                        <tr>
                            <td>Credit Term</td>
                            <td><input type="text" readonly="readonly" id="txt_ExstcrdTerm" style="width:200px;"/></td>
                            <td><input type="text" readonly="readonly" onkeypress="return isNumberKey(event,'txt_ReccrdTerm')" id="txt_ReccrdTerm" onclick="javascript:LookUpDataCredTerms('txt_ReccrdTerm', 'ListOfPaymentGroup');" value="- Cash Basic -" style="width:200px;"/></td>
                        </tr>
                    </table>
            </div>

            <div style="width: 622px">
            </div>
              <div class="simple_box" id="div_ssm" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
                <h2>Remarks from Marketing Director</h2>
                <table cellpadding="1" id="remarks_ssm" cellspacing="0" border="0" style="color:#000000;" >
                    <tr>
                        <td>
                            <textarea rows="2" id="txt_remarksfromSSM" style="width: 560px; margin-left: 0px; height:80px;" cols=""><%
                              if (docStatusId != "0" || docStatusId != "1") { foreach (DataRow tmp_remarks in SSM_Remarks.Rows){ if (SSM_Remarks.Rows.Count > 0 && tmp_remarks["SSM_Remarks"].ToString() != "" && done_SSM == false){
                                Response.Write(tmp_remarks["SSM_Remarks"].ToString().Replace("/&gt;", "").Replace("\r", "&#10;").Replace("&#8220;", "&#8221;").Replace("&#8216;", "&#8217;").Trim());
                                Response.Write("\n\n\n");
                                Response.Write("\n " + tmp_remarks["SSM_Approver"].ToString().Replace("/&gt;", "").Replace("\r", "&#10;").Replace("&#8220;", "&#8221;").Replace("&#8216;", "&#8217;").Trim());
                                done_SSM = true;
                                }
                            } 
                        } 
                         %></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="simple_box" id="div_fnm" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
                <h2>Remarks from Finance Manager</h2>
                <table cellpadding="1" id="Table5" cellspacing="0" border="0" style="color:#000000;" >
                    <tr>
                        <td>
                        <textarea rows="2" id="txt_remarksfromFNM" style="width: 560px; margin-left: 0px; height:80px;" cols=""><%
                            if (docStatusId != "0" || docStatusId != "1" || docStatusId != "2" || docStatusId != "3") {
                                foreach (DataRow tmp_remarks in FNM_Remarks.Rows) {
                                    if (FNM_Remarks.Rows.Count > 0 && tmp_remarks["FNM_Remarks"].ToString() != "" && done_FNM == false){
                                    Response.Write(tmp_remarks["FNM_Remarks"].ToString().Replace("/&gt;", "").Replace("\r", "&#10;").Replace("&#8220;", "&#8221;").Replace("&#8216;", "&#8217;").Trim());
                                    Response.Write("\n\n\n");
                                    Response.Write("\n" + tmp_remarks["FNM_Approver"].ToString().Replace("\n", "").Replace("\r", "&#10;").Trim());
                                    done_FNM = true;
                                    }
                                }
                            }    
                        %></textarea>
                        </td>
                    </tr>
                </table>
            </div>
         
            <div class="simple_box" id="div_finance" style="border-style: none; font-size:12px; width: 619px;">
                <h2>For Finance Use Only</h2>
                <table cellpadding="1" id="finance_table" cellspacing="1" border="0" style="color:#000000; width: 655px;" >
                    <tr>
                        <td style="width: 800px">Effective Length of Payment</td>
                        <td style="width: 100px"><input type="text" id="txt_length_of_payment" onkeypress="return isNumberKey(event,'txt_length_of_payment')" style="width: 190px" />Days</td>
                       
                    </tr>
                    <tr>
                        <td style="width: 800px">Existing Credit Term</td>
                        <td style="width: 756px"><input type="text" readonly="readonly" id="txt_exst_credit_term" style="width: 190px"/></td>
                    </tr>
                    <tr>
                        <td style="width: 800px">Remarks</td>
                        <td style="width: 756px"><textarea rows="2" id="txt_remarks" style="width: 350px; height:30px; margin-top: 0px;" ></textarea></td>
                    </tr>
                    <tr>
                        <td style="width: 800px">Incidents of Dishonored Checks</td>
                        <td style="width: 756px">
                            <input type="text" id="txt_disChecks" style="width: 190px" onkeypress="return isNumberKey(event,'txt_disChecks')" /><a href="javascript:;" onclick="javascript:LookForBouncedChecks();">View Details</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 800px">Remarks</td>
                        <td style="width: 756px"><textarea rows="2" id="txt_disremarks" style="width: 350px; height:30px;" ></textarea></td>
                    </tr>
                </table>
            </div>
       <%if (docStatusId != "4"){%>     
            <div class="simple_box" id="comments" style="border-style:none; border-color:inherit; border-width:0px; padding:10px; font-size:12px;">
                <h2>Remarks from VP-Finance</h2>
                <table cellpadding="1" id="tbl_areas" cellspacing="0" border="0" style="color:#000000;" >
                    <tr>
                        <td>
                        <textarea rows="2" id="txt_area_field" style="width: 560px; margin-left: 0px; height:80px;" ><%
                             if (docStatusId != "0" || docStatusId != "1" || docStatusId != "2" || docStatusId != "3" || docStatusId != "4" || docStatusId != "5"){
                                foreach (DataRow tmp_remarks in VPTFI_Remarks.Rows){
                                    if (VPTFI_Remarks.Rows.Count > 0 && tmp_remarks["VPTFI_Remarks"].ToString() != "" && done_VPTFI == false){
                                    Response.Write(tmp_remarks["VPTFI_Remarks"].ToString().Replace("/&gt;", "").Replace("\r", "&#10;").Replace("&#8220;", "&#8221;").Replace("&#8216;", "&#8217;").Trim());
                                    Response.Write("\n\n\n");
                                    Response.Write("\n" + tmp_remarks["VPTFI_Approver"].ToString().Replace("\n", "").Replace("\r", "&#10;").Trim());
                                    done_VPTFI = true;
                                    }
                                }                           
                            } 
                         %></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <%} %>
            <div class="simple_box" id="area_remarks" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
                <h2>Remarks from Sales Director</h2>
                <table cellpadding="1" id="Table1" cellspacing="0" border="0" style="color:#000000;" >
                    <tr>
                        <td>
                        <textarea rows="2" id="txt_areaRemarks" style="width: 560px; margin-left: 0px; height:80px;" cols=""><%
                                                                                                                                 if (docStatusId == "9" || docStatusId == "10" || docStatusId == "8" || docStatusId == "7" || docStatusId == "11" || (docStatusId == "5" && previous_action_type == "SEND_BACK_TO_REQUESTER"))
                                                                                                                                 {
                                foreach (DataRow tmp_remarks in VPBSM_Remarks.Rows){
                                    if (VPBSM_Remarks.Rows.Count > 0 && tmp_remarks["VPBSM_Remarks"].ToString() != "" && done_VPBSM==false){
                                        Response.Write(tmp_remarks["VPBSM_Remarks"].ToString().Replace("/&gt;", "").Replace("\r", "&#10;").Replace("&#8220;", "&#8221;").Replace("&#8216;", "&#8217;").Trim());
                                        Response.Write("\n\n\n");
                                        Response.Write("\n" + tmp_remarks["VPBSM_Approver"].ToString().Replace("\n", "").Replace("\r", "&#10;").Trim());
                                        done_VPBSM = true;
                                    }
                                }
                        } 
                      %> </textarea>
                    </td>
                    </tr>
                </table>
            </div>
<%if (docStatusId == "9" || docStatusId == "10" || docStatusId == "11" || (docStatusId == "8" && UserRoleList.Contains("CEO")))
{%>
            <div class="simple_box" id="ceo_areaRemarks" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px;">
                <h2>Remarks from CEO</h2>
                <table cellpadding="1" id="Table4" cellspacing="0" border="0" style="color:#000000;" >
                    <tr>
                        <td>
                        <textarea rows="2" id="txt_ceoRemarks" style="width: 560px; margin-left: 0px; height:80px;" cols=""><%
                            if (docStatusId == "9" || docStatusId == "10" || docStatusId == "11"){
                                foreach (DataRow tmp_remarks in CEO_Remarks.Rows){         
                                    if (CEO_Remarks.Rows.Count > 0 && tmp_remarks["CEO_Remarks"].ToString() != "" && done_CEO == false){
                                        Response.Write(tmp_remarks["CEO_Remarks"].ToString().Replace("/&gt;", "").Replace("\r", "&#10;").Replace("&#8220;", "&#8221;").Replace("&#8216;", "&#8217;").Trim());
                                        Response.Write("\n\n\n");
                                        Response.Write("\n" + tmp_remarks["CEO_Approver"].ToString().Replace("\n", "").Replace("\r", "&#10;").Trim());
                                        done_CEO = true;
                                    }
                                }
                         } 
                        %></textarea>
                    </td>
                    </tr>
                </table>
            </div>
<%} %>
        <% if ((docStatusId == "0" && UserRoleList.Contains("CSR")) || (docStatusId == "0" && UserRoleList.Contains("CA")))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" id="btn_Save" value="Save" style="color:#000000;" onclick="javascript:BusReviewDocSave()"/></td>
                        <td><input type="button" value="Cancel" onclick="javascript:Cancel();" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }
        
        //else if (docStatusId == "1" && roles == "mad")
        else if (docStatusId == "1" && UserRoleList.Contains("MAD"))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" id="btn_a" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_remarksfromSSM');"  /></td>
                        <td><input type="button" id="btn_d" value="Disapprove" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_remarksfromSSM');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }

        else if (docStatusId == "3" && UserRoleList.Contains("FNM"))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" id="btn_approve" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_remarksfromFNM');"  /></td>
                        <td><input type="button" id="btn_disapprove" value="Disapprove" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_remarksfromFNM');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }

        else if (docStatusId == "5" && UserRoleList.Contains("FNM") && busRevFinanceUse.Rows.Count==0)
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td colspan="2"><input type="button" id="btn_approved" value="Send to VP - Finance" style="color:#000000;" onclick="javascript:SaveFinanceDoc();"  /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }

        else if (docStatusId == "5" && UserRoleList.Contains("VPTFI"))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" value="Concur" style="color:#000000;" id="btn_concur" onclick="javascript:CallRouting('Approve','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_area_field');"  /></td>
                        <td><input type="button" value="Disapprove" style="color:#000000;" id="btn_dis" onclick="javascript:CallRouting('Disapprove','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_area_field');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }

        else if (docStatusId == "7" && UserRoleList.Contains("VPBSM"))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" id="e" value="Concur" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_areaRemarks');"  /></td>
                        <td><input type="button" id="f" value="Send Back to VP - Finance" style="color:#000000;" onclick="javascript:CallRouting('SendBackToVpFinance','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_areaRemarks');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }

        else if (docStatusId == "8" && UserRoleList.Contains("CEO"))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" id="x" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_ceoRemarks');"  /></td>
                        <td><input type="button" id="y" value="Disapprove" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: Request.QueryString["busReviewNo"].ToString() %>','txt_ceoRemarks');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% }

        else if ((docStatusId == "10" && UserRoleList.Contains("CSR")) || (docStatusId == "10" && UserRoleList.Contains("CA")))
        { %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td colspan="2"><input type="button" id="btn_update" value="Update" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: Request.QueryString["busReviewNo"].ToString() %>','')"/></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% } %>
        
    <!--</div>-->
    </div>
</asp:Content>
