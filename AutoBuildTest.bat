cd C:\Users\320053937
IF EXIST "C:\Users\320053937\Downloads\PhilipsJune2019Dotnet-master\PhilipsJune2019Dotnet-master\SampleConApp\CompleteExample.cs" (
   TICS "C:\Users\320053937\Documents\CaseStudy\SAToolReportGenerator\SAToolReportGenerator\ReportGenerator.cs" -out "C:\Users\320053937\Documents\TICSReport.txt"
   IF errorlevel 1 ( echo TICS unable to generate report ) ELSE (
   call devenv /build debug "%~dp0SAToolReportGenerator\SAToolReportGenerator.sln" 
   call "C:\Users\320053937\Documents\CaseStudy\SAToolReportGenerator\SAToolReportGenerator\bin\Debug\SAToolReportGenerator.exe"
   start "" "C:\Temp\Report.html"
    )
) ELSE ( echo File doesn't Exist )
pause