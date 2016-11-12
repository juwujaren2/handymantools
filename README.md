# handymantools
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

**Reports**

_Inventory Report_

* Mistakenly reported only profit made on those tools there were sold instead of excluding them from the query result as per the requirements.
* The Join condition on the inventory report was wrong. As per the requirements, which indicated that every item in the inventory should be listed,
along with its rental profit, cost of repairs and total profit, what was required was actually a LEFT OUTER JOIN condition on computing rental profit and
cost of repairs instead of a INNER JOIN, which excluded every tool in the inventory that was not rented (which is incorrect).
* In order to deal with the NULL situation that occurred from performing the LEFT OUTER JOIN, a default value of 0 was used in the ISNULL function 
(ISNULL is the equivalent of COALESCE in Postgres)

_Clerk Progress_

* Again here a LEFT OUTER JOIN was required instead of a INNER JOIN on pickups and dropoffs to deal with the situation where a clerk only handled
pickup reservations or only handled dropoff reservations.
* A default value of 0 was used in the case where NULLs were returned from the LEFT OUTER JOIN.

_Customer Rentals_

* The GROUP BY operation was corrected to correctly aggregate tools, since the requirements said the report needed to display the "list of 
all rental customers who rented a tool over the past month". The issue was originally we GROUP BY ToolId which was not correct, and resulted
in what looked like duplicate results of customers renting a single tool when in reality it was the same customer renting different tools.
