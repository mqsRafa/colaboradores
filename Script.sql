use sys;
create database teste;
use teste;

create table colaborador
(
	id_colaborador int not null auto_increment,
	nome_colaborador varchar(40) not null,
	id_cargo_colaborador int,
	id_departamento_colaborador int,
	constraint pk_colaboradores primary key (id_colaborador)
);

create table empresa
(
	id_empresa int not null auto_increment,
	razao_social_empresa varchar(100) not null,
	nome_fantasia_empresa varchar(100) not null,
	telefone_empresa varchar(14),
	endereco_empresa varchar(50),
	numero_endereco varchar(6),
	complemento_endereco varchar(20),
	cep_empresa varchar(8),
	cidade_empresa varchar(35),
	uf_empresa varchar(2),
	constraint pk_empresas primary key (id_empresa)
);

CREATE TABLE empresa_colaborador (
    id_empresa int not null,
    id_colaborador int not null,
    constraint pk_empresa_colaborador primary key (id_empresa, id_colaborador),
    constraint fk_empresa_colaborador_empresa 
        foreign key (id_empresa) references empresa (id_empresa)
        on delete cascade on update cascade,
    constraint fk_empresa_colaborador_colaborador
        foreign key (id_colaborador) references colaborador (id_colaborador)
        on delete cascade on update cascade
);
