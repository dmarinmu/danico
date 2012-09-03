select * from Hotel
select * from HotelService
select * from Room
select * from Service
select * from Town
select * from TripType

select * from vHotel
where Hpk_idHotel =4

select COUNT(*) from vHotel
where Hpk_idHotel =4
select distinct
--drop view vHotel 

CREATE VIEW vHotel AS
SELECT  ISNULL( Hotel.pk_idHotel,-999) Hpk_idHotel, NULLIF(Hotel.name,'') Hname, NULLIF(Hotel.address,'') Haddress,NULLIF(Hotel.description,'') Hdescription,Hotel.phone1 Hphone1,Hotel.phone2 Hphone2,Hotel.email Hemail,NULLIF(Hotel.fk_idTown,'') Hfk_idTown,Hotel.imagesDirectory HimagesDirectory,Hotel.coverImage HcoverImage,  Hotel.stars Hstars,NULLIF(Hotel.state,'') Hstate,
	    HotelService.idHotel HSidHotel,HotelService.idService HSidService,HotelService.price HSprice,HotelService.description HSdescription ,HotelService.imagesDirectory HSimagesDirectory,HotelService.coverImage0 HScoverImage0, HotelService.coverImage1 HScoverImage1, HotelService.coverImage2 HScoverImage2,HotelService.idRoom HSidRoom,
	    Service.name Sname, Service.type Stype, Service.pk_idService Spk_idService,
	    Room.pkidHabitacion RpkidHabitacion,Room.type Rtype
FROM	Hotel  
LEFT OUTER JOIN HotelService
ON		Hotel.pk_idHotel = HotelService.idHotel
LEFT OUTER JOIN  Service
on      HotelService.idService = Service.pk_idService
LEFT OUTER JOIN  Room
ON		HotelService.idRoom = Room.pkidHabitacion
WHERE	Hotel.state = 1
and Hotel.pk_idHotel = 4


select * from vHotel  where Hpk_idHotel  = 4
SELECT  Hotel.*, Room.*
FROM	Hotel  
 LEFT OUTER JOIN Room
ON		Hotel.pk_idHotel = Room.fk_idHotel