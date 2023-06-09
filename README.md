In the project directory, you can run:

```sh
dotnet dev-certs https --clean
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p password
```
Copy the generated pfx file and replace the pfx files inside /geolocation and /user
```sh
docker-compose up
```

Starts up the docker containers locally<br />
The endpoints can be reached at: https://localhost:5001 and https://localhost:6001