create table if not exists app_user_role (
    roleid int auto_increment not null primary key,
    abbreviation varchar(16) not null,
    roleName varchar(50) not null
);

create table if not exists app_user_role_user (
    userid binary(16) not null,
    roleid int not null,
    primary key (userid, roleid)
);