@echo off

cd src
call :sub_delete
cd ..
goto :EOF

:sub_delete
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO (
    echo deleting %%G ...
    RMDIR /S /Q "%%G"
)
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO (
    echo deleting %%G ...
    RMDIR /S /Q "%%G"
)
