-- SCHEMA VERSION 11: 2020-10-15 --
-- Privacy 2.0

alter table system_guild add column private_guild boolean default false;

update info set schema_version = 11;