use master;

CREATE DATABASE TestDb
GO

CREATE TABLE OutboundDelivery(
	[Id] int IDENTITY(1,1) NOT NULL,
	[SenderId] int not null,
	CONSTRAINT [PK_FOO] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_BAR_FOO] 
		FOREIGN KEY ([SenderId])     
		REFERENCES [Site](Id)  
);

CREATE TABLE [Site] (
	[Id] int IDENTITY(1,1) NOT NULL,
	[SapSiteId] nvarchar(4) NOT NULL,
	CONSTRAINT [PK_BAR] PRIMARY KEY ([Id])
)
GO