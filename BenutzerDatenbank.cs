
class BenutzerDatenbank
{
    private static BenutzerDatenbank _Instance = null;

    public BenutzerListe List<Benutzer>();         //es gibt ne Liste
  
    private BenutzerDatenbank()
    {
        BenutzerListe = new ArrayList();        //Liste anlegen
    }

    public static BenutzerDatenbank Instance
    {
        get
        {
            // The first call will create the one and only instance.
            if (_Instance == null)
            {
                _Instance = new BenutzerDatenbank();
            }

            // Every call afterwards will return the single instance created above.
            return _Instance;
        }
    }
}