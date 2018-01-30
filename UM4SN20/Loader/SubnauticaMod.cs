using System.Reflection;
using System.Resources;

namespace UM4SN
{
    public class SubnauticaMod
    {
        public string Title { get; private set; }
        public string Desc { get; private set; }
        public string Ver { get; private set; }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnGameStartLoad()
        {

        }

        public virtual void OnGameFinishLoad()
        {

        }

        public virtual void OnGameReset()
        {

        }

        public virtual void OnCraftCreated()
        {

        }

        internal void SetName()
        {
            Assembly assembly = GetType().Assembly;
            Title = assembly.GetName().Name;
            Desc = "No description found.";
            Ver = "v1.0";

            string manifest = assembly.GetManifestResourceNames()[0].Replace(".resources", "");
            ResourceManager manager = new ResourceManager(manifest, assembly);
            string title = manager.GetString("title");
            string desc = manager.GetString("desc");
            string ver = manager.GetString("ver");
            if (title != null) Title = title;
            if (desc != null) Desc = desc;
            if (ver != null) Ver = ver;
            /*string[] names = assembly.GetManifestResourceNames();
            string name = null;
            foreach (string str in names)
            {
                if (str.Contains("config.txt"))
                {
                    name = str;
                }
            }


            if (name != null)
            {
                using (Stream stream = assembly.GetManifestResourceStream(name))
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string str = reader.ReadLine();
                        string prop = str.Split('=')[0].Trim().ToLower();
                        string val = str.Substring(str.IndexOf('=') + 1);
                        switch (prop)
                        {
                            case "title":
                            case "name":
                                Title = val;
                                break;
                            case "desc":
                            case "description":
                                Desc = val;
                                break;
                            case "ver":
                            case "version":
                                Ver = val;
                                break;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Mod " + Title + " has no config properties!");
            }*/
        }
    }
}
