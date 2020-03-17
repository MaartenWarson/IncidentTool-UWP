# IncidentTool-UWP
 
## General Summary
The goals of this project is to create an application that runs on both an iOS device as an Android Device using **Xamarin Forms**. This application is intended to use in a work environment. This is one of 3 repositories in this project:
- [IncidentTool-UWP](https://github.com/MaartenWarson/IncidentTool-UWP)
- [IncidentTool-Xamarin](https://github.com/MaartenWarson/IncidentTool-Xamarin)
- [IncidentTool-Backend](https://github.com/MaartenWarson/IncidentTool-Backend)

**IncidentTool-UWP**
With the UWP application, a coordinator of the company is able to register devices such as printer, coffee machines, beamers, ... This devices will be linked to a few incidents that can happen to it: printer ran out of paper, printer ran out of ink, coffee machine ran out of coffee, ...
The coordinator generates a QR code for each device and places it next/on the device. An employee can scan this QR code with the application created for it. Then he reports the incident that has happened to the coordinator using the application. 
The report from the employee reaches the coordinator, who can fix this problem.

**IncidentTool-Xamarin**
With the Xamarin application, an employee of the company is able to scan a QR code from a printer, coffee machine, beamer, ... to report an incident that occured. After it, he can see which incidents he has reported and which of them are solved already.

**IncidentTool-Backend**
This application is a RESTFul Web API that created the connection with a local SQL Server database. Here the data from the devices, incidents, occurred incidents, users, ... will be stored.

## Repository Summary
This repository will focus on the UWP part of the project.

## Running the project
**IncidentTool-Backend**
1. Run backend directly
2. Visit `localhost:5000` to consult data
3. Use SharpProxy to connect Internal Port 5000 to External Port 8080 using your IP address
4. Start SharpProxy

**IncidentTool-UWP**
1. Run this application

**IncidentTool-Xamarin**
Consult a physical device to make use of the camera to scan a QR code
1. Go to `IncidentTool\Constants\ApiConstants.cs` and change the IP address in `BaseApiUrl` to your own IP address
2. Connect your physical device to your system
3. Enable developed mode and USB debugging
4. Run the application

## Further Information
This project is written in the **MVVM Architecture**.
