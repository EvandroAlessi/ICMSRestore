-- Table: public.ProcessosUpload

-- DROP TABLE public."ProcessosUpload";

CREATE TABLE public."ProcessosUpload"
(
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "ProcessoID" integer NOT NULL,
    "QntArq" integer NOT NULL,
    "Ativo" boolean NOT NULL DEFAULT true,
    "PastaZip" character varying COLLATE pg_catalog."default" NOT NULL,
    "DataInicio" timestamp with time zone NOT NULL,
    "Entrada" boolean NOT NULL DEFAULT false,
    CONSTRAINT "PK" PRIMARY KEY ("ID"),
    CONSTRAINT "Processo_FK" FOREIGN KEY ("ProcessoID")
        REFERENCES public."Processos" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public."ProcessosUpload"
    OWNER to postgres;