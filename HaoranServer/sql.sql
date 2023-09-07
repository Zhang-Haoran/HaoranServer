IF NOT EXISTS
(SELECT 1 FROM information_schema.columns where column_name='commentid' and table_name= 'comment' and table_schema='dbo')
	BEGIN
		CREATE TABLE comment(
		commentId int IDENTITY(1,1) NOT NULL,
		title varchar(255) NULL,
		content varchar(255) NULL,
		createdTime varchar(255) NULL,
		updatedTime varchar(255) NULL,
		CONSTRAINT pk_comment PRIMARY KEY(commentId))
	End
	GO


IF NOT EXISTS
(SELECT 1 FROM information_schema.columns where column_name='userid' and table_name= 'user' and table_schema='dbo')
	BEGIN
		CREATE TABLE [user](
		userId int IDENTITY(1,1) NOT NULL,
		firstname varchar(255) NOT NULL,
		lastname varchar(255) NOT NULL,
		dateofbirth varchar(255) NOT NULL,
		role varchar(255) NULL,
		password varchar(255) NOT NULL,
		isDeleted bit NOT NULL DEFAULT 0
		CONSTRAINT pk_user PRIMARY KEY(userId))
	End
GO


IF NOT EXISTS
(SELECT 1 FROM information_schema.columns where column_name='reviewid' and table_name= 'review' and table_schema='dbo')
	BEGIN
		CREATE TABLE review(
		reviewId int IDENTITY(1,1) NOT NULL,
		rating int NOT NULL,
		comment varchar(255) NOT NULL,
		userId int not null,
		CONSTRAINT pk_review PRIMARY KEY(reviewId),
		CONSTRAINT fk_review_user_userid FOREIGN KEY (userId) REFERENCES [user](userId))
	End
GO

IF NOT EXISTS
(SELECT 1 FROM information_schema.columns where column_name='tourId' and table_name= 'tour' and table_schema='dbo')
	BEGIN
		CREATE TABLE tour(
		tourId int IDENTITY(1,1) NOT NULL,
		price int not null,
		startDate varchar(255) not null,
		endDate varchar(255) not null,
		state varchar(255) Null,
		city varchar(255) NULL,
		title varchar(255) null,
		subtitle varchar(255) null,
		introduction varchar(255) null,
		highlights varchar(255) null,
		included varchar(255) null,
		itinerary varchar(255) null,
		[image] varchar(255) null,
		map varchar(255) null,
		CONSTRAINT pk_tour PRIMARY KEY(tourId),
		)
	End
GO

IF NOT EXISTS
(SELECT 1 FROM information_schema.columns where column_name='bookingId' and table_name= 'booking' and table_schema='dbo')
	BEGIN
		CREATE TABLE booking(
		bookingId int IDENTITY(1,1) NOT NULL,
		price int not null,
		paid bit NOT NULL DEFAULT 0,
		userId int not null,
		tourId int not null

		CONSTRAINT pk_booking PRIMARY KEY(bookingId),
		CONSTRAINT fk_booking_user_userid FOREIGN KEY (userId) REFERENCES [user](userId),
		CONSTRAINT fk_booking_tour_tourid FOREIGN KEY (tourId) REFERENCES tour(tourId)
		)
	End
GO
