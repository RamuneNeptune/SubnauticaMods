

namespace RamuneLib.Utils
{
    internal static class PingUtils
    {
        /// <summary>
        /// 
        /// </summary>
        internal static Dictionary<string, PingInstance> CachedPings = new();


        /// <summary>
        /// 
        /// </summary>
        internal enum PingColor
        {
            Blue,
            Orange,
            Red,
            Cyan,
            Yellow,
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static PingType RegisterPingType(string name)
        {
            return EnumHandler.AddEntry<PingType>(name)
                .WithIcon(ImageUtils.GetSprite(name)).Value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        internal static PingType RegisterPingType(string name, Sprite sprite)
        {
            return EnumHandler.AddEntry<PingType>(name)
                .WithIcon(sprite).Value;
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static PingType GetModdedPingType(string name)
        {
            if(EnumHandler.TryGetValue<PingType>(name, out var pingType))
                return pingType;

            Logfile.Warning($"PingUtils.GetPingType: Couldn't find PingType with name '{name}'");
            return PingType.None;
        }


        /// <summary>
        /// Create a new ping instance and add it to the CachedPings dictionary.
        /// </summary>
        /// <param name="id">Unique identifier for the ping.</param>
        /// <param name="label">Label for the ping</param>
        /// <param name="color">Color of the ping using the games existing ping color set</param>
        /// <param name="type">Type of the ping, e.g. seamoth, cyclops, signal..</param>
        /// <param name="visible">Initial visibility state of the ping</param>
        /// <returns>The created PingInstance.</returns>
        internal static PingInstance Create(string id, string label, PingColor color, PingType type, bool visible = true)
        {
            if(CachedPings.ContainsKey(id))
                throw new ArgumentException($"Ping with ID '{id}' already exists in cache");

            PingInstance ping = new();
            ping.SetColor((int)color);
            ping.SetLabel(label);
            ping.SetType(type);
            ping.SetVisible(visible);

            CachedPings.Add(id, ping);
            return ping;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ping"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        internal static PingInstance WithParent(this PingInstance ping, Transform parent)
        {
            ping.transform.parent = parent;
            return ping;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static PingInstance Get(string id)
        {
            return CachedPings.ContainsKey(id) ? CachedPings[id] : null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        internal static void Remove(string id)
        {
            if(CachedPings.ContainsKey(id))
            {
                PingInstance ping = CachedPings[id];
                GameObject.Destroy(ping);
                CachedPings.Remove(id);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        internal static void ClearCache()
        {
            foreach(var ping in CachedPings.Values)
                GameObject.Destroy(ping);
            
            CachedPings.Clear();
        }
    }
}