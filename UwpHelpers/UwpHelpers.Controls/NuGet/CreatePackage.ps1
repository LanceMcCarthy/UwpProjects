# Any assembly matching this filter will be transformed into an AnyCPU assembly.
$referenceDllFilter = "UwpHelpers.Controls.dll"

$programfilesx86 = "${Env:ProgramFiles(x86)}"
$corflags = Join-Path $programfilesx86 "Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\x64\CorFlags.exe"

If (!(Test-Path $corflags))
{
	Throw "Unable to find CorFlags.exe"
}

$solutionRoot = Resolve-Path ..\..
$topLevelDirectories = Get-ChildItem $solutionRoot -Directory
$binDirectories = $topLevelDirectories | %{ Get-ChildItem $_.FullName -Directory -Filter "bin" }

# Create reference assemblies, because otherwise the NuGet packages cannot be used.
# This creates them for all outputs that match the filter, in all output directories of all projects.
# It's a bit overkill but who cares - the process is very fast and keeps the script simple.
Foreach ($bin in $binDirectories)
{
	$x86 = Join-Path $bin.FullName "x86"
	$any = Join-Path $bin.FullName "Reference"
		
	If (!(Test-Path $x86))
	{
		Write-Host "Skipping reference assembly generation for $($bin.FullName) because it has no x86 directory."
		continue;
	}
	
	if (Test-Path $any)
	{
		Remove-Item -Recurse $any
	}
	
	New-Item $any -ItemType Directory
	New-Item "$any\Release" -ItemType Directory
	
	$dlls = Get-ChildItem "$x86\Release" -File -Filter $referenceDllFilter
	
	Foreach ($dll in $dlls)
	{
		Copy-Item $dll.FullName "$any\Release"
	}
	
	$dlls = Get-ChildItem "$any\Release" -File -Filter $referenceDllFilter
	
	Foreach ($dll in $dlls)
	{
		Write-Host "Converting to AnyCPU: $dll"
		
		& $corflags /32bitreq- $($dll.FullName)
	}
}

# Delete any existing output.
Remove-Item *.nupkg

# Create new packages for any nuspec files that exist in this directory.
Foreach ($nuspec in $(Get-Item *.nuspec))
{
	.\NuGet.exe pack "$nuspec"
}