<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="@Url.Content("~/Media/layout.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Scripts/jquery-1.4.1.min.js")" type="text/javascript"></script>

    <script src="@Url.Content("~/Scripts/DayPilot/daypilot-all.min.js?v=1")" type="text/javascript"></script>
    <link href="@Url.Content("~/Themes/calendar_white.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Themes/month_white.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Themes/navigator_white.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Themes/areas.css")" rel="stylesheet" type="text/css" />    
</head>

<body>
        <div id="header">
			<div class="bg-help">
				<div class="inBox">
					<h1 id="logo"><a href='http://code.daypilot.org/33944/event-calendar-day-week-month-for-asp-net-mvc'>Event Calendar Day/Week/Month View for ASP.NET MVC 4</a></h1>
					<p id="claim"><a href="http://mvc.daypilot.org/">DayPilot for ASP.NET MVC</a> - AJAX Calendar/Scheduling Controls for ASP.NET MVC</p>
					<hr class="hidden" />
				</div>
			</div>
        </div>
        <div class="shadow"></div>
        <div class="hideSkipLink">
        </div>
        <div class="main">
            @RenderBody()
        </div>
        <div class="clear">
        </div>
</body>
</html>
