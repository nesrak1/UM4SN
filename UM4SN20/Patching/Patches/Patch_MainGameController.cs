namespace UM4SN
{
    [SubPatch(SubPatchType.Internal)]
    [SubPatch(typeof(MainMenuMusic))]
    [SubPatch("Stop")]
    public class Patch_MainGameController
    {
        public static void Postfix()
        {
            PluginLoader.GameStarting();
        }
    }
}
