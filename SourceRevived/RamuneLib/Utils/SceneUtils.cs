

namespace RamuneLib.Utils
{
    internal static class SceneUtils
    {
        static SceneUtils() => SceneManager.sceneLoaded += OnSceneLoaded;


        internal static Dictionary<string, List<Action>> SceneLoadedCallbacks = new()
        {
            { "PreStartScreen", new() },
            { "StartScreen", new() },
            { "MenuEnvironment", new() },
            { "XMenu", new() },
            { "Main", new() },
            { "Essentials", new() },
            { "Cyclops", new() },
            { "EscapePod", new() },
            { "Aurora", new() }
        };


        internal static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(!SceneLoadedCallbacks.TryGetValue(scene.name, out var callbacks))
                return;

            foreach(var callback in callbacks.ToList())
            {
                try
                {
                    callback?.Invoke();

                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to invoke SceneUtils callback for scene '{scene.name}':\n{ex}");
                }
            }
        }


        private static void RegisterOnSceneLoaded(string sceneName, Action callback) => SceneLoadedCallbacks[sceneName].Add(callback);


        internal static void RegisterOnPreStartScreenLoaded(Action callback) => RegisterOnSceneLoaded("PreStartScreen", callback);


        internal static void RegisterOnStartScreenLoaded(Action callback) => RegisterOnSceneLoaded("StartScreen", callback);


        internal static void RegisterOnMenuEnvironmentLoaded(Action callback) => RegisterOnSceneLoaded("MenuEnvironment", callback);


        internal static void RegisterOnXMenuLoaded(Action callback) => RegisterOnSceneLoaded("XMenu", callback);


        internal static void RegisterOnMainLoaded(Action callback) => RegisterOnSceneLoaded("Main", callback);


        internal static void RegisterOnEssentialsLoaded(Action callback) => RegisterOnSceneLoaded("Essentials", callback);


        internal static void RegisterOnCyclopsLoaded(Action callback) => RegisterOnSceneLoaded("Cyclops", callback);


        internal static void RegisterOnEscapePodLoaded(Action callback) => RegisterOnSceneLoaded("EscapePod", callback);


        internal static void RegisterOnAuroraLoaded(Action callback) => RegisterOnSceneLoaded("Aurora", callback);
    }
}