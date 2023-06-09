using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace AuthWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;

        public string UserName { get; set; }
        public string PrincipalName { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task OnGet()
        {
            try
            {
                TokenAcquisitionTokenCredential cred = new TokenAcquisitionTokenCredential(_tokenAcquisition);

                GraphServiceClient client = new GraphServiceClient(cred);

                var user = await client.Me.Request().GetAsync();

                UserName = user.DisplayName;
                PrincipalName = user.UserPrincipalName;
            }
            catch (Exception ex)
            {
                UserName = ex.ToString();
            }
        }
    }
}