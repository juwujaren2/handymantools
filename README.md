Team 22 Phase 3
===

**Georgia Institute of Technology OMSCS**  
CS 6400 Database Systems & Analysis

###Implementation details
We built our project using a combination of technologies:

- Microsoft SQL Server
- Microsoft ASP.NET MVC
- Microsoft ASP.NET Web API
- Bootstrap
- HTML5
- jQuery
- jQuery UI

Development was performed in Visual Studio 2015 and Microsoft SQL Server Management Studio 2014, with Google Chrome utilized as our primary testing web browser.

###Changelog

**Available Tools Query**

The available tools query was updated after initial testing found that the query developed in phase II did not work correctly.
Instead of using left joins to find tools without reservations for a date range, it is more effective to query for tools *with* reservations 
in the date range, and then remove them from the list of all tools. 

The original query also was written so that the "end date" would be an unavailable day for a tool.  Based on feedback from office hours
and Piazza, this needed to be changed so that the end date was considered an available day and could be used as a start date for another reservation for the same tool.  This was done in the query by decrementing any reservation end date by 1 day.

**Service Order Insert**

As with the available tools query, this statement was checking for any reservations prior to creating a service order and was considering the "end date" as unavailable.  It was updated with the same logic 
so that the end date can be used as a start date for a service order.

**Normalization**

Our relational schema in the previous phase mapped customers and clerks into separate relations.  For implementation, we decided to normalize and move shared attributes from both relations into a Users table.

*TODO: Ram needs to add his explanation for the report query*

