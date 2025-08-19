

namespace RamuneLib.Utils
{
    public static class CompatUtils
    {
        public static void Initialize() => Variables.instance.StartCoroutine(WaitForChainloader());


        public static Dictionary<string, PluginInfo> CachedPluginInfos = new();


        public static Dictionary<string, List<Action>> ModLoadedCallbacks = new();


        public static Dictionary<(string pluginName, string pluginVersion), List<Action>> AdvancedModLoadedCallbacks = new();


        private static void InvokeCallbacks<TKey>(this Dictionary<TKey, List<Action>> callbacks, Func<TKey, bool> predicate)
        {
            foreach(var kvp in callbacks)
            {
                if(!predicate(kvp.Key))
                    continue;

                foreach (var callback in kvp.Value)
                    callback?.Invoke();
            }
        }


        internal static IEnumerator WaitForChainloader()
        {
            yield return PatchingUtils.WaitForChainloader();

            CachedPluginInfos = Chainloader.PluginInfos;

            ModLoadedCallbacks.InvokeCallbacks(x => CachedPluginInfos.ContainsKey(x));

            AdvancedModLoadedCallbacks.InvokeCallbacks(x => TryGetPluginInfo(x.pluginName, out var pluginInfo) && pluginInfo.Metadata.Version.ToString() == x.pluginVersion);
        }


        public static void RegisterOnModLoadedEvent(string pluginGuid, Action callback) => (ModLoadedCallbacks[pluginGuid] = ModLoadedCallbacks.TryGetValue(pluginGuid, out var list) ? list : new()).Add(callback);


        public static void RegisterOnModLoadedEvent(string pluginGuid, string pluginVersion, Action callback) => (AdvancedModLoadedCallbacks[(pluginGuid, pluginVersion)] = AdvancedModLoadedCallbacks.TryGetValue((pluginGuid, pluginVersion), out var list) ? list : new()).Add(callback);


        public static void RegisterOnModLoadedEvents(Dictionary<string, Action> events) => events.ForEach(x => RegisterOnModLoadedEvent(x.Key, x.Value));


        public static void RegisterOnModLoadedEvent(Dictionary<(string pluginGuid, string pluginVersion), Action> events) => events.ForEach(x => RegisterOnModLoadedEvent(x.Key.pluginGuid, x.Key.pluginVersion, x.Value));


        public static bool TryGetPluginInfo(string pluginGuid, out PluginInfo pluginInfo) => CachedPluginInfos.TryGetValue(pluginGuid, out pluginInfo);
    }
}