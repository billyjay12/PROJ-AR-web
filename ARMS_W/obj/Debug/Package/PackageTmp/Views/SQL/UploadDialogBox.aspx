<% Response.CacheControl = "no-cache"; %><% Response.AddHeader("Pragma", "no-cache"); %><% Response.Expires = -1; %><%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UploadDialogBox</title>
</head>
<body>
    <link href="<%=ResolveUrl("~/") %>Content/UploadDialogBox.css" rel="stylesheet" type="text/css" />
    <div>
    <form method="post" action="<%=ResolveUrl("~/") %>SQL/TestUpload" enctype="multipart/form-data">
		<table>
			<tr>
				<td>
					<input type="file" id="upl_file" name="upl_file" />
				</td>
			</tr>
			<tr>
				<td>
					<input type="submit" value="Upload" />
				</td>
			</tr>
		</table>
	</form>
    </div>
</body>
</html>
