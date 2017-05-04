<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        // Request.QueryString["ccanum"]
        string DocCCaNum = Request.QueryString["ccanum"].ToString();
        const int IS_NOT_FOUND = -1;

        _User oUsr = new _User(Session["username"].ToString());
        
        DataTable customerHeader;
        customerHeader = SqlDbHelper.getDataDT("select * from customerHeader where ccanum='" + DocCCaNum + "'");
        
        DataTable busTypeHdr;
        busTypeHdr = SqlDbHelper.getDataDT("select *,convert(varchar(10), dateOfIncorporation, 101) as 'dateOfIncorporationFormatted' from dbo.busTypeHdr where ccanum='" + DocCCaNum + "'");

        DataTable busTypeDtl;
        busTypeDtl = SqlDbHelper.getDataDT("select * from dbo.busTypeDtl where ccanum='" + DocCCaNum + "'");

        DataTable empInventory;
        empInventory = SqlDbHelper.getDataDT("select * from dbo.empInventory where ccanum='" + DocCCaNum + "'");
        
        DataTable custAttachment;
        custAttachment = SqlDbHelper.getDataDT("select * from dbo.custAttachment where ccanum='" + DocCCaNum + "'");

        DataTable custBusHdr;
        custBusHdr = SqlDbHelper.getDataDT("select * from dbo.custBusHdr where ccanum='" + DocCCaNum + "'");

        DataTable propsedPrice;
        propsedPrice = SqlDbHelper.getDataDT("select * from dbo.propsedPrice where ccanum='" + DocCCaNum + "'");
        
        DataTable custOutlets;
        custOutlets = SqlDbHelper.getDataDT("select * from dbo.custOutlets where ccanum='" + DocCCaNum + "'");

        DataTable majorCustomer;
        majorCustomer = SqlDbHelper.getDataDT("select * from dbo.majorCustomer where ccanum='" + DocCCaNum + "'");

        DataTable products;
        products = SqlDbHelper.getDataDT("select * from dbo.products where ccanum='" + DocCCaNum + "'");

        DataTable otherWoodSupp;
        otherWoodSupp = SqlDbHelper.getDataDT("select * from dbo.otherWoodSupp where ccanum='" + DocCCaNum + "'");

        // depositoryBank
        DataTable depositoryBank;
        depositoryBank = SqlDbHelper.getDataDT("select * from dbo.depositoryBank where ccanum='" + DocCCaNum + "'");
        
        // assets
        DataTable assets_land;
        assets_land = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + DocCCaNum + "' and assetClass='land'");
        DataTable assets_building;
        assets_building = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + DocCCaNum + "' and assetClass='building'");
        DataTable assets_vehicle;
        assets_vehicle = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + DocCCaNum + "' and assetClass='vehicles'");
        DataTable assets_other;
        assets_other = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + DocCCaNum + "' and assetClass='other'");
        
        // otherBusiness
        DataTable otherBusiness;
        otherBusiness = SqlDbHelper.getDataDT("select * from dbo.otherBusiness where ccanum='" + DocCCaNum + "'");
        
        // get users's position, region
        DataRow userHeader;
        userHeader = SqlDbHelper.getDataDT("select * from userHeader where userName='" + Session["username"] + "'").Rows[0];
        
        // get doc stateid
        DataRow docStatusId;
        docStatusId = SqlDbHelper.getDataDT("select rtrim(ltrim(status)) as 'status' from dbo.customerHeader where ccaNum = '" + DocCCaNum + "'").Rows[0];
        string curr_doc_DocStatus = docStatusId["status"].ToString();
        
        /* proposedChangesCA */
        // get doc 2nd part stateid
        DataRow docChangesStatusId;
        docChangesStatusId = SqlDbHelper.getDataDT("select rtrim(ltrim(status)) as 'status' from dbo.proposedChangesCA where ccaNum='" + DocCCaNum + "'").Rows[0];
        string curr_doc_DocChangesStatus = docChangesStatusId["status"].ToString();
        
        // get the proposed changes
        DataTable proposedChangesCA;
        proposedChangesCA = SqlDbHelper.getDataDT("select * from proposedChangesCA where ccaNum='" + DocCCaNum + "' and routeType != ''");

        // get the proposed changes
        DataTable proposedChangesCA1;
        proposedChangesCA1 = SqlDbHelper.getDataDT("select * from proposedChangesCA1 where ccaNum='" + DocCCaNum + "'");
        
        string cca_region = "";

        string CurrDocMessage = AppHelper.GetAccDocStatusMessage(curr_doc_DocStatus, curr_doc_DocChangesStatus, DocCCaNum);
        
        
    %>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountMasterFile.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/AccountMasterFile.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        var customerHeaderStatus = '<%:docStatusId["status"].ToString() %>';

        /* other data to TRACK */
        var g_list_of_partners = "";
        var g_list_of_corpos = "";
        var g_list_of_employees = "";
        var g_list_of_outlets = "";
        var g_list_of_majorcustomers = "";
        var g_list_of_wood_suppliers = "";
        var g_list_of_banks = "";
        var g_list_of_lands = "";
        var g_list_of_buildings = "";
        var g_list_of_vehicles = "";
        var g_list_of_other_business = "";

        /* used to track changes */
        var g_acct_name;
        var g_acct_acct_officer;
        var g_acct_territory;
        var g_acct_area;
        var g_acct_region;
        var g_acct_reg_name;
        var g_acct_business_add;
        var g_acct_delivery_add;
        var g_acct_prop_credit_term;
        var g_acct_prop_credit_limit;
        var g_acct_mw_price_code;
        var g_acct_mw_price_desc;
        var g_acct_mw_price_remarks;
        var g_acct_ww_price_code;
        var g_acct_ww_price_desc;
        var g_acct_ww_price_remarks;
        var g_acct_pwf_price_code;
        var g_acct_pwf_price_desc;
        var g_acct_pwf_price_remarks;
        var g_acct_pwr_price_code;
        var g_acct_pwr_price_desc;
        var g_acct_pwr_price_remarks;
        var g_acct_gw_price_code;
        var g_acct_gw_price_desc;
        var g_acct_gw_price_remarks;
        var g_acct_tw_price_code;
        var g_acct_tw_price_desc;
        var g_acct_tw_price_remarks;

        $(function () {
            $("#tabs").tabs({ disabled: [11] });
            $("#sub_tab").tabs({
                select: function (event, ui) {
					clear_values_sub_tab();
				}
            });

            LoadData();

            $("#slt_acct_classification").change(
                function (){
                    SwitchR();
                }
            );

             $("#acc_type_direct").change(
                function (){
                    SwitchR();
                }
             );

             $("#acc_type_indirect").change(
                function (){
                    SwitchR();
                }
             );

            $("#txt_corpo_inc_date").datepicker();

            SwitchR();
        });

        function SwitchR(){
            RemoveClass();

            var acct_type = ""; // = $("#acc_type_direct").attr('checked') == true ? "direct": "indirect";
            
            if ($("#acc_type_direct").attr('checked') == 'checked'){
                acct_type = "direct";
            } else {
                acct_type = "indirect";
            }

            SwitchRequiredFields($("#slt_acct_classification").attr("value"), acct_type);

            $("#txt_acct_classification").attr("value", $("#slt_acct_classification").attr("value"));
        }

        function clear_values_sub_tab() {
			$("#txt_sole_owner_name").attr('value', '');
			$("#txt_sole_nationality").attr('value', '');
			$("#txt_sole_gen_manager").attr('value', '');
			$("#txt_sole_fin_manager").attr('value', '');
			$("#txt_sole_others").attr('value', '');

			// list_of_partner
			var rowid = $("#tbl_partner_list tr").length - 2;
			for (var i = 0; i < rowid; i++) {
				$("#tbl_partner_list tr:nth-child(2)").remove();
			}

			$("#txt_partner_gen_manager").attr('value', '');
			$("#txt_partner_fin_manager").attr('value', '');
			$("#txt_partner_others").attr('value', '');
			$("#txt_corpo_inc_date").attr('value', '');
			$("#txt_corpo_auth_cap_stock").attr('value', '');
			$("#txt_corpo_subscb_cap_stock").attr('value', '');
			$("#txt_corpo_paidin_cap_stock").attr('value', '');

			// list_major_stockholder
			rowid = $("#tbl_corpo_list tr").length - 2;
			for (var i = 0; i < rowid; i++) {
				$("#tbl_corpo_list tr:nth-child(2)").remove();
			}

			$("#txt_corpo_ceo").attr('value', '');
			$("#txt_corpo_vp_fin").attr('value', '');
			$("#txt_corpo_gen_man").attr('value', '');
		}

        function LoadData() {
            var m_acct_ccanum;
            var m_acct_type;
            var m_acct_classification;
            var m_acct_key_account;
            var m_acct_code;
            var m_acct_class;
            var m_acct_name;
            var m_acct_phone_no;
            var m_acct_acct_officer;
            var m_acct_fax_no;
            var m_acct_territory;
            var m_acct_email;
            var m_acct_office_hours;
            var m_acct_area;
            var m_acct_store_hours;
            var m_acct_region;
            var m_acct_years_in_business;
            var m_acct_years_with_matimco;
            var m_acct_tax_id;
            var m_acct_vat_no;
            var m_acct_reg_name;
            var m_acct_business_add;
            var m_acct_delivery_add;
            var m_acct_num_employees;
            var m_final_acct_code;

            <%  foreach (DataRow row in customerHeader.Rows) { %>
                m_acct_ccanum = '<% Response.Write(row["ccaNum"].ToString().Trim()); %>';
                m_acct_type = '<% Response.Write(row["acctType"].ToString().Trim()); %>';
                m_acct_classification = '<% Response.Write(row["acctClassfxn"].ToString().Trim()); %>';
                m_acct_key_account = '<% Response.Write(row["keyAcct"].ToString().Trim()); %>';
                m_acct_code = '<% Response.Write(row["acctCode"].ToString().Trim()); %>';
                m_acct_class = '<% Response.Write(row["acctClass"].ToString().Trim()); %>';
                m_acct_name = '<% Response.Write(StringHelper.InsertSlashes(row["acctName"].ToString().Trim())); %>';
                m_acct_phone_no = '<% Response.Write(row["telNum"].ToString().Trim()); %>';
                m_acct_acct_officer = '<% Response.Write(row["acctOffcr"].ToString().Trim()); %>';
                m_acct_fax_no = '<% Response.Write(row["faxNum"].ToString().Trim()); %>';
                m_acct_territory = '<% Response.Write(row["territory"].ToString().Trim()); %>';
                m_acct_email = '<% Response.Write(row["emailAdd"].ToString().Trim()); %>';
                m_acct_office_hours = '<% Response.Write(row["offceHrs"].ToString().Trim()); %>';
                m_acct_area = '<% Response.Write(row["area"].ToString().Trim()); %>';
                m_acct_store_hours = '<% Response.Write(row["storeHrs"].ToString().Trim()); %>';
                m_acct_region = '<% Response.Write(row["region"].ToString().Trim()); %>';
                <% cca_region = row["region"].ToString().Trim(); %>
                m_acct_years_in_business = '<% Response.Write(row["yrsInBusiness"].ToString().Trim()); %>';
                m_acct_years_with_matimco = '<% Response.Write(row["yrsWdMTC"].ToString().Trim()); %>';
                m_acct_tax_id = '<% Response.Write(row["TIN"].ToString().Trim()); %>';
                m_acct_vat_no = '<% Response.Write(row["VATregNum"].ToString().Trim()); %>';
                m_acct_reg_name = '<% Response.Write(StringHelper.InsertSlashes(row["regBusName"].ToString().Trim())); %>';
                m_acct_business_add = '<% Response.Write(StringHelper.InsertSlashes(row["bussAdd"].ToString().Trim())); %>';
                m_acct_delivery_add = '<% Response.Write(StringHelper.InsertSlashes(row["delAdd"].ToString().Trim())); %>';
                m_acct_num_employees = '<% Response.Write(row["TotalNumOfEmp"].ToString().Trim()); %>';
                m_final_acct_code = '<% Response.Write(row["AcctCode"].ToString().Trim()); %>';
            <%  } %>

            // required fields
            // SwitchRequiredFields(m_acct_classification);

            if (m_acct_type == "direct"){
                $("#acc_type_direct").attr('checked', 'checked');
            } else {
                $("#acc_type_indirect").attr('checked', 'checked');
            }

            if (m_acct_key_account == "False"){
                $("#acc_key_no").attr('checked', 'checked');
            } else {
                $("#acc_key_yes").attr('checked', 'checked');
            }

            $("#txt_acct_ccanum").attr('value', m_acct_ccanum);
            $("#txt_acct_classification").attr('value', m_acct_classification);

            // SAVE ORIGINAL VALUE
            $("#txt_acct_classification").attr('orig_value', m_acct_classification);
            
            $("#slt_acct_classification option").removeAttr("selected");
            $("#slt_acct_classification option[value=" + m_acct_classification + "]").attr('selected', 'selected');

            $("#txt_acct_code").attr('value', m_acct_code);
            $("#txt_acct_class").attr('value', m_acct_class);
            $("#txt_acct_name").attr('value', m_acct_name);
            $("#txt_phone_no").attr('value', m_acct_phone_no);
            $("#txt_acct_officer").attr('value', m_acct_acct_officer);
            $("#txt_fax_no").attr('value', m_acct_fax_no);
            $("#txt_acct_territory").attr('value', m_acct_territory);
            $("#txt_email_add").attr('value', m_acct_email);
            $("#txt_office_hours").attr('value', m_acct_office_hours);
            $("#txt_area").attr('value', m_acct_area);
            $("#txt_store_hours").attr('value', m_acct_store_hours);
            $("#txt_region").attr('value', m_acct_region);
            $("#txt_yrs_business").attr('value', m_acct_years_in_business);
            $("#txt_yrs_matimco").attr('value', m_acct_years_with_matimco);
            $("#txt_tax_id").attr('value', m_acct_tax_id);
            $("#txt_vat_no").attr('value', m_acct_vat_no);
            $("#txt_reg_name").attr('value', m_acct_reg_name);
            $("#txt_business_add").attr('value', m_acct_business_add);
            $("#txt_delivery_add").attr('value', m_acct_delivery_add);
            $("#txt_no_employees").attr('value', m_acct_num_employees);
            $("#txt_final_acct_code").attr('value', m_final_acct_code);

            /* SAVE ORIGINAL VALUES */
            $("#txt_acct_code").attr('orig_value', m_acct_code);
            $("#txt_acct_class").attr('orig_value', m_acct_class);
            $("#txt_acct_name").attr('orig_value', m_acct_name);
            $("#txt_phone_no").attr('orig_value', m_acct_phone_no);
            $("#txt_acct_officer").attr('orig_value', m_acct_acct_officer);
            $("#txt_fax_no").attr('orig_value', m_acct_fax_no);
            $("#txt_acct_territory").attr('orig_value', m_acct_territory);
            $("#txt_email_add").attr('orig_value', m_acct_email);
            $("#txt_office_hours").attr('orig_value', m_acct_office_hours);
            $("#txt_area").attr('orig_value', m_acct_area);
            $("#txt_store_hours").attr('orig_value', m_acct_store_hours);
            $("#txt_region").attr('orig_value', m_acct_region);
            $("#txt_yrs_business").attr('orig_value', m_acct_years_in_business);
            $("#txt_yrs_matimco").attr('orig_value', m_acct_years_with_matimco);
            $("#txt_tax_id").attr('orig_value', m_acct_tax_id);
            $("#txt_vat_no").attr('orig_value', m_acct_vat_no);
            $("#txt_reg_name").attr('orig_value', m_acct_reg_name);
            $("#txt_business_add").attr('orig_value', m_acct_business_add);
            $("#txt_delivery_add").attr('orig_value', m_acct_delivery_add);
            $("#txt_no_employees").attr('orig_value', m_acct_num_employees);
            $("#txt_final_acct_code").attr('orig_value', m_final_acct_code);

            // BUSINESS TYPE
            var m_acct_business_type;
            <%  foreach (DataRow row in busTypeHdr.Rows) { %>
                m_acct_business_type = '<% Response.Write(row["busType"].ToString().Trim()); %>';
            <%  } %>

            // NOTE
            $("#sub_tab").prev().hide();

            if( m_acct_business_type == "0" ){
                // select default tab
                $('#sub_tab').tabs("select", 0);
                
                /* SOLE PROPRIETORSHIP */
                var m_sole_owner_name;
                var m_sole_nationality;
                var m_sole_gen_manager;
                var m_sole_fin_manager;
                var m_sole_others;
            
                <%  foreach (DataRow row in busTypeHdr.Rows) { %>
                    m_sole_owner_name = '<% Response.Write(row["ownerCEO"].ToString().Trim()); %>';
                    m_sole_nationality = '<% Response.Write(row["nationality"].ToString().Trim()); %>';
                    m_sole_gen_manager = '<% Response.Write(row["genMgr"].ToString().Trim()); %>';
                    m_sole_fin_manager = '<% Response.Write(row["financeHead"].ToString().Trim()); %>';
                    m_sole_others = '';
                <%  } %>
            
                $("#txt_sole_owner_name").attr('value', m_sole_owner_name);
                $("#txt_sole_nationality").attr('value', m_sole_nationality);
                $("#txt_sole_gen_manager").attr('value', m_sole_gen_manager);
                $("#txt_sole_fin_manager").attr('value', m_sole_fin_manager);
                $("#txt_sole_others").attr('value', m_sole_others);

                /* SAVE ORIGINAL VALUES */
                $("#txt_sole_owner_name").attr('orig_value', m_sole_owner_name);
                $("#txt_sole_nationality").attr('orig_value', m_sole_nationality);
                $("#txt_sole_gen_manager").attr('orig_value', m_sole_gen_manager);
                $("#txt_sole_fin_manager").attr('orig_value', m_sole_fin_manager);
                $("#txt_sole_others").attr('orig_value', m_sole_others);
            }

            if( m_acct_business_type == "1" ){
                // select default tab
                $('#sub_tab').tabs("select", 1);

                /* PARNERSHIP */
                var m_partner_gen_manager;
                var m_partner_fin_manager;

                <%  foreach (DataRow row in busTypeHdr.Rows) { %>
                    m_partner_gen_manager = '<% Response.Write(row["genMgr"].ToString().Trim()); %>';
                    m_partner_fin_manager = '<% Response.Write(row["financeHead"].ToString().Trim()); %>';
                <%  } %>

                $("#txt_partner_gen_manager").attr('value', m_partner_gen_manager);
                $("#txt_partner_fin_manager").attr('value', m_partner_fin_manager);

                /* SAVE ORIGINAL VALUES */
                $("#txt_partner_gen_manager").attr('orig_value', m_partner_gen_manager);
                $("#txt_partner_fin_manager").attr('orig_value', m_partner_fin_manager);

                // list of people
                <%  foreach (DataRow row in busTypeDtl.Rows) { %>
                    <% 
                        Response.Write("AddPartners(");
                        Response.Write("'" + row["partnerStockHolder"].ToString().Trim() + "'"); Response.Write(",");
                        Response.Write("'" + row["nationality"].ToString().Trim() + "'"); Response.Write(",");
                        Response.Write("'" + row["capitalPerOwned"].ToString().Trim() + "'"); 
                        Response.Write(");\n");
                    %>
                    if (g_list_of_partners.length > 0){ g_list_of_partners = g_list_of_partners + "$"; }
                    g_list_of_partners = g_list_of_partners + '<% Response.Write(row["partnerStockHolder"].ToString().Trim() + "|" + row["nationality"].ToString().Trim() + "|" + row["capitalPerOwned"].ToString().Trim() + "|");  %>';
                <%  } %>
            }

            if( m_acct_business_type == "2" ){
                // select default tab
                $('#sub_tab').tabs("select", 2);

                /* CORPORATION */
                var m_corp_date_inc;
                var m_corp_gen_man;
                var m_corp_vp_fin;
                var m_corp_ceo;
                var m_corp_paidin_cap_stock;
                var m_corp_subc_cap_stock;
                var m_corp_auth_cap_stock;

                <%  foreach (DataRow row in busTypeHdr.Rows) { %>
                    m_corp_date_inc = '<% Response.Write(row["dateOfIncorporationFormatted"].ToString().Trim()); %>';
                    m_corp_gen_man = '<% Response.Write(row["genMgr"].ToString().Trim()); %>';
                    m_corp_vp_fin = '<% Response.Write(row["financeHead"].ToString().Trim()); %>';
                    m_corp_ceo = '<% Response.Write(row["ownerCEO"].ToString().Trim()); %>';
                    m_corp_paidin_cap_stock = '<% Response.Write(row["paidInCapStocks"].ToString().Trim()); %>';
                    m_corp_subc_cap_stock = '<% Response.Write(row["subscribedCapStocks"].ToString().Trim()); %>';
                    m_corp_auth_cap_stock = '<% Response.Write(row["authorizedCapStocks"].ToString().Trim()); %>';
                <%  } %>

                $("#txt_corpo_inc_date").attr('value', m_corp_date_inc);
                $("#txt_corpo_auth_cap_stock").attr('value', m_corp_auth_cap_stock);
                $("#txt_corpo_subscb_cap_stock").attr('value', m_corp_subc_cap_stock);
                $("#txt_corpo_paidin_cap_stock").attr('value', m_corp_paidin_cap_stock);
                $("#txt_corpo_ceo").attr('value', m_corp_ceo);
                $("#txt_corpo_vp_fin").attr('value', m_corp_vp_fin);
                $("#txt_corpo_gen_man").attr('value', m_corp_gen_man);

                // SAVE ORIGINAL VALUES
                $("#txt_corpo_inc_date").attr('orig_value', m_corp_date_inc);
                $("#txt_corpo_auth_cap_stock").attr('orig_value', m_corp_auth_cap_stock);
                $("#txt_corpo_subscb_cap_stock").attr('orig_value', m_corp_subc_cap_stock);
                $("#txt_corpo_paidin_cap_stock").attr('orig_value', m_corp_paidin_cap_stock);
                $("#txt_corpo_ceo").attr('orig_value', m_corp_ceo);
                $("#txt_corpo_vp_fin").attr('orig_value', m_corp_vp_fin);
                $("#txt_corpo_gen_man").attr('orig_value', m_corp_gen_man);

                // list of people
                <%  foreach (DataRow row in busTypeDtl.Rows) { %>
                    <% 
                        Response.Write("AddCorpo(");
                        Response.Write("'" + row["partnerStockHolder"].ToString().Trim() + "'"); Response.Write(",");
                        Response.Write("'" + row["nationality"].ToString().Trim() + "'"); Response.Write(",");
                        Response.Write("'" + row["capitalPerOwned"].ToString().Trim() + "'"); 
                        Response.Write(");\n");
                    %>
                    if (g_list_of_corpos.length > 0){ g_list_of_corpos = g_list_of_corpos + "$"; }
                    g_list_of_corpos = g_list_of_corpos + '<% Response.Write(row["partnerStockHolder"].ToString().Trim() + "|" + row["nationality"].ToString().Trim() + "|" + row["capitalPerOwned"].ToString().Trim() + "|");  %>';
                <%  } %>

            }

            // list of employee no.
            <%  foreach (DataRow row in empInventory.Rows) { %>
                <% 
                    Response.Write("AddEmployees(");
                    Response.Write("'" + row["position"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["numOfEmp"].ToString().Trim() + "'"); 
                    Response.Write(");\n");
                %>
                if (g_list_of_employees.length > 0){ g_list_of_employees = g_list_of_employees + "$"; }
                g_list_of_employees = g_list_of_employees + '<% Response.Write(row["position"].ToString().Trim() + "|" + row["numOfEmp"].ToString().Trim() + "|");  %>';
            <%  } %>

            // attachments
            var m_attch_AOI;
            var m_attch_FS;
            var m_attch_ITR;
            var m_attch_BIR;
            var m_attch_LBP;

            <%  foreach (DataRow row in custAttachment.Rows) { %>
                <% if ( row["attachType"].ToString().Trim() == "AOI" ){ %>
                    m_attch_AOI = '<% Response.Write(StringHelper.GetFileName(row["AttachPath"].ToString().Trim())); %>';

                    // for viewing the file
                    $("#txt_articles_of_inc").parent().parent().find("td:last").append("<a href=\"<%=ResolveUrl("~/") %>SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_AOI + "&id=" + m_acct_ccanum + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page_white_get.png\" style=\"border:0;\" /></a>");
                <% } %>

                <% if ( row["attachType"].ToString().Trim() == "FS" ){ %>
                    m_attch_FS = '<% Response.Write(StringHelper.GetFileName(row["AttachPath"].ToString().Trim())); %>';

                    // for viewing the file
                    $("#txt_financial_statement").parent().parent().find("td:last").append("<a href=\"<%=ResolveUrl("~/") %>SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_FS + "&id=" + m_acct_ccanum + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page_white_get.png\" style=\"border:0;\" /></a>");
                <% } %>

                <% if ( row["attachType"].ToString().Trim() == "ITR" ){ %>
                    m_attch_ITR = '<% Response.Write(StringHelper.GetFileName(row["AttachPath"].ToString().Trim())); %>';

                    // for viewing the file
                    $("#txt_ITR").parent().parent().find("td:last").append("<a href=\"<%=ResolveUrl("~/") %>SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_ITR + "&id=" + m_acct_ccanum + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page_white_get.png\" style=\"border:0;\" /></a>");
                <% } %>

                <% if ( row["attachType"].ToString().Trim() == "BIR" ){ %>
                    m_attch_BIR = '<% Response.Write(StringHelper.GetFileName(row["AttachPath"].ToString().Trim())); %>';

                    // for viewing the file
                    $("#txt_bir_reg").parent().parent().find("td:last").append("<a href=\"<%=ResolveUrl("~/") %>SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_BIR + "&id=" + m_acct_ccanum + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page_white_get.png\" style=\"border:0;\" /></a>");
                <% } %>

                <% if ( row["attachType"].ToString().Trim() == "LBP" ){ %>
                    m_attch_LBP = '<% Response.Write(StringHelper.GetFileName(row["AttachPath"].ToString().Trim())); %>';

                    // for viewing the file
                    $("#txt_business_permit").parent().parent().find("td:last").append("<a href=\"<%=ResolveUrl("~/") %>SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_LBP + "&id=" + m_acct_ccanum + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page_white_get.png\" style=\"border:0;\" /></a>");
                <% } %>

            <%  } %>
            

            $("#txt_articles_of_inc").attr('value', m_attch_AOI);
            $("#txt_financial_statement").attr('value', m_attch_FS);
            $("#txt_ITR").attr('value', m_attch_ITR);
            $("#txt_bir_reg").attr('value', m_attch_BIR);
            $("#txt_business_permit").attr('value', m_attch_LBP);

            // SAVE ORIGINAL VALUES
            $("#txt_articles_of_inc").attr('orig_value', m_attch_AOI);
            $("#txt_financial_statement").attr('orig_value', m_attch_FS);
            $("#txt_ITR").attr('orig_value', m_attch_ITR);
            $("#txt_bir_reg").attr('orig_value', m_attch_BIR);
            $("#txt_business_permit").attr('orig_value', m_attch_LBP);
            
            // custBusHdr
            var m_acct_prop_credit_term;
            var m_acct_prop_credit_limit;
            var m_acct_socio_eco_class;
            var m_acct_num_outlets;

            <%  foreach (DataRow row in custBusHdr.Rows) { %>
                m_acct_prop_credit_term = '<% Response.Write(row["propCredTerms"].ToString().Trim()); %>';
                m_acct_prop_credit_limit = '<% Response.Write(row["probCredLimit"].ToString().Trim()); %>';
                m_acct_socio_eco_class = '<% Response.Write(row["sociaEcoClass"].ToString().Trim()); %>';
                m_acct_num_outlets = '<% Response.Write(row["numOfOutlet"].ToString().Trim()); %>';
            <%  } %>

            $("#txt_credit_terms").attr('value', m_acct_prop_credit_term);
            $("#txt_credit_limit").attr('value', m_acct_prop_credit_limit);
            $("#txt_eco_class_of_customer").attr('value', m_acct_socio_eco_class);
            $("#txt_no_of_outlets").attr('value', m_acct_num_outlets);

            // SAVE ORIGINAL VALUES
            $("#txt_credit_terms").attr('orig_value', m_acct_prop_credit_term);
            $("#txt_credit_limit").attr('orig_value', m_acct_prop_credit_limit);
            $("#txt_eco_class_of_customer").attr('orig_value', m_acct_socio_eco_class);
            $("#txt_no_of_outlets").attr('orig_value', m_acct_num_outlets);

            var m_acct_mw_price_code = "";
            var m_acct_mw_price_desc = "";
            var m_acct_mw_price_remarks = "";
            var m_acct_ww_price_code = "";
            var m_acct_ww_price_desc = "";
            var m_acct_ww_price_remarks = "";
            var m_acct_pwf_price_code = "";
            var m_acct_pwf_price_desc = "";
            var m_acct_pwf_price_remarks = "";
            var m_acct_pwr_price_code = "";
            var m_acct_pwr_price_desc = "";
            var m_acct_pwr_price_remarks = "";
            var m_acct_gw_price_code = "";
            var m_acct_gw_price_desc = "";
            var m_acct_gw_price_remarks = "";
            var m_acct_tw_price_code = "";
            var m_acct_tw_price_desc = "";
            var m_acct_tw_price_remarks = "";

            // propsedPrice
            <%  foreach (DataRow row in propsedPrice.Rows) { %>
                
                <% if(row["brandType"].ToString().Trim() == "MW"){ %>
                    m_acct_mw_price_code = '<% Response.Write(row["priceListCode"].ToString().Trim()); %>';
                    m_acct_mw_price_desc = '<% Response.Write(row["codeDesc"].ToString().Trim()); %>';
                    m_acct_mw_price_remarks = '<% Response.Write(row["remarks"].ToString().Trim()); %>';
                <% } %>

                <% if(row["brandType"].ToString().Trim() == "WW"){ %>
                    m_acct_ww_price_code = '<% Response.Write(row["priceListCode"].ToString().Trim()); %>';
                    m_acct_ww_price_desc = '<% Response.Write(row["codeDesc"].ToString().Trim()); %>';
                    m_acct_ww_price_remarks = '<% Response.Write(row["remarks"].ToString().Trim()); %>';
                <% } %>

                <% if(row["brandType"].ToString().Trim() == "PWF"){ %>
                    m_acct_pwf_price_code = '<% Response.Write(row["priceListCode"].ToString().Trim()); %>';
                    m_acct_pwf_price_desc = '<% Response.Write(row["codeDesc"].ToString().Trim()); %>';
                    m_acct_pwf_price_remarks = '<% Response.Write(row["remarks"].ToString().Trim()); %>';
                <% } %>

                <% if(row["brandType"].ToString().Trim() == "PWR"){ %>
                    m_acct_pwr_price_code = '<% Response.Write(row["priceListCode"].ToString().Trim()); %>';
                    m_acct_pwr_price_desc = '<% Response.Write(row["codeDesc"].ToString().Trim()); %>';
                    m_acct_pwr_price_remarks = '<% Response.Write(row["remarks"].ToString().Trim()); %>';
                <% } %>

                <% if(row["brandType"].ToString().Trim() == "GW"){ %>
                    m_acct_gw_price_code = '<% Response.Write(row["priceListCode"].ToString().Trim()); %>';
                    m_acct_gw_price_desc = '<% Response.Write(row["codeDesc"].ToString().Trim()); %>';
                    m_acct_gw_price_remarks = '<% Response.Write(row["remarks"].ToString().Trim()); %>';
                <% } %>

                <% if(row["brandType"].ToString().Trim() == "TW"){ %>
                    m_acct_tw_price_code = '<% Response.Write(row["priceListCode"].ToString().Trim()); %>';
                    m_acct_tw_price_desc = '<% Response.Write(row["codeDesc"].ToString().Trim()); %>';
                    m_acct_tw_price_remarks = '<% Response.Write(row["remarks"].ToString().Trim()); %>';
                <% } %>
                
            <%  } %>

            $("#txt_mw_price_code").attr('value', m_acct_mw_price_code);
            $("#txt_mw_price_desc").attr('value', m_acct_mw_price_desc);
            $("#txt_mw_price_remarks").attr('value', m_acct_mw_price_remarks);

            $("#txt_ww_price_code").attr('value', m_acct_ww_price_code);
            $("#txt_ww_price_desc").attr('value', m_acct_ww_price_desc);
            $("#txt_ww_price_remarks").attr('value', m_acct_ww_price_remarks);

            $("#txt_pwf_price_code").attr('value', m_acct_pwf_price_code);
            $("#txt_pwf_price_desc").attr('value', m_acct_pwf_price_desc);
            $("#txt_pwf_price_remarks").attr('value', m_acct_pwf_price_remarks);

            $("#txt_pwr_price_code").attr('value', m_acct_pwr_price_code);
            $("#txt_pwr_price_desc").attr('value', m_acct_pwr_price_desc);
            $("#txt_pwr_price_remarks").attr('value', m_acct_pwr_price_remarks);

            $("#txt_gw_price_code").attr('value', m_acct_gw_price_code);
            $("#txt_gw_price_desc").attr('value', m_acct_gw_price_desc);
            $("#txt_gw_price_remarks").attr('value', m_acct_gw_price_remarks);

            $("#txt_tw_price_code").attr('value', m_acct_tw_price_code);
            $("#txt_tw_price_desc").attr('value', m_acct_tw_price_desc);
            $("#txt_tw_price_remarks").attr('value', m_acct_tw_price_remarks);

            // custOutlets
            // list of outlets
            <%  foreach (DataRow row in custOutlets.Rows) { %>
                <% 
                    Response.Write("AddOutlets(");
                    Response.Write("'" + StringHelper.InsertSlashes(row["name"].ToString().Trim()) + "'"); Response.Write(",");
                    Response.Write("'" + StringHelper.InsertSlashes(row["location"].ToString().Trim()) + "'"); Response.Write(",");
                    Response.Write("'" + row["storeSize"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["wreHouseSize"].ToString().Trim() + "'"); 
                    Response.Write(");\n");
                %>
                if (g_list_of_outlets.length > 0){ g_list_of_outlets = g_list_of_outlets + "$"; }
                g_list_of_outlets = g_list_of_outlets + '<% Response.Write(row["name"].ToString().Trim() + "|" + row["location"].ToString().Trim() + "|" + row["storeSize"].ToString().Trim() + "|" + row["wreHouseSize"].ToString().Trim() + "|");  %>';
            <%  } %>

            // majorCustomer
            // list of customer
            <%  foreach (DataRow row in majorCustomer.Rows) { %>
                <% 
                    Response.Write("AddMajorCustomers(");
                    Response.Write("'" + StringHelper.InsertSlashes(row["name"].ToString().Trim()) + "'"); Response.Write(",");
                    Response.Write("'" + StringHelper.InsertSlashes(row["address"].ToString().Trim()) + "'"); Response.Write(",");
                    Response.Write("'" + row["sellingTerms"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["estMonthPur"].ToString().Trim() + "'"); 
                    Response.Write(");\n");
                %>
                if (g_list_of_majorcustomers.length > 0){ g_list_of_majorcustomers = g_list_of_majorcustomers + "$"; }
                g_list_of_majorcustomers = g_list_of_majorcustomers + '<% Response.Write(row["name"].ToString().Trim() + "|" + row["address"].ToString().Trim() + "|" + row["sellingTerms"].ToString().Trim() + "|" + row["estMonthPur"].ToString().Trim() + "|");  %>';
            <%  } %>

            // products
            var m_acct_major_prod_line;
            var m_acct_other_prod_line;
            var m_acct_supplier_on_plywood;
            var m_acct_supplier_on_steel;
            var m_acct_supplier_on_cement;
            var m_acct_supplier_on_con_hollowblock;
            var m_acct_supplier_on_others;
            var m_acct_major_vol_business;
            var m_acct_monthly_wood_vol;
            var m_acct_discount_enjoyed;

            <%  foreach (DataRow row in products.Rows) { %>
                m_acct_major_prod_line = '<% Response.Write(row["majorLine"].ToString().Trim()); %>';
                m_acct_supplier_on_plywood = '<% Response.Write(StringHelper.InsertSlashes(row["suppPlywood"].ToString().Trim())); %>';
                m_acct_supplier_on_steel = '<% Response.Write(StringHelper.InsertSlashes(row["suppSteel"].ToString().Trim())); %>';
                m_acct_supplier_on_cement = '<% Response.Write(StringHelper.InsertSlashes(row["suppCement"].ToString().Trim())); %>';
                m_acct_supplier_on_con_hollowblock = '<% Response.Write(StringHelper.InsertSlashes(row["suppCHB"].ToString().Trim())); %>';
                m_acct_supplier_on_others = '<% Response.Write(StringHelper.InsertSlashes(row["suppOthers"].ToString().Trim())); %>';
                m_acct_major_vol_business = '<% Response.Write(row["volValueDriver"].ToString().Trim()); %>';
                m_acct_monthly_wood_vol = '<% Response.Write(row["woodVolValue"].ToString().Trim()); %>';
                m_acct_discount_enjoyed = '<% Response.Write(row["discounts"].ToString().Trim()); %>';
            <%  } %>

            $("#txt_prod_major_line").attr('value', m_acct_major_prod_line);
            $("#txt_const_mat_plywood").attr('value', m_acct_supplier_on_plywood);
            $("#txt_const_mat_steel").attr('value', m_acct_supplier_on_steel);
            $("#txt_const_mat_cement").attr('value', m_acct_supplier_on_cement);
            $("#txt_const_mat_hb").attr('value', m_acct_supplier_on_con_hollowblock);
            $("#txt_const_mat_others").attr('value', m_acct_supplier_on_others);
            $("#txt_major_vol_business").attr('value', m_acct_major_vol_business);
            $("#txt_wood_vol").attr('value', m_acct_monthly_wood_vol);
            $("#txt_discount_enjoyed").attr('value', m_acct_discount_enjoyed);

            // SAVE ORIGINAL VALUES
            $("#txt_prod_major_line").attr('orig_value', m_acct_major_prod_line);
            $("#txt_const_mat_plywood").attr('orig_value', m_acct_supplier_on_plywood);
            $("#txt_const_mat_steel").attr('orig_value', m_acct_supplier_on_steel);
            $("#txt_const_mat_cement").attr('orig_value', m_acct_supplier_on_cement);
            $("#txt_const_mat_hb").attr('orig_value', m_acct_supplier_on_con_hollowblock);
            $("#txt_const_mat_others").attr('orig_value', m_acct_supplier_on_others);
            $("#txt_major_vol_business").attr('orig_value', m_acct_major_vol_business);
            $("#txt_wood_vol").attr('orig_value', m_acct_monthly_wood_vol);
            $("#txt_discount_enjoyed").attr('orig_value', m_acct_discount_enjoyed);

            // otherWoodSupp
            <%  foreach (DataRow row in otherWoodSupp.Rows) { %>
                <% 
                    Response.Write("AddOtherSuppliers(");
                    Response.Write("'" + row["supplier"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["monthVolVal"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["contactPerson"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["contactNum"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["prodPurchase"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["creditTerms"].ToString().Trim() + "'"); 
                    Response.Write(");\n");
                %>
                if (g_list_of_wood_suppliers.length > 0){ g_list_of_wood_suppliers = g_list_of_wood_suppliers + "$"; }
                g_list_of_wood_suppliers = g_list_of_wood_suppliers + '<% Response.Write(row["supplier"].ToString().Trim() + "|" + row["monthVolVal"].ToString().Trim() + "|" + row["contactPerson"].ToString().Trim() + "|" + row["contactNum"].ToString().Trim() + "|" + row["prodPurchase"].ToString().Trim() + "|" + row["creditTerms"].ToString().Trim() + "|");  %>';
            <%  } %>

            // depositoryBank
            <%  foreach (DataRow row in depositoryBank.Rows) { %>
                <% 
                    Response.Write("AddBanks(");
                    Response.Write("'" + row["bankName"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["branch"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["address"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["account"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["contactPerson"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["contactNumber"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["aveDeposit"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["remarks"].ToString().Trim() + "'");
                    Response.Write(");\n");
                %>
                if (g_list_of_banks.length > 0){ g_list_of_banks = g_list_of_banks + "$"; }
                g_list_of_banks = g_list_of_banks + '<% Response.Write(row["bankName"].ToString().Trim() + "|" + row["branch"].ToString().Trim() + "|" + row["address"].ToString().Trim() + "|" + row["account"].ToString().Trim() + "|" + row["contactPerson"].ToString().Trim() + "|" + row["contactNumber"].ToString().Trim() + "|" + row["aveDeposit"].ToString().Trim() + "|" + row["remarks"].ToString().Trim() + "|");  %>';
            <%  } %>

            // asset_land
            <%  foreach (DataRow row in assets_land.Rows) { %>
                <% 
                    Response.Write("AddLands(");
                    Response.Write("'" + row["type"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["area"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["location"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["owner"].ToString().Trim() + "'");
                    Response.Write(");\n");
                %>
                if (g_list_of_banks.length > 0){ g_list_of_banks = g_list_of_banks + "$"; }
                g_list_of_banks = g_list_of_banks + '<% Response.Write(row["type"].ToString().Trim() + "|" + row["area"].ToString().Trim() + "|" + row["location"].ToString().Trim() + "|" + row["owner"].ToString().Trim() + "|");  %>';
            <%  } %>

            // assets_building
            <%  foreach (DataRow row in assets_building.Rows) { %>
                <% 
                    Response.Write("AddBuilding(");
                    Response.Write("'" + row["type"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["area"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["location"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["owner"].ToString().Trim() + "'");
                    Response.Write(");\n");
                %>
                if (g_list_of_buildings.length > 0){ g_list_of_buildings = g_list_of_buildings + "$"; }
                g_list_of_buildings = g_list_of_buildings + '<% Response.Write(row["type"].ToString().Trim() + "|" + row["area"].ToString().Trim() + "|" + row["location"].ToString().Trim() + "|" + row["owner"].ToString().Trim() + "|");  %>';
            <%  } %>

            // assets_vehicle
            <%  foreach (DataRow row in assets_vehicle.Rows) { %>
                <% 
                    Response.Write("AddVehicles(");
                    Response.Write("'" + row["type"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["model"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["quantity"].ToString().Trim() + "'");
                    Response.Write(");\n");
                %>
                if (g_list_of_vehicles.length > 0){ g_list_of_vehicles = g_list_of_vehicles + "$"; }
                g_list_of_vehicles = g_list_of_vehicles + '<% Response.Write(row["type"].ToString().Trim() + "|" + row["model"].ToString().Trim() + "|" + row["quantity"].ToString().Trim() + "|");  %>';
            <%  } %>

            // assets_other
            var m_acct_other_asset;

            <%  foreach (DataRow row in assets_other.Rows) { %>
                m_acct_other_asset = '<%: row["otherAssets"].ToString().Trim() %>';
            <%  } %>

            $("#txt_other_assets").attr('value', m_acct_other_asset);

            // SAVE ORIGINAL VALUES
            $("#txt_other_assets").attr('orig_value', m_acct_other_asset);

            // otherBusiness
            <%  foreach (DataRow row in otherBusiness.Rows) { %>
                <% 
                    Response.Write("AddOtherBusiness(");
                    Response.Write("'" + row["regName"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["nature"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["location"].ToString().Trim() + "'"); Response.Write(",");
                    Response.Write("'" + row["percentOwnership"].ToString().Trim() + "'");
                    Response.Write(");\n");
                %>
                if (g_list_of_other_business.length > 0){ g_list_of_other_business = g_list_of_other_business + "$"; }
                g_list_of_other_business = g_list_of_other_business + '<% Response.Write(row["regName"].ToString().Trim() + "|" + row["nature"].ToString().Trim() + "|" + row["location"].ToString().Trim() + "|" + row["percentOwnership"].ToString().Trim() + "|");  %>';
            <%  } %>

            // OTHER CHANGES
            var tmp_other_changes = "";
            <%  foreach (DataRow row in proposedChangesCA1.Rows) { %>
                tmp_other_changes = '<% Response.Write(row["OthrChanges"].ToString().Trim().Replace("\n", "\\n")); %>';
                $("#tabs").tabs("option", "disabled", []);
            <%  } %>

            // $("#div_other_changes").attr('value', tmp_other_changes);
            $("#div_other_changes").html(tmp_other_changes);

            <% 
                //if(userHeader["position"].ToString() != "csr")
                if ( oUsr.Roles.IndexOf("csr") == IS_NOT_FOUND )
                {
                    %> DisableEditing(m_acct_business_type); <%
                }
            %>

            <% 
                //if ( userHeader["position"].ToString() == "csr" )
                if ( oUsr.Roles.IndexOf("csr") != IS_NOT_FOUND )
                {
                    if( 
                        (
                            docStatusId["status"].ToString() != "1000" && 
                            docStatusId["status"].ToString() != AppHelper.GetUserPositionId("csr")
                        ) || 
                        docChangesStatusId["status"].ToString() != AppHelper.GetUserPositionId("csr") 
                      )
                    {
                        %> DisableEditing(m_acct_business_type); <%
                    }

                    // check if user's region and document's region mathed
                    if ( oUsr.Region.IndexOf(StringHelper.GetRegion(cca_region)) == IS_NOT_FOUND )
                    {
                        %> DisableEditing(m_acct_business_type); <%
                    }
                }
            %>

            <% 

            if (
                oUsr.Roles.IndexOf("cnc") == IS_NOT_FOUND &&
                oUsr.Roles.IndexOf("fnm") == IS_NOT_FOUND
            )
            {
                // disable
                %> DisableEditingCI(); <%
            }
            else 
            { 
                if (
                    oUsr.Roles.IndexOf("fnm") != IS_NOT_FOUND &&
                    curr_doc_DocStatus != "1008"
                )
                {
                    // if FNM but not 1008
                    // disable
                    %> DisableEditingCI(); <%
                }
            }
                
            // ALWAYS DISABLE
            %> DisableEditingCI(); <%

            %>

            // status message
            // var doc_stat_msg = '<%:AppHelper.GetDocStateMsg(docStatusId["status"].ToString()).Trim() %>';
            var doc_stat_msg = '<%: CurrDocMessage %>';
            $("#doc_stat_msg").html(doc_stat_msg);

            // save original values
            g_acct_name = m_acct_name;
            g_acct_acct_officer = m_acct_acct_officer;
            g_acct_territory = m_acct_territory;
            g_acct_area = m_acct_area;
            g_acct_region = m_acct_region;
            g_acct_reg_name = m_acct_reg_name;
            g_acct_business_add = m_acct_business_add;
            g_acct_delivery_add = m_acct_delivery_add;
            g_acct_prop_credit_term = m_acct_prop_credit_term;
            g_acct_prop_credit_limit = m_acct_prop_credit_limit;
            g_acct_mw_price_code = m_acct_mw_price_code;
            g_acct_mw_price_desc = m_acct_mw_price_desc;
            g_acct_mw_price_remarks = m_acct_mw_price_remarks;
            g_acct_ww_price_code = m_acct_ww_price_code;
            g_acct_ww_price_desc = m_acct_ww_price_desc;
            g_acct_ww_price_remarks = m_acct_ww_price_remarks;
            g_acct_pwf_price_code = m_acct_pwf_price_code;
            g_acct_pwf_price_desc = m_acct_pwf_price_desc;
            g_acct_pwf_price_remarks = m_acct_pwf_price_remarks;
            g_acct_pwr_price_code = m_acct_pwr_price_code;
            g_acct_pwr_price_desc = m_acct_pwr_price_desc;
            g_acct_pwr_price_remarks = m_acct_pwr_price_remarks;
            g_acct_gw_price_code = m_acct_gw_price_code;
            g_acct_gw_price_desc = m_acct_gw_price_desc;
            g_acct_gw_price_remarks = m_acct_gw_price_remarks;
            g_acct_tw_price_code = m_acct_tw_price_code;
            g_acct_tw_price_desc = m_acct_tw_price_desc;
            g_acct_tw_price_remarks = m_acct_tw_price_remarks;

            /* if the proposedChanges.routeType != "" */
            /* display the proposed changes instead of the original */
            /*
            <%  foreach (DataRow row in proposedChangesCA.Rows) { %>
                var n_val_acctName = '<% Response.Write(row["acctName"].ToString().Trim()); %>';
                var n_val_acctOffcr = '<% Response.Write(row["acctOffcr"].ToString().Trim()); %>';
                var n_val_territory = '<% Response.Write(row["territory"].ToString().Trim()); %>';
                var n_val_area = '<% Response.Write(row["area"].ToString().Trim()); %>';
                var n_val_region = '<% Response.Write(row["region"].ToString().Trim()); %>';
                var n_val_regBusName = '<% Response.Write(row["regBusName"].ToString().Trim()); %>';
                var n_val_bussAdd = '<% Response.Write(row["bussAdd"].ToString().Trim()); %>';
                var n_val_delAdd = '<% Response.Write(row["delAdd"].ToString().Trim()); %>';
                var n_val_propCredTerms = '<% Response.Write(row["propCredTerms"].ToString().Trim()); %>';
                var n_val_propCredLimit = '<% Response.Write(row["propCredLimit"].ToString().Trim()); %>';
                var n_val_pl_priceListCode_mw = '<% Response.Write(row["pl_priceListCode_mw"].ToString().Trim()); %>';
                var n_val_pl_codeDesc_mw = '<% Response.Write(row["pl_codeDesc_mw"].ToString().Trim()); %>';
                var n_val_pl_remarks_mw = '<% Response.Write(row["pl_remarks_mw"].ToString().Trim()); %>';
                var n_val_pl_priceListCode_ww = '<% Response.Write(row["pl_priceListCode_ww"].ToString().Trim()); %>';
                var n_val_pl_codeDesc_ww = '<% Response.Write(row["pl_codeDesc_ww"].ToString().Trim()); %>';
                var n_val_pl_remarks_ww = '<% Response.Write(row["pl_remarks_ww"].ToString().Trim()); %>';
                var n_val_pl_priceListCode_pwf = '<% Response.Write(row["pl_priceListCode_pwf"].ToString().Trim()); %>';
                var n_val_pl_codeDesc_pwf = '<% Response.Write(row["pl_codeDesc_pwf"].ToString().Trim()); %>';
                var n_val_pl_remarks_pwf = '<% Response.Write(row["pl_remarks_pwf"].ToString().Trim()); %>';
                var n_val_pl_priceListCode_pwr = '<% Response.Write(row["pl_priceListCode_pwr"].ToString().Trim()); %>';
                var n_val_pl_codeDesc_pwr = '<% Response.Write(row["pl_codeDesc_pwr"].ToString().Trim()); %>';
                var n_val_pl_remarks_pwr = '<% Response.Write(row["pl_remarks_pwr"].ToString().Trim()); %>';
                var n_val_pl_priceListCode_gw = '<% Response.Write(row["pl_priceListCode_gw"].ToString().Trim()); %>';
                var n_val_pl_codeDesc_gw = '<% Response.Write(row["pl_codeDesc_gw"].ToString().Trim()); %>';
                var n_val_pl_remarks_gw = '<% Response.Write(row["pl_remarks_gw"].ToString().Trim()); %>';
                var n_val_pl_priceListCode_tw = '<% Response.Write(row["pl_priceListCode_tw"].ToString().Trim()); %>';
                var n_val_pl_codeDesc_tw = '<% Response.Write(row["pl_codeDesc_tw"].ToString().Trim()); %>';
                var n_val_pl_remarks_tw = '<% Response.Write(row["pl_remarks_tw"].ToString().Trim()); %>';
            <%  } %>
            */

            /* display */
            /*
            <% if (proposedChangesCA.Rows.Count > 0) { %>
                $("#txt_acct_name").attr('value', n_val_acctName);
                $("#txt_acct_officer").attr('value', n_val_acctOffcr);
                $("#txt_acct_territory").attr('value', n_val_territory);
                $("#txt_area").attr('value', n_val_area);
                $("#txt_region").attr('value', n_val_region);
                $("#txt_reg_name").attr('value', n_val_regBusName);
                $("#txt_business_add").attr('value', n_val_bussAdd);
                $("#txt_delivery_add").attr('value', n_val_delAdd);
                $("#txt_credit_terms").attr('value', n_val_propCredTerms);
                $("#txt_credit_limit").attr('value', n_val_propCredLimit);
                $("#txt_mw_price_code").attr('value', n_val_pl_priceListCode_mw);
                $("#txt_mw_price_desc").attr('value', n_val_pl_codeDesc_mw);
                $("#txt_mw_price_remarks").attr('value', n_val_pl_remarks_mw);
                $("#txt_ww_price_code").attr('value', n_val_pl_priceListCode_ww);
                $("#txt_ww_price_desc").attr('value', n_val_pl_codeDesc_ww);
                $("#txt_ww_price_remarks").attr('value', n_val_pl_remarks_ww);
                $("#txt_pwf_price_code").attr('value', n_val_pl_priceListCode_pwf);
                $("#txt_pwf_price_desc").attr('value', n_val_pl_codeDesc_pwf);
                $("#txt_pwf_price_remarks").attr('value', n_val_pl_remarks_pwf);
                $("#txt_pwr_price_code").attr('value', n_val_pl_priceListCode_pwr);
                $("#txt_pwr_price_desc").attr('value', n_val_pl_codeDesc_pwr);
                $("#txt_pwr_price_remarks").attr('value', n_val_pl_remarks_pwr);
                $("#txt_gw_price_code").attr('value', n_val_pl_priceListCode_gw);
                $("#txt_gw_price_desc").attr('value', n_val_pl_codeDesc_gw);
                $("#txt_gw_price_remarks").attr('value', n_val_pl_remarks_gw);
                $("#txt_tw_price_code").attr('value', n_val_pl_priceListCode_tw);
                $("#txt_tw_price_desc").attr('value', n_val_pl_codeDesc_tw);
                $("#txt_tw_price_remarks").attr('value', n_val_pl_remarks_tw);
            <%  } %>
            */

            var m_sec_state_doc_status = '<%:AppHelper.GetDocStateMsg(docChangesStatusId["status"].ToString()) %>';
            var m_sec_state_doc_status_id = '<%:docChangesStatusId["status"].ToString() %>';
            
        }

         function txt_tax_id_onkeypress(e) 
         {
                var keycode = (e.keyCode)? e.keyCode : e.which;
           
            if(keycode == 8) return true;

            if( !(keycode > 47 && keycode < 58 ) || $("#txt_tax_id").attr('value').length == 15)  
             {
                return false;
             }
             else
             {
               if( $("#txt_tax_id").attr('value').length == 3 || $("#txt_tax_id").attr('value').length == 7 || $("#txt_tax_id").attr('value').length == 11 )
               {
                  $("#txt_tax_id").attr('value',$("#txt_tax_id").attr('value') + '-');
               }
             }
            return true;
         }

        function txt_creditlimit_onkeypress(e) 
        {
                var keycode = (e.keyCode)? e.keyCode : e.which;
           
           if(keycode == 8) return true;

            if( !( (keycode > 47 && keycode < 58) || keycode == 46) )  
             {
                
                return false;
             }
             else
             {
               if(keycode == 46)
               {
                
                    for(i=0;i < $("#txt_credit_limit").attr('value').length;i++)
                    {
                        if($("#txt_credit_limit").attr('value').charAt(i) == ".")
                            return false;
                    }
               }
            
             }
            return true;
        }
    </script>


    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Account Details</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    
   
    <div id="tabs">
	<ul>
		<li><a href="#tabs-1">Main Info</a></li>
		<li><a href="#tabs-2">Business</a></li>
		<li><a href="#tabs-3">Products</a></li>
		<li><a href="#tabs-4">Credit Investigation</a></li>
        <li style="display:none;"><a href="#tabs-5">Personal Info</a></li>
        <li style="display:none;"><a href="#tabs-6">Strategies</a></li>
        <li style="display:none;"><a href="#tabs-7">Activities and Events</a></li>
        <li style="display:none;"><a href="#tabs-8">Business Review</a></li>
        <li style="display:none;"><a href="#tabs-9">Contracts and Agreements</a></li>
        <li style="display:none;"><a href="#tabs-10">Change Log</a></li>
        <li style="display:none;"><a href="#tabs-21">Financial Information</a></li>
        <li><a href="#tabs-22">Other Changes</a></li>
	</ul>
	<div id="tabs-1">
		<!-- TAB-1 CONTENT START -->
		<table width="100%" cellpadding="1" cellspacing="0" border="0">
            <tr>
				<td colspan="5" align="right" style="height: 21px">
                    <span id="doc_stat_msg"></span>
                </td>
			</tr>
            <tr>
                <td colspan="5" align="right">
                    &nbsp;
                </td>
            </tr>
			<tr>
				<td style="height: 28px">CCA Number</td>
				<td style="height: 28px"><input type="text" id="txt_acct_ccanum" readonly="readonly" /></td>
				<td style="height: 28px"></td>
				<td style="height: 28px">Final Account Code</td>
                <td style="height: 28px">
                    <input type="text" id="txt_final_acct_code" readonly="readonly" class="readonly_fields" />
                </td>
			</tr>
            <tr>
				<td style="height: 28px">Account Classification</td>
				<td style="height: 28px">
                    <select id="slt_acct_classification">
                        <option value="REGULAR" >Regular</option>
                        <option value="WALKIN" >Walk-In</option>
                    </select>

                    <input type="hidden" id="txt_acct_classification" readonly="readonly" />
                   
                </td>
				<td style="height: 28px"></td>
                <td style="height: 28px"></td>
				<td style="height: 28px"></td>
			</tr>
			<tr>
				<td>Account Type</td>
				<td align="left" colspan="4">
					<label><input type="radio" id="acc_type_direct" name="option1" value="Direct" checked="checked" />Direct</label>
					&nbsp;&nbsp;
					<label><input type="radio" id="acc_type_indirect" name="option1" value="Indirect" />Indirect</label>
				</td>
			</tr>
			<tr>
				<td>Key Account</td>
				<td colspan="4" align="left">
					<label><input type="radio" id="acc_key_yes" name="option2" value="Direct" checked="checked" />Yes</label>
				
					<label><input type="radio" id="acc_key_no" name="option2" value="Indirect" />No</label>
				</td>
			</tr>
			<tr>
				<td>Proposed Account Code</td>
				<td>
					<input type="text" id="txt_acct_code" />
				</td>
				<td></td>
				<td>
                    <!-- Account Class -->
                </td>
				<td>
					<input type="hidden" onclick="javascript:LookUpData('txt_acct_class', 'ListOfBPClass');" id="txt_acct_class" name="txt_acct_class" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Account Name</td>
				<td>
					<input type="text" id="txt_acct_name" />
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>Phone no.</td>
				<td>
					<input type="text" id="txt_phone_no" onkeypress="return isNumberKey(event)" />
				</td>
				<td></td>
				<td>Account Officer</td>
				<td>
					<input type="text" onclick="javascript:LookUpData('txt_acct_officer', 'ListOfSo');" id="txt_acct_officer" name="txt_acct_officer" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Fax</td>
				<td>
					<input type="text" id="txt_fax_no" onkeypress="return isNumberKey(event)"/>
				</td>
				<td></td>
				<td>Territory</td>
				<td>
					<input type="text" onclick="javascript:LookUpData('txt_acct_territory', 'ListOfTerritory');" id="txt_acct_territory" name="txt_acct_territory" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>E-mail</td>
				<td>
					<input type="text" id="txt_email_add" />
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>Office Hours</td>
				<td>
					<input type="text" id="txt_office_hours" />
				</td>
				<td></td>
				<td>Area</td>
				<td>
					<input type="text" id="txt_area" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Store Hours</td>
				<td>
					<input type="text" id="txt_store_hours" />
				</td>
				<td></td>
				<td>Region</td>
				<td>
					<input type="text" id="txt_region" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>No. of yrs in Business</td>
				<td>
					<input type="text" id="txt_yrs_business" onkeypress="return isNumberKey(event)"/>
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>No. of yrs w/ Matimco Inc.</td>
				<td>
					<input type="text" id="txt_yrs_matimco" onkeypress="return isNumberKey(event)"/>
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>Tax ID
                </td>
				<td>
					<input type="text" id="txt_tax_id" onkeypress="return txt_tax_id_onkeypress(event)"/>
				</td>
				<td></td>
				<td>VAT Type</td>
				<td>
					<input type="text" id="txt_vat_no" onclick="javascript:LookUpData('txt_vat_no', 'ListOfVat');" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Registered Name</td>
				<td colspan="4">
					<input type="text" id="txt_reg_name" />
				</td>
			</tr>
			<tr>
				<td>Business Address</td>
				<td colspan="4">
					<!--<input type="text" id="txt_business_add" /> -->
                    <textarea id="txt_business_add" name="S1" rows="1" 
                        style="width:38%; height:50px;"></textarea></td>
			</tr>
			<tr>
				<td>Delivery Address</td>
				<td colspan="4">
					<!-- <input type="text" id="txt_delivery_add" /> -->
                    <textarea id="txt_delivery_add" name="S1" rows="1" 
                        style="width:38%; height:50px;"></textarea>
				</td>
			</tr>
			<tr>
				<td colspan="5" >
					<!-- BUSINESS TYPE - START -->
					<div class="margin_box" style="background-color:#ededed;">

						<div class="simple_box" style="font-weight:bold; font-size:11px; background-color:#fff9df; color:#000000; border:1px solid #ffe787;">
							
								<table cellspacing="0" cellpadding="0" border="0">
								<tr>
									<td valign="middle"><img src="<%=ResolveUrl("~/") %>Images/information.png" style="border:0;" /></td>
									<td valign="middle">&nbsp;&nbsp;</td>
									<td valign="middle"><b>Note:</b></td>
								</tr>
								</table>
								<i> 
									Under business type [Sole Proprietorship/Partnership/Corporation], 
									click first the [TAB] before filling in the info/data.
									Clicking other businness type will erase the info/data under 
									business type.
								</i>
						</div>

						<div id="sub_tab">
							<ul>
								<li><a href="#tabs-11">Sole Proprietorship</a></li>
								<li><a href="#tabs-12">Partnership</a></li>
								<li><a href="#tabs-13">Corporation</a></li>
							</ul>
							<div id="tabs-11">
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>Name of Owner</td>
										<td>
											<input type="text" id="txt_sole_owner_name" />
										</td>
									</tr>
									<tr>
										<td>Nationality</td>
										<td>
											<input type="text" id="txt_sole_nationality" />
										</td>
									</tr>
									<tr>
										<td>General Manager</td>
										<td>
											<input type="text" id="txt_sole_gen_manager" />
										</td>
									</tr>
									<tr>
										<td>Finance Manager</td>
										<td>
											<input type="text" id="txt_sole_fin_manager" />
										</td>
									</tr>
									<tr>
										<td>Others</td>
										<td>
											<input type="text" id="txt_sole_others" />
										</td>
									</tr>
								</table>
							</div>
							<div id="tabs-12">
								<table id="tbl_partner_list" cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td align="center">Partner</td>
										<td align="center">Nationality</td>
										<td align="center">Contributed Capital</td>
										<td></td>
									</tr>
									<tr>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td><a href="javascript:AddEntryPartnership();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
									</tr>
								</table>
								<br />
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>General Manager</td>
										<td><input type="text" id="txt_partner_gen_manager" /></td>
									</tr>
									<tr>
										<td>Finance Manager</td>
										<td><input type="text" id="txt_partner_fin_manager" /></td>
									</tr>
									<tr>
										<td>Others</td>
										<td><input type="text" id="txt_partner_others" /></td>
									</tr>
								</table>
							</div>
							<div id="tabs-13">
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>Date of Incorporation</td>
										<td><input type="text" id="txt_corpo_inc_date" readonly="readonly" /></td>
									</tr>
									<tr>
										<td>Authorized Capital Stock</td>
										<td><input type="text" id="txt_corpo_auth_cap_stock" /></td>
									</tr>
									<tr>
										<td>Subscribed Capital Stock</td>
										<td><input type="text" id="txt_corpo_subscb_cap_stock" /></td>
									</tr>
									<tr>
										<td>Paid-In Capital Stock</td>
										<td><input type="text" id="txt_corpo_paidin_cap_stock" /></td>
									</tr>
								</table>
								<br />
								<table id="tbl_corpo_list" cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td align="center">Major Stockholders</td>
										<td align="center">Nationality</td>
										<td align="center">%Owned</td>
										<td></td>
									</tr>
									<tr>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td><a href="javascript:AddEntryCorporation();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
									</tr>
								</table>
								<br />
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>President / CEO</td>
										<td><input type="text" id="txt_corpo_ceo" /></td>
									</tr>
									<tr>
										<td>VP Finance</td>
										<td><input type="text" id="txt_corpo_vp_fin" /></td>
									</tr>
									<tr>
										<td>General Manager</td>
										<td><input type="text" id="txt_corpo_gen_man" /></td>
									</tr>
								</table>
							</div>
						</div>

					</div>
					<!-- BUSINESS TYPE - END -->
				</td>
			</tr>
			<tr>
				<td colspan="5" style="padding:5px;">
					<div class="simple_box">
						<table id="tbl_emp_pos_list" cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td>No. of Employees</td>
								<td><input type="text" id="txt_no_employees" /></td>
							</tr>
							<tr>
								<td align="center">Position</td>
								<td align="center">No. of Employees</td>
								<td></td>
							</tr>
							<tr>
								<td><input type="text" /></td>
								<td><input type="text" /></td>
								<td><a href="javascript:AddEntryEmployeePos();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="5" style="padding:5px;">
					<div class="simple_box">
						<h2>Attachments (Pre-Enrollment Documents)</h2>
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td>Articles of Incorporation</td>
								<td>
									<input type="text" id="txt_articles_of_inc" name="txt_articles_of_inc" onclick="javascript:CreateUploadingBox('txt_articles_of_inc');" readonly="readonly" />
                                    <input type="hidden" id="txt_articles_of_inc_forupload" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('AOI');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
                                <td></td>
							</tr>
							<tr>
								<td>Financial Statements</td>
								<td>
									<input type="text" id="txt_financial_statement" name="txt_financial_statement" onclick="javascript:CreateUploadingBox('txt_financial_statement');" readonly="readonly" />
                                    <input type="hidden" id="txt_financial_statement_forupload" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('FS');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
                                <td></td>
							</tr>
							<tr>
								<td>Income Tax Return</td>
								<td>
									<input type="text" id="txt_ITR" name="txt_ITR" onclick="javascript:CreateUploadingBox('txt_ITR');" readonly="readonly" />
                                    <input type="hidden" id="txt_ITR_forupload" />
								</td>
                                <td>   
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('ITR');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
                                <td></td>
							</tr>
							<tr>
								<td>BIR Registration</td>
								<td>
									<input type="text" id="txt_bir_reg" name="txt_bir_reg" onclick="javascript:CreateUploadingBox('txt_bir_reg');" readonly="readonly" />
                                    <input type="hidden" id="txt_bir_reg_forupload" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('BIR');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
                                <td></td>
							</tr>
							<tr>
								<td>Latest Business Permit</td>
								<td>
									<input type="text" id="txt_business_permit" name="txt_business_permit" onclick="javascript:CreateUploadingBox('txt_business_permit');" readonly="readonly" />
                                    <input type="hidden" id="txt_business_permit_forupload" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('BP');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
                                <td></td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
		</table>
         <div ><i style="color:Red; font-size: 10">To fast track your account approval process, please provide 
             complete bank information</i> </div>
		<!-- TAB-1 CONTENT END -->
	</div>
	<div id="tabs-2">
		<!-- TAB-2 CONTENT START -->
        <table cellpadding="1" cellspacing="0" border="0">
			<tr>
				<td>Proposed Credit Terms</td>
				<td><input type="text" id="txt_credit_terms" onclick="javascript:LookUpData('txt_credit_terms', 'ListOfPaymentGroup');" /></td>
			</tr>
			<tr>
				<td>Proposed Credit Limit</td>
				<td><input type="text" id="txt_credit_limit" onkeypress="return txt_creditlimit_onkeypress(event)" /></td>
			</tr>
		</table>
		<table cellpadding="1" cellspacing="0" border="0">
			<tr>
				<td align="center">Proposed Price Lists</td>
				<td align="center">Code</td>
				<td align="center">Description</td>
				<td align="center">Remarks</td>
			</tr>
			<tr>
				<td>Matwood</td>
				<td><input type="text" id="txt_mw_price_code" onclick="javascript:LookUpData('txt_mw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_mw_price_desc" readonly="readonly" /></td>
				<td><input type="text" id="txt_mw_price_remarks" /></td>
			</tr>
			<tr>
				<td>WeatherWood</td>
				<td><input type="text" id="txt_ww_price_code" onclick="javascript:LookUpData('txt_ww_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_ww_price_desc" readonly="readonly" /></td>
				<td><input type="text" id="txt_ww_price_remarks" /></td>
			</tr>
			<tr>
				<td>PCW Frames</td>
				<td><input type="text" id="txt_pwf_price_code" onclick="javascript:LookUpData('txt_pwf_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_pwf_price_desc" readonly="readonly" /></td>
				<td><input type="text" id="txt_pwf_price_remarks" /></td>
			</tr>
            <tr>
				<td>PCW Regular Items</td>
				<td><input type="text" id="txt_pwr_price_code" onclick="javascript:LookUpData('txt_pwr_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_pwr_price_desc" readonly="readonly" /></td>
				<td><input type="text" id="txt_pwr_price_remarks" /></td>
			</tr>
			<tr>
				<td>GudWood</td>
				<td><input type="text" id="txt_gw_price_code" onclick="javascript:LookUpData('txt_gw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_gw_price_desc" readonly="readonly" /></td>
				<td><input type="text" id="txt_gw_price_remarks" /></td>
			</tr>
            <tr>
				<td>TrussWood</td>
				<td><input type="text" id="txt_tw_price_code" onclick="javascript:LookUpData('txt_tw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_tw_price_desc" readonly="readonly" /></td>
				<td><input type="text" id="txt_tw_price_remarks" /></td>
			</tr>
		</table>
		<br />
		<div class="simple_box">
			Socio Economic Class of Customers: <input type="text" id="txt_eco_class_of_customer" /> <br />
			Number of Outlets: <input type="text" id="txt_no_of_outlets" /> <br />
		</div><br />
		<div class="simple_box">
			Outlets <br />
			<table id="tbl_outlet_list" width="100%" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Name of Outlets</td>
					<td align="center">Location</td>
					<td align="center">Store Size</td>
					<td align="center">Warehouse Size</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryOutlet();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Major Customers <br />
			<table id="tbl_mjcust_list" width="100%" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Name</td>
					<td align="center">Address</td>
					<td align="center">Selling Terms</td>
					<td align="center">Est. Monthly Purchases</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryMajorCustomer();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<!-- TAB-2 CONTENT END -->
	</div>
	<div id="tabs-3">
		<table width="100%" cellpadding="1px" cellspacing="0" border="0">
			<tr>
				<td>Major Line</td>
				<td><input type="text" id="txt_prod_major_line" /></td>
			</tr>
			<tr>
				<td>Other Product Lines</td>
				<td><input type="text" id="txt_prod_other_line" /></td>
			</tr>
		</table>
		<div class="simple_box">
			Construction Materials
			<table cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td></td>
					<td align="center">Top Suppliers</td>
				</tr>
				<tr>
					<td>Plywood</td>
					<td><input type="text" id="txt_const_mat_plywood" /></td>
				</tr>
				<tr>
					<td>Steel</td>
					<td><input type="text" id="txt_const_mat_steel" /></td>
				</tr>
				<tr>
					<td>Cement</td>
					<td><input type="text" id="txt_const_mat_cement" /></td>
				</tr>
				<tr>
					<td>Concrete HollowBlock</td>
					<td><input type="text" id="txt_const_mat_hb" /></td>
				</tr>
				<tr>
					<td>Others</td>
					<td><input type="text" id="txt_const_mat_others" /></td>
				</tr>
			</table>
		</div>
		<table cellpadding="1px" cellspacing="0" border="0">
			<tr>
				<td>Major Volume/ Value Drivers of the Business</td>
				<td><input type="text" id="txt_major_vol_business" /></td>
			</tr>
			<tr>
				<td>Monthly Wood Volume/Value</td>
				<td><input type="text" id="txt_wood_vol" /></td>
			</tr>
			<tr>
				<td>Discounts Enjoyed</td>
				<td><input type="text" id="txt_discount_enjoyed" /></td>
			</tr>
		</table>
		<div class="simple_box">
			Other Wood Suppliers
			<table id="tbl_wood_supplier" width="100%" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Supplier</td>
					<td align="center">Monthly Volume/Value</td>
					<td align="center">Contact Person</td>
					<td align="center">Contact Number</td>
					<td align="center">Products Usually Purchased</td>
					<td align="center">Credit Terms</td>
					<td align="center">Other Deals Offered</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryWoodSupplier();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
	</div>
	<div id="tabs-4">
         <div class="simple_box" style="border:none;"><i style="color:Red; font-size: 12">The information under this tab is confidential and should not be shared with the customer.</i> </div>
		
		<div class="simple_box">
			Bank
			<table width="100%" id="tbl_bank_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Bank</td>
					<td align="center">Branch</td>
					<td align="center">Address</td>
					<td align="center">Acct. No.</td>
					<td align="center">Contact No.</td>
					<td align="center">Contact Person</td>
					<td align="center">Avg. Daily Bal.</td>
					<td align="center">Remarks</td>
					<td align="center"></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryBank();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Land
			<table id="tbl_land_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Type</td>
					<td align="center">Area (sq. m.)</td>
					<td align="center">Location</td>
					<td align="center">Owned by</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" onclick="javascript:LookUpData('tbl_land_list tr:last td:nth-child(1) input[type=text]', 'ListOfLandType');" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryLands();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Building
			<table id="tbl_building_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Type</td>
					<td align="center">Area (sq. m.)</td>
					<td align="center">Location</td>
					<td align="center">Owned by</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" onclick="javascript:LookUpData('tbl_building_list tr:last td:nth-child(1) input[type=text]', 'ListOfBuildingType');" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryBuildings();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Vehicles
			<table id="tbl_vehicle_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Type</td>
					<td align="center">Model</td>
					<td align="center">Quantity</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryVehicles();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Other Assets: <input type="text" id="txt_other_assets" />
		</div>
        <br />
        <div class="simple_box">
			Other Business
			<table id="tbl_asset_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Registered Name</td>
					<td align="center">Nature</td>
					<td align="center">Location</td>
                    <td align="center">% of Ownership</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
                    <td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryAssets();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
            
		</div>
	</div>
    <div id="tabs-5">
        <!-- Personal -->
        <table border="0" cellspacing="0" cellpadding="1">
            <tr>
                <td colspan="4">
                    <b>Personal</b>
                </td>
            </tr>
            <tr>
                <td>Owner/CEO Name</td>
                <td>
                    <input type="text" />
                </td>
                <td>Citizenship</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Age</td>
                <td>
                    <input type="text" />
                </td>
                <td>Status</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Birth Date</td>
                <td>
                    <input type="text" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>Birth Place</td>
                <td>
                    <input type="text" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>Language Spoken</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Language Written</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b>Educational Background</b>
                </td>
            </tr>
            <tr>
                <td>Highest Educational Attainment</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Degree Earned</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Year</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>School</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b>Family Background</b>
                </td>
            </tr>
            <tr>
                <td>Spouse</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Age</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Occupation</td>
                <td colspan="3">
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b>Affiliated Organization</b>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <!-- TABLE -->
                    <table border="0" cellspacing="0" cellpadding="1" >
                    <tr>
                        <td>Organization Name</td>
                        <td>Position</td>
                    </tr>
                    </table>
                    <!-- TABLE -->
                </td>
            </tr>
            <!-- FOR INDIRECT ACCOUNTS START -->
            <tr>
                <td colspan="4">
                    <b>Projects Handled</b>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <!-- TABLE -->
                    <table border="0" cellspacing="0" cellpadding="1" >
                    <tr>
                        <td>Project Name</td>
                        <td>Location</td>
                        <td>Others</td>
                    </tr>
                    </table>
                    <!-- TABLE -->
                </td>
            </tr>
            <!-- FOR INDIRECT ACCOUNTS END -->
        </table>
    </div>
    <div id="tabs-6">
        <!-- Strategies -->
        <table border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td colspan="2">
                <h2>Account Objectives and Strategies</h2>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Objectives</b>
            </td>
        </tr>
        <tr>
            <td valign="top">Long Term</td>
            <td>
                <textarea style="width:300px; height:50px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td valign="top">Short Term</td>
            <td>
                <textarea style="width:300px; height:50px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Strategies</b>
            </td>
        </tr>
        <tr>
            <td valign="top">Long Term</td>
            <td>
                <textarea style="width:300px; height:50px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td valign="top">Short Term</td>
            <td>
                <textarea style="width:300px; height:50px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Remarks</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <textarea style="width:100%; height:50px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>Prepared By</td>
            <td>
                <input type="text" />
            </td>
        </tr>
        </table>

    </div>
    <div id="tabs-7">
        <!-- Activities and Events -->
        <table border="0" cellspacing="0" cellpadding="1">
        <tr>
            <td colspan="2">
                <b>Significant Events and Activities</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                LIST OF EVENTS HERE
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Marketing Programs Availed</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" cellspacing="0" cellpadding="1">
                    <tr>
                        <td>Marketing Program No.</td>
                        <td>Program Name</td>
                        <td>Actual Cost Incurred</td>
                    </tr>
                    <tr>
                        <td><input type="text" /></td>
                        <td><input type="text" /></td>
                        <td><input type="text" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Inventory Level</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" cellspacing="0" cellpadding="1">
                    <tr>
                        <td>Date</td>
                        <td>Quantity</td>
                        <td>Volume</td>
                        <td>Value</td>
                    </tr>
                    <tr>
                        <td><input type="text" /></td>
                        <td><input type="text" /></td>
                        <td><input type="text" /></td>
                        <td><input type="text" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </div>
    <div id="tabs-8">
        <!-- Business Review -->
        <table border="0" cellpadding="1" cellspacing="0">
            <tr>
                <td>Business Review Number</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Next Business Review Number</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Frequency of Business Review</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
        </table>
    </div>
    <div id="tabs-9">
        <!-- Contracts and Agreements -->
        <table border="0" cellpadding="1" cellspacing="0">
            <tr>
                <td colspan="2"><b>RACK</b></td>
            </tr>
            <tr>
                <td>Type</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Setup Date</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Size</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td>Value</td>
                <td>
                    <input type="text" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>Stipulations</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="text" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>Signed Agreement</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Photos</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td colspan="2"><b>Other Agreements</b></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="1" cellspacing="0">
                        <tr>
                            <td>Agreement Name</td>
                            <td>Date</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="tabs-10">
        <!-- Change Log -->
        CHANGE LOG
    </div>
    <div id="tabs-21">
        <!-- Financial Information -->
        TAB 11
    </div>
    <div id="tabs-22">
        <!-- Other Changes -->
        <div id="div_other_changes" style="padding:11px;">
        </div>
    </div>
	</div>
    </div>
<hr />

<center>

    <!-- 2ND PART -->

    <% 
        if (
            curr_doc_DocStatus == "1000" && 
            docChangesStatusId["status"].ToString() == AppHelper.GetUserPositionId("csr") && 
            oUsr.Roles.IndexOf("csr") != IS_NOT_FOUND &&
            oUsr.Region.IndexOf(StringHelper.GetRegion(cca_region)) != IS_NOT_FOUND
            ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td><input type="button" value="Update Changes" onclick="javascript:Save_Doc();" /></td>
            <td>
                <!--
                    <input type="button" value="Cancel" />
                -->
            </td>
        </tr>
        </table>
    <% 
        } 
    %>

</center>

</asp:Content>
