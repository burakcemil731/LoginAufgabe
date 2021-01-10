public class Anmeldung : IAnmeldung
{
    // 1.Benutzer aufrufen, 2. existiert benutzer?, 3. richtiges Pw?, 4. token generieren, 5. anmeldunggueltig
    private string Token;

    string Anmelden(string anmeldename, string passwort)
    {

        HashPassword(passwort);
        BenutzerDatenbank datenbank = BenutzerDatenbank.Instance;

        // finde in benutzerListe Benutzer A mit angegebenem anmeldenamen
        // for loop über jeden b enutzer in datenbank (array von benutzern)
        foreach (Benutzer benutzer in datenbank.BenutzerListe)
        {
            if (anmeldename == benutzer.email || benutzer.name)
            {
                if (benutzer.Passwort == HashPasswort(passwort)) {
                    Token = GeneriereRandomToken();
                    return Token;
                }
                
            }
            else { return; };
        }
        
      return token;
    }

    bool IstAnmeldungGueltig(string token)
    {
        return Token == token;
    }

    void RuecksetzungDesPasswortsBeantragen(string email)
    {
        BenutzerDatenbank datenbank = BenutzerDatenbank.Instance;
        foreach (Benutzer benutzer in datenbank.BenutzerListe)
        {
            if (email == benutzer.email)
            {
                SendResetMail();
            }
            else { return; };
        }
    }
    
    void PasswortZuruecksetzen(string email)
    {
        if (IstRuecksetzungDesPasswortsGueltig()) {
            neuesPasswort: String = GenerateNeuesPasswort();
            Benutzer.Passwort = HashPasswort(neuesPasswort);

            SendMailNewPasswort(neuesPasswort); // (klartext)
        }
    }
}