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
