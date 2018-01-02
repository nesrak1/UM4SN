# UM4SN
## A Subnautica Mod Loader
To get started, check out the example here: [WinterBlast.zip](https://github.com/nesrak1/UM4SN/files/1598991/WinterBlast.zip)

# Installation
Launch the installer here: [install.zip](https://github.com/nesrak1/UM4SN/files/1599005/install.zip)
Create a folder called SNUnityMod in your Subnautica folder. Then place your mods in the folder and start the game.

# Creating mods
The loader searches for classes with a base class of SubnauticaMod.

| Name | Description |
|------|-------------|
| OnDisable | Called when the plugin is disabled in the main menu. Currently not implemented. |
| OnEnable | Called when the plugin is enabled at start. |
| OnGameStartLoad | Called when the game starts the loading process. |
| OnGameFinishLoad | Called when the game starts. |
| OnGameReset | Called when the game goes back to the main menu. |
| OnCraftCreated | Called when craft is created. Use this hook to add custom items. |

Your mod should also contain resources strings (required for name/description)
Go to Project -> Properties -> Resources. Create a default resources file.
On the top, switch to strings. The strings are "title", "desc", "ver".

# Patching
To patch a class, create a public class with three attributes above it like so:
```cs
[SubPatch(SubPatchType.ModInitialize)]
[SubPatch(typeof(<CLASS>))]
[SubPatch("<METHOD>")]
```
Where <CLASS> is the class and <METHOD> is the method name.
Then create a method with a name of Prefix, Postfix, or Transpiler.
Then you can patch normally with Harmony.

# Compiling UM4SN yourself
Dependencies are not resolved automatically so you may need to reconfigure those.
The project also automatically copies the dll to a folder so you can choose that by editing the csproj file.