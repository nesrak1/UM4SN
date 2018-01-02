using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UM4SN.Misc
{
    public class MainMenuModsPanel : MonoBehaviour
    {
        public GameObject savedGamesRef;
        public GameObject loadedModsRef;
        public void Start()
        {
            GameObject modButton = savedGamesRef.transform.Find("SavedGameArea").Find("SavedGameAreaContent").Find("NewGame").gameObject;
            Transform savedGameAreaContent = loadedModsRef.transform.Find("SavedGameArea").Find("SavedGameAreaContent");
            foreach (SubnauticaMod mod in PluginLoader.mods)
            {
                GameObject modButtonInst = Instantiate(modButton);
                bool loaded = true;
                string statusText = "error";
                if (loaded)
                {
                    statusText = "Status: <color=#00ff00ff>loaded</color>.";
                } else
                {
                    statusText = "Status: <color=#ff0000ff>unloaded</color>.";
                }
                modButtonInst.transform.Find("NewGameButton").Find("Text").GetComponent<Text>().text = mod.Title + "\n" + mod.Desc + "\n" + statusText;
                Button modButtonButton = modButtonInst.transform.Find("NewGameButton").GetComponent<Button>();
                modButtonButton.onClick.RemoveAllListeners();
                modButtonButton.onClick.AddListener(delegate { DoModAction(mod.Title); });
                modButtonInst.transform.SetParent(savedGameAreaContent, false);
            }
        }

        public void DoModAction(string title)
        {
            SubnauticaMod mod = PluginLoader.mods.Where(t => t.Title.Equals(title)).Select(t => t).First();
            PluginLoader.mods.Remove(mod);
        }
    }
}
