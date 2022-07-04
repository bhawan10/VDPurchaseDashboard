CREATE TYPE UT_ExpeditorForm AS TABLE  
(  
POItemID bigint NOT NULL,  
OperationId int NOT NULL,  
entryBy nvarchar(50) NOT NULL,  
entryDate datetime NOT NULL,
totalQuantity int NOT NULL,
doneQuantity int,
isActive bit NOT NULL,
isCompleted bit NOT NULL
) 
