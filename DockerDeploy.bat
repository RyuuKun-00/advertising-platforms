@echo "Running a project in docker"

docker build -t advertising-platforms ./AdvertisingPlatforms

docker run -d -p 5000:8080 advertising-platforms

@start http://localhost:5000

@pause