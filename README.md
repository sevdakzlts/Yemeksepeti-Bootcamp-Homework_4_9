
# Worker Service 

[![](https://img.shields.io/badge/Docs-microsoft.com-orange.svg)](https://docs.microsoft.com/tr-tr/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio)  


With the .Net Core 2.1 version, we could use worker services. With the release of .Net Core 3.0, you can create it as a separate project via CLI or Visual Studio. Worker Service is a cross-platform background service. We can think of it just like the Windows Services we have created.

With this worker service, you will be able to sort your files in a certain location according to the last access date.

## [](#table-of-contents)Table of contents

1.  [Usage](#usage)
2.  [Dependencies](#dependencies)
3.  [Referances](#referances)
   

## [](#usage)Usage
After cloning this repository and installing [Visual Studio](https://visualstudio.microsoft.com/tr/downloads/) enter the project's folder through the command line and type the following code to run the program: 
`dotnet run`



## [](#dependencies)Dependencies

-   [.Net5.0](https://dotnet.microsoft.com/download/dotnet/5.0)


### Appsettings.json File 

You must change the sections specified in the appsettings.json file.
```c#
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  //Type the path to the file you want to list
  "PathOfDirectory": "C:/Users/",
  //Write the date you want to sort in days
  "OlderThanDays": "90",
  //Specify the path to the file you want to save
  "FileSavePath": "\\..\\..\\..\\..\\Files.txt"
}
```


## [](#referances)Referances
- https://docs.microsoft.com/tr-tr/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio
