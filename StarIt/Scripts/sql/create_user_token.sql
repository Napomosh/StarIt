create table user_token (
    token_id binary(16) default(uuid_to_bin(uuid())) primary key,
    user_id binary(16),
    created timestamp
);