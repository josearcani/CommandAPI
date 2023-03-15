# CommandAPI

Command API from the book The Complete ASP.NET Core 3 API Tutorial - Les Jackson

### docker-compose.yml
```yml
version: "3.8"

services:
  postgres:
    container_name: postgres
    image: postgres
    restart: always
    ports:
      - "5432:5432"
    environment:
      - DATABASE_HOST=127.0.0.1
      - POSTGRES_USER=cmddbuser
      - POSTGRES_PASSWORD=pa55w0rd!
      - POSTGRES_DB=jose_arcani
    volumes:
      - pgdata:/var/lib/postgresql/data
    networks:
      - skynet
volumes:
  pgdata:
networks:
  skynet: {}
```