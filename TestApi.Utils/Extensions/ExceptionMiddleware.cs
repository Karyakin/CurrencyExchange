using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Serilog;
using TestApi.Utils.Models;
using TestApi.Utils.Settings;

namespace TestApi.Utils.Extensions;
/// <summary>
/// Глобальный обработчик ошибок. Все исключения возникшие в приложении пробрасываются в настоящий класс.
/// В методе InvokeAsync определяется тип исключения и обрабатываются они в соответсвии с типом. Если частный тип не определен,
/// обработка происходит по головному(родительскому типу.)
/// Перед обработкой любого типа исключение проверяется на тип AuthenticationException и в случае совпадения, происходит обработка AuthenticationException
/// </summary>
public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
           
            catch (HttpRequestException ex)
            {
                if (ex.InnerException?.GetType().ToString() == "System.Security.Authentication.AuthenticationException")
                {
                    Log.Error("\n\nException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                    await HandleAuthExceptionAsync(httpContext, ex, HttpStatusCode.Unauthorized);
                }
                else
                {
                    Log.Error("\n\nHttpRequestException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                    await HandleHttpRequestExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);    
                }
            }
           
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException?.GetType().ToString() == "System.Security.Authentication.AuthenticationException")
                {
                    Log.Error("\n\nException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                    await HandleAuthExceptionAsync(httpContext, ex, HttpStatusCode.Unauthorized);
                }
                else
                {
                    Log.Error("\n\nHttpRequestException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                    await HandleOperationExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);    
                }
            }
            
            catch (AuthenticationException ex)
            {
                Log.Error("\n\nAuthenticationException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                await HandleAuthExceptionAsync(httpContext, ex, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                if (ex.InnerException?.GetType().ToString() == "System.Security.Authentication.AuthenticationException")
                {
                    Log.Error("\n\nException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                    await HandleAuthExceptionAsync(httpContext, ex, HttpStatusCode.Unauthorized);
                }
                else
                {
                    Log.Error("\n\nException: {DateTime} {@Exception} {ExceptionFromMethod} {InnerException} {StackTrace}", DateTime.Now, ex, ex.Message, ex.InnerException, ex.StackTrace);
                    await HandleExceptionAsync(httpContext, ex);    
                }
            }
        }
        
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var message = exception switch
            {
                AccessViolationException => ApplicationParams.IsDevelopment ? exception.Message : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}",
                InvalidOperationException => ApplicationParams.IsDevelopment ? exception.Message : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}",
                NullReferenceException => ApplicationParams.IsDevelopment ? exception.Message : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}",
                ArgumentException => ApplicationParams.IsDevelopment ? exception.Message : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}",
                AuthenticationException => ApplicationParams.IsDevelopment ? exception.Message : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}",
                _ =>  ApplicationParams.IsDevelopment ? exception.Message : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}"
            };

            await context.Response.WriteAsync(new ExceptionModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
        private async Task HandleHttpRequestExceptionAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            string messageForUser;
            try
            {
                messageForUser = ex.Message.Split('\'')[1];
            }
            catch
            {
                messageForUser = ex.Message;
            }

            ExceptionModel httpRequestExceptionModel = new()
            {
                Message = ApplicationParams.IsDevelopment ? messageForUser : $"Возникли проблемы. Попробуйте повторить операцию еще раз и, если ситуация не изменится, обратитесь в службу поддержки. Хорошего дня! {DateTime.Now:u}",
                StatusCode = (int)httpStatusCode
            };

            await response.WriteAsync(httpRequestExceptionModel.ToString());
        }
        private async Task HandleAuthExceptionAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            string messageForUser;
            try
            {
                messageForUser = ex.Message.Split('\'')[1];
            }
            catch
            {
                messageForUser = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message;
            }

            ExceptionModel httpRequestExceptionModel = new()
            {
                Message = ApplicationParams.IsDevelopment ? messageForUser : $"Пройдите процедуру авторизации. {DateTime.Now:u}",
                StatusCode = 401
            };

            await response.WriteAsync(httpRequestExceptionModel.ToString());
        }
        
        private async Task HandleOperationExceptionAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            string messageForUser;
            try
            {
                messageForUser = ex.Message.Split('\'')[1];
            }
            catch
            {
                messageForUser = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message;
            }

            ExceptionModel httpRequestExceptionModel = new()
            {
                Message = messageForUser,
                StatusCode = 500
            };

            await response.WriteAsync(httpRequestExceptionModel.ToString());
        }
    }
    