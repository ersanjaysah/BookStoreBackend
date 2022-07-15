
---creating table for Cart
create Table CartTable
(
CartId int primary key identity(1,1),
BooksQty int,
UserId int Foreign Key References Users(UserId),
BookId int Foreign Key References BookTable(BookId)
);

select * from CartTable;

---create store procedure to addcart---
create Procedure SPAddCart
(
@BooksQty int,
@UserId int,
@BookId int
)
As
Begin
Insert into CartTable (BooksQty,UserId,BookId) 
values (@BooksQty,@UserId,@BookId);
End;

---Create procedure to deletecart
create procedure SPDeleteCart
@CartId int
As
Begin 
Delete CartTable where CartId = @CartId
End;

---create procedure to UpdateCart
create procedure spUpdateCart
@BooksQty int,
@CartId int
As
Begin
update CartTable set BooksQty = @BooksQty
where CartId = @CartId
End;

--create procedure to GetAllBookinCart by UserId
create procedure SPGetAllBookinCart
@UserId int
As
Begin
select CartTable.CartId,CartTable.UserId,CartTable.BookId,CartTable.BooksQty,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from CartTable inner join BookTable on CartTable.BookId = BookTable.BookId
where CartTable.UserId = @UserId
End;

