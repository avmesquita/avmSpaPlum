Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports Data
Imports DayPilot.Web.Mvc
Imports DayPilot.Web.Mvc.Enums
Imports DayPilot.Web.Mvc.Events.Calendar
Imports DayPilot.Web.Mvc.Data

Namespace TutorialCS.Controllers
    Public Class BackendController
        Inherits Controller

        '
        ' GET: /Scheduler/

        Public Function Day() As ActionResult
            Return (New Dpc()).CallBack(Me)
        End Function

        Public Function Week() As ActionResult
            Return (New Dpc()).CallBack(Me)
        End Function

        Public Function Month() As ActionResult
            Return (New Dpm()).CallBack(Me)
        End Function

        Private Class Dpc
            Inherits DayPilotCalendar

            Protected Overrides Sub OnInit(ByVal e As InitArgs)
                Update(CallBackUpdateType.Full)
            End Sub

            Protected Overrides Sub OnEventResize(ByVal e As EventResizeArgs)
                CType(New EventManager(), EventManager).EventMove(e.Id, e.NewStart, e.NewEnd)
                Update()
            End Sub

            Protected Overrides Sub OnEventMove(ByVal e As EventMoveArgs)
                CType(New EventManager(), EventManager).EventMove(e.Id, e.NewStart, e.NewEnd)
                Update()
            End Sub

            Protected Overrides Sub OnTimeRangeSelected(ByVal e As TimeRangeSelectedArgs)
                CType(New EventManager(), EventManager).EventCreate(e.Start, e.End, "New event")
                Update()
            End Sub

            Protected Overrides Sub OnBeforeEventRender(ByVal e As BeforeEventRenderArgs)
                e.Areas.Add((New Area()).Right(3).Top(3).Width(15).Height(15).CssClass("event_action_delete").JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"))
            End Sub

            Protected Overrides Sub OnCommand(ByVal e As CommandArgs)
                Select Case e.Command
                    Case "navigate"
                        StartDate = CDate(e.Data("day"))
                        Update(CallBackUpdateType.Full)
                    Case "refresh"
                        Update(CallBackUpdateType.EventsOnly)
                    Case "delete"
                        CType(New EventManager(), EventManager).EventDelete(CStr(e.Data("e")("id")))
                        Update(CallBackUpdateType.EventsOnly)
                End Select
            End Sub

            Protected Overrides Sub OnFinish()
                If UpdateType = CallBackUpdateType.None Then
                    Return
                End If

                Events = (New EventManager()).FilteredData(StartDate, StartDate.AddDays(Days)).AsEnumerable()

                DataIdField = "id"
                DataTextField = "name"
                DataStartField = "eventstart"
                DataEndField = "eventend"
            End Sub
        End Class


        Private Class Dpm
            Inherits DayPilotMonth

            Protected Overrides Sub OnInit(ByVal e As DayPilot.Web.Mvc.Events.Month.InitArgs)
                Update()
            End Sub

            Protected Overrides Sub OnEventResize(ByVal e As DayPilot.Web.Mvc.Events.Month.EventResizeArgs)
                CType(New EventManager(), EventManager).EventMove(e.Id, e.NewStart, e.NewEnd)
                Update()
            End Sub

            Protected Overrides Sub OnEventMove(ByVal e As DayPilot.Web.Mvc.Events.Month.EventMoveArgs)
                CType(New EventManager(), EventManager).EventMove(e.Id, e.NewStart, e.NewEnd)
                Update()
            End Sub

            Protected Overrides Sub OnTimeRangeSelected(ByVal e As DayPilot.Web.Mvc.Events.Month.TimeRangeSelectedArgs)
                CType(New EventManager(), EventManager).EventCreate(e.Start, e.End, "New event")
                Update()
            End Sub

            Protected Overrides Sub OnBeforeEventRender(ByVal e As DayPilot.Web.Mvc.Events.Month.BeforeEventRenderArgs)
                e.Areas.Add((New Area()).Right(3).Top(3).Width(15).Height(15).CssClass("event_action_delete").JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"))
            End Sub

            Protected Overrides Sub OnCommand(ByVal e As DayPilot.Web.Mvc.Events.Month.CommandArgs)
                Select Case e.Command
                    Case "navigate"
                        StartDate = CDate(e.Data("day"))
                        Update(CallBackUpdateType.Full)
                    Case "refresh"
                        Update(CallBackUpdateType.EventsOnly)
                    Case "delete"
                        CType(New EventManager(), EventManager).EventDelete(CStr(e.Data("e")("id")))
                        Update(CallBackUpdateType.EventsOnly)
                End Select
            End Sub

            Protected Overrides Sub OnFinish()
                If UpdateType = CallBackUpdateType.None Then
                    Return
                End If

                Events = (New EventManager()).FilteredData(VisibleStart, VisibleEnd).AsEnumerable()

                DataIdField = "id"
                DataTextField = "name"
                DataStartField = "eventstart"
                DataEndField = "eventend"
            End Sub
        End Class


    End Class
End Namespace
