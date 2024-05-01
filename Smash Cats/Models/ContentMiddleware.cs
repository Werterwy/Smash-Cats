namespace Smash_Cats.Models
{
    public class ContentMiddleware
    {
        private RequestDelegate nextDelegate;

        public ContentMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString() == "/middleware")
            {
                await context.Response.WriteAsync("это содержимое с middleware");
            }
            else
            {
                context.Request.Headers["User.Agent"] = "new vlue";


                await nextDelegate.Invoke(context);
            }
        }
    }
}
