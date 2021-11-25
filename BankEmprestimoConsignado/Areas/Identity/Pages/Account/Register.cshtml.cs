using BankEmprestimoConsignado.Areas.Identity.Data;
using BankEmprestimoConsignado.Areas.Identity.Pages.Account.Manage;
using BankEmprestimoConsignado.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BankEmprestimoConsignado.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        // Adicionado para adicionar tipos de acesso
        //private readonly UserManager<ApplicationRole> _userAcess;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,

            // Adicionado para adicionar tipos de acesso
            //UserManager<ApplicationRole> userAcess,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            // Adicionado para adicionar tipos de acesso
            //_userAcess = userAcess;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo SENHA é obrigatório")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "O campo SENHA é obrigatório")]
            [Display(Name = "Confirme a Senha")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            //Adicionando numero do tipo de acesso
            [Required(ErrorMessage = "O campo ACESSO é obrigatório")]
            [Display(Name = "Tipo de Acesso")]
            public int Tipo_Acesso1 { get; set; }

            [Required(ErrorMessage = "O campo ACESSO é obrigatório")]
            [Display(Name = "Tipo de Acesso")]
            public string Tipo_Acesso { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, TipoAcesso = Input.Tipo_Acesso};
                //var tipoAcesso1 = await _userAcess.AddToRoleAsync(user, Input.Tipo_Acesso);
                var tipoAcesso = new ApplicationRole { Name = Input.Tipo_Acesso, NormalizedName = Input.Tipo_Acesso, ConcurrencyStamp = Input.Tipo_Acesso};

                var result = await _userManager.CreateAsync(user, Input.Password);
                //var resultAcess = await _userManager.AddToRoleAsync(user, Input.Tipo_Acesso);
                if (result.Succeeded)
                {
                    _logger.LogInformation("O usuário criou uma nova conta com senha.");


                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirme seu E-mail",
                        $"Por favor, confirme sua conta até <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }

                    }
                    foreach (var error in result.Errors)
                    {
                    ModelState.AddModelError(string.Empty, error.Description);
                    }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
