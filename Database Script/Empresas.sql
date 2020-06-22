-- Table: public.Empresas

-- DROP TABLE public."Empresas";

CREATE TABLE public."Empresas"
(
    "Nome" character varying COLLATE pg_catalog."default",
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Cidade" character varying COLLATE pg_catalog."default",
    "UF" character varying COLLATE pg_catalog."default",
    "CNPJ" character varying COLLATE pg_catalog."default",
    CONSTRAINT "ID_PK" PRIMARY KEY ("ID")
)

TABLESPACE pg_default;

ALTER TABLE public."Empresas"
    OWNER to postgres;