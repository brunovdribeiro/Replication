CREATE PUBLICATION replica_publication FOR ALL TABLES;

SELECT * FROM pg_create_logical_replication_slot('replica_slot', 'pgoutput');

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

CREATE USER rep_user WITH PASSWORD 'rep_pwd';
ALTER ROLE rep_user WITH REPLICATION;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO rep_user;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO rep_user;

