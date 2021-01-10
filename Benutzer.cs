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

	public Benutzer(string Email, string Nickname, string Passwort) {
		this.Id = GenerateRandomId();
		this.Email = Email;
		this.Nickname = Nickname;
		this.Passwort = Passwort;
		this.Bestaetigt = false;
		this.Registrierungsdatum = new Date();
	}
}