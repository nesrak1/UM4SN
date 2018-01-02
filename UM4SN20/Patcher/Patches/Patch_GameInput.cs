using UM4SN.Misc;
using UnityEngine;

namespace UM4SN
{
    [SubPatch(SubPatchType.Internal)]
    [SubPatch(typeof(uGUI_MainMenu))]
    [SubPatch("Start")]
    public class Patch_GameInput
    {
        public static void Prefix(uGUI_MainMenu __instance)
        {
            new GameObject().AddComponent<MainMenuModsButton>();
        }
    }
}
