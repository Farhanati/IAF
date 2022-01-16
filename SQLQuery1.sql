use project
create table user_info(
user_id int primary key,
user_email varchar(max),
user_password varchar(max),
user_age varchar(max),
user_address varchar(max),
user_category varchar(max),
user_name varchar(max),
user_img varchar(max)
)
create table expo(
exp_id int primary key,
exp_title varchar(max),
exp_img varchar(max),
exp_date varchar(max),
exp_discription varchar(max),
exp_owner varchar(max),
exp_status varchar(max)
)
create table review(
r_id int primary key,
r_like varchar(max),
r_comment varchar(max)
)
alter table expo add name varchar(max)
select * from user_info