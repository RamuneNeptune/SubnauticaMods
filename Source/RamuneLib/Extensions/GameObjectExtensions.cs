

namespace RamuneLib.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Loops over all the children of this GameObject and runs '<c>GameObject.Destroy</c>' for each
        /// </summary>
        public static void DestroyChildren(this GameObject gameObject)
        {
            foreach(Transform child in gameObject.transform)
                GameObject.Destroy(child.gameObject);
        }


        /// <summary>
        /// Loops over all the children of this GameObject and runs '<c>GameObject.DestroyImmediate</c>' for each
        /// </summary>
        public static void DestroyChildrenImmediate(this GameObject gameObject)
        {
            foreach(Transform child in gameObject.transform) 
                GameObject.DestroyImmediate(child.gameObject);
        }


        /// <summary>
        /// Loops '<c>GameObject.EnsureComponent</c>' for all passed components
        /// </summary>
        /// <param name="types">Array of components to ensure</param>
        public static void EnsureComponents(this GameObject obj, params Type[] types) => types.ForEach(t => obj.EnsureComponent(t));


        /// <summary>
        /// Loops '<c>GameObject.EnsureComponent</c>' for all passed components
        /// </summary>
        /// <param name="types">Array of components to ensure</param>
        public static void EnsureComponents<T>(this GameObject obj, params Type[] types) where T : Component
        {
            obj.EnsureComponent<T>();
            types.ForEach(t => obj.EnsureComponent(t));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="component"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component, bool includeInactive = false) where T : Component
        {
            component = null;

            if(gameObject == null)
                return false;

            return(component = includeInactive ? gameObject.GetComponentInChildren<T>(true) : gameObject.GetComponentInChildren<T>()) != null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static bool TryGetComponents<T>(this GameObject gameObject, out T[] components) where T : Component
        {
            components = null;

            if(gameObject == null)
                return false;

            return(components = gameObject.GetComponents<T>()).Length > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="components"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] components, bool includeInactive = false) where T : Component
        {
            components = null;

            if(gameObject == null)
                return false;

            return(components = includeInactive ? gameObject.GetComponentsInChildren<T>(true) : gameObject.GetComponentsInChildren<T>()).Length > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = null;

            if(gameObject == null)
                return false;

            var parent = gameObject.transform.parent;

            if(parent == null)
                return false;

            return(component = parent.GetComponent<T>()) != null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static bool TryGetComponentsInParent<T>(this GameObject gameObject, out T[] components) where T : Component
        {
            components = null;

            if(gameObject == null)
                return false;

            var parent = gameObject.transform.parent;

            if(parent == null)
                return false;

            return(components = parent.gameObject.GetComponents<T>()).Length > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool TryGetComponentEverywhere<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = null;

            if(gameObject == null)
                return false;

            if(gameObject.TryGetComponent(out component))
            {
                Logfile.Warning($"[TryGetComponentEverywhere]: Found component '{typeof(T).Name}' in root (TryGetComponent)");
                return true;
            }
            else if(gameObject.TryGetComponentInChildren(out component))
            {
                Logfile.Warning($"[TryGetComponentEverywhere]: Found component '{typeof(T).Name}' in children (TryGetComponentInChildren)");
                return true;
            }
            else if(gameObject.TryGetComponentInParent(out component))
            {
                Logfile.Warning($"[TryGetComponentEverywhere]: Found component '{typeof(T).Name}' in parent (TryGetComponentInParent)");
                return true;
            }

            return false;
        }
    }
}