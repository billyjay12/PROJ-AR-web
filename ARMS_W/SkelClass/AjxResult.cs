using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS_W.SkelClass
{
    public class AjxResult
    {
        public bool iserror { get; set; }
        public bool isplanned { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}