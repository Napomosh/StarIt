create table app_game_card (
    game_id bigint unsigned auto_increment not null primary key,
    title varchar(100),
    description longtext,
    rate tinyint unsigned, check (rate <= 10),
    images_path varchar(100)
);