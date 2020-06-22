-- Table: public.Processos

-- DROP TABLE public."Processos";

CREATE TABLE public."Processos"
(
    "EmpresaID" integer NOT NULL,
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Nome" character varying COLLATE pg_catalog."default",
    "DataCriacao" date NOT NULL,
    "InicioPeriodo" date NOT NULL,
    "FimPeriodo" date NOT NULL,
    CONSTRAINT "ID_PFK" PRIMARY KEY ("ID"),
    CONSTRAINT "Empresa_FK" FOREIGN KEY ("EmpresaID")
        REFERENCES public."Empresas" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public."Processos"
    OWNER to postgres;