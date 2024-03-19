This is a public repository of sample applications written by Kevin McKenna/Baseline Software Solutions.

The applications in this repository make use of the famous Microsoft "Northwinds" sample database from the the 1990s.  This data has been updated to be used in modern SQL Server instances.  

The NorthwindsDB directory contains SQL server backups and also SQL script that can be used to recreate the database.  It also contains NorthwindsDB.xml which is the entire database in XML.
All the demo applications can load the XML version for an easy demo without having to load a SQL database.

**WPFSampleApp** - This application is written entirely in C# with a WPF based GUI.  It makes heavy use of layout, styles, templates, converter functions and binding.
The database is accessed using EntityFramework with Linq operations. The configuration file allows the application to switch between a SQL Server database and "in memory" version of the database loaded from an XML file as not everyone can or wants to install a new database.
We also have some slick animations of windows loading and unloading.

**WCFApplications** - This folder contains three projects.  

***- WCFSampleService*** This library is a WCF service implementation. It can be hosted within a desktop/console application or it can be hosted via IIS.  For this demo we choose to host it via a console application.

***- WCFSampleServer*** This console application is simply used to host the WCFSampleService library which contains all the important code. **Important:** This application must be started as an administrator because it opens a network port.

***- WCFSampleClient*** This WPF based GUI application serves as the client application that displays data from the WCFSampleService.  The GUI is almost identical to the WPFSample application.  The difference is that this application retrieves the data over WCF SOAP and REST connections.  Two different windows allow access the web service via SOAP and REST.


