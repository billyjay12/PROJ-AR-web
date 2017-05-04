<% Response.CacheControl = "no-cache"; %><% Response.AddHeader("Pragma", "no-cache"); %><% Response.Expires = -1; %><%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Error</title>
    <style type="text/css">
        .div_error {
            border:1px solid #a84c15; 
            background:#fff5e4;
            padding:5px;
            margin:5px;
            font-family:Arial;
            font-size:12px;
        }
        .div_error a { color:#blue; text-decoration:none; }
    </style>
</head>
<body>
    <div class="div_error">
        <b>An error has occured.</b> 
        <br />
        <br />
        Possible reason are:
        <ul style="border:0; padding:10px; margin:0px; font-style:italic;">
            <li>The session expired.</li>
            <li>The user accesed a page without logging-in.</li>
            <li>An unexpected error occured.</li>
        </ul>
        <br />
        <br />
        Click <a href="<%=ResolveUrl("~/") %>Home/Index">here</a> to log-in.
    </div>
</body>
</html>
