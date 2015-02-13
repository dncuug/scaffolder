![logo](http://habr.habrastorage.org/post_images/cd1/e11/044/cd1e110448292251b0a55262ab63c46f.png)

##X.Scaffolding

Library for extend ASP.NET Scaffolding functionality with next packages:
* Twitter Bootstrap
* CKEditor
* Bootstrap Datepicker
* Azure Storage

Also including HtmlHelper extended methods  for
* FileUpload
* Datepicker
* HTML WYSIWYG Editor
 

X.Scaffolding is a part of X-Framework library.

To install X.Scaffolding, run the following command in the Package Manager Console 

PM> Install-Package xscaffolding


###Important update!

With las relaese was added new editor extension functions, that render with glyphicons and native html input type:

* BootstrapDatePickerFor  
* BootstrapDropDownList   
* BootstrapFileUploadFor  
* BootstrapNumberEditorFor
* BootstrapPhoneEditorFor 
* BootstrapSearchEditorFor
* BootstrapTextAreaFor    
* BootstrapTextBoxFor     
* BootstrapThumbnailFor   
* BootstrapUrlEditorFor   

WARNING!
Some extension function was renamed. 

Version available at https://www.nuget.org/packages/xscaffolding.core/1.4.0.1

###Install
In NuGet.org I divided the project into two parts: X.Scaffolding and X.Scaffolding.Core:<br />
* <b>X.Scaffolding.Core</b> contains only core functionality of library<br />
* <b>X.Scaffolding contains</b> dependency to X.Scaffolding.Core and styles for web-application, dependecy for Entity Framework 6.0, dependency for Windows Azure Storage


####NuGet
You can install X.Scaffolding package from NuGet at https://www.nuget.org/packages/xscaffolding/
 


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/ernado-x/x.scaffolding/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

