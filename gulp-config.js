module.exports = function () {
	var instanceRoot = "C:\\inetpub\\SIM\\lsim";
	var config = {
		websiteRoot: instanceRoot + "\\Website",
		sitecoreLibraries: instanceRoot + "\\Website\\bin",
		sitecoreLibDestination: "./lib/Sitecore82x",
		licensePath: instanceRoot + "\\Data\\license.xml",
		solutionName: "Layout Simulator",
		buildConfiguration: "Debug82x",
		buildToolsVersion: 15.0,
		buildMaxCpuCount: 0,
		buildVerbosity: "minimal",
		buildPlatform: "Any CPU",
		publishPlatform: "AnyCpu",
		runCleanBuilds: false
	};
	return config;
}