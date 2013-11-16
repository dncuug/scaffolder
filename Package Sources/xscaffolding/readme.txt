X.Scaffolding Project

Project
==================================
https://github.com/ernado-x/X.Scaffolding



Auth
==================================
<authentication mode="Forms">
    <forms defaultUrl="~/" loginUrl="~/system/login">
	</forms>
</authentication>

Also, for disable auto-redirect to /Acccount/Login, add:

<appSettings>
    ...
    <add key="enableSimpleMembership" value="false"/>
    <add key="autoFormsAuthentication" value="false"/>
</appSettings>



Error handlign (Global.asax)
==================================
void Application_Error(object sender, EventArgs e)
{
    var exception = Server.GetLastError();
    
    if (exception is Exception)
    {
        Session["application_error"] = exception;
        Server.ClearError();
        HttpContext.Current.Response.Redirect("/system/exception");
    }
}


Budnling
==================================
Add bundle by /bundles/jqueryval for correct work scafolded pages

bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));