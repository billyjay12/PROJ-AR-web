<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%=ResolveUrl("~/") %>Scripts/uploadcoveragepreview_details.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
    <% string counterid = ViewData["counter_id"].ToString();
       string Eventday = ViewData["event_day"].ToString();
       string Eventmonth = ViewData["event_month"].ToString();
       string Eventyear = ViewData["event_year"].ToString();
       string soId = ViewData["soId"].ToString();
       string Eventdate = ViewData["Eventdate"].ToString();
    %>

    <style type="text/css">
        .lnk_objcode { width:120px; height:30px; }
        .lnk_objcode a
        {
           display:block;
           text-align:center;
           width:100%;
           height:100%;
           text-decoration: none;
           color:Black;
           white-space:nowrap;
        }
        .highlight
        {
            background-color:white;
        }
        .tbl_header
        {
            background-color:#ededed;
            font-weight:bold;
            white-space:nowrap;
        }
    </style>

    <div class="title_bar">
        <b> Upload Coverage Preview </b>
    </div>

    <script  language="javascript" type="text/javascript">
        var counter_id = "<%: counterid %>";
        var event_day = "<%: Eventday %>";
        var event_month = "<%: Eventmonth %>";
        var event_year = "<%: Eventyear %>";
        var soId = "<%: soId %>";
        var Eventdate = "<%: Eventdate %>";
    </script>
   
    <div class="div_main" style=" position:absolute; width:98%;">
            <div style="width:100%">
                <div style="float:left">
                Date: <span id="spn_date">8/16/2013</span>
                </div>
                <div style="float:right">
                    <input type="button" value="Save" id="btn_save" style="width:70px; height:25px;" />
                    <input type="button" value="Cancel upload" id="btn_cancel" style="height:25px;" />
                </div>
                <br />
                <table id="tbl_objective_menu" border="1" cellpadding="0" cellspacing="0" style="width:100%; background:#ededed">
                    <tr>
                        <td colspan="5" style="white-space:nowrap;" id="obj_desc"><b>Objectives:</b></td>
                    </tr>
                    <tr>
                        <td class="lnk_objcode">
                            <a href="#">Collection</a>
                            </td>   
                        <td class="lnk_objcode">
                            <a href="#">Merchandise</a>
                        </td>
                        <td class="lnk_objcode">
                            <a href="#">Customer Service</a>
                        </td>
                        <td class="lnk_objcode">
                            <a href="#">Sales</a>
                        </td>
                        <td class="lnk_objcode">
                            <a href="#">Inventory</a>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="div_objectivedetails" style=" overflow-y:scroll;">
                <div  id="div_collection">
                    <b>Collection</b>
                    <table id="tbl_collection" style="font-size:12px; color:black;" border="0" cellpadding="2" cellspacing="1">
                        <tr class="tbl_header">
                            <td>Account Code</td>
                            <td>Account Name</td>
                            <td>Account Address</td>
                            <td>Contact Person</td>
                            <td>Contact Person No.</td>
                            <td>Hotel Name</td>
                            <td>Hotel Num</td>
                            <td>View Collection</td>
                        </tr>
                    </table>
                </div>
                <div id="div_sales">
                    <b>Sales</b>
                    <table id="tbl_sales" style="font-size:12px; color:black;" border="0" cellpadding="2" cellspacing="1">
                        <tr class="tbl_header">
                            <td>Account Code</td>
                            <td>Account Name</td>
                            <td>Account Address</td>
                            <td>Contact Person</td>
                            <td>Contact Person No.</td>
                            <td>Hotel Name</td>
                            <td>Hotel Num</td>
                            <td>View Details</td>
                        </tr>
                    </table>
                </div>
                <div id="div_merchandise">
                    <b>Merchandise</b>
                    <table id="tbl_merchandise" style="font-size:12px; color:black;" border="0" cellpadding="2" cellspacing="1">
                        <tr class="tbl_header">
                            <td>Account Code</td>
                            <td>Account Name</td>
                            <td>Account Address</td>
                            <td>Contact Person</td>
                            <td>Contact Person No.</td>
                            <td>Hotel Name</td>
                            <td>Hotel Num</td>
                            <td>Store Checking</td>
                            <td>View Details</td>
                        </tr>
                    </table>
                </div>
                <div id="div_customerservice">
                    <b>Customer Service</b>
                    <table id="tbl_customerservice" style="font-size:12px; color:black;" border="0" cellpadding="2" cellspacing="1">
                        <tr class="tbl_header">
                            <td>Account Code</td>
                            <td>Account Name</td>
                            <td>Account Address</td>
                            <td>Contact Person</td>
                            <td>Contact Person No.</td>
                            <td>Hotel Name</td>
                            <td>Hotel Num</td>
                            <td>Issues & Concern</td>
                        </tr>
                    </table>
                </div>
            </div>
    </div>

</asp:Content>
