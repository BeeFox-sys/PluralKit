
-- SCHEMA VERSION 6: 2020-05-22 --

alter table members add column birthday_string;
alter table members rename column birthday to birthday_date;
update info set schema_version = 6;