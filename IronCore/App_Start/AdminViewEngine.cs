using System.Linq;
using System.Web.Mvc;

public class AdminViewEngine : RazorViewEngine
{
    public AdminViewEngine()
    {
        var adminLocations = new[]
        {
            "~/Views/Admin/{1}/{0}.cshtml",    
            "~/Views/Admin/Shared/{0}.cshtml"
        };

        ViewLocationFormats = ViewLocationFormats.Concat(adminLocations).ToArray();
        PartialViewLocationFormats = PartialViewLocationFormats.Concat(adminLocations).ToArray();
        MasterLocationFormats = MasterLocationFormats.Concat(adminLocations).ToArray();
    }
}
