<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
                h2 { text-indent:30px; }
        </style>
        <script type="text/javascript" language="javascript">
            $(function () {
                $('#accordion2').accordion({
                    active: true,
                    collapsible: true,
                    autoHeight: false,
                    icons: false
                });
                $('#accordion3').accordion({
                    active: false, 
                    collapsible: true,
                    autoHeight: false,
                    icons: false
                });
                $('#accordion4').accordion({
                    active: true, 
                    collapsible: true,
                    autoHeight: false,
                    icons: false
                });
                $('#accordion5').accordion({
                    active: false, 
                    collapsible: true,
                    autoHeight: false,
                    icons: false
                });
                $('#accordion6').accordion({
                    active: false, 
                    collapsible: true,
                    autoHeight: false,
                    icons: false
                });

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
        <h1>Frequently Asked Questions (volume 2)</h1>

        <div>
            <h2>What will I do if my Geo Locator in Call Report does not tag the right location?</h2>
            <div >
                <p>
                 There are a number of settings you need to check:
                </p>
                    <h2>1.) Wi-fi Settings</h2>
                    <div  id="accordion2" style="margin:0px 50px 0px 50px; width:80%;" >
                        <h2>Step 1 - Go to Settings</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting1.jpg"  alt="Go to settings"/>
                        </div>
                        <h2>Step 2 - Tap Control Panel</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting2.jpg"  alt="Tap Control Panel"/>
                        </div>
                        <h2>Step 3 - Go to Network and Sharing Connections</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting3.jpg"  alt="Network and Sharing Connections"/>
                        </div>
                        <h2>Step 4 - Tap Change adapter settings</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting4.jpg"  alt="change adapter settings"/>
                        </div>
                        <h2>Step 5 - Long-press "Wi-Fi"</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting5.jpg"  alt="Wi-fi settings"/>
                        </div>
                        <h2>Step 6 - Tap "Enable" in the drop box</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting6.jpg"  alt="Wi-fi Enable"/>
                        </div>
                        <h2>Step 7 - Wait until you see a message box.</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/wifisetting7.jpg"  alt=""/>
                        </div>
                    </div>
                    <h2>2.) Elite Pad 900's PC "Privacy" Settings</h2>
                    <div  id="accordion3"  style="margin:0px 50px 0px 50px; width:80%;" >
                        <h2>Step 1 - Go to Settings. </h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/privacysetting1.jpg"  alt="Go to Settings"/>
                        </div>
                        <h2>Step 2 - Tap Change PC Setting.</h2>
                        <div>
                            <img src="<%=ResolveUrl("~/") %>Images/privacysetting2.jpg"  alt=""/>
                        </div>
                        <h2>Step 3 - Go to <span class="hightlight">Privacy</span> and ensure that the following are turned <span class="hightlight">ON</span> </h2>
                        <div>
                             <img src="<%=ResolveUrl("~/") %>Images/privacysetting3.jpg"  alt=""/></span> 
                        </div>
                    </div>
                    <h2>3.) Browser's Settings</h2>
                    <div  id="accordion4"  style="margin:0px 0px 0px 50px; width:80%;" >
                        <h2> • Mozilla Firefox </h2>
                        <div  id="accordion5" style="width:auto;">
                            <h2>Step 1 - Hit Firefox</h2>
                            <div>
                                 <img src="<%=ResolveUrl("~/") %>Images/browsersetting1.jpg"  alt=""/>
                            </div>
                            <h2>Step 2 - Go to Options > Option. </h2>
                            <div>
                                 <img src="<%=ResolveUrl("~/") %>Images/browsersetting2.jpg"  alt=""/>
                            </div>
                            <h2>Step 3 - Go to the <span class="hightlight">Edit Settings</span> in the drop down menu.</h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting3.jpg"  alt=""/>
                            </div>
                            <h2>Step 4 - Tap the Privacy Icon.</h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting4.jpg"  alt=""/>
                            </div>
                            <h2>Step 5 - Tick <span class="hightlight">"Tell sites that I want to be tracked."</span></h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting5.jpg"  alt=""/>
                            </div>
                            <h2>Step 6 - Click <span class="hightlight">OK</span>.</h2>
                        </div>
                        <h2> • Google Chrome</h2>
                        <div  id="accordion6" style="width:auto;">
                            <h2>Step 1 - In the browser, go to <span class="hightlight">"Customize and Control</span> icon.</h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting6.jpg"  alt=""/>
                            </div>
                            <h2>Step 2 - Tap Settings</h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting7.jpg"  alt=""/>
                            </div>
                            <h2>Step 3 - Tap <span class="hightlight">"Show Advanced settings..."</span></h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting8.jpg"  alt=""/>
                            </div>
                            <h2>Step 4 - Tap <span class="hightlight">"Content Settings"</span></h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting9.jpg"  alt=""/>
                            </div>
                            <h2>Step 5 - Tick <span class="hightlight">"Allow all sites to track my physical location"</span></h2>
                            <div>
                                <img src="<%=ResolveUrl("~/") %>Images/browsersetting10.jpg"  alt=""/>
                            </div>
                            <h2>Step 6 - Tap <span class="hightlight">DONE</span>.</h2>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.accordion.js" type="text/javascript"></script>

</asp:Content>
