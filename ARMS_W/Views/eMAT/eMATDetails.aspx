<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        // Request.QueryString["eMAT"]

        //_User Ousr = new _User(Session["username"].ToString());
        _User Ousr = (_User)Session["Ousr"];
        
        string EmatNo = Request.QueryString["eMATno"].ToString();
        
        DataTable eMAT;
        eMAT = SqlDbHelper.getDataDT("select *, convert(varchar(10), deliveryDate, 101) as 'deliveryDateFormatted',  convert(varchar(10), confirmedDelBy, 101) as 'confirmedDelByFormatted' from dbo.eMAT where eMATno='" + EmatNo + "'");

        DataTable eMATdtls;
        eMATdtls = SqlDbHelper.getDataDT("select * from dbo.eMATdtls where eMATno='" + EmatNo + "'");
       
        // select the position of the user
        DataRow userInfo;
        userInfo = SqlDbHelper.getDataDT("select * from userHeader where userName='" + Ousr.UserName + "'").Rows[0];

        // get doc stateid
        DataRow docStatusId;
        docStatusId = SqlDbHelper.getDataDT("select rtrim(ltrim(status)) as 'status' from dbo.eMAT where eMATno= '" + EmatNo + "'").Rows[0];
        
    %>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/eMATdetails.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
            LoadValues("<%: EmatNo %>");

            DisableEditing();

        });

        var EMATaccCode = "";

    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                   <b> E-MAT Details</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    
    <table align="center" style="margin:5px;" >
        <tr>
            <td colspan="5" align="right" valign="top">
                <span id="doc_stat_msg"></span>
            </td>
        </tr>
    </table>

    <div style="padding:15px; font-size:12px;">
        <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td> Buyer </td>
                <td>
                    <input type="text" id="txt_ED_buyer"  size="27" readonly="readonly" />
                </td>
            </tr>

            <tr>
                <td> E-MAT Document No. </td>
                <td><input type="text" id="txt_ematdocnum" size="27" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Address </td>
                <td>
                    <input type="text" id="txt_ED_address" size="27" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Tel. No.</td>
                <td>
                    <input type="text" id="txt_ED_telno"  size="27"  readonly="readonly"/>
                </td>
            </tr>
            <tr>
                <td>Contact Person</td>
                <td>
                    <input type="text" id="txt_ED_contactperson"  size="27" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td valign="top">Terms</td>
                <td>
                    <textarea style="height: 65px; width: 100%;" id="txt_ED_terms"  readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>Delivery Date</td>
                <td>
                    <input type="text" id="txt_ED_deliverydate"  size="27" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td valign="top">Delivery Instruction</td>
                <td>
                    <textarea style="height: 65px; width: 100%;" id="txt_ED_deliveryinstruction" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Referred To (Direct Account)</td>
                <td>
                    <input type="text" id="txt_ED_referredto" size="27" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Submitted To (Account Clerk)	</td>
                <td>
                    <input type="text" id="txt_ED_Submittedto"  size="27" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Contact Number</td>
                <td>
                    <input type="text" id="txt_ED_contactnumber"  size="27" readonly="readonly"/>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Confirmed Delivery By (Dealer)</td>
                <td>
                    <input type="text" id="txt_ED_confirmeddelivery"  size="27" readonly="readonly"/>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Sales Representative</td>
                <td>
                    <input type="text" id="txt_ED_tradesales_rep"  size="27" readonly="readonly" />
                </td>
            </tr>
        </table>
    </div>

    <div style="padding:15px; font-size:12px;">
        <b>Details</b>
        <table cellpadding="2" cellspacing="0" border="0" width="100%" id="table_details" >
            <tr>
                <td style="width:120px;">Item Code</td>
                <td>Item Description</td>
                <td style="width:70px;">Price</td>
                <td style="width:70px;">Unit of Measure</td>
                <td style="width:70px;">Quantity</td>
                <td style="width:70px;">Total</td>
            </tr>
            <tr>
                <!--<td><input type="text" id="txt_itemcode" style="width:99%;" onclick="javascript:LookUpData('txt_itemcode','ListOfitem');" /></td>-->
                <td><input type="text" id="txt_itemcode" style="width:99%;" /></td>
                <td><input type="text" id="txt_product" style="width:99%;" /></td>
                <td><input type="text" id="txt_price" style="width:99%; text-align:right;" /></td>
                <td><input type="text" id="txt_unit" style="width:99%; text-align:left;" readonly="readonly" /></td>
                <td><input type="text" id="txt_quantity" style="width:95%; text-align:right;" /></td>
                <td><input type="text" id="txt_total" style="width:99%; text-align:right;" readonly="readonly" /></td>
                <!--<td><a href="javascript:AddProductE();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>-->
            </tr>
            <tr>
                <td colspan="4" style="height: 21px"><input style="width:99%;" type="text" id="text" readonly="readonly" value="Total" /></td>
                <!--<td ><input type="text" id="total_quantity" value="0" style="width:98%; text-align:right;" readonly="readonly"  onclick="return total_quantity_onclick()" /></td>-->
                <td ><input type="text" id="total_quantity" value="0" style="width:98%; text-align:right;" readonly="readonly" /></td>
                <td ><input type="text" id="txt_overall_total" value="0" style="width:98%; text-align:right;" readonly="readonly"  /></td>
            </tr>
        </table>
    </div>
    <div style="margin:5px; padding:9px; font-size:12px;">
        <table cellpadding="1" id="Table1" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td style="width: 100px;"> Encoded by </td>
                <td>
                    <input type="text" id="txt_ED_encodedby" readonly="readonly" />
                </td>
            </tr>
        </table>
    </div>
        
    <% if ( Ousr.HasPositionOf("csm") != -1 && docStatusId["status"].ToString() == "1" ) { %>
    <div  style="padding:15px; font-size:12px; height: 40px;">
        <table cellpadding="2" cellspacing="0" border="0" align="left" 
            style="height: 44px">
            <tr>
                <td style="width:185px"></td>
                <td><input type="button" value="Post Transaction" style="color:#000000;" onclick="javascript:asApproved('<%: Request.QueryString["eMATno"].ToString() %>');"  /></td>
                <td><input type="button" value="Disapprove" style="color:#000000;" onclick="javascript:asDispproved('<%: Request.QueryString["eMATno"].ToString() %>');" /></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
    </div>
    <% } %> 
    </div>
</asp:Content>
