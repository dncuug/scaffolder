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