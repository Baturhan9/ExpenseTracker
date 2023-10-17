create table Client(
    id int primary key AUTO_INCREMENT,
    c_login varchar(50) not null,
    c_password varchar(50) not null,
    c_name varchar(50) not null
);

create table Expense_group(
    id int primary key AUTO_INCREMENT,
    client_id int,
    eg_name varchar(50),
    eg_amount_spends decimal(10,2),
    eg_extra_info varchar(150),  
    constraint fk_client_id foreign key (client_id) references Client(id),
    constraint check_positive_amount_spends check(eg_amount_spends > 0)
);

CREATE TABLE Spending(
    id int primary key AUTO_INCREMENT,
    expense_group_id int,
    client_id int,
    s_value decimal(10,2),
    s_extra_info varchar(150),
    constraint fk_client_id foreign key (client_id) references Client(id),
    constraint check_positive_value check(s_value > 0)
);

