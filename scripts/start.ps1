# Запуск (-NoNewWindow)
Start-Process "dotnet" "run --urls http://0.0.0.0:5001" -WorkingDirectory "C:\Users\0Shur\Desktop\DISTRIBUTED-PROGRAMMING\Valuator" 
Start-Process "dotnet" "run --urls http://0.0.0.0:5002" -WorkingDirectory "C:\Users\0Shur\Desktop\DISTRIBUTED-PROGRAMMING\Valuator"
Start-Process "dotnet" "run --urls http://0.0.0.0:5003" -WorkingDirectory "C:\Users\0Shur\Desktop\DISTRIBUTED-PROGRAMMING\Valuator"

# Запуск Nginx
Start-Process "C:\Users\0Shur\Desktop\DISTRIBUTED-PROGRAMMING\nginx\nginx.exe" -WorkingDirectory "C:\Users\0Shur\Desktop\DISTRIBUTED-PROGRAMMING\nginx"