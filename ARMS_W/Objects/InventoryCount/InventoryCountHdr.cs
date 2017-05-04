//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ARMS_W.Class;
//using ARMS_W.SkelClass;


//namespace ARMS_W.Objects.InventoryCount
//{
//    public class InventoryCountHdr:ARMS_W.Interface.InventoryCount.InventoryCountHdr
//    {
//        public string act_type { get; set; }
//        public bool newWhsIncharge { get; set; }

//        public string inventoryCountId { get; set; }
//        public string empId { get; set; }
//        public string empFirstName { get; set; }
//        public string empLastName { get; set; }
//        public string acctCode { get; set; }
//        public int custOutletsID { get; set; }
//        public string whsInchargeID { get; set; }
//        public DateTime? prevCountDate { get; set; }
//        public DateTime date { get; set; }
//        public DateTime nextCountDate { get; set; }
//        public int countRange { get; set; }
//        public string remarks { get; set; }
//        public decimal totalAmount { get; set; }
//        string pareto { get; set; }
//        public string area { get; set; }
//        public int documentstatusid { get; set; }
//        public int doctypeid { get; set; }
//        public string inventoryCountStatus { get; set; }


//        public decimal? totalVarianceAmt { get; set; }

//        public decimal? totalForecastAmt { get; set; }

//        public decimal? totalSuggestedForecastAmt { get; set; }


//        public DateTime? CountDueDateOn { get; set; }

//        public DateTime forthemonth { get; set; }

//        public List<inventoryCountdetails> inventorycount_list { get; set; }

//        public whsIncharge whs_details { get; set; }

//        //FUNCTIONS
//        public int updateToDB(ref  SQLTransaction sql_trans)
//        {

//            //SAVE INVENTORY COUNT
//            #region Save to InventoryCountHdr [Inventory Count Header and Details]

//            sql_trans.UpdateTo("InventoryCountHdr",
//                    new Dictionary<string, object>() { 
//                    {"inventoryCountId",this.inventoryCountId }
//                    ,{"docTypeId", (int)Globals.InfoType.InventoryCountHdr }
//                    ,{"whsInchargeID",this.whsInchargeID}
//                    ,{"empId",this.empId }
//                    ,{"acctCode",this.acctCode}
//                    ,{"custOutletsID",this.custOutletsID}
//                    ,{"prevCountDate",this.prevCountDate}
//                    ,{"date",this.date}
//                    ,{"nextCountDate",this.nextCountDate}
//                    ,{"countRange",this.countRange}
//                    ,{"totalAmount",this.totalAmount}
//                    ,{"remarks",this.remarks}
//                }, new Dictionary<string, object>() { { "inventoryCountId", this.inventoryCountId } });


//            foreach (var itm in this.inventorycount_list)
//            {
//                sql_trans.UpdateTo("InventoryCountDtl",
//                        new Dictionary<string, object>(){
//                        // {"inventoryCountId",inventoryId_GeneratedCode},
//                        {"lineId",itm.lineId},
//                        {"itemCode",itm.itemCode},
//                        {"ssr",itm.ssr},
//                        {"begNvPcs",itm.begNvPcs},
//                        {"endNvPcs",itm.endNvPcs},
//                        {"actualSellOutPcs",itm.actualSellOutPcs},
//                        {"actualSelloutAmt",itm.actualSellOutAmt},
//                        {"forecastFTMpcs",itm.forecastFTMpcs},
//                        {"forecastFTMamt",itm.forecastFTMamt},
//                        {"variancePcs",itm.variancePcs},
//                        {"varianceAmt",itm.varianceAmt},
//                        {"sForecastpcs",itm.sForecastpcs},
//                        {"sForecastamt",itm.sForecastamt},
//                        {"remarks",itm.remarks}
//                    }, new Dictionary<string, object>() { { "lineId", itm.lineId } });
//            }

//            #endregion

//            return 0;
//        }
//        public int UpdateDocStatus(ref SQLTransaction sql_trans, Globals.InfoType action_type, _User current_user, string Remarks = null);
//        public Globals.iUserPermission GetPermission(_User current_user);
//    }


//    public class whsIncharge : ARMS_W.Interface.InventoryCount.InventoryCountHdr
//    {
//        public string whsInchargeID { get; set; }
//        public string whsInchargeFirstName { get; set; }
//        public string whsInchargeMiddleName { get; set; }
//        public string whsInchargeLastName { get; set; }
//        public string whsInchargeContactNo { get; set; }
//    }

//    public class custoutletDetails : ARMS_W.Interface.InventoryCount.InventoryCountHdr
//    {
//        public int custOutletsID { get; set; }
//        public string outletName { get; set; }
//        public string outletLocation { get; set; }
//    }

//    public class inventoryCountdetails : ARMS_W.Interface.InventoryCount.InventoryCountHdr
//    {
//        public string inventoryCountId { get; set; }
//        public int lineId { get; set; }
//        public string itemCode { get; set; }
//        public  string ssr { get; set; }
//        public  int begNvPcs { get; set; }
//        public  int endNvPcs { get; set; }

//        public int actualSellOutPcs { get; set; }
//        public  decimal? actualSellOutAmt { get; set; }

//        public int forecastFTMpcs { get; set; }
//        public decimal? forecastFTMamt { get; set; }

//        public int sForecastpcs { get; set; }
//        public decimal? sForecastamt { get; set; }

//        public int variancePcs { get; set; }
//        public  decimal? varianceAmt { get; set; }

//        public string remarks { get; set; }

//        //item details
//        public  string brand { get; set; }
//        public  string prodGrp { get; set; }
//        public  string itemDesc { get; set; }
//        public decimal? itemPrice { get; set; }
//    }
//}