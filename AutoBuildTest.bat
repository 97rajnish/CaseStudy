
call Devenv /build debug %~dp0\Helloworld\Helloworld.sln

call %~dp0\Helloworld\Helloworld\bin\Debug\Helloworld.exe

pause