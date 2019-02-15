@imports DayPilot.Web.Mvc
@imports DayPilot.Web.Mvc.Events.Calendar
@imports DayPilot.Web.Mvc.Enums.Calendar

@Code
    ViewData("Title") = "Event Calendar Day/Week/Month View for ASP.NET MVC 4"
End Code

<style>
    #toolbar 
    {
    	margin-bottom: 10px;
    }
    
    #toolbar a 
    {
    	display: inline-block;
    	height: 20px;
    	text-decoration: none;
    	padding: 5px;
    	color: #666;
    	border: 1px solid #aaa;

	    background: -webkit-gradient(linear, left top, left bottom, from(#fafafa), to(#e2e2e2));
	    background: -webkit-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: -moz-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: -ms-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: -o-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: linear-gradient(top, #fafafa 0%, #e2e2e2);
	    filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#fafafa", endColorStr="#e2e2e2");    	
    }

</style>

<div style="float:left; width: 150px">
@Html.DayPilotNavigator("dp_navigator", New DayPilotNavigatorConfig With
{
    .CssOnly = True,
    .CssClassPrefix = "navigator_white",
    .ShowMonths = 3,
    .SkipMonths = 3
})
</div>
<div id="tabs" style="margin-left:150px">
    <div id="toolbar">
    <a href="#" id="toolbar_day">Day</a>
    <a href="#" id="toolbar_week">Week</a>
    <a href="#" id="toolbar_month">Month</a>
    </div>

    <div>
    @Html.DayPilotCalendar("dp_day", New DayPilotCalendarConfig With
    {
        .BackendUrl = Url.Content("~/Backend/Day"),
        .EventResizeHandling = EventResizeHandlingType.CallBack,
        .EventMoveHandling = EventMoveHandlingType.CallBack,
        .CssOnly = True,
        .CssClassPrefix = "calendar_white",
        .ViewType = DayPilot.Web.Mvc.Enums.Calendar.ViewType.Day,
        .EventClickHandling = EventClickHandlingType.JavaScript,
        .EventClickJavaScript = "edit(e.id())",
        .TimeRangeSelectedHandling = TimeRangeSelectedHandlingType.JavaScript,
        .TimeRangeSelectedJavaScript = "create(start, end)"
    })
    @Html.DayPilotCalendar("dp_week", New DayPilotCalendarConfig With
    {
        .BackendUrl = Url.Content("~/Backend/Week"),
        .EventResizeHandling = EventResizeHandlingType.CallBack,
        .EventMoveHandling = EventMoveHandlingType.CallBack,
        .CssOnly = True,
        .CssClassPrefix = "calendar_white",
        .ViewType = DayPilot.Web.Mvc.Enums.Calendar.ViewType.Week,
        .EventClickHandling = EventClickHandlingType.JavaScript,
        .EventClickJavaScript = "edit(e.id())",
        .TimeRangeSelectedHandling = TimeRangeSelectedHandlingType.JavaScript,
        .TimeRangeSelectedJavaScript = "create(start, end)"
    })
    @Html.DayPilotMonth("dp_month", new DayPilotMonthConfig With
    {
        .BackendUrl = Url.Content("~/Backend/Month"),
        .EventResizeHandling = DayPilot.Web.Mvc.Events.Month.EventResizeHandlingType.CallBack,
        .EventMoveHandling = DayPilot.Web.Mvc.Events.Month.EventMoveHandlingType.CallBack,
	    .CssOnly = true,
        .CssClassPrefix = "month_white",
        .EventHeight = 25,
        .EventClickHandling = DayPilot.Web.Mvc.Events.Month.EventClickHandlingType.JavaScript,
        .EventClickJavaScript = "edit(e.id())",
        .TimeRangeSelectedHandling = DayPilot.Web.Mvc.Events.Month.TimeRangeSelectedHandlingType.JavaScript,
        .TimeRangeSelectedJavaScript = "create(start, end)"
    })
    </div>
</div>

<script type="text/javascript">

	function edit(id) {
	    var modal = new DayPilot.Modal();
        modal.closed = function() { 
            if(this.result == "OK") { 
                switcher.active.control.commandCallBack('refresh'); 
            }
        };
	    modal.showUrl("Event/Edit/" + id);
	}

	function create(start, end) {
	    var modal = new DayPilot.Modal();
	    modal.closed = function () {
	        if (this.result == "OK") {
	            switcher.active.control.commandCallBack('refresh');
	        }
	        switcher.active.control.clearSelection();
      };
	    modal.showUrl("Event/Create?start=" + start + "&end=" + end);
	}


    var switcher = new DayPilot.Switcher();

    switcher.addButton("toolbar_day", dp_day);
    switcher.addButton("toolbar_week", dp_week);
    switcher.addButton("toolbar_month", dp_month);

    switcher.addNavigator(dp_navigator);

    switcher.show(dp_day);

</script>