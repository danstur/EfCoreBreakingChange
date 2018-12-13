use master;

DROP DATABASE TestDb
GO

CREATE DATABASE TestDb
GO

use TestDb;

CREATE TABLE [Site] (
	[Id] int IDENTITY(1,1) NOT NULL,
	[SiteId] nvarchar(4) NOT NULL,
	CONSTRAINT [PK_BAR] PRIMARY KEY ([Id])
)
GO

CREATE TABLE OutboundDelivery(
	[Id] int IDENTITY(1,1) NOT NULL,
	[SenderId] int not null,
	CONSTRAINT [PK_FOO] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_BAR_FOO] 
		FOREIGN KEY ([SenderId])     
		REFERENCES [Site](Id)  
)
GO
