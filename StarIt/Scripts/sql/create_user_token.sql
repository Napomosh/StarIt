create table user_token (
    tokenid uuid default  gen_random_uuid() primary key,
    userid uuid not null,
    created timestamp default current_timestamp
);