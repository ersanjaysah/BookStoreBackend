-------------------------------------commands for WishList----------------------

---create wishlist table
create Table WishlistTable
(
WishlistId int identity(1,1) primary key,
UserId INT FOREIGN KEY REFERENCES Users(UserId),
BookId INT FOREIGN KEY REFERENCES BookTable(BookId)
);

select * from WishlistTable

---create procedure to Add in Wishlist
create procedure SPAddInWishlist
@UserId int,
@BookId int
As
Begin
Insert Into WishlistTable (UserId,BookId) values (@UserId,@BookId)
End;

--create procedure to delete from wishlist
create procedure SPDeleteFromWishlist
@WishListId int
As
Begin
Delete WishlistTable where WishListId=@WishListId
End;

---create procedure to get all Books from wishlist
create procedure SPGetAllBooksinWishList
@UserId int
As
Begin
select WishlistTable.WishListId,WishlistTable.UserId,WishlistTable.BookId,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from WishlistTable inner join BookTable on WishlistTable.BookId=BookTable.BookId
where WishlistTable.userId=@UserId
End;