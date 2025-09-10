
@echo 
cd ./AdvertisingPlatforms

dotnet build -c Release

cd ./AdvertisingPlatforms/bin/Release/net8.0/

start AdvertisingPlatforms.exe

timeout 5

@start http://localhost:5000

@pause