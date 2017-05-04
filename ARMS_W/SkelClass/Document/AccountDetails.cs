using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.SkelClass.Document
{
    public class AccountDetails
    {
        #region CLASSES
        public class busTypeDtl
        {
            public string partnerStockHolder { get; set; }
            public string nationality { get; set; }
            public string capitalPerOwned { get; set; }
        }

        public class empInventory
        {
            public string position { get; set; }
            public string numOfEmp { get; set; }
        }

        public class custAttachment
        {
            public string attachType { get; set; }
            public string AttachPath { get; set; }
        }

        public class propsedPrice
        {
            public string brandType { get; set; }
            public string priceListCode { get; set; }
            public string codeDesc { get; set; }
            public string CommisionDiscounts { get; set; }
            public string remarks { get; set; }
        }

        public class custOutlets
        {
            public string name { get; set; }
            public string location { get; set; }
            public string storeSize { get; set; }
            public string wreHouseSize { get; set; }
        }

        public class Activities
        {
            public string action { get; set; }
            public string type { get; set; }
            public string contactperson { get; set; }
            public string subject { get; set; }
            public string startdatetme { get; set; }
            public string enddatetime { get; set; }
            public string content { get; set; }
            public string remarks { get; set; }
        }

        public class majorCustomer
        {
            public string name { get; set; }
            public string address { get; set; }
            public string sellingTerms { get; set; }
            public string estMonthPur { get; set; }
        }

        public class products
        {
            public string majorLine { get; set; }
            public string otherProductLine { get; set; }
            public string suppPlywood { get; set; }
            public string suppSteel { get; set; }
            public string suppCement { get; set; }
            public string suppCHB { get; set; }
            public string suppOthers { get; set; }
            public string volValueDriver { get; set; }
            public string woodVolValue { get; set; }
            public string discounts { get; set; }
        }

        public class otherWoodSupp
        {
            public string supplier { get; set; }
            public string monthVolVal { get; set; }
            public string contactPerson { get; set; }
            public string contactNum { get; set; }
            public string prodPurchase { get; set; }
            public string creditTerms { get; set; }
        }

        public class depositoryBank
        {
            public string bankName { get; set; }
            public string branch { get; set; }
            public string address { get; set; }
            public string account { get; set; }
            public string contactPerson { get; set; }
            public string contactNumber { get; set; }
            public string aveDeposit { get; set; }
            public string remarks { get; set; }
        }

        public class assets_land
        {
            public string type { get; set; }
            public string area { get; set; }
            public string location { get; set; }
            public string owner { get; set; }
        }

        public class assets_building
        {
            public string type { get; set; }
            public string area { get; set; }
            public string location { get; set; }
            public string owner { get; set; }
        }

        public class assets_vehicle
        {
            public string type { get; set; }
            public string model { get; set; }
            public string quantity { get; set; }
        }

        public class assets_other
        {
            public string otherAssets { get; set; }
        }

        public class otherBusiness
        {
            public string regName { get; set; }
            public string nature { get; set; }
            public string location { get; set; }
            public string percentOwnership { get; set; }
        }

        public class _proposedChangesCA
        {
            public string acctName { get; set; }
            public string acctOffcr { get; set; }
            public string territory { get; set; }
            public string area { get; set; }
            public string region { get; set; }
            public string regBusName { get; set; }
            public string bussAdd { get; set; }
            public string delAdd { get; set; }

            /* Code added by Billy Jay (04/23/2015) */
            public string initialPODetails { get; set; }
            public string propCredTermsArchitecturalBrand { get; set; }
            public string propCredTermsEcoforLumber { get; set; }
            public string propCredTermsEcoforPlywood { get; set; }

            public string CredTermRemarksArchitecturalBrand { get; set; }
            public string CredTermRemarksEcoforLumber { get; set; }
            public string CredTermRemarksEcoforPlywood { get; set; }

            public string probOrderLimit_AB { get; set; }
            public string probOrderLimit_TR { get; set; }

            public string OrderLimitRemarks_AB { get; set; }
            public string OrderLimitRemarks_TR { get; set; }
            /* End Code added by Billy Jay (04/23/2015) */

            public string propCredTerms { get; set; }
            public string propCredLimit { get; set; }
            public string CredTermRemarks { get; set; }
            public string CredLimitRemarks { get; set; }
            public string pl_priceListCode_mw { get; set; }
            public string pl_codeDesc_mw { get; set; }
            public string pl_CommDisc_mw { get; set; }
            public string pl_remarks_mw { get; set; }
            public string pl_priceListCode_ww { get; set; }
            public string pl_codeDesc_ww { get; set; }
            public string pl_CommDisc_ww { get; set; }
            public string pl_remarks_ww { get; set; }
            public string pl_priceListCode_pwf { get; set; }
            public string pl_codeDesc_pwf { get; set; }
            public string pl_CommDisc_pwf { get; set; }
            public string pl_remarks_pwf { get; set; }
            public string pl_priceListCode_pwr { get; set; }
            public string pl_codeDesc_pwr { get; set; }
            public string pl_CommDisc_pwr { get; set; }
            public string pl_remarks_pwr { get; set; }
            public string pl_priceListCode_gw { get; set; }
            public string pl_codeDesc_gw { get; set; }
            public string pl_CommDisc_gw { get; set; }
            public string pl_remarks_gw { get; set; }
            public string pl_priceListCode_tw { get; set; }
            public string pl_codeDesc_tw { get; set; }
            public string pl_CommDisc_tw { get; set; }
            public string pl_remarks_tw { get; set; }

            public string pl_priceListCode_mz { get; set; }
            public string pl_codeDesc_mz { get; set; }
            public string pl_CommDisc_mz { get; set; }
            public string pl_remarks_mz { get; set; }

            public string pl_priceListCode_nw { get; set; }
            public string pl_codeDesc_nw { get; set; }
            public string pl_CommDisc_nw { get; set; }
            public string pl_remarks_nw { get; set; }

            public string pl_priceListCode_ec { get; set; }
            public string pl_codeDesc_ec { get; set; }
            public string pl_CommDisc_ec { get; set; }
            public string pl_remarks_ec { get; set; }

            public string pl_priceListCode_ecu { get; set; }
            public string pl_codeDesc_ecu { get; set; }
            public string pl_CommDisc_ecu { get; set; }
            public string pl_remarks_ecu { get; set; }

            public string is_sent_back { get; set; }
            public string route_type { get; set; }
        }

        public class customerEvents
        {
            public string ch_name { get; set; }
            public string ch_event { get; set; }
            public DateTime? dt_date { get; set; }
            public string ch_contactnumber { get; set; }
            public bool bool_specifiedevent { get; set; }
        }

        public class delivery_addresses
        {
            public string ch_code { get; set; }
            public string ch_address { get; set; }
        }
        #endregion

        public const int IS_NOT_FOUND = -1;
        public string CardCode { get; set; }

        public string DocCCaNum { get; set; }
        public _User oUsr { get; set; }

        public _Document oDocumnt { get; set; }

        // customer header
        public string ccaNum { get; set; }
        public string acctType { get; set; }
        public string acctClassfxn { get; set; }
        public string keyAcct { get; set; }
        public string acctCode { get; set; }
        public string acctClass { get; set; }
        public string acctName { get; set; }
        public string telNum { get; set; }
        public string telNum2 { get; set; }
        public string MobileNum { get; set; }
        public string acctOffcr { get; set; }
        public string faxNum { get; set; }
        public string territory { get; set; }
        public string emailAdd { get; set; }
        public string offceHrs { get; set; }
        public string area { get; set; }
        public string storeHrs { get; set; }
        public string region { get; set; }
        public string yrsInBusiness { get; set; }
        public string yrsWdMTC { get; set; }
        public string TIN { get; set; }
        public string VATregNum { get; set; }
        public string regBusName { get; set; }
        public string bussAdd { get; set; }
        public string delAdd { get; set; }
        public string TotalNumOfEmp { get; set; }
        public string SapAcctCode { get; set; }
        public string acctCategoryVal { get; set; }
        public string acctCategoryPrem { get; set; }
        public string acctBusinessClass { get; set; }
        public string acctTypeOfAccount { get; set; }

        public string MTDSales { get; set; }
        public string GrpName { get; set; }


        // busTypeHdr
        public string busType { get; set; }
        public string ownerCEO { get; set; }
        public string nationality { get; set; }
        public string genMgr { get; set; }
        public string financeHead { get; set; }
        public string dateOfIncorporationFormatted { get; set; }
        public string paidInCapStocks { get; set; }
        public string subscribedCapStocks { get; set; }
        public string authorizedCapStocks { get; set; }

        // busTypeDtl
        public List<busTypeDtl> busTypeDtls { get; set; }

        // empInventory
        public List<empInventory> empInventorys { get; set; }

        // custAttachment
        public List<custAttachment> custAttachments { get; set; }

        // custBusHdr
        public string propCredTerms { get; set; }
        public string probCredLimit { get; set; }
        public string sociaEcoClass { get; set; }
        public string numOfOutlet { get; set; }
        public string CredTermRemarks { get; set; }
        public string CredLimitRemarks { get; set; }
        /* Code added by Billy Jay (04/23/2015) */
        public string initialPODetails { get; set; }
        public string propCredTermsArchitecturalBrand { get; set; }
        public string propCredTermsEcoforLumber { get; set; }
        public string propCredTermsEcoforPlywood { get; set; }

        public string CredTermRemarksArchitecturalBrand { get; set; }
        public string CredTermRemarksEcoforLumber { get; set; }
        public string CredTermRemarksEcoforPlywood { get; set; }

        public string probOrderLimit_AB { get; set; }
        public string probOrderLimit_TR { get; set; }

        public string OrderLimitRemarks_AB { get; set; }
        public string OrderLimitRemarks_TR { get; set; }
        /* End Code added by Billy Jay (04/23/2015) */

        // propsedPrice
        public List<propsedPrice> propsedPrices { get; set; }

        // custOutlets
        public List<custOutlets> custOutletss { get; set; }

        // majorCustomer
        public List<majorCustomer> majorCustomers { get; set; }


        // majorCustomer
        public List<Activities> activities { get; set; }

        // custCredInves
        public string CIBI_remarks { get; set; }
        public string SupplyInfo_remarks { get; set; }

        // products
        public List<products> productss { get; set; }

        // otherWoodSupp
        public List<otherWoodSupp> otherWoodSupps { get; set; }

        // depositoryBank
        public List<depositoryBank> depositoryBanks { get; set; }

        // assets_land
        public List<assets_land> assets_lands { get; set; }

        // assets_building
        public List<assets_building> assets_buildings { get; set; }

        // assets_vehicle
        public List<assets_vehicle> assets_vehicles { get; set; }

        // assets_other
        public List<assets_other> assets_others { get; set; }

        // otherBusiness
        public List<otherBusiness> otherBusinesss { get; set; }

        // customer Events
        public List<customerEvents> customerEventss { get; set; }

        // delivery addresses
        public List<delivery_addresses> delAddresses { get; set; }

        //proposed delivery
        public List<delivery_addresses> proposedDelAddresses { get; set; }

        public _proposedChangesCA proposedChangesCA { get; set; }

        public string curr_doc_DocStatus { get; set; }
        public string curr_doc_DocChangesStatus { get; set; }

    }
}