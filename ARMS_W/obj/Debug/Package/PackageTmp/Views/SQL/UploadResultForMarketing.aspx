<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UploadResultForMarketing</title>
</head>
<body>
    <div>

    <% 
        if (ViewData["listofAccounts"] != "")
        {
            Response.Write(ViewData["listofAccounts"]);
				//Response.Write("<br/ >");
				//Response.Write("was uploaded.");
				//Response.Write("<!-- " + ViewData["fname"] + " -->");
            Response.Write("<input type=\"hidden\" id=\"data\" value=\"" + ViewData["listofAccounts"] + "\" />");
			}
			else{
				Response.Write("Error:" + ViewData["ferror"]);
			}	
		%>



    
    </div>
</body>
</html>
