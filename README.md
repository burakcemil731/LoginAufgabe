# LoginAufgabe

https://ccd-school.de/coding-dojo/library-katas/benutzeranmeldung/

Entwickeln Sie eine Bibliothek, mit der Benutzeranmeldungen bei Websites verwaltet werden können.

Der Kontrakt für die Leistungen der Bibliothek soll so aussehen:

interface IRegistrierung {
	void Registrieren(string email, string passwort, string nickname);
	void Bestätigen(string registrierungsnummer);
}

interface IAnmeldung {
	string Anmelden(string anmeldename, string passwort);
	bool Ist_Anmeldung_gültig(string token);

	void Rücksetzung_des_Passworts_beantragen(string email);
	void Passwort_zurücksetzen(string email);
}

interface IVerwaltung {
	Benutzer Aktueller_Benutzer(string token);

	void Umbenennen(string benutzerId, string email, string nickname);
	void Passwort_ändern(string benutzerId, string passwort);
	void Löschen(string benutzerId, string passwort);
}

class Benutzer {
	string Id;
	string Email;
	string Nickname;
	DateTime Registrierungsdatum;
	DateTime LetzteAnmeldung;
	DateTime LetzteAktualisierung;
}
Neue Benutzer registrieren sich zunächst. Sie müssen mindestens ihre Email-Adresse angeben. Wenn kein Passwort gewählt wurde, generiert die Registrierung eines, das der Benutzer später ändern kann.

Wer sich registriert, bekommt eine Registrierungsemail geschickt mit einem Link zur Bestätigung, in dem eine Registrierungsnummer enthalten ist. Erst wenn die Registrierung mit dieser Nummer bestätigt ist, ist der Benutzer permanent im System (Benutzer.Bestätigt ist dann true).

Die Anmeldung erfolgt mit Email-Adresse oder Nickname und Passwort nach Bestätigung. Ist sie erfolgreich, liefert sie ein Token zurück. Das kann später immer wieder bei Bedarf zur Prüfung vorgelegt werden, ob Anfragen von einem Client gültig sind. Zu diesem Zweck sollte das Token so beschaffen sein, dass Clients sie nicht fälschen können. Für den Rest der Welt ist das Token opaque.

Wer sein Passwort vergessen hat, kann eine Zurücksetzung beantragen. Es wird dann eine Nachricht an die Email-Adresse gesendet, in der ein Link steht, über den man ein neues Passwort anfordern kann. Wird der angeklickt, wird ein Passwort generiert und an die beantragende Email-Adresse versandt.

Benutzer können ihre Anmeldedaten verändern. Sie greifen darauf über ihr Token zu, das sie bei der Anmeldung bekommen. Anschließend werden die Benutzerdaten über eine interne Id identifiziert; Benutzer sind mithin Entitäten, deren Daten bis auf die Id veränderbar sind.

Daten
Wie die Benutzerdaten gespeichert werden, soll nicht vorgeschrieben werden. In jedem Fall jedoch dürfen Passworte nicht im Klartext in der Benutzerdatenbank stehen.

Unbestätigte Registrierungen sollten ein Verfallsdatum bekommen und danach automatisch gelöscht werden.

Die Bibliothek ist mit einigen Angaben zu konfigurieren, z.B.

Verbindungsdaten für die Benutzerdatenbank
SMTP-Server und -Konto
URLs für Seiten, auf die in Emails verwiesen wird
Wie die Konfigurationsdaten gespeichert werden, wird ebenfalls nicht vorgegeben.
