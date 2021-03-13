-- SQLite
create table DailyCovidData (
    id integer PRIMARY KEY AUTOINCREMENT,
    pruid integer,
    prname text,
    prnameFR text,
    date text,
    numconf integer,
    numprob integer,
    numdeaths integer,
    numtotal integer,
    numtoday integer,
    ratetotal integer
)

select * from DailyCovidData;