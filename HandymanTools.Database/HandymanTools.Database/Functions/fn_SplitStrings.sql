--Based on http://sqlperformance.com/2012/07/t-sql-queries/split-strings
CREATE FUNCTION [dbo].[fn_SplitStrings_Integer]
(
	@List varchar(MAX),
	@Delimiter varchar(255)
)
RETURNS TABLE
AS
	RETURN
	(
		SELECT Id = y.i.value('(./text())[1]', 'int')
		FROM 
		( 
			SELECT x = CONVERT(XML, '<i>' 
				+ REPLACE(@List, @Delimiter, '</i><i>') 
				+ '</i>').query('.')
		) AS a CROSS APPLY x.nodes('i') AS y(i)
	);
GO
