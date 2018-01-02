using System.Collections.Generic;
using UM4SN.Misc;
using UnityEngine;
using UWE;

namespace UM4SN
{
    public static class TechTypeItems
    {
        public static Dictionary<string, string> customTechNames = new Dictionary<string, string>();
        public static Dictionary<string, string> customTechDescs = new Dictionary<string, string>();
        public static Dictionary<string, string> customTechUuids = new Dictionary<string, string>();
        public static Dictionary<string, GameObject> customTechGameObjects = new Dictionary<string, GameObject>();
        public static Dictionary<TechType, string> customPath2Tech = new Dictionary<TechType, string>();
        public static Dictionary<TechType, TechTreeData[]> customTechTreeNeeds = new Dictionary<TechType, TechTreeData[]>();

        public static Dictionary<string, TechType> entClassTechTable = null;
        public static Dictionary<TechType, EquipmentType> equipmentTypes = null;
        public static Dictionary<TechType, string> techMapping = null;

        public static Dictionary<TechGroup, Dictionary<TechCategory, List<TechType>>> groups = null;
        public static List<TechType> buildables = null;

        public static void RegisterCustomTechType(int id, string uuid, string name, string description, EquipmentType type, GameObject gameObject, string gameObjectPath)
        {
            Language language = Language.main;
            Dictionary<string, string> strings = (Dictionary<string, string>)language.ReflectionGet("strings");
            strings["id_" + id.ToString()] = name;
            strings["Tooltip_id_" + id.ToString()] = description;
            Debug.Log("[UM4SN] Registering " + id + " (UUID " + uuid + ")");
            customTechNames.Add("id_" + id.ToString(), name);
            customTechDescs.Add("id_" + id.ToString(), description);
            customTechUuids.Add(uuid, gameObjectPath);
            customTechGameObjects.Add(gameObjectPath, gameObject);
            TechType techType = (TechType)id;
            customPath2Tech.Add(techType, gameObjectPath);
            if (entClassTechTable == null)
                entClassTechTable = (Dictionary<string, TechType>) typeof(CraftData).ReflectionGet("entClassTechTable", false, true);
            if (equipmentTypes == null)
                equipmentTypes = (Dictionary<TechType, EquipmentType>) typeof(CraftData).ReflectionGet("equipmentTypes", false, true);
            if (techMapping == null)
                techMapping = (Dictionary<TechType, string>) typeof(CraftData).ReflectionGet("techMapping", false, true);
            //CraftData.entClassTechTable.Add(uuid, techType);
            //CraftData.equipmentTypes.Add(techType, type);
            //CraftData.techMapping.Add(techType, uuid);
        }

        public struct TechTreeData
        {
            public TechType needs;
            public int count;
            public TechTreeData(TechType needs, int count)
            {
                this.needs = needs;
                this.count = count;
            }
        }

        public static void RegisterCustomTechNeed(TechType techType, TechTreeData[] ingredients, TechGroup techGroup, TechCategory techCategory)
        {
            Debug.Log("[UM4SN] Registering " + (int)techType);
            customTechTreeNeeds.Add(techType, ingredients);
            
            if (groups == null)
                groups = (Dictionary<TechGroup, Dictionary<TechCategory, List<TechType>>>) typeof(CraftData).ReflectionGet("groups", false, true);
            if (buildables == null)
                buildables = (List<TechType>)typeof(CraftData).ReflectionGet("buildables", false, true);
            groups[techGroup][techCategory].Add(techType);
            buildables.Add(techType);

            //CraftData.groups[techGroup][techCategory].Add(techType);
            //CraftData.buildables.Add(techType);
        }

        public static void FixPrefabDB()
        {
            Debug.Log("[UM4SN] Fixing prefab database");
            foreach (KeyValuePair<string, GameObject> kvp in customTechGameObjects)
            {
                PrefabDatabase.AddToCache(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<string, string> kvp in customTechUuids)
            {
                PrefabDatabase.prefabFiles[kvp.Key] = kvp.Value;
            }
        }

        public static void FixTechTree()
        {
            Debug.Log("[UM4SN] Fixing tech tree needs database");
            /*foreach (KeyValuePair<TechType, TechTreeData[]> kvp in customTechTreeNeeds)
            {
                TechTree mainVar = TechTree.main;
                mainVar.root.Unlocks(mainVar.New(kvp.Key));
                TechTree.InternalAction intAction = mainVar.GetInternal(kvp.Key);
                foreach (TechTreeData kvpv in kvp.Value)
                {
                    intAction.Needs(kvpv.needs, kvpv.count);
                }
            }*/
        }

        public static GameObject cTGO(string name)
        {
            return customTechGameObjects[name];
        }
    }
}
