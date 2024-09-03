
CREATE DATABASE [TekusVendorData]

CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 02/09/2024 06:20:30 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[IdCountry] [int] IDENTITY(1,1) NOT NULL,
	[OfficialName] [nvarchar](max) NOT NULL,
	[ServiceId] [int] NOT NULL,
	[CommonName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[IdCountry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomFields]    Script Date: 02/09/2024 06:20:30 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFields](
	[IdCustomField] [int] IDENTITY(1,1) NOT NULL,
	[ProviderId] [int] NOT NULL,
	[FieldName] [nvarchar](max) NOT NULL,
	[FieldValue] [nvarchar](max) NOT NULL,
	[ServicesProviderIdServices] [int] NULL,
 CONSTRAINT [PK_CustomFields] PRIMARY KEY CLUSTERED 
(
	[IdCustomField] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProvidersTekus]    Script Date: 02/09/2024 06:20:30 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProvidersTekus](
	[IdProviders] [int] IDENTITY(1,1) NOT NULL,
	[NIT] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ProvidersTekus] PRIMARY KEY CLUSTERED 
(
	[IdProviders] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCountry]    Script Date: 02/09/2024 06:20:30 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCountry](
	[CountryId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
 CONSTRAINT [PK_ServiceCountry] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicesProvider]    Script Date: 02/09/2024 06:20:30 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicesProvider](
	[IdServices] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[HourlyRate] [decimal](18, 2) NOT NULL,
	[ProviderId] [int] NOT NULL,
 CONSTRAINT [PK_ServicesProvider] PRIMARY KEY CLUSTERED 
(
	[IdServices] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Countries] ADD  DEFAULT (N'') FOR [CommonName]
GO
ALTER TABLE [dbo].[CustomFields]  WITH CHECK ADD  CONSTRAINT [FK_CustomFields_ProvidersTekus_ProviderId] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[ProvidersTekus] ([IdProviders])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomFields] CHECK CONSTRAINT [FK_CustomFields_ProvidersTekus_ProviderId]
GO
ALTER TABLE [dbo].[CustomFields]  WITH CHECK ADD  CONSTRAINT [FK_CustomFields_ServicesProvider_ServicesProviderIdServices] FOREIGN KEY([ServicesProviderIdServices])
REFERENCES [dbo].[ServicesProvider] ([IdServices])
GO
ALTER TABLE [dbo].[CustomFields] CHECK CONSTRAINT [FK_CustomFields_ServicesProvider_ServicesProviderIdServices]
GO
ALTER TABLE [dbo].[ServiceCountry]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCountry_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([IdCountry])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceCountry] CHECK CONSTRAINT [FK_ServiceCountry_Countries_CountryId]
GO
ALTER TABLE [dbo].[ServiceCountry]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCountry_ServicesProvider_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[ServicesProvider] ([IdServices])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceCountry] CHECK CONSTRAINT [FK_ServiceCountry_ServicesProvider_ServiceId]
GO
ALTER TABLE [dbo].[ServicesProvider]  WITH CHECK ADD  CONSTRAINT [FK_ServicesProvider_ProvidersTekus_ProviderId] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[ProvidersTekus] ([IdProviders])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServicesProvider] CHECK CONSTRAINT [FK_ServicesProvider_ProvidersTekus_ProviderId]
GO


---INSERT DATA TABLES

-- Datos iniciales para ProvidersTekus
INSERT INTO ProvidersTekus (NIT, Name, Email, IsActive) VALUES
('123456789', 'Proveedor Uno', 'uno@ejemplo.com', 1),
('234567890', 'Proveedor Dos', 'dos@ejemplo.com', 1),
('345678901', 'Proveedor Tres', 'tres@ejemplo.com', 1),
('456789012', 'Proveedor Cuatro', 'cuatro@ejemplo.com', 0),
('567890123', 'Proveedor Cinco', 'cinco@ejemplo.com', 1),
('678901234', 'Proveedor Seis', 'seis@ejemplo.com', 0),
('789012345', 'Proveedor Siete', 'siete@ejemplo.com', 1),
('890123456', 'Proveedor Ocho', 'ocho@ejemplo.com', 1),
('901234567', 'Proveedor Nueve', 'nueve@ejemplo.com', 0),
('012345678', 'Proveedor Diez', 'diez@ejemplo.com', 1);
GO

-- Datos iniciales para ServicesProvider
INSERT INTO ServicesProvider (Name, HourlyRate, ProviderId) VALUES
('Servicio A', 100.00, 1),
('Servicio B', 150.00, 2),
('Servicio C', 200.00, 3),
('Servicio D', 250.00, 4),
('Servicio E', 300.00, 5),
('Servicio F', 350.00, 6),
('Servicio G', 400.00, 7),
('Servicio H', 450.00, 8),
('Servicio I', 500.00, 9),
('Servicio J', 550.00, 10);
GO

INSERT INTO Countries (CommonName, OfficialName, ServiceId) VALUES
('Argentina', 'República Argentina', 1),
('Brasil', 'República Federativa do Brasil', 2),
('Chile', 'República de Chile', 3),
('Colombia', 'República de Colombia', 4),
('Ecuador', 'República del Ecuador', 5),
('México', 'Estados Unidos Mexicanos', 6),
('Perú', 'República del Perú', 7),
('Uruguay', 'República Oriental del Uruguay', 8),
('Venezuela', 'República Bolivariana de Venezuela', 9),
('Paraguay', 'República del Paraguay', 10);
GO

-- Datos iniciales para CustomFields
INSERT INTO CustomFields (ProviderId, FieldName, FieldValue) VALUES
(1, 'Número de Contacto', '123-456-7890'),
(2, 'Cantidad de Mascotas', '2'),
(3, 'Área de Cobertura', 'América del Sur'),
(4, 'Horas de Servicio', '24/7'),
(5, 'Especialidad', 'Transporte'),
(6, 'Fecha de Registro', '2024-01-01'),
(7, 'Comentarios', 'Proveedor confiable'),
(8, 'Contacto de Emergencia', 'emergencia@ejemplo.com'),
(9, 'Licencia', 'Valida hasta 2025'),
(10, 'Horario de Atención', 'Lunes a Viernes');
GO

