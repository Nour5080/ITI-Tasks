using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace MyFirstProject.Filters
{
    public class AuthorizeStudentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var path = httpContext.Request.Path.Value?.ToLower();

            // تجاهل صفحات تسجيل الدخول و Register و AccessDenied لتجنب Redirect Loop
            if (path.Contains("/students/login") ||
                path.Contains("/students/create") ||
                path.Contains("/students/accessdenied"))
            {
                base.OnActionExecuting(context);
                return;
            }

            // تحقق من Identity - يسمح لأي مستخدم مسجل دخول (Admin أو HR أو Student)
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Students", null);
                return;
            }

            // تحقق من Session - فقط للطلاب
            var studentId = httpContext.Session.GetInt32("StudentId");

            // لو Session غير موجود → المستخدم ممكن Admin أو HR، نسمح له بالدخول على صفحات الإدارة
            // فقط الصفحات الخاصة بالطلاب تحتاج Session
            if (studentId == null && path.StartsWith("/students"))
            {
                context.Result = new RedirectToActionResult("Login", "Students", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
