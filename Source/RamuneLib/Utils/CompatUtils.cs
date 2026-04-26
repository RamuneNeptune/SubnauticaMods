

namespace RamuneLib.Utils
{
    internal static class CompatUtils
    {
        internal static bool ChainloaderFinished { get; private set; }


        internal static void Initialize() => Variables.instance.StartCoroutine(WaitForChainloader());


        internal static Dictionary<string, PluginInfo> CachedPluginInfos = [];


        internal static List<Action> ChainloaderFinishedCallbacks = [];


        internal static Dictionary<string, List<Action>> ModLoadedCallbacks = [];


        internal static Dictionary<(string pluginName, string pluginVersion), List<Action>> AdvancedModLoadedCallbacks = [];


        private static void InvokeCallbacks(this List<Action> callbacks)
        {
            foreach (var callback in callbacks.ToArray())
            {
                try
                {
                    callback?.Invoke();
                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to invoke a CompatUtils OnChainloaderFinished callback:\n{ex}");
                }
            }
        }


        private static void InvokeCallbacks<TKey>(this Dictionary<TKey, List<Action>> callbacks, Func<TKey, bool> predicate)
        {
            foreach(var kvp in callbacks.ToArray())
            {
                if(kvp.Value == null)
                    continue;

                try
                {
                    if(!predicate(kvp.Key))
                        continue;
                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to invoke CompatUtils predicate for '{kvp.Key}':\n{ex}");
                    continue;
                }

                foreach(var callback in kvp.Value.ToArray())
                {
                    try
                    {
                        callback?.Invoke();
                    }
                    catch(Exception ex)
                    {
                        Logfile.Error($"Failed to invoke CompatUtils callback for '{kvp.Key}':\n{ex}");
                    }
                }
            }
        }


        internal static IEnumerator WaitForChainloader()
        {
            yield return PatchingUtils.WaitForChainloader();

            ChainloaderFinished = true;

            CachedPluginInfos = Chainloader.PluginInfos;

            ChainloaderFinishedCallbacks.InvokeCallbacks();

            ModLoadedCallbacks.InvokeCallbacks(x => CachedPluginInfos.ContainsKey(x));

            AdvancedModLoadedCallbacks.InvokeCallbacks(x => TryGetPluginInfo(x.pluginName, out var pluginInfo) && pluginInfo.Metadata.Version.ToString() == x.pluginVersion);
        }


        internal static void RegisterOnChainloaderFinishedEvent(Action callback)
        {
            if(callback == null)
                return;

            if(ChainloaderFinished)
            {
                try
                {
                    callback();
                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to invoke CompatUtils OnChainloaderFinished callback:\n{ex}");
                }

                return;
            }

            ChainloaderFinishedCallbacks.Add(callback);
        }


        internal static void RegisterOnModLoadedEvent(string pluginGuid, Action callback)
        {
            if(callback == null)
                return;

            if(ChainloaderFinished && IsPluginLoaded(pluginGuid))
            {
                try
                {
                    callback();
                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to invoke CompatUtils callback for '{pluginGuid}':\n{ex}");
                }

                return;
            }

            (ModLoadedCallbacks[pluginGuid] = ModLoadedCallbacks.TryGetValue(pluginGuid, out var list) ? list : []).Add(callback);
        }


        internal static void RegisterOnModLoadedEvent(string pluginGuid, string pluginVersion, Action callback)
        {
            if(callback == null)
                return;

            if(ChainloaderFinished && IsPluginLoaded(pluginGuid, pluginVersion))
            {
                try
                {
                    callback();
                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to invoke CompatUtils callback for '{pluginGuid} {pluginVersion}':\n{ex}");
                }

                return;
            }

            (AdvancedModLoadedCallbacks[(pluginGuid, pluginVersion)] = AdvancedModLoadedCallbacks.TryGetValue((pluginGuid, pluginVersion), out var list) ? list : []).Add(callback);
        }


        internal static void RegisterOnModLoadedEvents(Dictionary<object, Action> events)
        {
            foreach(var kvp in events)
            {
                var key = kvp.Key;

                if(key is string keyString)
                {
                    RegisterOnModLoadedEvent(keyString, kvp.Value);
                    continue;
                }

                if(key is ValueTuple<string, string> keyTuple)
                {
                    RegisterOnModLoadedEvent(keyTuple.Item1, keyTuple.Item2, kvp.Value);
                    continue;
                }

                Logfile.Warning($"RegisterOnModLoadedEvents :: Invalid type: {kvp.Key.GetType()}");
            }
        }


        internal static bool TryGetPluginInfo(string pluginGuid, out PluginInfo pluginInfo) => CachedPluginInfos.TryGetValue(pluginGuid, out pluginInfo);


        internal static bool IsPluginLoaded(string pluginGuid) => CachedPluginInfos.ContainsKey(pluginGuid);


        internal static bool IsPluginLoaded(string pluginGuid, string pluginVersion) => TryGetPluginInfo(pluginGuid, out var pluginInfo) && pluginInfo.Metadata.Version.ToString() == pluginVersion;
    }
}