Team 22 Phase 3
===

###Changelog

**Available Tools Query**

The available tools query was updated after initial testing found that the query developed in phase II did not work correctly.
Instead of using left joins to find tools without reservations for a date range, it is more effective to query for tools *with* reservations 
in the date range, and then remove them from the list of all tools. 

**Normalization**

Our relational schema in the previous phase separated customers and clerks into separate relations.  For implementation, we decided to normalize further and moved shared attributes from both relations into a Users table.

**Reports**

_Inventory Report_
* mistakenly reported only profit made on those tools there were sold instead of excluding them from the query result as per the requirements.
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
* the GROUP BY operation was corrected to correctly aggregate tools, since the requirements said the report needed to display the "list of 
all rental customers who rented a tool over the past month". The issue was originally we GROUP BY ToolId which was not correct, and resulted
in what looked like duplicate results of customers renting a single tool when in reality it was the same customer renting different tools.
