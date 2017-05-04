using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ARMS_W.Class
{
    public class oBusinessPartner
    {
        private string _CardCode; // code
        private string _CardName; // name
        private long _Territory;
        private long _SlpCode; // sales employee
        private string _DfTcnician; 
        private string _Phone1;
        private string _Phone2;
        private string _Cellular;
        private string _Fax;
        private string _E_mail;
        private string _InterntSite;
        private string _Shiptype;
        private string _Password;
        private string _Indicator;
        private string _ProjectCod;
        private string _GroupNum;
        private string _CreditLine;
        private string _DebtLine;
        /* UDF */
        private string _BPSUBGROUP; // class
        // matwood_price_code


        public string CardCode { get { return _CardCode; } }
        public string CardName { get { return _CardName; } }
        public long Territory { get { return _Territory; } }
        public long SlpCode { get { return _SlpCode; } }
        public string DfTcnician { get { return _DfTcnician; } }
        public string Phone1 { get { return _Phone1; } }
        public string Phone2 { get { return _Phone2; } }
        public string Cellular { get { return _Cellular; } }
        public string Fax { get { return _Fax; } }
        public string E_mail { get { return _E_mail; } }
        public string InterntSite { get { return _InterntSite; } }
        public string Shiptype { get { return _Shiptype; } }
        public string Password { get { return _Password; } }
        public string Indicator { get { return _Indicator; } }
        public string ProjectCod { get { return _ProjectCod; } }
        public string GroupNum { get { return _GroupNum; } }
        public string CreditLine { get { return _CreditLine; } }
        public string DebtLine { get { return _DebtLine; } }
        /* UDF */
        public string BPSUBGROUP; // class
        // matwood_price_code

        public void Fill(string acct_ccanum)
        {
            DataTable customerHeader = SqlDbHelper.getDataDT("select * from customerHeader where ccaNum='" + acct_ccanum + "'");

            foreach (DataRow customerHeader_row in customerHeader.Rows) {

                _CardCode = customerHeader_row["acctCode"].ToString();

                _CardName = customerHeader_row["acctName"].ToString();

                

            }
        }

        

    }
}