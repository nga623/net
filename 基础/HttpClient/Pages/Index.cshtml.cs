using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace HttpClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
           
        }
        
          
    }
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-API-KEY"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(
                        "The API key header X-API-KEY is required.")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}