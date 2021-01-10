public class Registrierung:IRegistrierung{

    private Benutzer Benutzer;
    private string RegistrierungsNummer;

    Benutzer Registrieren(string email, string passwort, string nickname)
    {
        if (String.IsNullOrEmpty(email))
        {
            return throw new ArgumentException("Parameter cannot be null", nameof(email));
        }

        // wenn kein passwort angegeben, wird passwort automatisch generiert und schickt per mail

        if (String.IsNullOrEmpty(passwort)) 
        {
            passwort = GenerateZufaelligePasswort();
        }

        Benutzer = new Benutzer(email, HashPassword(passwort), nickname);
        RegistrierungsNummer = GeneriereZufaelligeRegistrNr(); // uuid (zB 5945c961-e74d-478f-8afe-da53cf4189e3)

        // email erstellen, mit link zu rbestätigung, email enth. registr.nr

        SendMail(email, registrierungsNummer, nickname, passwort);

        return Benutzer;
    }

  void Bestaetigen(string registrierungsNummer){

     if (RegistrierungsNummer == registrierungsNummer) 
     {  //wenn gespeicherte RNr = best.Rnr=true
         benutzer.Bestaetigt = true;
     } 
  }  
}