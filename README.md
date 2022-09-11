# Sistema de Login utilizando banco dedos MySQL

# Configuração
Criar arquivo .env com a string de conexão
```
mysql_connection="server=localhost;port=3306;User Id=root;database=alexandre_d3_avaliacao;password=senha;"
```
<ol>
    <li> server: No caso foi colocado localhost (host local)</li>
    <li> port: Foi utilizada a porta padrão do MySQL</li>
    <li> User Id: Usuário</li>
    <li> database: Banco de dados</li>
    <li> password: Senha</li>
</ol>
Notar que seré necessário criar na mão o banco de dado. Caso não tenha sido criado o banco, provavelmente, o código não irá funcionar.

O código foi criado utilizando o VSCode e utilizando a extensão NuGet.
As extensões do NuGet utilizados foram:
<div>
    <li> DotNetEnv</li>
    <li> MySql.Data</li>
</div>

## Instação do MySQL
O instalador do mysql installer pode ser encontrado em:
https://dev.mysql.com/downloads/installer/

Para o ambiente de teste foi utilizado também o mysql-workbench.

Para conectar com o banco de dados utilizando o comando mysql:
```
mysql --host=localhost --user=root --password=senha alexandre_d3_avaliacao
```

Caso queira criar um novo usuário basta utilizando mysql:

Criação do Usuário com credenciais.

```
CREATE USER 'novo_usuário'@'localhost' IDENTIFIED BY 'senha';
```

Adição de privilégios totais.

```
GRANT ALL PRIVILEGES ON * . * TO 'novo_usuario'@'localhost';
```

Salvar alterações:
```
FLUSH PRIVILEGES;
```

Uma vez configurada o banco de dados não esquecer de modificar a string de conexão no arquivo .env



# Inicialização
Caso esteja utilizando o VSCode utilizar dotnet run.

Caso esteja utilizando o Visual Studio provavelmente basta clicar play.

Não esquecendo de instalar os pacotes DotNetEnv e MySql.Data antes e configurar o banco de dados mysql corretamente.

# Docker

Caso queira é possível fazer o teste utilizando docker.

Inicialmente é necessário instalar docker e docker-compose em sua máquina.

Em seguida crie o arquivo .env qude deve ser algo do tipo:
```
mysql_connection="server=mysql;port=3306;User Id=root;database=alexandre_d3_avaliacao;password=senha;"
```
Como o password agora vem do arquivo docker, não esqueça de modificar MYSQL_ROOT_PASSWORD o arquivo Docker/docker-compose-dev.yaml para a senha desejada.

```
<ol>
    <li> server: mysql que é o server do container mysql (manter como mysql)</li>
    <li> port: Foi utilizada a porta padrão do MySQL que é 3306</li>
    <li> User Id: Usuário</li>
    <li> database: Banco de dados</li>
    <li> password: Senha</li>
</ol>
```

Utilize o comando a seguir para levantar o container
```
 docker-compose -f Docker/docker-compose-dev.yaml up -d 
```

Utilize o comando para refazer o build
```
docker-compose -f Docker/docker-compose-dev.yaml up -d --build
```

Utilize o comando para destruir o container junto com o volume de dados e a imagem
```
 docker-compose -f Docker/docker-compose-dev.yaml down --volumes --rmi all (remove volumes and images)
```

Quando rodar docker compose up (o primero dos 3 comandos), serão instalados tanto o mysql V5.7 como a aplicação em si.

A criação do banco de dados é manual. Assim, inicialmente terá que entrar no container do mysql e criar a tabela usuario.

Acesse se dve conectar ao container mysql (verifique o nome do container antes utilizando o comando docker ps) com:
```
docker exec -it docker_mysql_1 bash
```

Acesse ao mysql utilizando o comando:
```
mysql --host=localhost --user=root --password=senha alexandre_d3_avaliacao
```

Conecte-se inicialmente à tabela alexandre_d3_avaliacao utilizando o comando:
```
use alexandre_d3_avaliacao;
```

Crie a tabela usuário com o seguinte comando:
```
create table usuario(
  id int auto_increment,
  email varchar(500),
  senha varchar(500),
  nome varchar(500),
  primary key(id)
);
```

Uma vez cconfigurado, entre no container da acplicão com o comando: 
```
docker exec -it aplication_container bash
```
Rode a aplicação com o comando:
```
dotnet run
```

Finalmente o sistema vai começar a funcionar.

# Funcionamento

