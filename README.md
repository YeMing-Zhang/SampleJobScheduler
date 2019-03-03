# SampleJobScheduler
This is sample job scheduler with Topshelf, Autofac, Quartz, log4Net. Currently using DotNet Framework 4.7.2. 

This repository implemetns a simple console application with following libraries

- Topshelf : For creating service
- Autofac : For dependency injection 
- Quartz : For scheduling job, also uses xml based job configuration 
- log4net : For logging 

# Installation 

Build the solution in release mode and copy to server directory. Open command prompt in the directory. 

Run below command : 

SampleJobScheduler.exe install -username “DOMAIN\Service Account” -password “Its A Secret” -servicename “SampleJobScheduler” –autostart

