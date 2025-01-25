create table app_game_card (
    gameid bigserial primary key ,
    title varchar(100),
    description text,
    rate smallint check (rate >= 0 and rate <= 10),
    images_path varchar(100)
);