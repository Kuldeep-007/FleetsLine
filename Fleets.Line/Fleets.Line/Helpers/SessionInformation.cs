using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Helpers
{
    public static class SessionInformation
    {
        /// <summary>
        /// Create Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <returns></returns>
        public static HttpCookie CreateCookie(string cookieName, string cookieValue)
        {
            HttpCookie StudentCookies = new HttpCookie(cookieName);
            StudentCookies.Value = cookieValue;
            return StudentCookies;
        }

        /// <summary>
        /// Delete Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void DeleteCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                var c = new HttpCookie(cookieName);
                c.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }
    }
}