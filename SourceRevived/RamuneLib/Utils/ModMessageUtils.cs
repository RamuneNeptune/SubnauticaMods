

namespace RamuneLib.Utils
{
    internal class ModMessageUtils
    {
        internal static void RegisterGlobalInbox(string address, Action<object[]> callback)
        {
            var inbox = new ModInbox(address, true);

            ModMessageSystem.RegisterInbox(inbox);

            var reader = new BasicModMessageReader(address, callback);

            inbox.AddMessageReader(reader);
        }
    }
}