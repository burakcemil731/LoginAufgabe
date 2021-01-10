interface IRegistrierung
{
	Benutzer Registrieren(string email, string passwort, string nickname);
	void Bestaetigen(string registrierungsnummer);
}