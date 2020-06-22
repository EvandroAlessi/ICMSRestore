-- Table: public.Itens

-- DROP TABLE public."Itens";

CREATE TABLE public."Itens"
(
    "nItem" integer,
    "cProd" character varying COLLATE pg_catalog."default" NOT NULL,
    "cEAN" character varying COLLATE pg_catalog."default",
    "xProd" character varying COLLATE pg_catalog."default",
    "uCom" character varying COLLATE pg_catalog."default",
    "qCom" real,
    "vUnCom" real,
    orig integer,
    "modBC" integer,
    "vBC" real,
    "pICMS" real,
    "vICMS" real,
    "cEnq" integer,
    "CST_IPI" character varying COLLATE pg_catalog."default",
    "CST_PIS" character varying COLLATE pg_catalog."default",
    "vBC_PIS" real,
    "pPIS" real,
    "vPIS" real,
    "CST_COFINS" character varying COLLATE pg_catalog."default",
    "vBC_COFINS" real,
    "pCOFINS" real,
    "vCOFINS" real,
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "NFeID" integer NOT NULL,
    "NCM" integer NOT NULL,
    "CFOP" integer NOT NULL,
    "CST" integer,
    "CSOSN" integer,
    "vProd" real,
    "cEANTrib" integer,
    "uTrib" character varying COLLATE pg_catalog."default",
    "qTrib" real,
    "vUnTrib" real,
    "indTot" integer,
    CONSTRAINT "ID" PRIMARY KEY ("ID"),
    CONSTRAINT "NFe_FK" FOREIGN KEY ("NFeID")
        REFERENCES public."NFe" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public."Itens"
    OWNER to postgres;
-- Index: CFOP_Index

-- DROP INDEX public."CFOP_Index";

CREATE INDEX "CFOP_Index"
    ON public."Itens" USING btree
    ("CFOP" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: CSOSN_Index

-- DROP INDEX public."CSOSN_Index";

CREATE INDEX "CSOSN_Index"
    ON public."Itens" USING btree
    ("CSOSN" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: CST_Index

-- DROP INDEX public."CST_Index";

CREATE INDEX "CST_Index"
    ON public."Itens" USING btree
    ("CST" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: NCM_Index

-- DROP INDEX public."NCM_Index";

CREATE INDEX "NCM_Index"
    ON public."Itens" USING btree
    ("NCM" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Trigger: Filter

-- DROP TRIGGER "Filter" ON public."Itens";

CREATE TRIGGER "Filter"
    AFTER INSERT
    ON public."Itens"
    FOR EACH ROW
    EXECUTE PROCEDURE public."Filter"();