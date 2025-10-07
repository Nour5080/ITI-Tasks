using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyFirstProject.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath = "logs.txt"; // ممكن تغيّره لو حابب مكان تاني

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // جلب اسم الطالب من Session، أو "Anonymous" لو مش مسجل
            var userName = context.Session.GetString("StudentName") ?? "Anonymous";

            // مسار الطلب
            var path = context.Request.Path;

            // توقيت الطلب
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // صياغة السطر اللي هيتسجل
            var logLine = $"{time} | User: {userName} | Path: {path}";

            // كتابة السطر في الملف
            await File.AppendAllTextAsync(_logFilePath, logLine + Environment.NewLine);

            // تمرير الطلب لبقية الميدلوير/المشروع
            await _next(context);
        }
    }
}
