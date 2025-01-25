create table if not exists app_user_role (
    roleid serial primary key,
    abbreviation varchar(16) not null,
    roleName varchar(50) not null
);

create table if not exists app_user_role_user (
    userid uuid not null,
    roleid int not null,
    primary key (userid, roleid)
);