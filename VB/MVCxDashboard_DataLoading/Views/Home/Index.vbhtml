﻿@Code
    ViewBag.Title = "Index"
End Code

<div style="position: absolute; left: 0; right: 0; top:0; bottom:0;">
    @Html.DevExpress().Dashboard(Sub(settings)
                                      settings.Name = "Dashboard"
                                      settings.ControllerName = "DefaultDashboard"
                                      settings.Width = Unit.Percentage(100)
                                      settings.Height = Unit.Percentage(100)
                                  End Sub).GetHtml()
</div>