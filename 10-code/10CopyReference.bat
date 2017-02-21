@echo off

echo *******************Make QX_Frame.App.Base**************************

xcopy  %cd%"\QX_Frame.App.Base\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.Base" /y /E /S

echo *******************Make QX_Frame.App.Form**************************

xcopy  %cd%"\QX_Frame.App.Form\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.Form" /y /E /S

echo *******************Make QX_Frame.App.Web**************************

xcopy  %cd%"\QX_Frame.App.Web\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.App.Web" /y /E /S

echo *******************Make QX_Frame.Helper_DG_Framework**************************

xcopy  %cd%"\QX_Frame.Helper_DG_Framework_4_6\bin\Debug" %cd%"\QX_Frame.Reference\QX_Frame.Helper_DG_Framework" /y /E /S

pause
