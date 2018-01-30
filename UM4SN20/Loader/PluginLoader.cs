using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace UM4SN
{
    public class PluginLoader
    {
        private static bool loaded = false;
        public static List<SubnauticaMod> mods;
        public static void StartModLoader()
        {
            if (loaded) return;
            loaded = true;
            Debug.Log("[UM4SN] Mod loader loaded (WARPER)");
            Debug.Log("[UM4SN] Looking in: " + new FileInfo(@".\SNUnityMod\").FullName);
            mods = LoadPlugins(new FileInfo(@".\SNUnityMod\").FullName);
            Patcher.PatchAll(SubPatchType.Internal);
        }

        public static void MenuLoading()
        {
            foreach (SubnauticaMod mod in mods)
            {
                mod.OnGameReset();
            }
        }

        public static void GameLoading()
        {
            foreach (SubnauticaMod mod in mods)
            {
                mod.OnGameStartLoad();
            }
        }

        public static void GameStarting()
        {
            foreach (SubnauticaMod mod in mods)
            {
                mod.OnGameFinishLoad();
            }
        }

        public static void CraftCreated()
        {
            foreach (SubnauticaMod mod in mods)
            {
                mod.OnCraftCreated();
            }
        }
        
        public static List<SubnauticaMod> LoadPlugins(string path)
        {
            string[] dllFileNames = null;
            Debug.Log("[UM4SN] Attempting to load " + path);
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");

                List<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    Debug.Log("[UM4SN] " + dllFile + " was found!");
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(SubnauticaMod);
                List<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.BaseType == pluginType)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }

                List<SubnauticaMod> plugins = new List<SubnauticaMod>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    SubnauticaMod plugin = (SubnauticaMod)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                    try
                    {
                        plugin.OnEnable();
                        plugin.SetName();
                        Debug.Log("[UM4SN] Loaded " + plugin.Title);
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("[UM4SN] Failed to load plugin: " + ex);
                    }
                }
                return plugins;
            }
            return null;
        }
    }
}
