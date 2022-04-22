using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using System.Web.Mvc;
=======
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace SysIgreja
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

<<<<<<< HEAD
            if (String.IsNullOrEmpty(cssClass))
=======
            if (String.IsNullOrEmpty(cssClass)) 
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string IsSelectedMenuItem(this HtmlHelper html, IEnumerable<string> controller = null, IEnumerable<string> actions = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            bool someActionIsSelected = false;
            bool controllerIsSelected = false;

            if (controller.Any())
            {
                controllerIsSelected = controller.Contains(currentController);
            }

            if (actions.Any())
            {
                someActionIsSelected = actions.Contains(currentAction);
            }

            return controllerIsSelected && someActionIsSelected ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

<<<<<<< HEAD
    }
=======
	}
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
}
