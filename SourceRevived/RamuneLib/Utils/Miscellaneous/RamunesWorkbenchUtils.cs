

namespace RamuneLib.Utils.Miscellaneous
{
    internal static class RamunesWorkbenchUtils
    {
        internal static class Tabs
        {
            internal static string Tools => "Tools";

            internal static string Equipment => "Equipment";

            internal static string Consumables => "Consumables";


            internal static string PowerTab => "Power";

            internal static string[] Batteries => ["Power", "Batteries"];

            internal static string[] PowerCells => ["Power", "PowerCells"];


            internal static string Modules => "Modules";

            internal static string[] Seamoth => ["Modules", "Seamoth"];

            internal static string[] PrawnSuit => ["Modules", "Prawn suit"];

            internal static string[] Cyclops => ["Modules", "Cyclops"];
        }


        internal sealed class TabNode
        {
            internal string id { get; }

            internal string tabName { get; }

            internal Sprite sprite { get; }

            internal string[] stepsToTab { get; }

            internal TabNode(string tabName, Sprite sprite) : this(null, tabName, sprite, null) { }

            internal TabNode(string tabName, Sprite sprite, params string[] stepsToTab) : this(null, tabName, sprite, stepsToTab) { }

            internal TabNode(string id, string tabName, Sprite sprite) : this(id, tabName, sprite, null) { }

            internal TabNode(string id, string tabName, Sprite sprite, params string[] stepsToTab)
            {
                this.id = id;
                this.tabName = tabName;
                this.sprite = sprite;
                this.stepsToTab = stepsToTab;
            }
        }


        internal sealed class CraftNode
        {
            internal string techTypeString { get; }

            internal TechType? techType { get; }

            internal string[] stepsToTab { get; }

            internal CraftNode(TechType techType, params string[] stepsToTab) : this(null, techType, stepsToTab) { }

            internal CraftNode(string techTypeString, params string[] stepsToTab) : this(techTypeString, null, stepsToTab) { }

            private CraftNode(string techTypeString, TechType? techType, string[] stepsToTab)
            {
                this.techTypeString = techTypeString;
                this.techType = techType;
                this.stepsToTab = stepsToTab;
            }
        }


        internal const string RamunesWorkbench = "RamunesWorkbench";


        internal static void AddTabNode(string tabName, Sprite sprite) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, null, tabName, sprite, null, null, null);


        internal static void AddTabNode(string tabName, Sprite sprite, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, null, tabName, sprite, null, null, stepsToTab);


        internal static void AddTabNode(string id, string tabName, Sprite sprite, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, id, tabName, sprite, null, null, stepsToTab);


        internal static void AddTabNode(TabNode tabNode) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, tabNode.id, tabNode.tabName, tabNode.sprite, null, null, tabNode.stepsToTab);


        internal static void AddTabNodes(TabNode[] tabNodes) => tabNodes.ForEach(x => ModMessageSystem.SendGlobal(RamunesWorkbench, true, x.id, x.tabName, x.sprite, null, null, x.stepsToTab));


        internal static void AddCraftNode(string techTypeString, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, techTypeString, null, stepsToTab);


        internal static void AddCraftNode(TechType techType, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, null, techType, stepsToTab);


        internal static void AddCraftNode(CraftNode craftNode) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, craftNode.techTypeString, craftNode.techType, craftNode.stepsToTab);


        internal static void AddCraftNodes(CraftNode[] craftNodes) => craftNodes.ForEach(x => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, x.techTypeString, x.techType, x.stepsToTab));

        internal static void AddNodes(TabNode[] tabNodes, CraftNode[] craftNodes)
        {
            AddTabNodes(tabNodes);
            AddCraftNodes(craftNodes);
        }
    }
}