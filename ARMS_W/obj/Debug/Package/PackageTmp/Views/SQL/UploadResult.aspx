<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UploadResult</title>

	<script type="text/javascript" language="javascript">
		function call_remote(){
			// window.opener.TestFunction();
		}
	</script>

</head>
<body onload="javascript:call_remote();">
    <div>
		<% 
			if ( ViewData["fname"] != "" ) {
				Response.Write(ViewData["fname"] + " - " + ViewData["ftype"]);
				Response.Write("<br/ >");
				Response.Write("was uploaded.");
				Response.Write("<!-- " + ViewData["fname"] + " -->");
				Response.Write("<input type=\"hidden\" id=\"file_name\" value=\"" + ViewData["fname"] + "\" />");
			}
			else{
				Response.Write("Error:" + ViewData["ferror"]);
			}	
		%>
    </div>
</body>
</html>
