create database alexandre_d3_avaliacao;

use alexandre_d3_avaliacao;

create table usuario(
  id int auto_increment,
  email varchar(500) unique,
  senha varchar(500),
  nome varchar(500),
  primary key(id)
  
);

INSERT INTO usuario (email, senha, nome)
VALUES ("alexandre.oide@gmail.com", "1234", "alexandre");

SELECT * FROM usuario where email = "admin@email.com";

SELECT * FROM usuario ;

drop table usuario;

update usuario SET nome = "Oid"
where email = 'alexandre@gmail.com';
