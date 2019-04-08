use [ISC064_SECURITY]
update PAGE set Halaman = REPLACE(Halaman,'\ISC056\','\ISC064\')
update PAGEDENY set Halaman = REPLACE(Halaman,'\ISC056\','\ISC064\')
update PAGESEC set Halaman = REPLACE(Halaman,'\ISC056\','\ISC064\')

select * from PAGESEC


