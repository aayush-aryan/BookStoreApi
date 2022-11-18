create database BooKStoreDb;
use BooKStoreDb;
create table UserTable
(
UserId int identity(1,1) primary key,
FullName varchar(100) not null,
Email varchar(100) not null,
Password varchar(100) not null,
MobileNumber varchar(20) not null,
);

select * from UserTable

create procedure [dbo].[SP_UserRegister]
(
@FullName varchar(100),
@Email varchar(100),
@Password varchar(100),
@MobileNumber varchar(20)
)
as
begin
insert into UserTable values
(
@FullName,@Email,@Password,@MobileNumber
)
end
GO

Exec SP_UserRegister 'Aayush','aayush@gmail.com','aayush123','8111456789'

exec spUserLogin 'aayush@gmail.com','aayush123'




use BooKStoreDb;
create procedure [dbo].[spUserLogin]
(
@Email varchar(100),
@Password varchar(100)
)
as
begin
select * from UserTable where Email=@Email and Password= @Password;
end
GO

ALTER TABLE UserTable
ADD CONSTRAINT UQ_Email UNIQUE(Email);

create procedure [dbo].[spUserForgotPassword]
(
@Email varchar(100)
)
as
begin
select * from UserTable where Email=@Email;
end
GO

create procedure [dbo].[spUserResetPassword]
(
@Email varchar(100),
@Password varchar(100)
)
as
begin
update UserTable set Password =@Password where Email=@Email;
end
GO

create table AdminTable
(
AdminId int identity(1,1) primary key,
FullName varchar(100) not null,
Email varchar(100) unique not null,
Password varchar(100) not null,
MobileNumber varchar(20) not null
);

select * from AdminTable

insert into AdminTable values('Aayush','er.aayusharyan@gmail.com','aayush1234','8376000000');

create procedure [dbo].[spLoginAdmin]
(
@Email varchar(100),
@Password varchar(100)
)
as
begin
select * from AdminTable where Email=@Email and Password= @Password;
end
GO

create table BookTable
(
BookId int identity(1,1) primary key,
BookName varchar(250) not null,
AuthorName varchar(250) unique not null,
Rating int not null,
PeopleRating int not null,
OriginalPrice decimal(10,2) not null,
DiscountPrice decimal(10,2) not null,
BookDetails varchar(350) not null,
BookQuantity int not null,
BookImage varchar(500) 
);

select * from BookTable

create procedure [dbo].[SPAddBook]
(
@BookName varchar(100),
@AuthorName varchar(250),
@Rating int,
@PeopleRating int,
@OriginalPrice decimal(10,2),
@DiscountPrice decimal(10,2),
@BookDetails varchar(350),
@BookQuantity int,
@BookImage varchar(500) 

)
as
begin
insert into BookTable values
(
@BookName,@AuthorName,@Rating,@PeopleRating,@OriginalPrice,@DiscountPrice,@BookDetails,@BookQuantity,@BookImage
)
end
GO

create procedure [dbo].[spGetBookByBookId]
(
@BooKId int
)
as
begin
select * from BookTable where BookId=@BooKId
end
GO

create procedure [dbo].[spGetAllBook]
as
begin
select * from BookTable
end
GO

exec spGetAllBook

create procedure [dbo].[spDeleteBook]
(
@BooKId int
)
as
begin
Delete from BookTable where BooKId=@BooKId;
end
GO

create procedure [dbo].[spUpdateBook]
(
@BooKId int,
@BookName varchar(100),
@AuthorName varchar(250),
@Rating int,
@PeopleRating int,
@OriginalPrice decimal(10,2),
@DiscountPrice decimal(10,2),
@BookDetails varchar(350),
@BookQuantity int,
@BookImage varchar(500)
)
as
begin
update BookTable set BookName =@BookName,AuthorName=@AuthorName,Rating=@Rating,PeopleRating=@PeopleRating,OriginalPrice=@OriginalPrice,
DiscountPrice=@DiscountPrice,BookDetails=@BookDetails,BookQuantity=@BookQuantity,BookImage=@BookImage
where BooKId=@BooKId;
end
GO



create table CartTable
(
CartId int identity(1,1) primary key,
BooksQty int not null,
BooKId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(BooKId),
UserId int not null,
foreign key(UserId) REFERENCES UserTable(UserId)
);

select  * from CartTable

create procedure [dbo].[spAddCart]
(
@BooksQty int,
@BooKId int,
@UserId int
)
as
begin
insert into CartTable values
(
@BooksQty,@BooKId,@UserId
)
end
GO

create procedure [dbo].[spDeleteCart]
(
@CartId int
)
as
begin
Delete from CartTable where CartId=@CartId;
end
GO  

create procedure [dbo].[spUpdateCart]
(
@CartId int,
@BooksQty int
)
as
begin
update CartTable set BooksQty=@BooksQty  where CartId=@CartId;
end
GO 

create procedure [dbo].[spGetAllBookinCart]
(
@UserId int
)
as
begin
select CT.CartId,BT.BookName,BT.AuthorName,BT.Rating,BT.PeopleRating,BT.OriginalPrice,BT.DiscountPrice,BT.BookDetails,BT.BookQuantity,
BT.BookImage,CT.BooKId,CT.BooksQty
from CartTable as CT inner join BookTable as BT on BT.BookId=CT.BookId
where CT.UserId=@UserId;
end
GO 

