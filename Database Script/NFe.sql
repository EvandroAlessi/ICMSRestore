-- Table: public.NFe

-- DROP TABLE public."NFe";

CREATE TABLE public."NFe"
(
    "CEP" character varying COLLATE pg_catalog."default",
    "CEP_DEST" character varying COLLATE pg_catalog."default",
    "CNPJ" character varying COLLATE pg_catalog."default",
    "CNPJ_DEST" character varying COLLATE pg_catalog."default",
    "CPF_DEST" character varying COLLATE pg_catalog."default",
    "CRT" integer,
    "IE" character varying COLLATE pg_catalog."default",
    "IEST" character varying COLLATE pg_catalog."default",
    "UF" character varying COLLATE pg_catalog."default",
    "UF_DEST" character varying COLLATE pg_catalog."default",
    "cMun" character varying COLLATE pg_catalog."default",
    "cMun_DEST" character varying COLLATE pg_catalog."default",
    "cPais" integer,
    "cPais_DEST" integer,
    "cUF" integer,
    "email_DEST" character varying COLLATE pg_catalog."default",
    "indPag" integer,
    mod character varying COLLATE pg_catalog."default",
    "natOp" character varying COLLATE pg_catalog."default",
    nro character varying COLLATE pg_catalog."default",
    "nro_DEST" "char",
    serie integer,
    "xBairro" character varying COLLATE pg_catalog."default",
    "xBairro_DEST" character varying COLLATE pg_catalog."default",
    "xFant" character varying COLLATE pg_catalog."default",
    "xLgr" character varying COLLATE pg_catalog."default",
    "xLgr_DEST" character varying COLLATE pg_catalog."default",
    "xMun" character varying COLLATE pg_catalog."default",
    "xMun_DEST" character varying COLLATE pg_catalog."default",
    "xNome" character varying COLLATE pg_catalog."default",
    "xNome_DEST" character varying COLLATE pg_catalog."default",
    "xPais" character varying COLLATE pg_catalog."default",
    "xPais_DEST" character varying COLLATE pg_catalog."default",
    "cNF" integer NOT NULL,
    "ProcessoID" integer NOT NULL,
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "nNF" integer NOT NULL,
    "vBC_TOTAL" real,
    "vICMS_TOTAL" real,
    "vICMSDeson_TOTAL" real,
    "vFCP_TOTAL" real,
    "vBCST_TOTAL" real,
    "vST_TOTAL" real,
    "vFCPST_TOTAL" real,
    "vFCPSTRet_TOTAL" real,
    "vProd_TOTAL" real,
    "vFrete_TOTAL" real,
    "vSeg_TOTAL" real,
    "vDesc_TOTAL" real,
    "vII_TOTAL" real,
    "vIPI_TOTAL" real,
    "vIPIDevol_TOTAL" real,
    "vPIS_TOTAL" real,
    "vCOFINS_TOTAL" real,
    "vOutro_TOTAL" real,
    "vNF_TOTAL" real,
    "Entrada" boolean DEFAULT false,
    "dhSaiEnt" timestamp with time zone,
    "dhEmi" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_ID" PRIMARY KEY ("ID"),
    CONSTRAINT "cNF_nNF_ProcessoID_Unique" UNIQUE ("cNF", "nNF", "ProcessoID"),
    CONSTRAINT "Processo_FK" FOREIGN KEY ("ProcessoID")
        REFERENCES public."Processos" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public."NFe"
    OWNER to postgres;
-- Index: Entrada_Index

-- DROP INDEX public."Entrada_Index";

CREATE INDEX "Entrada_Index"
    ON public."NFe" USING btree
    ("Entrada" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: cNF_Index

-- DROP INDEX public."cNF_Index";

CREATE INDEX "cNF_Index"
    ON public."NFe" USING btree
    ("cNF" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: dhEmi_Index

-- DROP INDEX public."dhEmi_Index";

CREATE INDEX "dhEmi_Index"
    ON public."NFe" USING btree
    ("dhEmi" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: dhSaiEnt_Index

-- DROP INDEX public."dhSaiEnt_Index";

CREATE INDEX "dhSaiEnt_Index"
    ON public."NFe" USING btree
    ("dhSaiEnt" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: nNF_Index

-- DROP INDEX public."nNF_Index";

CREATE INDEX "nNF_Index"
    ON public."NFe" USING btree
    ("nNF" ASC NULLS LAST)
    TABLESPACE pg_default;