<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/MarketingProg.js" type="text/javascript"></script>
   
    


  <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
			$("#tabs").tabs();
			//$("#txt_prepby").attr('value', '<%: Session["username"] %>');
			$("#txt_date").datepicker();
            $("#txt_start").datepicker();
            $("#txt_end").datepicker();
            $("#txt_prepby").attr('value', '<%: Session["username"] %>');
//            $("txt_Amount").Attributes.Add("onblur", "AddComma()");
            
            // number formatting

		});

	</script>


    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>Marketing Program</b>
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
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingProgramDetails">>>List of Marketing Program</a>
                </td>
            </tr>
        </table>
    </div>
   

    



      

        <div class="simple_box" style="padding:15px; font-size:12px;">
        

        <table cellpadding="1" id="tbl_mktgProgs" cellspacing="2" border="0" style="color:#000000;">
       
       
            <tr>
                <td style="width: 150px;"> Program Name </td>
                <td><input type="text" id="txt_progName" size="27" /></td>
            </tr>

        
            <tr>
                <td style="width: 150px;"> Project Type </td>
                <td>
                    <select id="txt_projtType" style="width: 158px">
                        <option>--Select Project Type--</option>
                        <option>Above the Line</option>
                        <option>Below the Line</option>
                    </select>
                </td>
            </tr>

            <tr>
                <td style="width: 150px;"> Brand </td>
                <td><input type="text" id="txt_brand" size="27" readonly="readonly" onclick="javascript:LookUpData('txt_brand','ListofBrand');" /></td>
            </tr>

            <tr>
                <td style="width: 150px;"> Target Channel </td>
                <td><input type="text" id="txt_targetChan" size="27" readonly="readonly" onclick="javascript:LookUpData('txt_targetChan','TargetChannel');" /></td>
            </tr>

      

            <tr>
                <td style="width: 150px;"> Background </td>
                <td><textarea cols="40" id="txt_backgrnd" style="height: 60px"></textarea></td>
            </tr>

            <tr>
                <td style="width: 150px;"> Objective </td>
                <td><textarea cols="40" id="txt_objective" style="height: 60px"></textarea></td>
            </tr>

            <tr>
                <td style="width: 150px;"> Measures </td>
                <td><textarea cols="40" id="txt_measures" style="height: 60px"></textarea></td>
            </tr>

        </table>
       




        <div  style="padding:15px; font-size:12px;">
        <table cellpadding="1" id="Tbl_targetAccount" cellspacing="2" border="0" style="color:#000000;">
            <tr>
                <td style="width: 183px"><b>Target Account</b></td>
                <td colspan="4">&nbsp;</td>
            </tr>

            <tr>
                <td style="width: 183px">Upload your Excel File <a id="a_excel" href="javascript:CreateUploadingBoxs('a_excel');"><b>Here</b></a> </td>
                <!-- <td style="width: 386px"><input type="text" id="file_txt" size="40" />-->
                <td>
                    <a href="<%=ResolveUrl("~/") %>Downloads/MarketingProgram_Template.xlsx" target="_blank"><b>Download Template</b></a>
                </td>
                <td colspan="3"></td>
            </tr>

            <tr>
                <td style="width:183px; ">Account Code</td>
                <td style="width:386px; ">Account Name</td>
                <td style="width:150px; ">Area</td>
                <td style="width:150px; ">Amount Allocation</td>
                <td></td>
            </tr>

            <tr>
                <td colspan="3"><input type="text" id="text4" readonly="readonly" style="width:100%" value="Total" /></td>
                <td> <input type="text" id="Total" readonly="readonly" size="40" value="0.00"/></td>
                <td><a href="javascript:Undo();"><img src="<%=ResolveUrl("~/") %>Images/undo.png" style="border:0;" /></a></td>

            </tr>
        </table>
   


        <div  style="padding:15px; font-size:12px;">
            <table cellpadding="1" id="Tbl_resources" cellspacing="2" border="0" style="color:#000000;">

            <tr>
                <td colspan="4"><b>Resources</b></td>
            </tr>

            <tr>
                <td style="width:100px; ">Item</td>
                <td style="width:200px; ">Description</td>
                <td style="width:150px; ">Amount</td>
                <td></td>
            </tr>

            <tr>
                <td style="width:100px; "><input type="text" id="txt_item" size="40"/></td>
                <td style="width:100px; "><input type="text" id="txt_desc" style="width:97%"/></td>
                <td style="width:150px; "><input type="text" onblur="AddComma()" id="txt_Amount" size="40"/></td>
                <td><a href="javascript:AddResources();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
            </tr>

            <tr>
                <td colspan="2"><input type="text" id="text" readonly="readonly" style="width:100%" value="Total" /></td>
                <td> <input type="text" id="txt_totalAmt" readonly="readonly" size="40" value="0.00"/></td>
                <td></td>
            </tr>
       

            </table>
        </div>


        <div style="padding:15px; font-size:12px;">
            <table cellpadding="1" id="tbl_timeline" cellspacing="2" border="0" style="color:#000000;">
       
                <tr>
                    <td colspan="6"><b>Timeline</b></td>
                </tr>
                <tr>
                    <td style="width:150px; ">Start Date</td>
                    <td style="width:150px; ">End Date</td>
                    <td style="width:150px; ">Task</td>
                    <td style="width:150px; ">Responsible Person</td>
                    <td style="width:150px; ">Remarks</td>
                    <td></td>
                </tr>

                <tr>
                    <td style="width:150px; height: 26px;"><input type="text" id="txt_start" size="40"/></td>
                    <td style="width:150px; height: 26px;"><input type="text" id="txt_end" size="40"/></td>
                    <td style="width:150px; height: 26px;"><input type="text" id="txt_task" size="40"/></td>
                    <td style="width:150px; height: 26px;"><input type="text" id="txt_respPer" size="40"/></td>
                    <td style="width:150px; height: 26px;"><input type="text" id="txt-updates" size="40"/></td>
                    <td style="height: 26px"><a href="javascript:AddTimeline();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
                </tr>


            </table>
        </div>


        <div  style="padding:15px; font-size:12px;">
            <table cellpadding="1" id="tbl_attachment" cellspacing="2" border="0" style="color:#000000;">

            <tr>
                <td colspan="3"><b> Attachments</b></td>
            </tr>

            <tr>
                <td style="width:150px;"> File </td>
                <td style="width:150px;"> Brief Description</td>
                <td></td>
            </tr>

            <tr>
                <td style="width:150px; "><input type="text" id="txt_file" size="40" onclick="javascript:CreateUploadingBox('txt_file');" /></td>
                <td style="width:150px; "><input type="text" id="txt_bdiscussion" size="40"/></td>
                <td><a href="javascript:AddAttachment();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
            </tr>
            </table>
        </div>


        <div style="padding:15px; font-size:12px;">
            <table cellpadding="1" id="tbl_PAA" cellspacing="2" border="0" style="color:#000000;">

                <tr> 
                    <td colspan="3"> <b>Project Actual Activities </b> </td>
                </tr>

                <tr>
                    <td style="width:170px;"> Date </td>
                    <td style="width:150px;"> Activity/ Update </td>
                    <td></td>
                </tr>


                <tr>
                    <td style="width:170px;"> <input type="text" id="txt_date" size="28"/> </td>
                    <td style="width:150px;"> <input type="text" id="txt_Act/Upd" size="40"/> </td>
                    <td><a href="javascript:AddProjectActualActivities();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
                </tr>

            </table>
        </div>


        <div style="padding:15px; font-size:12px;">
            <table cellpadding="1" id="Table4" cellspacing="2" border="0" style="color:#000000;">
                <tr> 
                    <td> Prepared By:</td>
                    <td></td>
                    <td style="width:150px;"><input type="text" id="txt_prepby" size="40" readonly="readonly"/></td>
                </tr>
            </table>
        </div>

        </div>


    
		
        <table cellpadding="1" id="Table1" cellspacing="0" border="0" style="color:#000000;">
            <tr>
                <td><input type="button" id="btn_save" value="Save" style="color:#000000; width: 70px;" onclick="javascript:SaveMarktingProg();" /></td>
                <td></td>
                <td><input type="button" value="Cancel" id="btn_cancel" style="color:#000000; width: 70px; " onclick="javascript:CancelMarketingProgram();" /></td>
            </tr>
        </table>


        </div>
    </div>
</asp:Content>
