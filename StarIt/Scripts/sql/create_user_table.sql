create table app_user(
    user_id binary(16) default(uuid_to_bin(uuid())) primary key,
    email varchar(50),
    password varchar(100),
    salt varchar(50),
    status int
);

create table user_security(
    user_security_id serial primary key,
    user_id varchar(36),
    verification_code varchar(50)
);
