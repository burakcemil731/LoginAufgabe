/*
Entwickeln Sie eine Bibliothek, mit der Benutzeranmeldungen in Anwendungen verwaltet werden können. 
*/

interface IRegistrierung
{
	void Registrieren(string email, string passwort, string nickname);
	void Bestaetigen(string registrierungsnummer);
}


interface IAnmeldung
{
	string Anmelden(string anmeldename, string passwort);
	bool IstAnmeldungGueltig(string token);
	
	void RuecksetzungDesPasswortsBeantragen(string email);
	void PasswortZuruecksetzen(string email);
}


interface IVerwaltung
{
	Benutzer AktuellerBenutzer(string token);
	
	void PasswortAendern(string benutzerId, string passwort);
	void Loeschen(string benutzerId, string passwort);

}


class Benutzer
{
	string Id;
	string Email;
	string Nickname;
	string Passwort;
	bool Bestaetigt;
	DateTime Registrierungsdatum;
	DateTime LetzteAnmeldung;
	DateTime LetzteAktualisierung;

}

// Passwort Verschlüsselung, Tutorial: https://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp
// MS Doc https://docs.microsoft.com/de-de/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1


public class SaltedHash
{
public SaltedHash(string salt, string hash) {
Salt = salt;
Hash = hash;
}
public string Salt { get; set; }
public string Hash { get; set; }
}

// Instanzierung für Verschlüsselung
public class RegisteredEvent
{
public RegisteredEvent(string name, string salt, string hash)
	{
		Name = name;
		Salt = salt;
		Hash = hash;
}
public string Name { get; }
public string Salt { get; }
public string Hash { get; }
}


// Crypto Klasse für Verschlüsselung
using System;
using System.Security.Cryptography;
using System.Text;

{

public class Crypto

	{
		public static string Create Salt(int size) 
		{
			var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
			var buffer = new byte[size];
			rngCryptoServiceProvider.GetBytes(buffer);
			return Convert.ToBase64String(buffer);
		}
		
		public static string Create Hash(string password, string salt) 
		{
			var salted Password = Encoding.UTF8.GetBytes(password + salt);
			var salted Hash = newSHA256Managed().ComputeHash(saltedPassword);
			return Convert.ToBase64String(salted Hash);
		}
	}
}


//Passwort ändern

public class PasswordChanged
	{
		public PasswordChanged(string salt, string hash) 
		{
			Salt = salt;
			Hash = hash;
		}
	
	public string Salt { get; }
	public string Hash { get; }
	
	}

// USerLogin Klasse mit Registrierung

public class UserLogin 
{
	
		public void Register(string Email, string password) 
		{
			var salted Hash = Generate_salted_hash(password);

		}
	
	internal Salted Hash Generate_salted_hash(string password) 
	{
		var salt = Crypto.CreateSalt(32);
		var hash = Crypto.CreateHash(password, salt);
		var salted Hash = new Salted Hash(salt, hash);
		return salted Hash;
	}
}


// Registrierung mit Passwort, falls keins gewählt, wird automatisch Pw generiert

public class Registrierung
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Email eingeben: ");
		string Email = Console.ReadLine();
		
		Console.WriteLine("Nickname eingeben: ");
		string Nickname = Console.ReadLine();
			if(Nickname='')
			{Nickname = Email};
			else return;
		
		Console.WriteLine("Passwort eingeben: ");
		string Passwort = Console.ReadLine();
		if(Passwort !="")
		{
			// Wie kann ich hier das PW verschlüsselt eingeben, zurücksenden, oder automatisch generieren mit dem salt/hash?
		}
		return;
	}


// Registrierungsmail mit Bestätigungslink, falls Nutzer bestätigt dann Benutzer.Bestaetigt=true;

				//code aus Microsoft Docs,, HTTP Post Register method callen:

