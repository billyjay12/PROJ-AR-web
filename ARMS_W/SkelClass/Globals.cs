using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS_W.SkelClass
{
    public class Globals
    {
        // enums
        // this [InfoType] should not be changed
        public enum InfoType
        {
            SalesTarget = 11,
            DayCycleMaintenance = 12,
            InventoryCountHdr = 13,
            CalendarEvent = 14,
            ObjectiveMaintenance = 15,
            CalendarEventAmmended=16
        }

        public class db_changes
        {
            public string FieldName { get; set; }
            public string PrevValue { get; set; }
            public string NewValue { get; set; }
        }

        public class db_changes_hdr
        {
            public string username { get; set; }
            public DateTime log_datetime { get; set; }
            public List<db_changes> logs { get; set; }
        }

        public class db_route_changes
        {
            public DocActionType Action { get; set; }
            public string CurDocStatus { get; set; }
            public string PrevDocStatus { get; set; }
            public string UserName { get; set; }
            public string PositionName { get; set; }
            public DateTime DateTimeStamp { get; set; }
            public string Remarks { get; set; }
        }

        public class db_visit_logs
        {
            public string inout { get; set; }
            public DateTime datetime { get; set; }
            public string address { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
            public string isPlanned { get; set; }
        }

        public enum DocActionType
        {
            None, Approve, Disapprove, ReturnToRequestor, SaveAsDraft, Update, Save, Delete,Created, SaveAndSend
        }




        public enum EventType
        { 
        
            Collection,Merchandising, Sales, CustomerService, Inventory
        
        }

        public struct Settings
        {
            public const string Database = "ARMS";
        }

        #region USER PERMISSION
        public interface iUserPermission
        {
            bool AllowSave { get; }
            bool AllowEdit { get; }
            bool AllowApprove { get; }
        }

        public class cUserPermission : iUserPermission
        {
            public bool AllowSave { get { return this._AllowSave; } }
            public bool AllowEdit { get { return this._AllowEdit; } }
            public bool AllowApprove { get { return this._AllowApprove; } }

            public bool _AllowSave { get; set; }
            public bool _AllowEdit { get; set; }
            public bool _AllowApprove { get; set; }

            public cUserPermission()
            {
                this._AllowSave = false;
                this._AllowEdit = false;
                this._AllowApprove = false;
            }
        }

        public class inventoryCountPermission
        {
            public bool AllowSave { get; set; }
            public bool AllowEditSO { get; set; }
            public bool AllowApprove { get; set; }
            public bool AllowEditCA { get; set; }
        }
        #endregion

        #region 


        //public class db_changes
        //{
        //    public string FieldName { get; set; }
        //    public string PrevValue { get; set; }
        //    public string NewValue { get; set; }
        //}

        #endregion
    }
}
