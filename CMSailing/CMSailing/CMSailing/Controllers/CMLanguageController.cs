/*CMLanguageController.cs
 * sets and displays the current language
 * 
 * Revision History:
 *  Chris Mosey: 11.11.2012: created
 *  Chris Mosey: 12.11.2012: finished
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSailing.Controllers
{
    public class CMLanguageController : Controller
    {
        public ActionResult ChangeLanguage()
        {
            SetLanguages(Request.Cookies);
            SelectListItem en = new SelectListItem() { Text = "English", Value = "en"};;
            SelectListItem fr = new SelectListItem() { Text = "Français", Value = "fr"};

            string currentLanguage = "en";
            if (Request.Cookies["language"] != null)
            {
                currentLanguage = Request.Cookies["language"].Value;
            }

            SelectList languages = new SelectList(new SelectListItem[] { en, fr }, "Value", "Text", currentLanguage);
            ViewBag.lang = languages;
            Response.Cookies.Add(new HttpCookie("return", Request.UrlReferrer.PathAndQuery));
            return View();
        }

        [HttpPost]
        public void ChangeLanguage(string language)
        {
            SetLanguages(Request.Cookies);
            Response.Cookies.Add(new HttpCookie("language", language));
            if (Request.Cookies["return"] != null)
            {
                Response.Redirect(Request.Cookies["return"].Value);
            }
            else
            {
                Response.Redirect("/");
            }
        }
        public static void SetLanguages(HttpCookieCollection languageCookieCollection)
        {
            if (languageCookieCollection["language"] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageCookieCollection["language"].Value);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(languageCookieCollection["language"].Value);

            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            }
        }
    }
}