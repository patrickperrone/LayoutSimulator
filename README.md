# LayoutSimulator
In a nutshell: this module will help you reduce the footprint of test data you must maintain in Sitecore.

As Sitecore developers we must often develop layouts and renderings. If we wish to test these components 
from a browser we must create a page in Sitecore that consumes our component(s). Over time this test data
can accrue into a large library that must be managed and shared amongst different test environments like DEV and QA.

Using Layout Simulator lets you save your test data -- Sitecore's layout definitions in raw xml -- as a text file 
in source control. You can then POST this information to Layout Simulator's service end point and view a completely
assembled page as if the page actually existed in Sitecore. Since Layout Simulator uses a RESTful web service,
it is easy to script browser-based functional tests using Layout Simulator.

### Great, How Can I Get Started?
For a fast demonstration, after you install Layout Simulator browse to ```/SimulationBuilder``` and enter the
following layout xml:
```
<r xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<d id="{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}" l="{03547A29-7056-4278-94A3-3EB5BE765660}">
<r id="{C35D86EE-3DF1-48D0-84C0-2385DEF0C909}" ph="main" uid="{D38C8F63-5305-4167-9C13-B32093FA7232}" />
</d></r>
``` 
Click enter and see the double rainbow. What does it MEAN!? :)

The easiest way to begin experimenting with your own renderings is to get the layout definition of an existing 
page. In Content Editor, simply navigate to a page and click on the **Get Layout** button in the Presentation tab.

![ribbon-button]

Modify the layout definition xml as you see fit and click the **Simulate Layout!** button.


### Ok, Now What?
Once you have one or more simulated layouts, you can start automating your functional tests. Layout Simulator exposes
two routes that use may use.
<br />
<br />
##### Simulate a Layout
Issue a POST request to ```/services/layoutsimulator/simulate``` and pass it a JSON object. You may optionally use
a query string parameter to override the default simulation host page: ```/services/layoutsimulator/simulate?hostPageUrl=http://simtest/testhost```

Example:
```
POST /services/layoutsimulator/simulate HTTP/1.1
Host: simtest
Accept: application/json, text/javascript, */*
Content-Type: application/json
Cache-Control: no-cache

{
    "LayoutToSimulate" : "<r xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">
<d id=\"{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}\" l=\"{03547A29-7056-4278-94A3-3EB5BE765660}\">
<r id=\"{C35D86EE-3DF1-48D0-84C0-2385DEF0C909}\" ph=\"main\" uid=\"{D38C8F63-5305-4167-9C13-B32093FA7232}\" />
</d></r>"
}
```
<br />
##### Test If Layout Simulator Is Installed
Issue a GET request to ```/services/layoutsimulator/simulate```

### Advanced Usage
Layout Simulator installs two basic page types and registers them as defaults:
- SimulationHost page: This page acts as a dummy item. It uses an MVC layout which ensures that Sitecore process the
page as an MVC page. This enables Layout Simulator to dynamically replace the dummy layout with the desired layout and 
renderings.
- SimulationBuilder page: This page exposes a simple form that allows you to create or modify a layout definition.

You may move or create copies of these pages. Layout Simulator keeps track of the 'default' versions of these pages on
a Settings item located at ```/sitecore/system/Settings/Feature/LayoutSimulator/Settings```. Simply edit the field values
to change the defaults as you see fit.

Configuration Settings:
- Service Route Prefix: if necessary you may change the prefix by patching the ```Sitecore.Feature.LayoutSimulator.RoutePrefix``` setting.
- Query String Key: behind the scenes, Layout Simulator uses a query string key internally when responding to a simulate
request. In the unlikely event this query string key collides with one of your own, you may patch the ```Sitecore.Feature.LayoutSimulator.QueryStringParameterKey.SimulatorLayoutId```
setting

### Installation
1. Use Sitecore's Installation Wizard to Install the package **LayoutSimlulator-1.0.0.zip**
2. Publish.

### Limitations
- Currently, Layout Simulator only supports MVC!
- I've developed and tested against Sitecore 8.1. Sitecore 7.5 and 
8.0 should also work. Earlier versions will be an exercize in trial and error. ;)


[ribbon-button]: https://github.com/patrickperrone/LayoutSimulator/blob/master/get-layout-button.png "Get Layout button"
