interface IAnmeldung
{
	string Anmelden(string anmeldename, string passwort);
	bool IstAnmeldungGueltig(string token);
	
	void RuecksetzungDesPasswortsBeantragen(string email);
	void PasswortZuruecksetzen(string email);
}