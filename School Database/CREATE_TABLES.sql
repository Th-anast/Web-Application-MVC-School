/*Users*/
USE [school_db]
GO

/****** Object:  Table [dbo].[users]    Script Date: 10/1/2023 6:57:56 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[username] [varchar](45) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[role] [varchar](45) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/*Students*/
USE [school_db]
GO

/****** Object:  Table [dbo].[students]    Script Date: 10/1/2023 6:58:11 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[students](
	[RegistrationNumber] [int] NOT NULL,
	[Name] [varchar](45) NOT NULL,
	[Surname] [varchar](45) NOT NULL,
	[Department] [varchar](45) NOT NULL,
	[Users_username] [varchar](45) NOT NULL,
 CONSTRAINT [PK_students] PRIMARY KEY CLUSTERED 
(
	[RegistrationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [FK_students_users] FOREIGN KEY([Users_username])
REFERENCES [dbo].[users] ([username])
GO

ALTER TABLE [dbo].[students] CHECK CONSTRAINT [FK_students_users]
GO



/*Professors*/
USE [school_db]
GO

/****** Object:  Table [dbo].[professors]    Script Date: 10/1/2023 6:58:28 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[professors](
	[AFM] [int] NOT NULL,
	[Name] [varchar](45) NOT NULL,
	[Surname] [varchar](45) NOT NULL,
	[Department] [varchar](45) NOT NULL,
	[Users_username] [varchar](45) NOT NULL,
 CONSTRAINT [PK_professors] PRIMARY KEY CLUSTERED 
(
	[AFM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[professors]  WITH CHECK ADD  CONSTRAINT [FK_professors_users] FOREIGN KEY([Users_username])
REFERENCES [dbo].[users] ([username])
GO

ALTER TABLE [dbo].[professors] CHECK CONSTRAINT [FK_professors_users]
GO



/*Secretaries*/
USE [school_db]
GO

/****** Object:  Table [dbo].[secretaries]    Script Date: 10/1/2023 6:58:15 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[secretaries](
	[PhoneNumber] [int] NOT NULL,
	[Name] [varchar](45) NOT NULL,
	[Surname] [varchar](45) NOT NULL,
	[Department] [varchar](45) NOT NULL,
	[Users_username] [varchar](45) NOT NULL,
 CONSTRAINT [PK_secretaries] PRIMARY KEY CLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[secretaries]  WITH CHECK ADD  CONSTRAINT [FK_secretaries_users] FOREIGN KEY([Users_username])
REFERENCES [dbo].[users] ([username])
GO

ALTER TABLE [dbo].[secretaries] CHECK CONSTRAINT [FK_secretaries_users]
GO



/*Cources*/
USE [school_db]
GO

/****** Object:  Table [dbo].[course]    Script Date: 10/1/2023 6:58:36 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[course](
	[idCourse] [int] NOT NULL,
	[CourseTitle] [varchar](60) NULL,
	[CourseSemester] [varchar](25) NULL,
	[Professors_AFM]  [int] NULL,
 CONSTRAINT [PK_course] PRIMARY KEY CLUSTERED 
(
	[idCourse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[course]  WITH CHECK ADD  CONSTRAINT [FK_course_professors] FOREIGN KEY([Professors_AFM])
REFERENCES [dbo].[professors] ([AFM])
GO

ALTER TABLE [dbo].[course] CHECK CONSTRAINT [FK_course_professors]
GO



/*Cources Has Student*/
USE [school_db]
GO

/****** Object:  Table [dbo].[course_has_students]    Script Date: 10/1/2023 6:58:32 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[course_has_students](
	[id] [int] NOT NULL,
	[Course_idCourse] [int] NULL,
	[Students_RegistrationNumber] [int] NULL,
	[GradeCourseStudent] [int] NULL,
 CONSTRAINT [PK_course_has_students] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[course_has_students]  WITH CHECK ADD  CONSTRAINT [FK_course_has_students_course] FOREIGN KEY([Course_idCourse])
REFERENCES [dbo].[course] ([idCourse])
GO

ALTER TABLE [dbo].[course_has_students] CHECK CONSTRAINT [FK_course_has_students_course]
GO

ALTER TABLE [dbo].[course_has_students]  WITH CHECK ADD  CONSTRAINT [FK_course_has_students_students] FOREIGN KEY([Students_RegistrationNumber])
REFERENCES [dbo].[students] ([RegistrationNumber])
GO

ALTER TABLE [dbo].[course_has_students] CHECK CONSTRAINT [FK_course_has_students_students]
GO