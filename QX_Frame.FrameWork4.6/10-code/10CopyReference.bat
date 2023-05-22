@echo off

echo *******************Make QX_Frame.App.Base**************************

xcopy  %cd%"\QX_Frame.App.Base\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.Base" /y /E /S

echo *******************Make QX_Frame.App.Form**************************

xcopy  %cd%"\QX_Frame.App.Form\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.Form" /y /E /S

echo *******************Make QX_Frame.App.Web**************************

xcopy  %cd%"\QX_Frame.App.Web\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.Web" /y /E /S

echo *******************Make QX_Frame.App.WebApi**************************

xcopy  %cd%"\QX_Frame.App.WebApi\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.WebApi" /y /E /S

pause
