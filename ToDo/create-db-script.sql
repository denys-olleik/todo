create table [User]
(
		[UserID] int not null identity primary key,
		[FirstName] nvarchar(50) not null,
		[Email] nvarchar(50) not null,
		[Password] VARCHAR(255),
)

INSERT INTO [User] ([Email], [FirstName], [Password])
VALUES ('test@example.com', 'John', 'sha1:64000:18:IofuE0pk3LtysdvPabvlsENb9NJ4x7XZ:Ui8pLvVoSzlwUXVARJj8MFEL');