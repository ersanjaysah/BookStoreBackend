------------------------------- Commands for Adding  Address ------------------------------------
--- Creating Address Table----
create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(255)
);

select * from AddressTypeTable

---insert record for addresstype table
insert into AddressTypeTable values('Home'),('Office'),('Other');

---create address table
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address varchar(255),
City varchar(100),
State varchar(100),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
select * from AddressTable


--- Procedure To Add Address
create procedure SPAddAddress
(
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Insert into AddressTable 
values(@Address, @City, @State, @TypeId, @UserId);
end
Else
begin
select 2
end
End;

--create procedure for updateAddress
create procedure SPUpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
Else
begin
select 2
end
End;

--create procedure to delete address
create Procedure SPDeleteAddress
(
@AddressId int
)
as
BEGIN
Delete AddressTable where AddressId = @AddressId 
End;
