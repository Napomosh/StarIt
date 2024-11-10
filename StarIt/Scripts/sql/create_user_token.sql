create table user_token (
    tokenid binary(16) default(uuid_to_bin(uuid())) primary key,
    userid binary(16),
    created timestamp
);