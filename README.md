# An Example of metrics using .NET Core, Prometheus and Grafana.

## Stack of sample

| Option | Description | Default Port in docker-file
| :------:| :-----------:| :------: |
| .NET Core   | [.NET Core Version 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) |  ```http://localhost:8082```|
| Prometheus (set to go in docker compose file) | [From Docker Hub](https://hub.docker.com/r/prom/prometheus/) |```http://localhost:9090```|
| Grafana (set to go in docker compose file)   | [From Dcoker Hub](https://hub.docker.com/r/grafana/grafana/) |```http://localhost:3000```|


## Steps to run in your machine

### 1. Clone the repository and run the following commands:

``` csharp
dotnet restore
```

``` csharp
dotnet build
```

### 2. Build the image of your API:

Where "promgrafmetrics" is an example of name choosed for one image

```
docker build --no-cache -t promgrafmetrics .     
```

### 3. Run the "docker-file":

If you change the name of image, change the value of image in `docker-compose` file to mantaint it consistent. Then, in the directory where the docker-file is, run 

```
docker-compose up       
```

### 4. Play with the requests, and metrics created, do something

![](https://media.giphy.com/media/vzO0Vc8b2VBLi/giphy.gif)

### 5. After that, set the datasource of your prometheus in Grafana

- [See here how to add Datasources](https://grafana.com/docs/grafana/latest/features/datasources/add-a-data-source/) 

- Protip: get the IP of your Prometheus container (inspect your container) and set the IP like your host. Example: ```123.45.67.89:9090```. Try too ```prometheus:9090``` (or the name of your Prometheus service in docker-file)

- [Import the json file in Grafana](https://grafana.com/docs/grafana/latest/reference/export_import/)


## Remember! This is just a example! 

[You have more examples here](https://grafana.com/grafana/dashboards?orderBy=name&direction=asc) and of course, you can build for your own!

#iswe

