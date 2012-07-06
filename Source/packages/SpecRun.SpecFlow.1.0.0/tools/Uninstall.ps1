param($installPath, $toolsPath, $package, $project)

$version = ""

$packages = Get-Package -Filter SpecFlow
foreach ($package in $packages)
{
	# there is a bug in NuGet: the package id for all returned packages is "SpecFlow", so filtering that is not enough
	if ($package.Id = "SpecFlow" -and $package.ProjectUrl.ToString().Contains("specflow.org")) 
	{ 
		$version = $package.Version.ToString() 
	}
}

if ($version -ne "") 
{ 
	$specflowPackageToolsFolder = $installPath + "\..\SpecFlow." + $version + "\tools"

	$specFlowPlugin = $specflowPackageToolsFolder + "\TechTalk.SpecRun.SpecFlowPlugin.Generator.dll"

	Remove-Item $specFlowPlugin
}
