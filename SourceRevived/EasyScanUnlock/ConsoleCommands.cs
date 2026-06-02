

namespace Ramune.EasyScanUnlock
{
    public static class ConsoleCommands
    {
        [ConsoleCommand("retroactiveunlock")]
        public static string Retroactiveunlock()
        {
            List<TechType> blueprintsToUnlock = [];

            var entries = PDAScanner.GetAllEntriesData();

            while(entries.MoveNext())
            {
                var current = entries.Current;
                var entryData = current.Value;

                if(entryData == null || entryData.blueprint == TechType.None || entryData.totalFragments <= 1)
                    continue;

                if(!PDAScanner.GetPartialEntryByKey(current.Key, out PDAScanner.Entry entry))
                    continue;

                if(entry.unlocked <= 0)
                    continue;

                blueprintsToUnlock.Add(entryData.blueprint);
            }

            if(blueprintsToUnlock.Count == 0)
                return "No partially scanned blueprints were found";

            int affectedCount = 0;

            foreach(var blueprint in blueprintsToUnlock)
            {
                if(KnownTech.Contains(blueprint))
                {
                    PDAScanner.CompleteAllEntriesWhichUnlocks(blueprint);
                    affectedCount++;
                    continue;
                }

                if(KnownTech.Add(blueprint, verbose: true))
                    affectedCount++;
            }

            return $"Retroactively unlocked {affectedCount} blueprint(s)";
        }
    }
}