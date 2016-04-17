Team 22 Phase 3
===

###Changelog

**Available Tools Query**

The available tools query was updated after initial testing found that the query developed in phase II did not work correctly.
Instead of using left joins to find tools without reservations for a date range, it is more effective to query for tools *with* reservations 
in the date range, and then remove them from the list of all tools. 

**Normalization**

Our relational schema in the previous phase separated customers and clerks into separate relations.  For implementation, we decided to normalize and move shared attributes from both relations into a Users table.

*TODO: Ram needs to add his explanation for the report query*

