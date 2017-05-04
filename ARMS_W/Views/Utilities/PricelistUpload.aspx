<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        String DateTimeStamp = ViewData["DateTimeStamp"].ToString();
        bool isUploader = (bool)ViewData["isUploader"]; 
    %>

    <div class="bl_box">

    <style type="text/css">
        .btn_delete,
        .btn_downloadfile
        {
            opacity:0.4;
            filter:alpha(opacity=40); /* For IE8 and earlier */
        }
        .btn_delete:hover,
        .btn_downloadfile:hover
        {
            opacity:1.0;
            filter:alpha(opacity=100); /* For IE8 and earlier */
        }
        #tbl_downloader th
        {
            background-color:#A0A0A0
        }
        
        tr[clone=true]:hover,
        tr[clone=true]:hover .btn_delete,
        tr[clone=true]:hover a
        {
            background:#48647e;
            color:#ffffff;
            opacity:1.0;
            filter:alpha(opacity=100); /* For IE8 and earlier */
        }
        .odd { background:#ededed; border:0px; }
        .even { background:#ffffff; border:0px; }
        
    </style>

    <script type="text/javascript">
        var dateuploaded = "<%: DateTimeStamp %>";
        var isUploader = "<%: isUploader %>";
    </script>

        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                        <b>Pricelist Upload/Download</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
            </table>
        </div>

        <div class="content" style="margin:5px">
            <div id="div_uploader" style="display:none">
                <form id="formUpload" action="<%=ResolveUrl("~/") %>Utilities/UploadPricelist" method="post" enctype="multipart/form-data">
                    <table id="tbl_uploadfiles">
                        <tr>
                            <td>File 1: </td>
                            <td><input type="file" name="file" id="txtUpload" /></td>
                            <td>Description: </td>
                            <td><input type="text" name="file" /></td>
                        </tr>
                        <tr>
                            <td><input type="button" id="btn_addfile" value="Add File"/></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right"> 
                                <hr />
                                <input type="button" id="btn_upload" value="Upload" style="font-weight:bold"/>
                            </td>
                        </tr>
                    </table>
                            
                           
                </form>
            </div>
            <hr />
            <div id="div_downloader" style="display:none">
                <table id="tbl_downloader">
                    <tr>
                        <th colspan="6" style="font-size:16px; background-color:#000000; color:#ffffff">List of uploaded files</th>
                    </tr>
                    <tr>
                        <th style="color:#ffffff"></th>
                        <th style="color:#ffffff">File Name</th>
                        <th style="color:#ffffff">File Description</th>
                        <th style="color:#ffffff">Uploaded By</th>
                        <th style="color:#ffffff">Date Uploaded</th>
                        <th style="color:#ffffff"></th>
                    </tr>
                    <tr class="last_row"></tr>
                </table>
            </div>
            <div id="div_message" style="display:none;"><h2>No Uploaded Pricelist.</h2></div>
        </div>

    </div> 
  
    <script src="<%=ResolveUrl("~/") %>Scripts/utilities_pricelistupload.js" language="javascript" type="text/javascript"></script>

</asp:Content>
