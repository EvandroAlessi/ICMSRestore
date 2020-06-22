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
