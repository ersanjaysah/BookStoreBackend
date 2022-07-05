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