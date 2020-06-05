-- Database: icms_restore

-- DROP DATABASE icms_restore;

CREATE DATABASE icms_restore
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;
	
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	
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
	
	
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


-- Table: public.Itens

-- DROP TABLE public."Itens";

CREATE TABLE public."Itens"
(
    "nItem" integer,
    "cProd" character varying COLLATE pg_catalog."default" NOT NULL,
    "cEAN" character varying COLLATE pg_catalog."default",
    "xProd" character varying COLLATE pg_catalog."default",
    "NCM" character varying COLLATE pg_catalog."default" NOT NULL,
    "CFOP" character varying COLLATE pg_catalog."default" NOT NULL,
    "uCom" character varying COLLATE pg_catalog."default",
    "qCom" real,
    "vUnCom" real,
    orig integer,
    "CST" character varying COLLATE pg_catalog."default" NOT NULL,
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
    "cNF" integer NOT NULL,
    CONSTRAINT "Prod_NFe_PK" PRIMARY KEY ("cProd", "cNF"),
    CONSTRAINT "NFe_FK" FOREIGN KEY ("cNF")
        REFERENCES public."NFe" ("cNF") MATCH SIMPLE
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
    ("CFOP" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: CST_Index

-- DROP INDEX public."CST_Index";

CREATE INDEX "CST_Index"
    ON public."Itens" USING btree
    ("CST" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: NCM_Index

-- DROP INDEX public."NCM_Index";

CREATE INDEX "NCM_Index"
    ON public."Itens" USING btree
    ("NCM" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

	
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
    "dhEmi" date,
    "dhSaiEnt" character varying COLLATE pg_catalog."default",
    "email_DEST" character varying COLLATE pg_catalog."default",
    "indPag" integer,
    mod character varying COLLATE pg_catalog."default",
    "nNF" character varying COLLATE pg_catalog."default",
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
    CONSTRAINT "NFe_PK" PRIMARY KEY ("cNF"),
    CONSTRAINT "Processo_FK" FOREIGN KEY ("ProcessoID")
        REFERENCES public."Processos" ("ID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public."NFe"
    OWNER to postgres;
-- Index: dhSaiEnt_Index

-- DROP INDEX public."dhSaiEnt_Index";

CREATE INDEX "dhSaiEnt_Index"
    ON public."NFe" USING btree
    ("dhSaiEnt" COLLATE pg_catalog."default" DESC NULLS LAST)
    TABLESPACE pg_default;
	
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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