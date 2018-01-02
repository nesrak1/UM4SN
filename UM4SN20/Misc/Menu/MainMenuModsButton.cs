using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UM4SN.Misc
{
    public class MainMenuModsButton : MonoBehaviour
    {
        public void Start()
        {
            GameObject startButton = GameObject.Find("Menu canvas/Panel/MainMenu/PrimaryOptions/MenuButtons/ButtonPlay");
            GameObject showLoadedMods = Instantiate(startButton);
            Text buttonText = showLoadedMods.transform.Find("Circle/Bar/Text").gameObject.GetComponent<Text>() as Text;
            buttonText.text = "Mods";
            showLoadedMods.transform.SetParent(GameObject.Find("Menu canvas/Panel/MainMenu/PrimaryOptions/MenuButtons").transform, false);
            showLoadedMods.transform.SetSiblingIndex(4);
            Button showLoadedModsButton = showLoadedMods.GetComponent<Button>();
            showLoadedModsButton.onClick.RemoveAllListeners();
            showLoadedModsButton.onClick.AddListener(ShowLoadedModsButtonClick);

            MainMenuRightSide rightSide = MainMenuRightSide.main;
            GameObject savedGamesRef = FindObject(rightSide.gameObject, "SavedGames");
            GameObject LoadedMods = Instantiate(savedGamesRef);
            LoadedMods.name = "Mods";
            LoadedMods.transform.Find("Header").GetComponent<Text>().text = "Mods";
            Destroy(LoadedMods.transform.Find("SavedGameArea/SavedGameAreaContent/NewGame").gameObject);

            MainMenuModsPanel panel = LoadedMods.AddComponent<MainMenuModsPanel>();
            panel.savedGamesRef = savedGamesRef;
            panel.loadedModsRef = LoadedMods;

            Destroy(LoadedMods.GetComponent<MainMenuLoadPanel>());
            LoadedMods.transform.SetParent(rightSide.transform, false);
            rightSide.groups.Add(LoadedMods);
        }

        public void ShowLoadedModsButtonClick()
        {
            MainMenuRightSide rightSide = MainMenuRightSide.main;
            rightSide.OpenGroup("Mods");
        }

        public GameObject FindObject(GameObject parent, string name)
        {
            Component[] trs = parent.GetComponentsInChildren(typeof(Transform), true);
            foreach (Component t in trs)
            {
                if (t.name == name)
                {
                    return t.gameObject;
                }
            }
            return null;
        }
    }
}
