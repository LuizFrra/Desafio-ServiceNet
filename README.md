# Desafio-ServiceNet
Repositório responsável por conter o densenvolvimento de um sistema web simplificado, proposto por um <a href="https://drive.google.com/open?id=18OhTQEQ2h-tMfxKH8DWpMDF4om6_0TvB">desafio da ServiceNet</a>.
Para isso foi construído uma API utilizando o Framework .Net Core, banco de dados postgres e o angular e BootStrap para a construção da aplicação.

## Recursos Utilizados: :computer:
Para o desenvolvimento do projeto foi utilizado as seguintes aplicações/Frameworks :

* Banco de Dados
    - [PostgreSQL](https://www.postgresql.org/download/)

* Containers
    - [Docker E Docker-Compose](https://www.docker.com/)

* Visual Studio Code

    - [Visual Studio Code](https://code.visualstudio.com/)
    - [.NET Core SDK](https://www.microsoft.com/net/download)
    - [Node.js](https://nodejs.org/en/)
    - [@angular/cli](https://www.npmjs.com/package/@angular/cli)
    - [BootStrap](https://getbootstrap.com/)

* Visual Studio

    - [Visual Studio](https://bit.ly/2zBXxF8)
    - [.NET Core 3.0](https://www.microsoft.com/net/download)
    
Caso resolva usar o Visual Studio como IDE, durante a sua instalação procure instalar os seguintes itens:
    - ASP.NET & Web Development;
    - .NET Core Cross-Platform Development;
    
## Executando o Projeto Localmente :fire:
> __DockerToolBox__: Caso utilize o DockerToolBox, o ip que deve ser passado na string de conexão do serviço backend e do argumento no serviço fronted, deve ser o da máquina docker, bem como as requisições através do browser devem ser feitas a esse ip na porta da aplicação.

Se estiver utilizando linux é preciso ir até a pasta desafioSPA e executar este comando :
```
> chmod u+x env_setup.sh
```
Em seguida basta digitar o seguinte comando na pasta raiz :
```
>  docker-compose build --no-cache && docker-compose up --force-recreate

```
Por default, a aplicação angular estará rodando na portar 4200, a api na porta 5000 e o banco de dados na porta 5432.

Você pode alterar as portas e outras variáveis no arquivo [docker-compose.yml], qualquer alteração na string de conexão deve ser refletica no arquivo [Dockerfile] do banco de dados que se encontra na pasta [Postgres].
