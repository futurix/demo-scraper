# Hacker News scraper demo

Tech:
* C# 6
* [Visual Studio 2015](https://www.visualstudio.com/downloads/) (Update 3, Community)
* [NuGet](https://www.nuget.org)
* MSTest
* [JSON.NET](http://www.newtonsoft.com/json)
* [HTML Agility Pack](http://htmlagilitypack.codeplex.com)  
All of the above chosen due to familiarity and past good experiences, to allow to build demo quickly.

---

Build requirements:
* Visual Studio 2015 Community
* NuGet command-line tool (for command-line build only)

For IDE-based build, open `HackerNews.sln` in Visual Studio. To run unit tests use _Test Explorer_ window.

For command-line build:

1. Install standalone `NuGet.exe` anywhere in `%PATH%`
2. Open `Developer Command Prompt`
3. Navigate to the folder with `HackerNews.sln`
4. Restore third-party libraries with `nuget restore`
5. Build solution with `msbuild HackerNews.sln`
6. Run app from `bin\Debug` folder
7. Run unit tests with `mstest /testcontainer:HackerNews.Tests.dll`
