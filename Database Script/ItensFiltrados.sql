-- Table: public.ItensFiltrados

-- DROP TABLE public."ItensFiltrados";

CREATE TABLE public."ItensFiltrados"
(
    "cProd" character varying COLLATE pg_catalog."default",
    "xProd" character varying COLLATE pg_catalog."default",
    "NCM" character varying COLLATE pg_catalog."default",
    "CFOP" character varying COLLATE pg_catalog."default",
    "uCom" character varying COLLATE pg_catalog."default",
    "qCom" real,
    "vUnCom" real,
    orig integer,
    "vBC" real,
    "pICMS" real,
    "vICMS" real,
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "cNF" integer NOT NULL,
    "ProcessoID" integer NOT NULL,
    "ItemID" integer NOT NULL,
    "CSOSN" integer,
    "Entrada" boolean NOT NULL,
    "nNF" integer NOT NULL,
    "dhEmi" timestamp with time zone NOT NULL,
    "dhSaiEnt" timestamp with time zone,
    "CST" integer,
    CONSTRAINT "ItensFiltrados_pkey" PRIMARY KEY ("ID"),
    CONSTRAINT "Item_FK" FOREIGN KEY ("ItemID")
        REFERENCES public."Itens" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT "Processo_FK" FOREIGN KEY ("ProcessoID")
        REFERENCES public."Processos" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public."ItensFiltrados"
    OWNER to postgres;
-- Index: CFOP_Filtered_Index

-- DROP INDEX public."CFOP_Filtered_Index";

CREATE INDEX "CFOP_Filtered_Index"
    ON public."ItensFiltrados" USING btree
    ("CFOP" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: CSOSN_Filtered_Index

-- DROP INDEX public."CSOSN_Filtered_Index";

CREATE INDEX "CSOSN_Filtered_Index"
    ON public."ItensFiltrados" USING btree
    ("CSOSN" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: Entrada_Filtered_Index

-- DROP INDEX public."Entrada_Filtered_Index";

CREATE INDEX "Entrada_Filtered_Index"
    ON public."ItensFiltrados" USING btree
    ("Entrada" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: NCM_Filtered_Index

-- DROP INDEX public."NCM_Filtered_Index";

CREATE INDEX "NCM_Filtered_Index"
    ON public."ItensFiltrados" USING btree
    ("NCM" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;