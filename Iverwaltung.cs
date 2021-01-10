interface IVerwaltung
{	Benutzer AktuellerBenutzer(string token);
	void PasswortAendern(string benutzerId, string passwort);
	void Loeschen(string benutzerId, string passwort);

}