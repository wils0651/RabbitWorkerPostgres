# RabbitWorkerPostgres

Gets data from a queue and puts it in Postgres.

## PSQL Commands:
```
psql test2

select * 
From public."ProbeData"
--Where "ProbeName" = 'Probe_1'
Order by "Id" desc;
```

## dotnet core Commands:
```
dotnet restore
dotnet run
```