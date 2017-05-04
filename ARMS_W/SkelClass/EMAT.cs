using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS_W.SkelClass
{

    public class EMAT 
    {
        [Serializable]
        public class ematContents
        {
            public string encodedBy { get; set; }
            public string buyer { get; set; }
            public string ematDoc { get; set; }
            public string buyerAdd { get; set; }
            public string telNo { get; set; }
            public string contactPerson { get; set; }
            public string terms { get; set; }
            public string deliveryDateFormatted { get; set; }
            public string deliveryInstrct { get; set; }
            public string acctCode { get; set; }
            public string submttdTo { get; set; }
            public string submttdContactNum { get; set; }
            public string confirmedDelBy { get; set; }
            public string tradeSalesRep { get; set; }
            public string[] list_of_items { get; set; }

            public string DocStatusMsg { get; set; }
        }

        public class emat_confirmemattrans 
        { 
            public string status { get; set; }
            public string encodedBy { get; set; }
            public string buyer { get; set; }
            public string ematDoc { get; set; }
            public string buyerAdd { get; set; }
            public string telNo { get; set; }
            public string contactPerson { get; set; }
            public string terms { get; set; }
            public string deliveryDate { get; set; }
            public string deliveryInstrct { get; set; }
            public string acctCode { get; set; }
            public string submttdTo { get; set; }
            public string submttdContactNum { get; set; }
            public string tradeSalesRep { get; set; }
            public string confirmedDelBy { get; set; }
            public List<string> product { get; set; }
            public string branch  { get; set; }
        }

    }

    
}