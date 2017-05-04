<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%=ResolveUrl("~/") %>Content/ReportList.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlistindex.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-buttons.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-thumbs.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.fancybox.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
 <div class="bl_box">

    <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>PRICELIST</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
             </tr>
          </table>
   </div>

   <style type="text/css">
        #tbl_files th
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

   <%--<div class="page_header_y">
       
      </div>--%>

      <script type="text/javascript">
          $(function () {
              getFilesUploaded();
          });

          function getFilesUploaded() {

              $.ajax({
                  dataType: 'json', contentType: 'application/json; charset=utf-8',
                  type: "POST", url: baseUrl + "Utilities/getListOfFiles",
                  success: function (res) {
                      var counter = 0;
                      $(res.data.file).each(function (i, e) {
                          $("#tbl_files").find(".last_row").before('<tr clone="true" class="' + (counter % 2 == 0 ? "even" : "odd") + '">' +
                                                '<td align="center">' + counter + '</td>' +
                                                '<td style="width:250px;">' + e.FileDescription + '</td>' +
                                                '<td>' + e.FileAttachment + '</td>' +
                                                '<td>' + FormatDate(e.DateTimeStamp) + '</td>' +
                                                '<td><a href="javascript:showPDF(' + e.id + ');" style="text-decoration:none">View</a></td>' +
                                                '<td><a href="' + baseUrl + 'Utilities/DownloadFile?id=' + e.id + '" style="text-decoration:none">Download</a></td>' +
                                            '</tr>');
                          counter = counter + 1;
                      });

                  },
                  error: function (xhr, ajaxOption, thrownError) {
                      alert(xhr.status); alert(thrownError);
                  }
              });


          }

          function showPDF(id) {
              $.fancybox({
                  openEffect: 'elastic',
                  closeEffect: 'elastic',
                  type: 'iframe',
                  href: baseUrl + 'Utilities/PreviewFile?id=' + id,
                  'overlayShow': true,
                  'showCloseButton': true,
                  scrolling: 'no',
                  autoSize: false,
                  padding: 0,
                  helpers: {
                      overlay: {
                          css: { 'overflow': 'hidden' }
                      },
                      overlay: { closeClick: true }
                  }
              });
          }
      </script>

      <div>
            <table id="tbl_files">
                <tr>
                    <th colspan="6" style="font-size:16px; color:#ffffff">List of Pricelist</th>
                </tr>
                <tr style="background-color:#000000; ">
                    <th style="color:#ffffff">Line No.</th>
                    <th style="color:#ffffff">File Description</th>
                    <th style="color:#ffffff">File Name</th>
                    <th style="color:#ffffff">Date Uploaded</th>
                    <th style="color:#ffffff"></th>
                    <th style="color:#ffffff"></th>
                </tr>
                <tr class="last_row"></tr>
            </table>
      </div>

<%--<div class="simple_box">    
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/GudWoodPriceList.aspx');">Gudwood Pricelist</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/MatWoodPriceList.aspx');">Matwood Pricelist</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/PCWPriceList.aspx');">PCW Pricelist (GT, IS, Wood Center)</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/PCWPriceListMT.aspx');">PCW Pricelist (MT)</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/PCWPriceListCabDraw.aspx');">PCW Pricelist (Cabinets and Drawers)</a>
    </div>
    <div class="report_menu">
        <a href="javascript:LoadReport('', 'CS_REPORT/WeatherWoodPriceList.aspx');">Weatherwood Pricelist</a>
    </div>
    
</div>--%>
</div>

</asp:Content>
