

namespace RamuneLib.Utils.Miscellaneous
{
    public static class RamunesWorkbenchUtils
    {
        public static class Tabs
        {
            public static string Tools => "Tools";

            public static string Equipment => "Equipment";

            public static string Consumables => "Consumables";


            public static string PowerTab => "Power";

            public static string[] Batteries => ["Power", "Batteries"];

            public static string[] PowerCells => ["Power", "PowerCells"];


            public static string Modules => "Modules";

            public static string[] Seamoth => ["Modules", "Seamoth"];

            public static string[] PrawnSuit => ["Modules", "Prawn suit"];

            public static string[] Cyclops => ["Modules", "Cyclops"];
        }


        public const string RamunesWorkbench = "RamunesWorkbench";


        public static void AddTabNode(string tabName, Sprite sprite) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, null, tabName, sprite, null, null, null);


        public static void AddTabNode(string tabName, Sprite sprite, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, null, tabName, sprite, null, null, stepsToTab);


        public static void AddTabNode(string id, string tabName, Sprite sprite, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, true, id, tabName, sprite, null, null, stepsToTab);


        public static void AddCraftNode(string techTypeString) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, techTypeString, null, null);


        public static void AddCraftNode(TechType techType) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, null, techType, null);


        public static void AddCraftNode(string techTypeString, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, techTypeString, null, stepsToTab);


        public static void AddCraftNode(TechType techType, params string[] stepsToTab) => ModMessageSystem.SendGlobal(RamunesWorkbench, false, null, null, null, null, techType, stepsToTab);
    }
}