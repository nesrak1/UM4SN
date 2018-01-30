using System;

namespace UM4SN
{
    public class SubBaseAttribute : Attribute
    {
        public int id = 0;
        public Type type = null;
        public string method = "";
        public Type[] args = null;
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class SubPatchAttribute : SubBaseAttribute
    {
        public SubPatchAttribute(int id)
        {
            this.id = id;
        }

        public SubPatchAttribute(SubPatchType id)
        {
            this.id = (int)id;
        }

        public SubPatchAttribute(Type type)
        {
            this.type = type;
        }

        public SubPatchAttribute(string method, Type[] args = null)
        {
            this.method = method;
            this.args = args;
        }
    }

    public enum SubPatchType
    {
        Internal, //Used only in UM4SN
        ModInitialize, //Used only for mods when starting up
        ModReloadable //Used only for mods that need to be reloaded after returning to main menu
    }
}
