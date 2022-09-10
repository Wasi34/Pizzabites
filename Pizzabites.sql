Create Database Pizzabites

use Pizzabites 

create table Customer(
	CUSEmail varchar(50) primary key NOT NULL,
	CUSName varchar(20) NOT NULL,
	CUSPassword varchar (20) NOT NULL,
	CUSContactNo  varchar(11) NULL,
	CUSAddress  varchar(100) NULL
)

drop table Customer

Create table Product(
	PRID int primary key identity(1000,1),
	PRName varchar(50),
	PRImage nvarchar(200),
	PRType varchar(20),
	PRDescription varchar(8000),
	PRPrize Decimal(8,2)	
)
drop table Product

SELECT * FROM product

SELECT * FROM Customer

truncate table customer

Insert into Customer(CUSEmail,CUSName,CUSPassword) 
Values('wasi34@gmail.com','Wasi','wasi34')

truncate table customer

truncate table Product

Insert into Product(PRName,PRImage,PRType,PRDescription,PRPrize)
Values('Senorita','pizza-1.jpg','Pizza','A hugely popular margherita, with a deliciously tangy single cheese topping. (Pizza Size 12 Inch)','500'),
('Mexican Green Wave','pizza-3.jpg','Pizza','A pizza loaded with crunchy onions, crisp capsicum, juicy tomatoes and jalapeno with a liberal sprinkling of exotic Mexican herbs. (Pizza Size 12 Inch)','500'),
('Meatball pizza','pizza-29.jpg','Pizza','A combination of cheese and meatball.(Pizza Size 12 Inch)','500'),
('Naga Pizza- CLASSIC VEG','pizza-3.jpg','Pizza','Bite into delight, looks good and tastes great.(Pizza Size 12 Inch)','650'),
('Veggi Paradise','pizza-4.jpg','Pizza','Goldern Corn, Black Olives, Capsicum & Red Paprika. (Pizza Size 12 Inch)','550'),
('Cheese Burst','pizza-43.jpg','Pizza','Crust with oodles of yummy liquid cheese filled inside.(Pizza Size 12 Inch)','600'),
('Deluxe Veggie','pizza-5.jpg','Pizza','For a vegetarian looking for a BIG treat that goes easy on the spices.(Pizza Size 12 Inch)','600'),

('Cheese Burger','Burger1.jpg','Burger','Enjoy the best cheesy burger','250'),
('Double Decker','Burger2.jpg','Burger','Double Patty','300'),
('Charcoal Burger','Burger3.jpg','Burger','Black bread and filled with cheese','300'),
('Chicken Burger','Burger4.jpg','Burger','A burger loaded with crunchy onions, crisp capsicum and chicken','300'),
('Naga Burger','Burger5.jpg','Burger','For the naga lover, a best deal','250'),


('7Up','7UP.jpg','Soft Drinks','500ml','40'),
('Mirinda','Mirinda.jpg','Soft Drinks','500ml','35'),
('Pepsi','Pepsi.jpg','Soft Drinks','500ml','35'),
('Coka Cola','COCACOLA.jpg','Soft Drinks','500ml','35'),
('Mountain Dew','MountainDew.jpg','Soft Drinks','500ml','35'),

('Lava Cake','LavaCake.jpg','Dessert','Filled with delecious molten chocolate inside.','250'),
('PANEER & ONION','Butter.jpg','Dessert','Creamy Paneer | Onion','150'),
('Milk Shake','MilkShake.jpg','Dessert','a sweet flavouring such as fruit or chocolate, and typically ice cream, whisked until it is frothy.','200'),
('Strawberry Cheese cake','StrawberyCake.jpg','Dessert','A soft creamy strawberry cake.','250'),
('Red Velvet cake','RedVelvet.jpg','Dessert','A soft creamy red velvet cake.','350'),
('Red Cheese cake','ChesseCake.jpg','Dessert','A soft creamy cake.','250')

SELECT * FROM Product

UPDATE Product
SET PRImage = 'Pizza-3.jpg'
WHERE PRID=1001;

DELETE FROM Product WHERE PRID IN (1023);

Create table ContactUs(
	ContactID int primary key identity(10000,1),
	FullName varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	ContactMessage varchar(5000) NOT NULL,
	AdminReply varchar(5000)
)

select * from  ContactUs
drop table ContactUs

Create table OrderList(
	OrderID int primary key identity(1,1),
	itemName varchar(500),
	totalPrice int ,
	cusEmail varchar(500),
	cusAddress varchar(500),
	orderDate datetime ,
	PaymentMethod varchar(20),
	
	--DeliveryBoy varchar(20),
	--DeliveryBoyPhoneNumber varchar(11),
	--DeliveryTime varchar(20),
	--Confirmation varchar(20)
)

drop table OrderList
truncate table orderlist
select * from OrderList

truncate table OrderList

Create table Comment(
CommentID int primary key identity(100,1),
ProductID int Not Null,
CustName varchar (50) Not Null,
CommentMessage varchar(5000) Not Null
)
drop table comment

select * from Comment

Select * from Product

select * from Customer

select * from orderlist
