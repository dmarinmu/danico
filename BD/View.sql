select * from Hotel
select * from HotelService
select * from vHotel
select * from Service
select * from Room
select * from Town
select * from TripType
select * from vDiscount
select * from vQuote
select * from UserT
select * from Quote
/*En hotel service servicetype= h es excluyente con idroom.
es decir, si la fila esta asociada con un idroom, el servicetype de esa file no puede ser H
un tipo de habitacion puede tener hasta seis imagenes 
una habitacion puede no estar asociada a un tipo de servicio
todas las fotos de lugarescomunes si deben estar asociadas con un tipo de servicio H
un hotel puede tener  mas de una fila para un tipo de habitacion.
tipos de habitacion:
basica
especial
suite 
*/

/******************************************QUERIES***************************************************/

select * from vHotel
where Hpk_idHotel =4

select COUNT(*) from vHotel
where Hpk_idHotel =4
select distinct


select * from vHotel  where Hpk_idHotel  = 4
SELECT  Hotel.*, Room.*
FROM	Hotel  
 LEFT OUTER JOIN Room
ON		Hotel.pk_idHotel = Room.fk_idHotel

/******************************************VIEWS*****************************************************/
--drop view vHotel 
CREATE VIEW vHotel AS
SELECT  ISNULL( Hotel.pk_idHotel,-999) Hpk_idHotel, NULLIF(Hotel.name,'') Hname, NULLIF(Hotel.address,'') Haddress,NULLIF(Hotel.description,'') Hdescription,Hotel.phone1 Hphone1,Hotel.phone2 Hphone2,Hotel.email Hemail,NULLIF(Hotel.fk_idTown,'') Hfk_idTown,Hotel.imagesDirectory HimagesDirectory,Hotel.coverImage HcoverImage,  Hotel.stars Hstars,NULLIF(Hotel.state,'') Hstate,Hotel.lat Hlat,Hotel.lng Hlng,
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


--drop view vDiscount 
CREATE VIEW vDiscount AS
SELECT ISNULL( Hotel.pk_idHotel,-999) Hpk_idHotel,NULLIF(Hotel.name,'') Hname,
	   Discount.description Ddescription,Discount.percentage Dpercentage,Discount.price Dprice,Discount.startDate DstartDate, Discount.endDate DendDate,
	   Town.name Tname
FROM   Discount, Hotel left outer join Town
on     Hotel.fk_idTown = Town.pk_idTown
WHERE  Hotel.pk_idHotel = Discount.idHotel
AND    Discount.state =  1
AND    Hotel.state =1

select * from vQuote
--drop view vQuote 
CREATE  VIEW vQuote AS
SELECT  NULLIF(Hotel.name,'') Hname,
		UserT.name Uname, NULLIF(UserT.email,'') Uemail ,
	    Quote.requestDate QrequestDate,ISNULL(Quote.idHotel,'') QidHotel, Quote.description Qdescription,Quote.tripStartDate QtripStartDate,Quote.tripEndDate QtripEndDate ,idTripType
	    --TripType.name Tname, TripType.pk_idTripType Tpk_idTripType 
FROM    Hotel,UserT,Quote left outer join TripType 
	    	 
ON		Quote.idTripType = TripType.pk_idTripType
WHERE	UserT.pk_idUser = Quote.iduser
AND		Hotel.pk_idHotel = Quote.idHotel
