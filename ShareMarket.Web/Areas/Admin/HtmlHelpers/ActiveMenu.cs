using System.Web.Mvc;

namespace ShareMarket.Web.Areas.Admin.HtmlHelpers
{
    public static class ActiveMenu
    {
        /// <summary>
        /// Get css class name in routing 
        /// </summary>
        /// <param name="helper">Current html in view</param>
        /// <param name="action">Pass action Name </param>
        /// <param name="controller">Pass Controller Name</param>
        /// <returns>css actice class name </returns>
        public static string ActivePage(this HtmlHelper helper, string action, string controller)
        {
            string currentClass = string.Empty;
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString(); // Get Controller
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString(); // Get Action 

            // Matches Controller and Action
            if (currentController == controller )
            {
                currentClass = "active";
            }
            return currentClass;

        }
    }
}