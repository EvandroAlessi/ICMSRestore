-- Table: public.MVA

-- DROP TABLE public."MVA";

CREATE TABLE public."MVA"
(
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "CEST" integer,
    "NCM_SH" bigint NOT NULL,
    "Descricao" character varying COLLATE pg_catalog."default",
    "MVA_ST " real NOT NULL,
    "DataInicial" date NOT NULL,
    "DataFinal" date NOT NULL,
    CONSTRAINT "MVA_pkey" PRIMARY KEY ("ID")
)

TABLESPACE pg_default;

ALTER TABLE public."MVA"
    OWNER to postgres;
-- Index: MVA_NCM_Index

-- DROP INDEX public."MVA_NCM_Index";

CREATE INDEX "MVA_NCM_Index"
    ON public."MVA" USING btree
    ("NCM_SH" ASC NULLS LAST)
    TABLESPACE pg_default;