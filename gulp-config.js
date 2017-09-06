module.exports = function () {
	var instanceRoot = "C:\\inetpub\\SIM\\lsim-sc81u3";
	var config = {
		websiteRoot: instanceRoot + "\\Website",
		sitecoreLibraries: instanceRoot + "\\Website\\bin",
		sitecoreLibDestination: "./lib/Sitecore81x",
		licensePath: instanceRoot + "\\Data\\license.xml",
		solutionName: "Layout Simulator",
		buildConfiguration: "Debug81x",
		buildToolsVersion: 15.0,
		buildMaxCpuCount: 0,
		buildVerbosity: "minimal",
		buildPlatform: "Any CPU",
		publishPlatform: "AnyCpu",
		runCleanBuilds: false
	};
	return config;
}