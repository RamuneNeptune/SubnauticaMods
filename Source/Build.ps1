param(
    [ValidateScript({ Test-Path $_ -PathType Leaf })]
    [string]$SourceFile,
	
    [ValidateScript({ Test-Path $_ -PathType Leaf })]
    [string]$JsonFile
)

$content = Get-Content $SourceFile -Raw

if($content -match 'Version\s*=\s*"([^"]+)"') {
    $version = $Matches[1]
}
else {
    throw "Failed to extract version from '$SourceFile'"
}

$json = Get-Content $JsonFile | ConvertFrom-Json
$json.LatestVersion = $version

$jsonString = $json | ConvertTo-Json -Depth 2
$jsonString = $jsonString -replace ' {2}', ' '
$jsonString = $jsonString -replace ' {4}', '  '

Set-Content -Path $JsonFile -Value $jsonString -NoNewline

Write-Output ""
Write-Output "Updated '$JsonFile' version to '$version'"
Write-Output ""