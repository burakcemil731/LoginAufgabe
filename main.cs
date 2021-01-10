using System;
using HashDummySystem;

static void Main()
{
    BenutzerDatenbank datenbank = BenutzerDatenbank.Instance;
 
    registrierung: IRegistrierung = new Registrierung();

    neuerBenutzer: Benutzer = registrierung.Registrieren("email@,,", "passwort", "nick");
    datenbank.BenutzerListe.Add(NeuerBenutzer);

    // User erhält E-Mail mit Nr "5945c961-e74d-478f-8afe-da53cf4189e3"
    // User klickt auf Link in E-Mail, wodurch diese Methode aufgerufen wird
    registrierung.Bestaetigen("5945c961-e74d-478f-8afe-da53cf4189e3");

    anmeldung: Anmeldung = new Anmeldung();
    token: String = anmeldung.Anmelden("emailA", "klartextpasswort");

    anmeldung.IstAnmeldungGueltig(token); // ==> true

    // User beantragt Passwort Zurücksetzen
    anmeldung.RuecksetzungDesPasswortsBeantragen("emailA");    

   // User erhält vom Server eine Mail mit Link
   // User klickt Mail
   anmeldung.PasswortZuruecksetzen("emailA");
   // User erhält Mail mit neuem Passwort
}



