<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BlankPage</title>
    <style type="text/css">
        .style1
        {
            width: 105px;
        }
    </style>
</head>
<body style="background-color:#f7f4c5;">
<script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
<link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar.print.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.min.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/myObjective.js" type="text/javascript"></script>


 <div>
<div id="tabs">

            <ul>
                  <li><a href="#tabs-1"><%--<img align="left" src="<%=ResolveUrl("~/") %>Images/gift_icon.png"/>--%>&nbsp;Sales</a></li>
                  <li><a href="#tabs-2"><%--<img align="left" src="<%=ResolveUrl("~/") %>Images/log_icon.png"/>--%>&nbsp;Collection</a></li>
                  <li><a href="#tabs-3"><%--<img align="left" src="<%=ResolveUrl("~/") %>Images/log_icon.png"/>--%>&nbsp;Merchandising</a></li>
            </ul>

                  <!--start for tab 1 --->
                    <div id="tabs-1">

                    <table>

                        <tr>
                            <td>

                                    <table>

                                        <tr>
                                            <td>Account Name</td>
                                            <td><input type="text" id="txt_accountName" /></td>
                                            <td>Account Class</td>
                                            <td><input type="text" id="txt_accountClass" /></td>
                                        </tr>

                                        <tr>
                                            <td>Account Address</td>
                                            <td><input type="text" id="txt_accountAddress" /></td>
                                            <td>Contact Number</td>
                                            <td><input type="text" id="txt_cccountContactNumber" /></td>
                                        </tr>

                                        <tr>
                                            <td>Contact Person</td>
                                            <td><input type="text" id="Text2" /></td>
                                        </tr>

                                    
                                    </table>
                            
                            </td>

                            
                        
                        </tr>
                        <tr><td><br /></td></tr>
                        <tr>

                            <td>
                                    
                                    <table>
                                        
                                        <tr>
                                            <td>P.O Number &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td><input type="text" id="txt_PONum"/></td>
                                        </tr>
                                        <tr>
                                            <td>Date Sold</td>
                                            <td><input type="text" id="Text1"/></td>
                                        </tr>
                                        <tr>
                                            <td>Brand</td>
                                            <td><input type="text" id="Text3"/></td>
                                        </tr>
                                        <tr>
                                            <td>Amount</td>
                                            <td><input type="text" id="Text4"/></td>
                                        </tr>
                                    
                                    </table>
                            

                            </td>
                        
                        </tr>
                    
                    </table>


                    </div>

                    <!--end for tab 1 --->


</div>

</div>

    
</body>
</html>
