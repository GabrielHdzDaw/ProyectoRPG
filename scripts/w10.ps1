reg add "HKCU\Console" /v DelegationConsole /t REG_SZ /d "" /f
reg add "HKCU\Console\%%Startup" /v DelegationConsole /t REG_SZ /d "" /f

