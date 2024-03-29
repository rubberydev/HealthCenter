use bd_H

SELECT TOP (1000) [UserId]
      ,[FirstName]
      ,[LastName]
      ,[Email]
      ,[Telephone]
      ,[ImagePath]
      ,[UserTypeId]
  FROM [dbo].[Users]
  where UserId = 4
go
SELECT TOP (1000) [UserScheduleId]
      ,[AgendaId]
      ,[UserId]
  FROM [dbo].[UserSchedules] where UserId = 4
  go
  SELECT TOP (1000) [AgendaId]
      ,[startHour]
      ,[endHour]
      ,[DateSchedule]
      ,[idWorkDay]
      ,[StateId]
      ,[ApplicationUser_Id]
  FROM [dbo].[Schedulers]
  where AgendaId = 141

  --delete from [dbo].[UserSchedules] where AgendaId = 37 and UserId = 4

  {SELECT 
    [Extent1].[UserScheduleId] AS [UserScheduleId], 
    [Extent1].[AgendaId] AS [AgendaId], 
    [Extent1].[UserId] AS [UserId]
    FROM [dbo].[UserSchedules] AS [Extent1]}
