

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


        internal const string RamunesWorkbench = "RamunesWorkbench";


        internal static void AddTabNode(string tabName, Sprite sprite) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, null, tabName, sprite, null, null, null);


        internal static void AddTabNode(string tabName, Sprite sprite, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, null, tabName, sprite, null, null, stepsToTab);


        internal static void AddTabNode(string id, string tabName, Sprite sprite, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, id, tabName, sprite, null, null, stepsToTab);


        internal static void AddCraftNode(string techTypeString) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, techTypeString, null, null);


        internal static void AddCraftNode(TechType techType) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, null, techType, null);


        internal static void AddCraftNode(string techTypeString, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, techTypeString, null, stepsToTab);


        internal static void AddCraftNode(TechType techType, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, null, techType, stepsToTab);
    }
}