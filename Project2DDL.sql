--User: ID, Name, salary (nullable), stats (nullable), RankIDFK (nullable).
--LoginInfo: ID, username, password, permissionLvl.
--Request: ID, RankIDFK, description, completionRequirement, cost, adventurerpartyRewards, State (pending, declined, approved, accepted, in-progress, completed).
--RequestingParty (Customers_Requests): ID, Joint table of Customers and Requests they've placed.
--AdventurerParty (Adventurers_Requests): ID, Joint table of Requests and Adventurers who accepted them.
--Rank: ID, Name, Fee.
--AdventurerRankUpRequirements: ID, CurrentRankIDFK, NumberRequests, MinTotalStats, NextRankIDFK.

Create Table Ranks (
id int Not Null Identity Primary Key,
nam nvarchar(50) Unique Not Null, --name is keyword
fee money Not Null
);

Create Table Users (
id int Not Null Identity Primary Key,
firstName nvarchar(50) Not Null,
lastName nvarchar(50)  Not Null,
salary money,
strength int,
dex int,
wisdom int,
intelligence int,
charisma int,
constitution int,
rankID int,
Constraint FK_user_rank FOREIGN KEY (rankID) REFERENCES Ranks (id)
);

Create Table Progress (
id int Not Null Identity Primary Key,
nam nvarchar(50) Not Null
);


Create Table Request (
id int Not Null Identity Primary Key,
rankID int,
descript nvarchar(50) Not Null,
requirements nvarchar(50) Not Null,
reward money,
cost money, --redundant data to prevent updating rank cost changing history
progressID int Not Null
Constraint FK_request_rank FOREIGN KEY (rankID) REFERENCES Ranks (id),
Constraint FK_request_progress FOREIGN KEY (progressID) REFERENCES Progress (id),
);



Create Table RankRequirements (
id int Not Null Identity Primary Key,
currentRankID int Not Null, 
numberRequests int Not Null, 
minTotalStats int Not Null, 
nextRankID int Not Null,
Constraint FK_requirements_current_rank FOREIGN KEY (currentRankID) REFERENCES Ranks (id),
Constraint FK_requirements_next_rank FOREIGN KEY (nextRankID) REFERENCES Ranks (id)
);

Drop Table AdventurerParty;

Create Table AdventurerParty (
id int Not Null Identity Primary Key,
nam nvarchar(50) Unique Not Null, --name is keyword
adventurerID int Not Null,
requestID int Not Null,
Constraint FK_adventurerparty_adventurer FOREIGN KEY (adventurerID) REFERENCES Users (id),
Constraint FK_adventurerparty_request FOREIGN KEY (requestID) REFERENCES Request (id)
);


Create Table RequestingGroup (
id int Not Null Identity Primary Key,
requestID int Not Null,
customerID int Not Null,
Constraint FK_requestinggroup_request FOREIGN KEY (requestID) REFERENCES Request (id),
Constraint FK_requestinggroup_customer FOREIGN KEY (customerID) REFERENCES Users (id)
);