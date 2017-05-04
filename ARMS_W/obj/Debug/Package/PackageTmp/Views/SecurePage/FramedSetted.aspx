<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html>
<head>
<% 
    
    string doc_id = "";
    string doc_type = "";
    string doc_month = "";
    string doc_year = "";
    string doc_soId = "";

    string doc_id_name = "";
    string doc_month_name = "";
    string doc_year_name = "";
    string doc_soId_name = "";
    
    string doc_controller = "";
    string doc_view = "";
    
    
    if (Request.QueryString["id"] != null && Request.QueryString["doctype"] != null)
    {
        if (Request.QueryString["id"].ToString() != "" && Request.QueryString["doctype"].ToString() != "")
        {
            doc_id = Request.QueryString["id"].ToString();
            doc_type = Request.QueryString["doctype"].ToString();

            if (doc_type == "cca")
            {
                doc_id_name = "ccanum";
                doc_view = "AccountsDetails";
                doc_controller = "Document";
            }

            if (doc_type == "lcd" || doc_type == "ldi")
            {
                doc_id_name = "requestid";
                doc_view = "LeadAccountsDetails";
                doc_controller = "Document";
            }

            if (doc_type == "ldb")
            {
                doc_id_name = "requestid";
                doc_view = "LeadDbDetails";
                doc_controller = "LeadDb";
            }

            if (doc_type == "mma")
            {
                doc_id_name = "agreeno";
                doc_view = "MMAgreementDetails";
                doc_controller = "MMAgreement";
            }

            if (doc_type == "ematlink")
            {
                doc_id_name = "eMATno";
                doc_view = "eMATDetails";
                doc_controller = "eMAT";
            }

            if (doc_type == "mkr")
            {
                doc_id_name = "reqID";
                doc_view = "marketingReqDetails";
                doc_controller = "MrktngRequest";
            }

            if (doc_type == "brw")
            {
                //doc_id_name = "busrnum"; //commented by billy jay delima -replace to busReviewNo
                doc_id_name = "busReviewNo";
                doc_view = "BusinessReviewDetails";
                doc_controller = "BusinessReview";
            }
            
            if (doc_type == "newIC")
            {
                doc_id_name = "acctCode";
                doc_view = "CreateNewInventoryCount";
                doc_controller = "Inventory";
            }

            if (doc_type == "clndr")
            {
                doc_month = Request.QueryString["Month"].ToString();
                doc_year = Request.QueryString["Year"].ToString();
                doc_soId = Request.QueryString["soId"].ToString();

                doc_month_name = "month";
                doc_year_name = "year";
                doc_soId_name = "soId";
                
                doc_id_name = "EventId";
                doc_view = "CalendarView";
                doc_controller = "Calendar";
              
            
            }

            if (doc_type == "listusers")
            {
                doc_view = "ListOfUser";
                doc_controller = "SystemPage";
            }
            

        }
    }
%>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var baseUrl = "<%=ResolveUrl("~/") %>";
        var btm_frameset = null;
        var top_frameset=null;

        $(function () {
            <% if (doc_controller != "") { %>
            top.frames['content_frameset'].location.href = "<% =
                ResolveUrl("~/") + doc_controller + "/" + doc_view + "?" + 
                    doc_id_name + "=" + doc_id + "&" + 
                    doc_month_name + "=" + doc_month + "&" +
                    doc_year_name + "=" + doc_year + "&" +
                    doc_soId_name + "=" + doc_soId
                %>";
            <% } %>

            btm_frameset = $("#bottom_frameset");
            top_frameset=$("#top_frameset");
        });

        function GoHome() {
            sessionStorage.clear();
            window.location = "<%=ResolveUrl("~/") %>";
        }
        function RefreshPage() {
            location.reload();
        }

    </script>
    <script src="<%=ResolveUrl("~/") %>Scripts/n_reportviewer.js" type="text/javascript"></script>

</head>
    <frameset rows="135px,*" id="top_frameset" frameborder="no" framespacing="0" border="0" >
    <frame src="<%=ResolveUrl("~/") %>SecurePage/TopFrame" scrolling="no" noresize="noresize" />
        <frameset id="bottom_frameset" cols="300px,*">
            <frame name="menu_frameset" src="<%=ResolveUrl("~/") %>SecurePage/MainMenus" noresize="noresize" />
            <frame name="content_frameset" src="<%=ResolveUrl("~/") %>SecurePage/BlankDestinationPage" noresize="noresize" />
        </frameset>
    </frameset>
</html>
