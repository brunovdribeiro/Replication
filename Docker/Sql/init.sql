CREATE PUBLICATION replicaPublication FOR ALL TABLES;

SELECT * FROM pg_create_logical_replication_slot('replicaSlot', 'pgoutput');

CREATE TABLE "Author"
(
    "Id"   UUID PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL
);

CREATE TABLE "Book"
(
    "Id"       SERIAL PRIMARY KEY,
    "Name"     VARCHAR(40) UNIQUE NOT NULL,
    "AuthorId" UUID               NOT NULL REFERENCES "Author" ("Id")
);

CREATE USER repUser WITH PASSWORD 'repPwd';
ALTER ROLE repUser WITH REPLICATION;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO repUser;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO repUser;

