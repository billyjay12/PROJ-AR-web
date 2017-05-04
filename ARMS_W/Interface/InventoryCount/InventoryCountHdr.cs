//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ARMS_W.Class;
//using ARMS_W.SkelClass;

//namespace ARMS_W.Interface.InventoryCount
//{
//    public interface InventoryCountHdr
//    {
//        string act_type { get; set; }
//        bool newWhsIncharge { get; set; }

//        string inventoryCountId { get; set; }
//        string empId { get; set; }
//        string empFirstName { get; set; }
//        string empLastName { get; set; }
//        string acctCode { get; set; }
//        int custOutletsID { get; set; }
//        string whsInchargeID { get; set; }
//        DateTime? prevCountDate { get; set; }
//        DateTime date { get; set; }
//        DateTime nextCountDate { get; set; }
//        int countRange { get; set; }
//        string remarks { get; set; }
//        decimal totalAmount { get; set; }
//        string pareto { get; set; }
//        string area { get; set; }
//        int documentstatusid { get; set; }
//        int doctypeid { get; set; }
//        string inventoryCountStatus { get; set; }


//        decimal? totalVarianceAmt { get; set; }

//        decimal? totalForecastAmt { get; set; }

//        decimal? totalSuggestedForecastAmt { get; set; }


//        DateTime? CountDueDateOn { get; set; }

//        DateTime forthemonth { get; set; }

//        List<inventoryCountdetails> inventorycount_list { get; set; }

//        whsIncharge whs_details { get; set; }

//        int updateToDB (ref  SQLTransaction sql_trans);
//        int UpdateDocStatus(ref SQLTransaction sql_trans,Globals.InfoType action_type,_User current_user,string Remarks = null);
//        Globals.iUserPermission GetPermission(_User current_user);
//    }

//    public interface whsIncharge
//    {
//        string whsInchargeID { get; set; }
//        string whsInchargeFirstName { get; set; }
//        string whsInchargeMiddleName { get; set; }
//        string whsInchargeLastName { get; set; }
//        string whsInchargeContactNo { get; set; }
//    }

//    public interface custoutletDetails
//    {
//        int custOutletsID { get; set; }
//        string outletName { get; set; }
//        string outletLocation { get; set; }
//    }

//    public interface inventoryCountdetails
//    {
//        string inventoryCountId { get; set; }
//        int lineId { get; set; }
//        string itemCode { get; set; }
//        string ssr { get; set; }
//        int begNvPcs { get; set; }
//        int endNvPcs { get; set; }

//        int actualSellOutPcs { get; set; }
//        decimal? actualSellOutAmt { get; set; }

//        int forecastFTMpcs { get; set; }
//        decimal? forecastFTMamt { get; set; }

//        int sForecastpcs { get; set; }
//        decimal? sForecastamt { get; set; }

//        int variancePcs { get; set; }
//        decimal? varianceAmt { get; set; }

//        string remarks { get; set; }

//        //item details
//        string brand { get; set; }
//        string prodGrp { get; set; }
//        string itemDesc { get; set; }
//        decimal? itemPrice { get; set; }
//    }
//}