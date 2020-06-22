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


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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



------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- FUNCTION: public.Filter()

-- DROP FUNCTION public."Filter"();

CREATE FUNCTION public."Filter"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$DECLARE
   nfe record;
   accepted_cfop integer ARRAY;
   accepted_cst integer ARRAY;
   accepted_csosn integer ARRAY;
BEGIN
accepted_cfop = '{1403, 1409, 1411, 1415, 2403, 2409, 2411, 2415, 5403, 5405, 5409, 5411, 5415, 6403, 6409, 1411, 2415}';
accepted_cst = '{0, 60, 70}';
accepted_csosn = '{201, 202, 203, 500}';

IF NEW.orig = 0 AND (NEW."CST" is not null OR NEW."CSOSN" is not null) 
THEN
	IF (NEW."CST" = ANY(accepted_cst) 
			OR NEW."CSOSN" = ANY(accepted_csosn))
		AND NEW."CFOP" = ANY(accepted_cfop) 
	THEN
		SELECT INTO nfe 
			"cNF"
			, "nNF"
			, "dhEmi"
			, "dhSaiEnt"
			, "Entrada" 
			, "ProcessoID" 
		FROM public."NFe" 
		WHERE public."NFe"."ID" = NEW."NFeID";

		INSERT INTO public."ItensFiltrados"(
				"cProd"
				, "xProd"
				, "NCM"
				, "CFOP"
				, "uCom"
				, "qCom"
				, "vUnCom"
				, orig
				, "CST"
				, "vBC"
				, "pICMS"
				, "vICMS"
				, "dhEmi"
				, "dhSaiEnt"
				, "nNF"
				, "cNF"
				, "Entrada"
				, "CSOSN"
				, "ProcessoID"
				, "ItemID")
			VALUES (
				NEW."cProd"
				, NEW."xProd"
				, NEW."NCM"
				, NEW."CFOP"
				, NEW."uCom"
				, NEW."qCom"
				, NEW."vUnCom"
				, NEW.orig
				, NEW."CST"
				, NEW."vBC"
				, NEW."pICMS"
				, NEW."vICMS"
				, nfe."dhEmi"
				, nfe."dhSaiEnt"
				, nfe."nNF"
				, nfe."cNF"
				, nfe."Entrada"
				, NEW."CSOSN"
				, nfe."ProcessoID"
				, NEW."ID");
	END IF;
END IF;
RETURN NULL;

END$BODY$;

ALTER FUNCTION public."Filter"()
    OWNER TO postgres;
