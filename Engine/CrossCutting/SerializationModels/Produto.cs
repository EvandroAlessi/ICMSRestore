﻿namespace CrossCutting.SerializationModels
{
    public class Produto
    {
        public string cProd { get; set; }
        public string cEAN { get; set; }
        public string xProd { get; set; }
        public int NCM { get; set; }
        public int CFOP { get; set; }
        public string uCom { get; set; }
        public double qCom { get; set; }
        public double vUnCom { get; set; }
        public double? vProd { get; set; }
        public string cEANTrib { get; set; }
        public string uTrib { get; set; }
        public double? qTrib { get; set; }
        public double? vUnTrib { get; set; }
        public int indTot { get; set; }
    }
}
