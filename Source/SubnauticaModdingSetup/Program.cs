namespace Ramune.SubnauticaModdingSetup;


public static class Program
{
    public static HttpClient Client = new();

    public static string LogfilePath = Path.Combine(Environment.CurrentDirectory, "Ramune.SubnauticaModdingSetup.log");

    public static string? SubnauticaPath { get; private set; }

    public static bool OneClick { get; private set; }


    [STAThread]
    public static void Main()
    {
        if(File.Exists(LogfilePath)) 
            File.Delete(LogfilePath);         
        
        Log("===== Ramune.SubnauticaModdingSetup Installer =====");

        Log($"Writing logfile to: \"{LogfilePath}\"");

        if(Process.GetProcessesByName("Subnautica").Length > 0)
        {
            Log("Subnautica is currently running. Please close the game before proceeding. Exiting installer...", "WARN");
            MessageBox.Show("Subnautica is currently running. Please close the game before proceeding.\n\nClick OK to Exit.", "Subnautica is running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        OneClick = MessageBox.Show("Would you like to have a one-click install, or a step-by-step (prompts before each step) install?\n\nClick YES for one-click, or NO for step-by-step installation", "Ramune.SubnauticaModdingSetup", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        Log($"Installation mode selected: {(OneClick ? "One-click" : "Step-by-step")}");

        SubnauticaPath = GetSubnauticaPath();

        if(string.IsNullOrEmpty(SubnauticaPath))
            return;

        try
        {
            StartInstallation().GetAwaiter().GetResult();
        }
        catch(Exception ex)
        {
            Log($"A unexpected error has occurred:\n{ex.Message}", "ERROR");
            MessageBox.Show($"An unexpected error has occurred:\n\n{ex.Message}", "Installation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    public static string? GetSubnauticaPath()
    {
        using var fbd = new FolderBrowserDialog 
        { 
            Description = "Select your Subnautica installation folder (containing \"Subnautica.exe\")." 
        };

        while(fbd.ShowDialog() == DialogResult.OK)
        {
            if(File.Exists(Path.Combine(fbd.SelectedPath, "Subnautica.exe")))
            {
                Log($"Subnautica path selected: \"{fbd.SelectedPath}\"");
                return fbd.SelectedPath;
            }

            if(MessageBox.Show("The selected folder does not contain \"Subnautica.exe\". Please select the correct folder.\n\nClick OK to Reselect, or CANCEL to Exit.", "\"Subnautica.exe\" not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) 
                break;
        }

        Log("Folder browser interaction cancelled by user. Exiting installer...");
        return null;
    }


    public static async Task StartInstallation()
    {
        if(!OneClick && MessageBox.Show("The latest version of Tobey's BepInEx Pack for Subnautica and Nautilus will now be downloaded and installed.\n\nClick OK to Continue, or CANCEL to Exit.", "Ready to download", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
        {
            Log("Installation of Tobey's BepInEx Pack for Subnautica and Nautilus cancelled by user. Exiting installer...", "WARN");
            return;
        }

        Log("Fetching GitHub releases...");
        var githubClient = new GitHubClient(new ProductHeaderValue("Ramune.SubnauticaModdingSetup"));

        var options = new ApiOptions 
        { 
            PageSize = 1, PageCount = 1, StartPage = 1 
        };

        var bepinexRelease = await githubClient.Repository.Release.GetAll("toebeann", "BepInEx.Subnautica", options);
        var nautilusRelease = await githubClient.Repository.Release.GetAll("SubnauticaModding", "Nautilus", options);

        if(bepinexRelease.Count < 1 || nautilusRelease.Count < 1)
        {
            var failed = (bepinexRelease.Count < 1 && nautilusRelease.Count < 1) ? "BepInEx and Nautilus" : (bepinexRelease == null ? "BepInEx" : "Nautilus");
            Log($"Failed to retrieve the latest release of {failed}. Please check your internet connection and try again. Exiting installer...", "ERROR");
            MessageBox.Show($"Failed to retrieve the latest release of {failed}. Please check your internet connection and try again. Exiting installer...", "Failed to retrieve releases", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var bepinexAsset = bepinexRelease[0].Assets.FirstOrDefault();
        var nautilusAsset = nautilusRelease[0].Assets.FirstOrDefault(x => x.Name.Contains("_SN"));

        if(bepinexAsset == null || nautilusAsset == null)
        {
            var failed = (bepinexAsset == null && nautilusAsset == null) ? "BepInEx and Nautilus" : (bepinexAsset == null ? "BepInEx" : "Nautilus");
            Log($"Failed to retrieve the latest release of {failed}. Please check your internet connection and try again. Exiting installer...", "ERROR");
            MessageBox.Show($"Failed to retrieve the latest release of {failed}. Please check your internet connection and try again. Exiting installer...", "Failed to retrieve releases", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        await DownloadAndExtract(bepinexAsset.BrowserDownloadUrl, bepinexAsset.Name, SubnauticaPath!, "Tobey's BepInEx Pack for Subnautica");
        
        if(!OneClick)
            MessageBox.Show($"Tobey's BepInEx Pack for Subnautica ({bepinexAsset.Name}) has been successfully installed.\n\nClick OK to proceed with the Nautilus installation.", "BepInEx installed", MessageBoxButtons.OK, MessageBoxIcon.Information);

        await DownloadAndExtract(nautilusAsset.BrowserDownloadUrl, nautilusAsset.Name, Path.Combine(SubnauticaPath!, "BepInEx"), "Nautilus");

        if(!OneClick)
            MessageBox.Show($"Nautilus ({nautilusAsset.Name}) has been successfully installed.\n\nClick OK to finish.", "Nautilus installed", MessageBoxButtons.OK, MessageBoxIcon.Information);

        Log("Installation complete! Exiting installer...");
        MessageBox.Show("Installation complete! You can now install mods for Subnautica.", "Installation complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    public static async Task DownloadAndExtract(string url, string filename, string path, string displayName)
    {
        string temp = Path.Combine(Path.GetTempPath(), filename);

        Log($"Downloading file from \"{url}\" to \"{temp}\"");
        using(var stream = await Client.GetStreamAsync(url))
        using(var file = File.Create(temp))
            await stream.CopyToAsync(file);

        Log($"Download completed: \"{temp}\"");

        Log($"Extracting {displayName} to: \"{path}\"");
        ZipFile.ExtractToDirectory(temp, path, true);

        Log($"Extraction completed, deleting zip file: \"{temp}\"");
        File.Delete(temp);
    }


    public static void Log(string message, string level = "INFO")
    {
        string prefix = $"[{DateTime.Now:HH:mm:ss}] [{level}] ";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(prefix);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message);
        File.AppendAllText(LogfilePath, $"{prefix} {message}\n");
    }
}