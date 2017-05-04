<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%--<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>
--%>
<%--    <link href="<%=ResolveUrl("~/") %>Content/metroUi/MetroJs.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/metroUi/MetroJs.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/dialog.js" type="text/javascript"></script>--%>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>User Guide</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>
        <style type="text/css">
                .hightlight { color:Red; }
                p { text-indent: 30px; font-style:italic; }
                h2 { text-indent:15px; }
        </style>
        <script type="text/javascript" language="javascript">
            $(function () {
                $('#accordion').accordion({
                    active: false,
                    collapsible: true,
                    autoHeight: false//,
                    //  icons: true
                });
//                $("#aaa").click(function () {
//                    $.Dialog({
//                        'title': 'My dialog title',
//                        'content': $("#dialog_box").html(),// '<iframe id=\"frame_js\" src=\"' + baseUrl + 'Calendar/Memo?date=Wed Oct 30 00:00:00 UTC+0800 2013&day=30&month=10&year=2013&soId=148&acctCode=CVMATIMCO\" target=\"myframe\" frameborder=\"0\" style=\"width:500px; height:350px; background-color:#f7f4c5;\"></iframe>',
//                        'draggable': true,
//                        'overlay': true,
//                        'closeButton': false,
//                        'buttonsAlign': 'right',
//                        'keepOpened': true,
//                        //                        'position': {
//                        //                            'zone': 'right'
//                        //                        },
//                        'buttons': {
//                            'button1': {
//                                'action': function () { }
//                            },
//                            'button2': {
//                                'action': function () { }
//                            }
//                        }
//                    });
//                });

                //                var pGress = setInterval(function () {
                //                    var pVal = $('#progressbar').progressbar('option', 'value');
                //                    var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
                //                    if (pCnt > 100) {
                //                        clearInterval(pGress);
                //                    } else {
                //                        $('#progressbar').progressbar({ value: pCnt });
                //                    }
                //                }, 10);

            });
            
        </script>
        <h1>Frequently Asked Questions (volume 1)</h1>
      
        <div id="accordion"  style="margin:0px 50px 0px 20px; width:90%;">
            <h2>&nbsp;What is VPN?</h2>
            <div>
                <p >
                  Virtual Private Network (VPN) is a facility which extends a “private network” across a public network such as the internet. 
                  This will enable an employee to access in-house based systems like <span class="hightlight">Lotus Notes</span> even outside Matimco Inc. offices.
                </p>
            </div>
            <h2>&nbsp;How will I know if I am connected to the VPN?</h2>
            <div>
                <p>
                  In the Networks setting, you should see <img src="<%=ResolveUrl("~/") %>Images/VPN.jpg"  alt=""/>.  This will signify that you are connected to the MTC VPN. You can now access your <span class="hightlight">Lotus Notes</span>. 
                </p>
                 <p> 
                    <span class="hightlight">*Note: Always disconnect the VPN when updating Call Report</span>.
                </p>
            </div>
           
            <h2>&nbsp;What is 3G?</h2>
            <div>
                <p>
                    3G - Third Generation of mobile telecommunications technology is a mobile broadband. 3G telecommunication networks support services that provide an information transfer rate of at least 200kbit/s.
                </p>
                <p>
                   <img src="<%=ResolveUrl("~/") %>Images/applicationsof3g.jpg"  style="vertical-align:top" alt=""/>
                   <img src="<%=ResolveUrl("~/") %>Images/FAQNetwork.jpg"  alt=""/>   
                </p>
            </div>
            
            <h2>&nbsp;How will I know if I am connected to Globe 3G?</h2>
            <div>
                <p>
                   In the Networks setting, you should see <img src="<%=ResolveUrl("~/") %>Images/Globe3g.jpg"  alt=""/>.  This will signify that you are connected to Globe 3G. You can now browse the internet. 
                    
                </p>
            </div>
            <h2>&nbsp;How can I connect to VPN?</h2>
            <div>
                <ul>
                    <li>Go to the Networks setting and tap “QC VPN (PLDT)</li>
                    <li> Tap “Connect”</li>
                </ul>   
            </div>
            <h2>&nbsp;When do I need to connect in Matimco’s  VPN?</h2>
            <div>
                <p>
                    You will need to connect in Matimco’s VPN first, before you access  your Lotus Notes.   
                </p>
            </div>
        </div>
        <br />
    </div>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.accordion.js" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.progressbar.js" type="text/javascript"></script>--%>
</asp:Content>
