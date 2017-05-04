<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <link href="/Css/Themes/ui-darkness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="/Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="/Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="/Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
    var baseUrl = "<%= ResolveUrl("~/") %>";

    $(function () {
			$("#tabs").tabs();
			$("#txt_br_date").datepicker();
           
			
		}
        );

    </script>

    <h2>Business Review</h2>
    <div>
        <table align="right" >
            <tr>
                <td colspan="5" align="right" valign="top">
                    <span id=""></span>
                </td>
            </tr>
         </table>
     </div>

     <div class="simple_box" style="padding:10px; font-size:12px; width: 500px;">
		<table cellpadding="1" id="tbl_br1" cellspacing="0" border="0" style="color:#ffffff;" >
            <tr>
                <td style="width: 170px;"> Business Review Number</td>
                <td>
                    <input type="text" id="txt_brn" readonly="readonly" style="width: 154px" /></td>
            </tr>
            <tr>
                 <td style="width: 170px;"> Business Review Date </td>
                 <td>
		            <input type="text" id="txt_br_date" readonly="readonly" style="width: 154px" />
                 </td>
            </tr> 
          </table>      
        </div>

      <div class="simple_box" style="padding:10px; font-size:12px; width: 500px;">
		<table cellpadding="1" id="tbl_br2" cellspacing="0" border="0" style="color:#ffffff;" >
            <tr>
                <td style="width: 170px;"> Account Code</td>
                <td style="width:137px;"><input type="text" id="txt_acctCode"  size="40" onclick="javascript:LookUpData('txt_acctCode','ListOfDirAcccod');"/></td>
            </tr>
            <tr>
                 <td style="width: 170px;"> Account Name </td>
                 <td>
		            <input type="text" id="txt_acctName" readonly="readonly" style="width: 250px" />
                 </td>
            </tr> 
          </table>      
        </div>

        <div class="simple_box" style="padding:10px; font-size:12px; width: 500px;">
		<table cellpadding="1" id="tbl_br3" cellspacing="0" border="0" style="color:#ffffff;" >
            <tr>
                <td style="width: 170px;"> Account Officer</td>
                <td>
                    <input type="text" id="txt_acctOfficer" readonly="readonly" style="width: 154px" /></td>
            </tr>
            <tr>
                 <td style="width: 170px;"> Area Sales Manager </td>
                 <td>
		            <input type="text" id="txt_salesManager" readonly="readonly" style="width: 154px" />
                 </td>
            </tr> 
             <tr>
                 <td style="width: 170px;"> Channel Manager </td>
                 <td>
		            <input type="text" id="txt_channelManager" readonly="readonly" style="width: 154px" />
                 </td>
            </tr> 
          </table>      
        </div>

        <div class="simple_box">
               <h2>Comments on Existing Agreements</h2>
               <textarea rows="6" id="txt_comExAgr" style="width:98%;"></textarea>
        </div>

         <div class="simple_box">
               <h2>Comments on Current Account Performance</h2>
               <textarea rows="8" id="txt_comAcctPer" style="width:98%;"></textarea>
        </div>

        <div class="simple_box" style="padding:10px; font-size:12px; width: 715px;">
           <h2>Strategic Plans</h2>
                <table cellpadding="1" id="tbl_br4" cellspacing="0" border="0" 
                style="color:#ffffff; width: 572px;" >
                    <tr>
                        <td style="width: 180px"></td>
                                <td style="width: 103px">Original Annual</td>
                                <td style="width: 127px">Revised Annual</td>
                                <td style="width: 173px">Reason</td>
                    </tr>
                    <tr>
                        <td style="width: 180px">Sales Target</td>
                        <td style="width: 103px"><input type="text" id="txt_STOrigAnn" /></td>
                        <td style="width: 127px"><input type="text" id="txt_STRevAnn"/></td>
                        <td style="width: 173px"><input type="text" id="txt_STReason" style="width: 169px" /></td>
                    </tr>
                    
                  </table>
            </div>
             
            <div class="simple_box" style="padding:10px; font-size:12px; width: 715px;">
                <table cellpadding="1" id="tbl_br5" cellspacing="0" border="0" 
                    style="color:#ffffff; width: 572px;" >
                    <tr>
                        <td style="width: 139px">Incentive Planning</td>
                        <td>
                            <textarea rows="2" id="txt_plan" style="width: 426px" ></textarea>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="simple_box" style="padding:10px; font-size:12px; width: 715px;">
                <table cellpadding="1" id="tbl_br6" cellspacing="0" border="0" 
                    style="color:#ffffff; width: 572px;" >
                    <tr>
                        <td style="width: 140px">Support</td>
                        <td>
                            <textarea rows="2" id="txt_support" style="width: 426px" ></textarea>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="simple_box" style="padding:10px; font-size:12px; width: 715px;">
           <h2>Changes in Account Master File</h2>
                <table cellpadding="1" id="tbl_br7" cellspacing="0" border="0" 
                style="color:#ffffff; width: 572px;" >
                    <tr>
                    <td></td>
                      
                                <td style="width: 103px">Existing Value</td>
                                
                                <td style="width: 173px">Revised Value</td>
                    </tr>
                    <tr>
                        
                        <td style="width:137px;"><input type="text" id="txt_field"  size="40" onclick="javascript:LookUpData('txt_field','ListOfitem');"/></td>
                        <td style="width: 127px"><input type="text" id="txt_existing_val" /></td>
                        <td style="width: 173px"><input type="text" id="txt_revised_val" style="width: 169px" /></td>
                        <td><a href="javascript:AddField();"><img src="/Images/add.png" style="border:0;" /></a></td>
                    </tr>
                    
                  </table>
            </div>

            <div class="simple_box" style="padding:10px; font-size:12px; width: 715px;">
                <h2>Proposed Credit Change</h2>
                    <table cellpadding="1" id="tbl_br8" cellspacing="0" border="0" style="color:#ffffff; width: 588px;" >
                        <tr>
                            <td style="width: 96px"></td>
                            <td style="width: 61px">Existing</td>
                            <td style="width: 273px">Recommended</td>
                        </tr>
                        <tr>
                            <td style="width: 96px">Credit Limit</td>
                            <td style="width: 61px"><input type="text" id="txt_ExstcrdLimit" style="width: 190px" /></td>
                            <td style="width: 273px"><input type="text" id="txt_ReccrdLimit" style="width: 190px; margin-left: 0px; height: 21px;" /></td>
                        </tr>
                        <tr>
                            <td style="width: 96px">Credit Term</td>
                            <td style="width: 61px"><input type="text" id="txt_ExstcrdTerm" style="width: 190px" /></td>
                            <td style="width: 273px"><input type="text" id="txt_ReccrdTerm" style="width: 190px" /></td>
                        </tr>
                    </table>
            </div>

            <div>
            </div>
             <div class="simple_box" style="padding:10px; font-size:12px; width: 715px;">
                <h2>For Finance Use Only</h2>
                    <table cellpadding="1" id="Table1" cellspacing="0" border="0" 
                     style="color:#ffffff; width: 667px;" >
                        <tr>
                            <td style="width: 1244px">Effective Length of Payment</td>
                            <td style="width: 573px"><input type="text" id="txt_length_of_payment" style="width: 190px" /></td>
                        </tr>
                        <tr>
                             <td style="width: 1244px">Existing Credit Term</td>
                             <td style="width: 573px"><input type="text" id="txt_exst_credit_term" style="width: 190px" /></td>
                        </tr>
                        <tr>
                             <td style="width: 1244px">Remarks</td>
                             <td style="width: 573px"><textarea rows="2" id="txt_remarks" style="width: 426px" ></textarea></td>
                        </tr>
                        <tr>
                             <td style="width: 1244px">Incidents of Dishonored Checks</td>
                             <td style="width: 573px"><input type="text" id="txt_disChecks" style="width: 190px" /></td>
                        </tr>
                        <tr>
                             <td style="width: 1244px">Remarks</td>
                             <td style="width: 573px"><textarea rows="2" id="txt_remarks2" style="width: 426px" ></textarea></td>
                        </tr>
                   </table>
            </div>

            <div>
            </div>

            <div  style="padding:5px; font-size:12px;">
                <table cellpadding="1" id="tbl_br9" cellspacing="0" border="0" style="color:#ffffff;" align="right" >
                    <tr>
                        <td><button type="button" id="btn_set"onclick="javascript:DocSave();">Save</button></td>
                        <td><button type="button" id="btn_cancel" onclick="javascript:cancel();">Cancel</button></td>
                        <td style="width: 150px;"></td>
                        <td style="width: 100px;"></td>
                    </tr>
                 </table>
             </div>
            

            


            


    
            

            


            

</asp:Content>
