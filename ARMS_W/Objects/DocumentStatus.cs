using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS_W.Objects
{
    public class DocumentStatus
    {
        /**
        public int? DocumentStatusId { get; set; }
        public int? NextStatusId { get; set; }
        public int? CancelStatusId { get; set; }
        public string StatusDesc { get; set; }
        public bool IsFinal { get; set; }
        public bool IsStart { get; set; }
         * */

        public int? stateID { get; set;}
        public int roleID { get; set; }
        public string stateDesc { get; set;}

        public int? doctype { get; set; }
        public bool isStart { get; set; }
        public bool isFinal { get; set; }
        public int? nextStatusID { get; set; }
        public int? CancelStatusID { get; set; }


    }
}