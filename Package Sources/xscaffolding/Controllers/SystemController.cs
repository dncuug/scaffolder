using System;
using System.IO;
using System.Web;
using Core;
using System.Web.Mvc;
using System.Web.Security;
using X.Scaffolding.Core;

namespace Manage.Controllers
{
    public class SystemController : SecureController
    {
        public ActionResult Login()
        {
            return View(new Credentials());
        }        

        [HttpPost]
        public ActionResult Login(Credentials credentials)
        {
            if (credentials.Login == "test" && credentials.Password == "test")
            {
                FormsAuthentication.RedirectFromLoginPage(credentials.Login, true);
                return Redirect("~/");
            }

            credentials.Status = false;
            
            return View(credentials);
        }

        public ActionResult UploadFile()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            var url = String.Empty;

            if (file.ContentLength > 0)
            {
                var memoryStream = new MemoryStream();
                file.InputStream.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();

                var extension = Path.GetExtension(file.FileName);
                var name = Guid.NewGuid() + extension;

                url = FileManager.UploadFile(bytes, name);
            }

            return View((object)url);
        }
    }
}
