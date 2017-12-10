using System;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;

namespace Prova.MVC.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateCookieAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly ValidateAntiForgeryTokenAttribute _validateAntiForgeryTokenAttribute;
        private const string FieldName = "__RequestVerificationToken";
        public ValidateCookieAntiForgeryTokenAttribute()
        {
            _validateAntiForgeryTokenAttribute = new ValidateAntiForgeryTokenAttribute();
        }

        #region Implementation of IAuthorizationFilter

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.ContentType.ToLower().Contains("application/json"))
            {
                var bytes = new byte[filterContext.HttpContext.Request.InputStream.Length];
                filterContext.HttpContext.Request.InputStream.Read(bytes, 0, bytes.Length);
                filterContext.HttpContext.Request.InputStream.Position = 0;
                var json = Encoding.UTF8.GetString(bytes);
                var jsonObject = JObject.Parse(json);
                var value = (string)jsonObject[FieldName];
                var httpCookie = filterContext.HttpContext.Request.Cookies[AntiForgeryConfig.CookieName];
                if (httpCookie != null)
                {
                    System.Web.Helpers.AntiForgery.Validate(httpCookie.Value, value);
                }
                else
                {
                    throw new HttpAntiForgeryException("Anti forgery token cookie not found");
                }
            }
            else
            {
                _validateAntiForgeryTokenAttribute.OnAuthorization(filterContext);
            }
        }

        #endregion
    }
}