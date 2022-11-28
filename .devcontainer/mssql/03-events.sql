
INSERT INTO [Event]([Name], [Description]) VALUES (
    'Marge''s Zone-out session',
    'Join us as we escape from this world for a brief time'
);

INSERT INTO [EventRecurrence]([EventId], [DayOfWeek], [StartTime], [EndTime])
SELECT [Id], 0, '20:00:00', '20:30:00'
FROM [Event]
WHERE [Name] = 'Marge''s Zone-out session'
;

INSERT INTO [Event]([Name], [Description]) VALUES (
    'Vegan cooking class',
    'How to make delicious food without breaking the rules'
);

INSERT INTO [EventRecurrence]([EventId], [DayOfWeek], [StartTime], [EndTime])
SELECT [Id], 3, '18:00:00', '19:30:00'
FROM [Event]
WHERE [Name] = 'Vegan cooking class'
;

INSERT INTO [Event]([Name], [Description]) VALUES (
    'Yoga with Bert',
    'A beginner''s class that will have you bending like you are made from stuffed felt in no time.'
);

INSERT INTO [EventRecurrence]([EventId], [DayOfWeek], [StartTime], [EndTime])
SELECT [Id], 2, '19:00:00', '20:00:00'
FROM [Event]
WHERE [Name] = 'Yoga with Bert'
;

INSERT INTO [EventRecurrence]([EventId], [DayOfWeek], [StartTime], [EndTime])
SELECT [Id], 4, '19:00:00', '20:00:00'
FROM [Event]
WHERE [Name] = 'Yoga with Bert'
;