create table WishListTable
(
WishListId int identity(1,1) primary key,
BooKId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(BooKId),
UserId int not null,
foreign key(UserId) REFERENCES UserTable(UserId)
);

create procedure [dbo].[spAddInWishlist]
(
@BooKId int,
@UserId int
)
as
begin
insert into WishListTable values
(
@BooKId,@UserId
)
end
GO


create procedure [dbo].[spGetAllBooksinWishList]
(
@UserId int
)
as
begin
select WT.UserId,WT.WishListId,BT.BookName,BT.AuthorName,BT.Rating,BT.PeopleRating,BT.OriginalPrice,BT.DiscountPrice,BT.BookDetails,BT.BookQuantity,
BT.BookImage,WT.BooKId
from WishListTable as WT inner join BookTable as BT on BT.BookId=WT.BookId
where WT.UserId=@UserId;
end
GO

create procedure [dbo].[spDeleteFromWishlist]
(
@WishListId int,
@UserId int
)
as
begin
Delete from WishListTable where WishListId=@WishListId and UserId=@UserId ;
end
GO

create table FeedbackTable
(
FeedbackId int identity(1,1) primary key,
Rating int not null,
Comment varchar(350) not null,
BooKId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(BooKId),
UserId int not null,
foreign key(UserId) REFERENCES UserTable(UserId)
);

create procedure [dbo].[spAddFeedback]
(
@Comment varchar(350),
@Rating int,
@BookId int,
@UserId int
)  
as
begin
insert into FeedbackTable(Comment,Rating,BookId,UserId) values
(
@Comment,@Rating,@BookId,@UserId
)
end
GO

create procedure [dbo].[spGetFeedback]
(
@BooKId int
)
as
begin
select UT.FullName,FT.FeedbackId,FT.Comment,FT.Rating,FT.BooKId
from FeedbackTable as FT inner join UserTable as UT on FT.UserId=UT.UserId
where FT.BooKId=@BooKId;
end
GO

---create addressType table
create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(300) 
);
select count(*) from AddressTypeTable

insert into AddressTypeTable values('Home'),('Office'),('Other');

---create address table
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address varchar(300),
City varchar(250),
State varchar(350),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT 
FOREIGN KEY (UserId) REFERENCES UserTable(UserId)
);

---create procedure to AddAddress
--- Procedure To Add Address
create procedure spAddAddress
(
@Address varchar(300),
@City varchar(250),
@State varchar(350),
@TypeId int,
@UserId int
)
as
begin
Insert into AddressTable 
values(@Address, @City, @State, @TypeId, @UserId);
end
go


create procedure spUpdateAddress
(
@AddressId int,
@Address varchar(300),
@City varchar(250),
@State varchar(250),
@TypeId int
)
as
BEGIN
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
go

--create procedure to delete address
create Procedure spDeleteAddress
(
@AddressId int
)
as
BEGIN
Delete from AddressTable where AddressId = @AddressId 
end
go

----------------------GetAllAddress----------------------------

create procedure [dbo].[spGetAllAddress]
(
@UserId int
)
as
begin
select AT.AddressId,AT.Address,AT.City,AT.State,AT.TypeId,AT.UserId,ATT.AddressType
from AddressTable as AT inner join AddressTypeTable as ATT on AT.TypeId=ATT.TypeId
where AT.UserId=@UserId;
end
GO



-----orderTable-----

create table Orders(
	OrdersId int identity(1,1) not null primary key,
	OrderBookQuantity int not null,
	TotalPrice int not null,
	OrderDate Date not null,
	UserId int not null foreign key (UserId) references UserTable(UserId),
	BookId int not null foreign key (BookId) references BookTable(BookId),
	AddressId int not null foreign key (AddressId) references AddressTable(AddressId)
);

-------Sp for AddOrder---------------------------------------------------------------------------------------------

create or alter Proc spAddOrder
(
	@OrderBookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
begin
	set @TotalPrice = (select DiscountPrice from BookTable where BookId = @BookId);
	If(Exists(Select * from BookTable where BookId = @BookId))
		begin
			If(Exists (Select * from UserTable where UserId = @UserId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orders(TotalPrice, OrderBookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@OrderBookQuantity, @OrderBookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update BookTable set BookQuantity=BookQuantity-@OrderBookQuantity where BookId = @BookId;
						Delete from CartTable where BookId = @BookId and UserId = @UserId;
						select * from Orders;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				end
			Else
				Begin
					Select 3;
				End
		End
	Else
		Begin
			Select 2;
		End
end;


------------------------spForCancelOrder----------------------------------------------------------------------
USE [BooKStoreDb]
GO

/****** Object:  StoredProcedure [dbo].[spCancelOrder]    Script Date: 11/18/2022 10:57:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[spCancelOrder]
(
@OrdersId int
)
as
Declare @CancelBookQuantity int
declare @CancleBookid int
set @CancleBookid =(select BookId from Orders where OrdersId = @OrdersId);
set @CancelBookQuantity= (select OrderBookQuantity from Orders where OrdersId = @OrdersId);
BEGIN
	Begin try
		Begin Transaction
			Delete from Orders where OrdersId = @OrdersId;
			update BookTable set BookQuantity=BookQuantity+@CancelBookQuantity where BookId =@CancleBookid; 
		commit Transaction
	End try
	Begin Catch
		rollback;
	End Catch
end
go