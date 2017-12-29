var flashvars = {}; 
flashvars.SwfFile = escape(swfFile);
flashvars.Scale = 0.6;
flashvars.StartAtPage = 1;
flashvars.ZoomTransition = "easeOut";
flashvars.ZoomTime = 0.5;
flashvars.ZoomInterval = 0.1;
flashvars.FitPageOnLoad = true;
flashvars.FitWidthOnLoad = true;
flashvars.PrintEnabled = false;
flashvars.FullScreenAsMaxWindow = false;
flashvars.ProgressiveLoading = true;
flashvars.MinZoomSize = 0.2;
flashvars.MaxZoomSize = 5;
flashvars.SearchMatchAll = false;
flashvars.InitViewMode = 'Portrait';

//flashvars.PrintToolsVisible= true;
flashvars.PrintEnabled = true;
flashvars.ViewModeToolsVisible = true;
flashvars.ZoomToolsVisible = true;
flashvars.FullScreenVisible = true;
flashvars.NavToolsVisible = true;
flashvars.CursorToolsVisible = true;
flashvars.SearchToolsVisible = true;
flashvars.localeChain = "en_US";

var params = {};
params.quality = "high";
params.bgcolor = "#488e3e";
params.allowscriptaccess = "sameDomain";
params.allowfullscreen = "true";

var attributes = {};
attributes.id = "InsusDocumentViewer";
attributes.name = "InsusDocumentViewer";
attributes.codeBase = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0";

swfobject.embedSWF(
                "Swf/InsusDocumentViewer.swf", "flashContent",
                _width, _height, "10.0.0", "playerProductInstall.swf",
                flashvars, params, attributes);
swfobject.createCSS("#flashContent", "display:block;text-align:left;");