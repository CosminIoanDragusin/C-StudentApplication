CREATE TABLE course
(	
	id int primary key identity(1,1),
	curs varchar(MAX) NULL,
	descriere varchar(MAX) NULL,
	licenta varchar(MAX) NULL,
	department varchar(MAX) NULL,
	pret float(20, 2) NULL,
	statusul varchar(MAX) NULL,
	
)

select * from course;