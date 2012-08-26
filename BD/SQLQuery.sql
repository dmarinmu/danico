BULK
INSERT Town
FROM 'C:/csvtest.txt'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n'
)
GO-- esto no funciono entonces se insertan con los scripts

select * from Town
select * from hotel

CREATE  VIEW cvHotel AS
SELECT	Hotel.pk_idHotel,
		Hotel.name,
		Hotel.address,
		Hotel.description,
		Hotel.phone1,
		Hotel.phone2,
		Hotel.email,
		Room.price,
		Town.name
FROM	Town,
		Hotel,   
		(select Room.price from Hotel left join Room
		on  Hotel.pk_idHotel = Room.fk_idHotel) Room 
WHERE	Hotel.fk_idTown = Town.pk_idTown
