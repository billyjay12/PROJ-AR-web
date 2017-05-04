<%@ Page Title="EMAT" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/eMAT_con.js" type="text/javascript"></script>
    
<script type="text/javascript" language="javascript">
        
    var baseUrl = "<%= ResolveUrl("~/") %>";

    var cached_data = "";
    var cached_obj = ""
    // GROUP #2
    $(function () {
        
        BindToLookUpLive("txt_item");

        $("#txt_encoded_by").attr('value', '<%: Session["username"] %>');
		$("#txt_delivery_date").datepicker();

        var textBox1 = $('#txt_price').keyup(
            function (e) {
                var quantity = $("#txt_quantity").attr('value');
                if (quantity == "") { quantity = "0"; }

                var cur_price = $("#txt_price").attr('value');
                if (cur_price == "") { cur_price = "0"; }

                var total = parseFloat(quantity) * parseFloat(cur_price);
                
                
                $("#txt_total").attr('value', total );

            }
        );

        //controls the textbox quantity change
        var textBox2 = $('#txt_quantity').keyup(
            function (e) {
                var cur_value = $(this).attr('value');
                if (cur_value == "") { cur_value = "0"; }

                var cur_price = $("#txt_price").attr('value');
                if (cur_price == "") { cur_price = "0"; }

                var total = parseFloat(cur_value) * parseFloat(cur_price);
                var newnumber = Math.round(total*Math.pow(10,2))/Math.pow(10,2);

                $("#txt_total").attr('value', newnumber);

            }
        );
       
        function IsNumeric(input) {
            return (input - 0) == input && input.length > 0;
        }
    });
   
</script>

<div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>New E-MAT</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>


    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>eMAT/ListOfEMATS">>> List of eMAT</a>
                </td>
            </tr>
        </table>
    </div>

    
    <div style="padding:10px; font-size:12px;">
        <table cellpadding="2" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td> Buyer </td>
                <td>
                    <input type="text" id="txt_buyer" readonly="readonly" onclick="javascript:LookUpData('txt_buyer','ListOfINDirAcccod');" />
                </td>
            </tr>
            <tr>
                <td> E-MAT Document No. </td>
                <td>  
                    <input type="text" id="txt_doc_num" />
                    &nbsp;&nbsp;
                    <a href="javascript:CheckEmatDoc();"><img src="<%=ResolveUrl("~/") %>Images/check_icon.png" style="border:0;" /></a>
                </td>   
            </tr>
            <tr>
                <td> Address </td>
                <td>
                    <input type="text" id="txt_address" />
                </td>
            </tr>
            <tr>
                <td>Tel. No.</td>
                <td>
                    <input type="text" id="txt_tel_no" onkeypress="return isNumberKey(event)" />
                </td>
            </tr>
            <tr>
                <td>Contact Person</td>
                <td>
                    <input type="text" id="txt_contact_person" />
                </td>
            </tr>
            <tr>
                <td valign="top">Terms</td>
                <td>
                    <textarea rows="2" id="txt_terms" style="height: 65px; width: 100%;" ></textarea>
                </td>
            </tr>
            <tr>
                <td >Delivery date</td>
                <td >
                    <input type="text" id="txt_delivery_date" />
                </td>
            </tr>
            <tr>
                <td valign="top">Delivery Instruction</td>
                <td>
                    <textarea id="txt_delivery_instruction" style="height: 65px; width: 100%;" ></textarea>
                </td>
            </tr>
            <tr>
                <td>Referred To (Direct Account)</td>
                <td>
                    <input type="text" id="txt_referred_to" onclick="javascript:LookUpData('txt_referred_to','ListOfDirAcccod');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Submitted To (Account Clerk)	</td>
                <td>
                    <input type="text" id="txt_Submitted_to" />
                </td>
            </tr>
            <tr>
                <td>Contact Number</td>
                <td>
                    <input type="text" id="txt_contact_number" onkeypress="return isNumberKey(event)" />
                </td>
            </tr>
            <tr>
                <td>Confirmed Delivery By (Dealer)</td>
                <td>
                    <input type="text" id="txt_confirmed_delivery" />
                </td>
            </tr>
            <tr>
                <td>Sales Representative</td>
                <td>
                    <input type="text" id="txt_trade_sales_rep" />
                </td>
            </tr>
        </table>
    </div>

    
    <div style="padding:15px; font-size:12px;">
        <b>Details</b>
        <table cellpadding="2" width="100%" cellspacing="0" border="0" id="table_details" >
            <tr>
                <td style="width:120px;">Item Code</td>
                <td >Item Description</td>
                <td style="width:70px;">Price</td>
                <td style="width:70px;">Unit of Measure</td>
                <td style="width:70px;">Quantity</td>
                <td style="width:70px;">Total</td>
                <td></td>
            </tr>
            <tr>
                <!--<td ><input type="text" id="txt_item" style="width:98%;" onclick="javascript:LookUpData('txt_item','ListOfitemCode');" readonly="readonly" /></td>-->
                <td ><input type="text" id="txt_item" style="width:96%;" /></td>
                <td ><input type="text" id="txt_product" style="width:98%;" readonly="readonly" /></td>
                <td ><input type="text" id="txt_price" style="width:95%; text-align:left;" onclick="javascript:ShowPLButton('txt_price');" /></td>
                <td ><input type="text" id="txt_unit" style="width:95%; text-align:left;" onclick="javascript:LookUpData('txt_unit','ListOfunit');" readonly="readonly" /></td>
                <td ><input type="text" id="txt_quantity" style="width:95%; text-align:left;" /></td>
                <td ><input type="text" id="txt_total" style="width:98%; text-align:left;" readonly="readonly" /></td>
                <td><a href="javascript:;" onclick="javascript:AddProduct();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
            </tr>
            <tr >
                <td colspan="4" style="background:#f7f7f7;" >
                    <input style=" width:99%;" type="hidden" id="text" readonly="readonly" value="Total" />
                    <b>Total</b>
                </td>
                <td style="background:#ededed;" ><input type="text" id="total_quantity" value="0" style=" width:95%; text-align:left;" readonly="readonly"  /></td>
                <td style="background:#ededed;" ><input type="text" id="txt_overall_total" value="0" style="width:98%; text-align:left;" readonly="readonly"  /></td>
                <td></td>
            </tr>
        </table>
    </div>

    <div style="padding:15px; font-size:12px;">
        <table cellpadding="1" id="tbl_create_emat" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td style="width: 100px;"> Encoded by </td>
                <td>
                    <input type="text" id="txt_encoded_by" readonly="readonly" />
                </td>
            </tr> 
        </table>      
    </div>

    <div style="padding:5px; font-size:12px;">
        <center>
        <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td>
                    <input type="button" id="btn_sendto_csmanager" onclick="javascript:DocSave();" value="Send to CS Manager" />
                    &nbsp;&nbsp;&nbsp;
                    <input type="button" id="btn_cancel" onclick="javascript:cancel();" value="Cancel" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
        </center>
    </div>
</div>


</asp:Content>
