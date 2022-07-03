create database BookStore;

Create table Users
( UserId int identity(1,1) Primary key,
FullName Varchar(225) not null,
Email Varchar(225) not null unique,
Password varchar(225) not null,
MobileNumber bigint not null
)

select *from Users

Create procedure SPUserRegister
(
@FullName varchar(255),
@Email varchar(255),
@Password Varchar(255),
@MobileNumber Bigint
)
as
Begin
		insert Users
		values (@FullName, @Email, @Password, @MobileNumber) 
end

---this SP is for login
create procedure SPUserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Users
where Email = @Email and Password = @Password
End;

---This SP is used for Forget password
create procedure SPUserForgetPassword
(
@Email varchar(max)
)
AS
BEGIN
UPDATE Users
SET Password='Null'
WHERE Email=@Email;
select * from Users where Email=@Email;
End;

---This SP ie store Procedure  command is for Reset Password
create procedure SPUserResetPassword
(
@Email varchar(Max),
@Password varchar(Max)
)
AS
BEGIN
UPDATE Users 
SET 
Password = @Password 
WHERE Email = @Email;
End;