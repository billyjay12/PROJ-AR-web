using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS_W.SkelClass
{
    public class SalesStatus
    {

        public string Channel { get; set; }
        public string Area { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string SalesOfficer { get; set; }
        public string Territory { get; set; }

        public decimal POSTED_PREV { get; set; }
        public decimal POSTED_MTD { get; set; }
        public decimal POSTED_TOTAL { get; set; }

        public decimal CM_RETURNS { get; set; }
        public decimal CM_SALES_ADJ { get; set; }
        public decimal CM_TOTAL { get; set; }

        public decimal NET_POSTED { get; set; } // POSTED - CM

        public decimal UNPOSTED_TRF_PREV { get; set; }
        public decimal UNPOSTED_TRF_MTD { get; set; }
        public decimal UNPOSTED_TRF_TOTAL { get; set; }

        public decimal OUTSTANDING_PICKLIST { get; set; }
        public decimal SUBTOTAL { get; set; } // NET_POSTED + UNPOSTED_TRF + OUTSTANDING_PICKLIST

        public decimal PENDING_PREV { get; set; }
        public decimal PENDING_MTD { get; set; }
        public decimal PENDING_TOTAL { get; set; }

        public decimal BALANCE_ORDER_PREV { get; set; }
        public decimal BALANCE_ORDER_MTD { get; set; }
        public decimal BALANCE_ORDER_TOTAL { get; set; }

        public decimal GROSS_ORDER { get; set; }
        public decimal GROSS_BOOKINGS { get; set; } // POSTED + UNPOSTED_TRF + OUTSTANDING_PICKLIST + PENDING + GROSS_BOOKINGS

    }
}