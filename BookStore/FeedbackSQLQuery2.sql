-------------------------------------------commands used for FeedBack---------------------------------
-- Table For Feedback----
create Table Feedback
(
	FeedbackId int identity(1,1) not null primary key,
	Comment varchar(max) not null,
	TotalRating decimal not null,
	BookId int not null foreign key (BookId) references BookTable(BookId),
	UserId INT not null foreign key (UserId) references Users(UserId),
);

select *from Feedback


--creating store procedure of Add Feedback-------

 Create  Proc SPAddFeedback
(
	@Comment varchar(max),
	@TotalRating decimal,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
begin
	if (exists(SELECT * FROM Feedback where BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		if (exists(SELECT * FROM BookTable WHERE BookId = @BookId))
		Begin  select * from Feedback
			Begin try
				Begin transaction
					Insert into Feedback(Comment, TotalRating, BookId, UserId) values(@Comment, @TotalRating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(TotalRating) from Feedback where BookId = @BookId);
					Update BookTable set TotalRating = @AverageRating where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
end;

create Proc SPGetFeedback
(
	@BookId int
)
as
begin
	Select FeedbackId, Comment, TotalRating, BookId, u.FullName
	From Users u
	Inner Join Feedback f
	on f.UserId = u.UserId
	where
	 BookId = @BookId;
end;