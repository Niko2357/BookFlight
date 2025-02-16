use FlyAir;

create table Plane(
id int primary key identity(1,1),
name varchar(50), 
capacity int, 
producer varchar(50)
);

create table Flight(
id int primary key identity(1,1),
flightNumber varchar(30), 
planeId int foreign key references Plane(id),
departure datetime, 
arrival datetime, 
departurePlace varchar(50),
arrivalPlace varchar(50)
);

create table Passenger(
id int primary key identity(1,1),
firstname varchar(50),
surname varchar(50),
birthDate date,
email varchar(50) check(email like('%@%.%'))
);

create table UserAccount(
id int primary key identity(1,1),
username varchar(50) unique not null,
password varchar(50) not null, 
role varchar(30) check(role in('admin', 'customer'))
);

create table Seat(
id int primary key identity(1,1),
flightId int foreign key references Flight(id),
number varchar(30) not null,
isAvailable bit default 1
);

create table Reservation(
id int primary key identity(1,1),
flightId int foreign key references Flight(id),
passengerId int foreign key references Passenger(id),
seatId int foreign key references Seat(id),
price decimal,
isPaid bit default 0
);
