$sourceDirectory = $PSScriptRoot
$repositoryDirectory = Split-Path $sourceDirectory -Parent
$releaseRoot = Join-Path $sourceDirectory ".releases"
$openModsDirectory = Join-Path $repositoryDirectory ".openmods"
$mutex = New-Object System.Threading.Mutex($false, "Ramune.SubnauticaMods.OpenMods")

function Split-ModName {
    param([string]$Name)

    $value = $Name -creplace '([A-Z]+)([A-Z][a-z])', '$1 $2'
    $value = $value -creplace '([a-z0-9])([A-Z])', '$1 $2'
    $value = $value -replace '\s+', ' '

    return $value.Trim()
}

function Get-Slug {
    param([string]$Name)

    $slug = Split-ModName $Name
    $slug = $slug.ToLowerInvariant()
    $slug = $slug -replace '[^a-z0-9]+', '-'
    $slug = $slug.Trim('-')

    return $slug
}

function ConvertTo-JsonString {
    param([string]$Value)

    $escaped = $Value.Replace('\', '\\').Replace('"', '\"')

    return """$escaped"""
}

try {
    $lockTaken = $false

    try {
        $lockTaken = $mutex.WaitOne()

        if(Test-Path $releaseRoot -PathType Container) {
            New-Item -ItemType Directory -Force -Path $openModsDirectory | Out-Null
            Get-ChildItem $openModsDirectory -Filter "openmods-*.json" -File | Remove-Item -Force -ErrorAction SilentlyContinue

            Get-ChildItem $releaseRoot -Directory | Sort-Object Name | ForEach-Object {
                $releaseDirectory = $_
                $modName = $releaseDirectory.Name
                $versionJson = Join-Path (Join-Path $sourceDirectory $releaseDirectory.Name) "Version.json"

                if(Test-Path $versionJson -PathType Leaf) {
                    $versionInfo = Get-Content $versionJson -Raw | ConvertFrom-Json

                    if($versionInfo.ModName) {
                        $modName = [string]$versionInfo.ModName
                    }
                }

                $assetGlob = "$modName-*.zip"
                $manifestPath = Join-Path $openModsDirectory "openmods-$modName.json"
                $manifestJson = @'
{{
  "$schema": {0},
  "schemaVersion": 2,
  "slug": {1},
  "name": {2},
  "supportedGameId": 1,
  "releaseAssets": [
    {3}
  ],
  "primaryAsset": {3},
  "install": {{
    "path": "BepInEx/plugins"
  }}
}}
'@ -f `
                (ConvertTo-JsonString "https://openmods.net/manifest.schema.json"),
                (ConvertTo-JsonString (Get-Slug $modName)),
                (ConvertTo-JsonString (Split-ModName $modName)),
                (ConvertTo-JsonString $assetGlob)

                Set-Content -Path $manifestPath -Value $manifestJson -NoNewline
            }
        }
    }
    finally {
        if($lockTaken) {
            $mutex.ReleaseMutex() | Out-Null
        }
    }
}
finally {
    $mutex.Dispose()
}