public async Task<ActionResult> Register(RegisterViewModel model)
{
    if (ModelState.IsValid)
    {
        var nutzer = new Benutzer { Nickname = model.Email, Email = model.Email };
        var result = await UserManager.CreateAsync(nutzer, model.Passwort);
        if (result.Succeeded)
        {
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(nutzer.Id);
            var callbackUrl = Url.Action(
               "ConfirmEmail", "Account", 
               new { nutzerId = nutzer.Id, code = code }, 
               protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(nutzer.Id, 
               "Confirm your account", 
               "Please confirm your account by clicking this link: <a href=\"" 
                                               + callbackUrl + "\">link</a>");
            
            return View("DisplayEmail");
        }
        AddErrors(result);
    }

    return View(model);
}

// Setup für Bestätigungsmail über SendGrid Azure (code aus MS Docs):

public class EmailService : IIdentityMessageService
{
   public Task SendAsync(IdentityMessage message)
   {
      return configSendGridasync(message);
   }

   private Task configSendGridasync(IdentityMessage message)
   {
      var myMessage = new SendGridMessage();
      myMessage.AddTo(message.Destination);
      myMessage.From = new System.Net.Mail.MailAddress(
                          "burak@medifox.de", "Burak D.");
      myMessage.Subject = message.Subject;
      myMessage.Text = message.Body;
      myMessage.Html = message.Body;

      var credentials = new NetworkCredential(
                 ConfigurationManager.AppSettings["mailAccount"],
                 ConfigurationManager.AppSettings["mailPassword"]
                 );

      // Create a Web transport for sending email.
      var transportWeb = new Web(credentials);

      // Send the email.
      if (transportWeb != null)
      {
         return transportWeb.DeliverAsync(myMessage);
      }
      else
      {
         return Task.FromResult(0);
      }
   }
}

// Email versenden

void sendMail(Message message)
{
#region formatter
   string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
   string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

   html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
#endregion

   MailMessage msg = new MailMessage();
   msg.From = new MailAddress("burak@medifox.de");
   msg.To.Add(new MailAddress(message.Destination));
   msg.Subject = message.Subject;
   msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
   msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

   SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
   System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("burak@medifox.de", "XXXXXX");
   smtpClient.Credentials = credentials;
   smtpClient.EnableSsl = true;
   smtpClient.Send(msg);
}


// Email bestätigt?

public async Task<ActionResult> Bestaetigen(string nutzerId, string registrierungsnummer)
{
    if (nutzerId == null || registrierungsnummer == null)
    {
        return View("Error");
    }
    var result = await UserManager.ConfirmEmailAsync(nutzerId, registrierungsnummer);
    if (result.Succeeded)
    {
        return View("Bestaetigen");
    }
    AddErrors(result);
    return View();
}


// Passwort Vergessen , msg an Email mit Link zum PW generieren, bei Klick -> neues PW generieren  und an Email zurückgeben, PW nicht im klartext speichern

public async Task<ActionResult> PasswortZuruecksetzen(PasswortZuruecksetzenViewModel model)
{
    if (ModelState.IsValid)
    {
        var user = await UserManager.FindByNameAsync(model.Email);
        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        {
            return View("RuecksetzungDesPasswortsBeantragen");
        }

        var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        var callbackUrl = Url.Action("PasswortZuruecksetzen", "Benutzer", 
    new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
        await UserManager.SendEmailAsync(nutzer.Id, "PasswortZuruecksetzen", 
    "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");        
        return View("RuecksetzungDesPasswortsBeantragen");
    }

    return View(model);
}



// Login , bei Erfolg Token zurückliefern, bei Bedarf vorlegbar, 
// aus https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
// Voraussetzung  ist JSON Container (auskommentiert) namens AppSetting.Json

/*
{    
  "Jwt": {    
    "Key": "ThisismySecretKey",    
    "Issuer": "Test.com"    
  }    
}   */
//Backend :

using Microsoft.AspNetCore.Authorization;    
using Microsoft.AspNetCore.Mvc;    
using Microsoft.Extensions.Configuration;    
using Microsoft.IdentityModel.Tokens;    
using System;    
using System.IdentityModel.Tokens.Jwt;    
using System.Security.Claims;    
using System.Text;    
    
namespace JWTAuthentication.Controllers    
{    
    [Route("api/[controller]")]    
    [ApiController]    
    public class LoginController : Controller    
    {    
        private IConfiguration _config;    
    
        public LoginController(IConfiguration config)    
        {    
            _config = config;    
        }    
        [AllowAnonymous]    
        [HttpPost]    
        public IActionResult Login([FromBody]UserModel login)    
        {    
            IActionResult response = Unauthorized();    
            var Benutzer = AuthenticateUser(login);    
    
            if (Benutzer != null)    
            {    
                var tokenString = GenerateJSONWebToken(Benutzer);    
                response = Ok(new { token = tokenString });    
            }    
    
            return response;    
        }    
    
        private string GenerateJSONWebToken(UserModel userInfo)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],    
              _config["Jwt:Issuer"],    
              null,    
              expires: DateTime.Now.AddMinutes(120),    
              signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }    
    
        private UserModel AuthenticateUser(UserModel login)    
        {    
            UserModel user = null;    
    
  // eigentliche Idee ist es ein bool zurückzuliefern mit true falls der nickname/email in der DB existiert
            if (login.Nickname == "burak@medifox.de")    
            {    
                Benutzer = new UserModel { Nickname = "Burak", Email = "burak@medifox.de" };    
            }    
            return Benutzer;    
        }    
	
	
	}  
	
}   

//FRONT End vom Token:


// Dummys für Email senden, PW verschlüsseln, Benutzer speichern