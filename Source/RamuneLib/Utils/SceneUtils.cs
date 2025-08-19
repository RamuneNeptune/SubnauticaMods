

namespace RamuneLib.Utils
{
    public static class SceneUtils
    {
        static SceneUtils() => SceneManager.sceneLoaded += OnSceneLoaded;


        public static Dictionary<string, List<Action>> SceneLoadedCallbacks = new()
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

            foreach(var callback in callbacks)
                callback?.Invoke();
        }


        public static void RegisterOnSceneLoaded(string sceneName, Action callback) => SceneLoadedCallbacks[sceneName].Add(callback);


        public static void RegisterOnPreStartScreenLoaded(Action callback) => RegisterOnSceneLoaded("PreStartScreen", callback);


        public static void RegisterOnStartScreenLoaded(Action callback) => RegisterOnSceneLoaded("StartScreen", callback);


        public static void RegisterOnMenuEnvironmentLoaded(Action callback) => RegisterOnSceneLoaded("MenuEnvironment", callback);


        public static void RegisterOnXMenuLoaded(Action callback) => RegisterOnSceneLoaded("XMenu", callback);


        public static void RegisterOnMainLoaded(Action callback) => RegisterOnSceneLoaded("Main", callback);


        public static void RegisterOnEssentialsLoaded(Action callback) => RegisterOnSceneLoaded("Essentials", callback);


        public static void RegisterOnCyclopsLoaded(Action callback) => RegisterOnSceneLoaded("Cyclops", callback);


        public static void RegisterOnEscapePodLoaded(Action callback) => RegisterOnSceneLoaded("EscapePod", callback);


        public static void RegisterOnAuroraLoaded(Action callback) => RegisterOnSceneLoaded("Aurora", callback);
    }
}