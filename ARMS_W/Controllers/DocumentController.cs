using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ARMS_W.Class;
using ARMS_W.Models;

namespace ARMS_W.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/

        public ActionResult Accounts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AccountsDetails(string ccanum)
        {
            try
            {
                if (Session["isFSP"].ToString() == "TRUE")
                {
                    AppHelper.InsertActivityLog(
                        Session["username"].ToString(), "Viewed canum# " + ccanum
                    );
                }
            }
            catch { }


            // load every thing here
            SkelClass.Document.AccountDetails acct_dtls = new SkelClass.Document.AccountDetails();
            string strQuery = "";
            decimal d = 0;

            DataTable customerHeader = null;
            strQuery = "select * from customerHeader where ccanum='" + ccanum + "'";
            customerHeader = SqlDbHelper.getDataDT(strQuery);
            #region LOAD DATA
            foreach (DataRow itm in customerHeader.Rows)
            {
                acct_dtls.CardCode = itm["acctcode"].ToString().Trim();
                acct_dtls.ccaNum = itm["ccaNum"].ToString().Trim();
                acct_dtls.acctType = itm["acctType"].ToString().Trim();
                acct_dtls.acctClassfxn = itm["acctClassfxn"].ToString().Trim();
                acct_dtls.keyAcct = itm["keyAcct"].ToString().Trim();
                acct_dtls.acctCode = itm["acctCode"].ToString().Trim();
                acct_dtls.acctClass = itm["acctClass"].ToString().Trim();
                acct_dtls.acctName = StringHelper.CTJ(itm["acctName"].ToString().Trim());
                acct_dtls.telNum = itm["telNum"].ToString().Trim();
                acct_dtls.telNum2 = itm["telNum2"].ToString().Trim();
                acct_dtls.MobileNum = itm["MobileNum"].ToString().Trim();
                acct_dtls.acctOffcr = itm["acctOffcr"].ToString().Trim();
                acct_dtls.faxNum = itm["faxNum"].ToString().Trim();
                acct_dtls.territory = itm["territory"].ToString().Trim();
                acct_dtls.emailAdd = itm["emailAdd"].ToString().Trim();
                acct_dtls.offceHrs = itm["offceHrs"].ToString().Trim();
                acct_dtls.area = itm["area"].ToString().Trim();
                acct_dtls.storeHrs = itm["storeHrs"].ToString().Trim();
                acct_dtls.region = itm["region"].ToString().Trim();
                acct_dtls.yrsInBusiness = itm["yrsInBusiness"].ToString().Trim();
                acct_dtls.yrsWdMTC = itm["yrsWdMTC"].ToString().Trim();
                acct_dtls.TIN = itm["TIN"].ToString().Trim();
                acct_dtls.VATregNum = itm["VATregNum"].ToString().Trim();
                acct_dtls.regBusName = StringHelper.CTJ(itm["regBusName"].ToString().Trim());
                acct_dtls.bussAdd = StringHelper.CTJ(itm["bussAdd"].ToString().Trim());
                acct_dtls.delAdd = StringHelper.CTJ(itm["delAdd"].ToString().Trim());
                acct_dtls.TotalNumOfEmp = itm["TotalNumOfEmp"].ToString().Trim();
                acct_dtls.SapAcctCode = itm["SapAcctCode"].ToString().Trim();
                acct_dtls.acctCategoryVal = itm["acctCategoryVal"].ToString().Trim();
                acct_dtls.acctCategoryPrem = itm["acctCategoryPrem"].ToString().Trim();
                acct_dtls.acctBusinessClass = itm["acctBusinessClass"].ToString().Trim();
                acct_dtls.acctTypeOfAccount = GetTypeOfAccount(itm["TypeOfAccount"].ToString().Trim());

                acct_dtls.curr_doc_DocStatus = itm["status"].ToString().Trim();
            }
            #endregion

            DataTable busTypeHdr = null;
            strQuery = "select *,convert(varchar(10), dateOfIncorporation, 101) as 'dateOfIncorporationFormatted' from dbo.busTypeHdr where ccanum='" + ccanum + "'";
            busTypeHdr = SqlDbHelper.getDataDT(strQuery);
            #region LOAD DATA
            foreach (DataRow itm in busTypeHdr.Rows)
            {
                acct_dtls.busType = itm["busType"].ToString().Trim();
                acct_dtls.ownerCEO = StringHelper.CTJ(itm["ownerCEO"].ToString().Trim());
                acct_dtls.nationality = itm["nationality"].ToString().Trim();
                acct_dtls.genMgr = itm["genMgr"].ToString().Trim();
                acct_dtls.financeHead = itm["financeHead"].ToString().Trim();
                acct_dtls.dateOfIncorporationFormatted = itm["dateOfIncorporationFormatted"].ToString().Trim();
                acct_dtls.authorizedCapStocks = itm["authorizedCapStocks"].ToString().Trim();
                acct_dtls.subscribedCapStocks = itm["subscribedCapStocks"].ToString().Trim();
                acct_dtls.paidInCapStocks = itm["paidInCapStocks"].ToString().Trim();
            }
            #endregion

            DataTable busTypeDtl = null;
            strQuery = "select * from dbo.busTypeDtl where ccanum='" + ccanum + "'";
            busTypeDtl = SqlDbHelper.getDataDT(strQuery);
            #region LOAD DATA
            acct_dtls.busTypeDtls = new List<SkelClass.Document.AccountDetails.busTypeDtl>();
            foreach (DataRow itm in busTypeDtl.Rows)
            {
                acct_dtls.busTypeDtls.Add(new SkelClass.Document.AccountDetails.busTypeDtl()
                {
                    capitalPerOwned = itm["capitalPerOwned"].ToString().Trim(),
                    nationality = itm["nationality"].ToString().Trim(),
                    partnerStockHolder = itm["partnerStockHolder"].ToString().Trim()
                });
            }
            #endregion

            DataTable empInventory = null;
            strQuery = "select * from dbo.empInventory where ccanum='" + ccanum + "'";
            empInventory = SqlDbHelper.getDataDT(strQuery);
            #region LOAD DATA
            acct_dtls.empInventorys = new List<SkelClass.Document.AccountDetails.empInventory>();
            foreach (DataRow itm in empInventory.Rows)
            {
                acct_dtls.empInventorys.Add(new SkelClass.Document.AccountDetails.empInventory()
                {
                    numOfEmp = itm["numOfEmp"].ToString().Trim(),
                    position = itm["position"].ToString().Trim()
                });
            }
            #endregion

            DataTable custAttachment = null;
            strQuery = "select * from dbo.custAttachment where ccanum='" + ccanum + "'";
            custAttachment = SqlDbHelper.getDataDT(strQuery);
            #region LOAD DATA
            acct_dtls.custAttachments = new List<SkelClass.Document.AccountDetails.custAttachment>();
            foreach (DataRow itm in custAttachment.Rows)
            {
                acct_dtls.custAttachments.Add(new SkelClass.Document.AccountDetails.custAttachment()
                {
                    attachType = itm["attachType"].ToString().Trim(),
                    AttachPath = StringHelper.CTJ(StringHelper.GetFileName(itm["AttachPath"].ToString().Trim()))
                });
            }
            #endregion

            DataTable custBusHdr = null;
            custBusHdr = SqlDbHelper.getDataDT("select * from dbo.custBusHdr where ccanum='" + ccanum + "'");
            #region LOAD DATA
            foreach (DataRow itm in custBusHdr.Rows)
            {
                acct_dtls.propCredTerms = itm["propCredTerms"].ToString().Trim();
                acct_dtls.probCredLimit = Decimal.TryParse(StringHelper.CTJ(itm["probCredLimit"].ToString().Trim()), out d) ? Convert.ToDecimal(itm["probCredLimit"].ToString().Trim()).ToString("#,0") : "0";
                acct_dtls.sociaEcoClass = itm["sociaEcoClass"].ToString().Trim();
                acct_dtls.numOfOutlet = itm["numOfOutlet"].ToString().Trim();
                acct_dtls.CredTermRemarks = StringHelper.CTJ(itm["CredTermRemarks"].ToString().Trim());
                acct_dtls.CredLimitRemarks = StringHelper.CTJ(itm["CredLimitRemarks"].ToString().Trim());

                /* Code added by Billy Jay (04/23/2015) */
                acct_dtls.initialPODetails = StringHelper.CTJ(itm["initialPODetails"].ToString().Trim());

                acct_dtls.propCredTermsArchitecturalBrand = itm["propCredTermsArchitecturalBrand"].ToString().Trim();
                acct_dtls.propCredTermsEcoforLumber = itm["propCredTermsEcoforLumber"].ToString().Trim();
                acct_dtls.propCredTermsEcoforPlywood = itm["propCredTermsEcoforPlywood"].ToString().Trim();

                acct_dtls.CredTermRemarksArchitecturalBrand = StringHelper.CTJ(itm["CredTermRemarksArchitecturalBrand"].ToString().Trim());
                acct_dtls.CredTermRemarksEcoforLumber = StringHelper.CTJ(itm["CredTermRemarksEcoforLumber"].ToString().Trim());
                acct_dtls.CredTermRemarksEcoforPlywood = StringHelper.CTJ(itm["CredTermRemarksEcoforPlywood"].ToString().Trim());

                acct_dtls.probOrderLimit_AB = Decimal.TryParse(StringHelper.CTJ(itm["probOrderLimit_AB"].ToString().Trim()), out d) ? Convert.ToDecimal(StringHelper.CTJ(itm["probOrderLimit_AB"].ToString().Trim())).ToString("#,0") : "0";
                acct_dtls.probOrderLimit_TR = Decimal.TryParse(StringHelper.CTJ(itm["probOrderLimit_TR"].ToString().Trim()), out d) ? Convert.ToDecimal(StringHelper.CTJ(itm["probOrderLimit_TR"].ToString().Trim())).ToString("#,0") : "0";

                acct_dtls.OrderLimitRemarks_AB = StringHelper.CTJ(itm["OrderLimitRemarks_AB"].ToString().Trim());
                acct_dtls.OrderLimitRemarks_TR = StringHelper.CTJ(itm["OrderLimitRemarks_TR"].ToString().Trim());
                /* End Code added by Billy Jay (04/23/2015) */
            }
            #endregion

            DataTable propsedPrice = null;
            propsedPrice = SqlDbHelper.getDataDT("select * from dbo.propsedPrice where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.propsedPrices = new List<SkelClass.Document.AccountDetails.propsedPrice>();
            foreach (DataRow itm in propsedPrice.Rows)
            {
                acct_dtls.propsedPrices.Add(new SkelClass.Document.AccountDetails.propsedPrice()
                {
                    brandType = itm["brandType"].ToString().Trim().ToUpper(),
                    priceListCode = itm["priceListCode"].ToString().Trim(),
                    codeDesc = itm["codeDesc"].ToString().Trim(),
                    CommisionDiscounts = itm["CommisionDiscounts"].ToString().Trim(),
                    remarks = StringHelper.CTJ(itm["remarks"].ToString().Trim())
                });
            }
            #endregion

            DataTable custCredInves = null;
            custCredInves = SqlDbHelper.getDataDT("select * from custCredInves where ccanum='" + ccanum + "'");
            #region LOAD DATA
            foreach (DataRow itm in custCredInves.Rows)
            {
                acct_dtls.CIBI_remarks = StringHelper.CTJ(itm["CIBI_remarks"].ToString());
                acct_dtls.SupplyInfo_remarks = StringHelper.CTJ(itm["SupplyInfo_remarks"].ToString());
            }
            #endregion

            DataTable custOutlets = null;
            custOutlets = SqlDbHelper.getDataDT("select * from dbo.custOutlets where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.custOutletss = new List<SkelClass.Document.AccountDetails.custOutlets>();
            foreach (DataRow itm in custOutlets.Rows)
            {
                acct_dtls.custOutletss.Add(new SkelClass.Document.AccountDetails.custOutlets()
                {
                    name = StringHelper.CTJ(itm["name"].ToString().Trim()),
                    location = StringHelper.CTJ(itm["location"].ToString().Trim()),
                    storeSize = itm["storeSize"].ToString().Trim(),
                    wreHouseSize = itm["wreHouseSize"].ToString().Trim()
                });
            }
            #endregion

            DataTable majorCustomer = null;
            majorCustomer = SqlDbHelper.getDataDT("select * from dbo.majorCustomer where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.majorCustomers = new List<SkelClass.Document.AccountDetails.majorCustomer>();
            foreach (DataRow itm in majorCustomer.Rows)
            {
                acct_dtls.majorCustomers.Add(new SkelClass.Document.AccountDetails.majorCustomer()
                {
                    name = StringHelper.CTJ(itm["name"].ToString().Trim()),
                    address = StringHelper.CTJ(itm["address"].ToString().Trim()),
                    sellingTerms = itm["sellingTerms"].ToString().Trim(),
                    estMonthPur = itm["estMonthPur"].ToString().Trim()
                });
            }
            #endregion

            DataTable products = null;
            products = SqlDbHelper.getDataDT("select * from dbo.products where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.productss = new List<SkelClass.Document.AccountDetails.products>();
            foreach (DataRow itm in products.Rows)
            {
                acct_dtls.productss.Add(new SkelClass.Document.AccountDetails.products()
                {
                    majorLine = itm["majorLine"].ToString().Trim(),
                    otherProductLine = itm["otherProductLine"].ToString().Trim(),
                    suppPlywood = StringHelper.CTJ(itm["suppPlywood"].ToString().Trim()),
                    suppSteel = StringHelper.CTJ(itm["suppSteel"].ToString().Trim()),
                    suppCement = StringHelper.CTJ(itm["suppCement"].ToString().Trim()),
                    suppCHB = StringHelper.CTJ(itm["suppCHB"].ToString().Trim()),
                    suppOthers = StringHelper.CTJ(itm["suppOthers"].ToString().Trim()),
                    volValueDriver = itm["volValueDriver"].ToString().Trim(),
                    woodVolValue = itm["woodVolValue"].ToString().Trim(),
                    discounts = itm["discounts"].ToString().Trim()
                });
            }
            #endregion

            DataTable otherWoodSupp = null;
            otherWoodSupp = SqlDbHelper.getDataDT("select * from dbo.otherWoodSupp where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.otherWoodSupps = new List<SkelClass.Document.AccountDetails.otherWoodSupp>();
            foreach (DataRow itm in otherWoodSupp.Rows)
            {
                acct_dtls.otherWoodSupps.Add(new SkelClass.Document.AccountDetails.otherWoodSupp()
                {
                    supplier = StringHelper.CTJ(itm["supplier"].ToString().Trim()),
                    monthVolVal = itm["monthVolVal"].ToString().Trim(),
                    contactPerson = StringHelper.CTJ(itm["contactPerson"].ToString().Trim()),
                    contactNum = itm["contactNum"].ToString().Trim(),
                    prodPurchase = itm["prodPurchase"].ToString().Trim(),
                    creditTerms = itm["creditTerms"].ToString().Trim()
                });
            }
            #endregion

            // depositoryBank
            DataTable depositoryBank = null;
            depositoryBank = SqlDbHelper.getDataDT("select * from dbo.depositoryBank where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.depositoryBanks = new List<SkelClass.Document.AccountDetails.depositoryBank>();
            foreach (DataRow itm in depositoryBank.Rows)
            {
                acct_dtls.depositoryBanks.Add(new SkelClass.Document.AccountDetails.depositoryBank()
                {
                    bankName = StringHelper.CTJ(itm["bankName"].ToString().Trim()),
                    branch = StringHelper.CTJ(itm["branch"].ToString().Trim()),
                    address = itm["address"].ToString().Trim(),
                    account = itm["account"].ToString().Trim(),
                    contactPerson = StringHelper.CTJ(itm["contactPerson"].ToString().Trim()),
                    contactNumber = itm["contactNumber"].ToString().Trim(),
                    aveDeposit = itm["aveDeposit"].ToString().Trim(),
                    remarks = StringHelper.CTJ(itm["remarks"].ToString().Trim())
                });
            }
            #endregion

            // assets
            DataTable assets_land;
            assets_land = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + ccanum + "' and assetClass='land'");
            #region LOAD DATA
            acct_dtls.assets_lands = new List<SkelClass.Document.AccountDetails.assets_land>();
            foreach (DataRow itm in assets_land.Rows)
            {
                acct_dtls.assets_lands.Add(new SkelClass.Document.AccountDetails.assets_land()
                {
                    type = itm["type"].ToString().Trim(),
                    area = itm["area"].ToString().Trim(),
                    location = StringHelper.CTJ(itm["location"].ToString().Trim()),
                    owner = itm["owner"].ToString().Trim()
                });
            }
            #endregion

            DataTable assets_building;
            assets_building = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + ccanum + "' and assetClass='building'");
            #region LOAD DATA
            acct_dtls.assets_buildings = new List<SkelClass.Document.AccountDetails.assets_building>();
            foreach (DataRow itm in assets_building.Rows)
            {
                acct_dtls.assets_buildings.Add(new SkelClass.Document.AccountDetails.assets_building()
                {
                    type = itm["type"].ToString().Trim(),
                    area = itm["area"].ToString().Trim(),
                    location = StringHelper.CTJ(itm["location"].ToString().Trim()),
                    owner = itm["owner"].ToString().Trim()
                });
            }
            #endregion

            DataTable assets_vehicle;
            assets_vehicle = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + ccanum + "' and assetClass='vehicles'");
            #region LOAD DATA
            acct_dtls.assets_vehicles = new List<SkelClass.Document.AccountDetails.assets_vehicle>();
            foreach (DataRow itm in assets_vehicle.Rows)
            {
                acct_dtls.assets_vehicles.Add(new SkelClass.Document.AccountDetails.assets_vehicle()
                {
                    type = itm["type"].ToString().Trim(),
                    model = itm["model"].ToString().Trim(),
                    quantity = itm["quantity"].ToString().Trim()
                });
            }
            #endregion

            DataTable assets_other;
            assets_other = SqlDbHelper.getDataDT("select * from dbo.assets where ccanum='" + ccanum + "' and assetClass='other'");
            #region LOAD DATA
            acct_dtls.assets_others = new List<SkelClass.Document.AccountDetails.assets_other>();
            foreach (DataRow itm in assets_other.Rows)
            {
                acct_dtls.assets_others.Add(new SkelClass.Document.AccountDetails.assets_other()
                {
                    otherAssets = itm["otherAssets"].ToString().Trim()
                });
            }
            #endregion

            // otherBusiness
            DataTable otherBusiness;
            otherBusiness = SqlDbHelper.getDataDT("select * from dbo.otherBusiness where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.otherBusinesss = new List<SkelClass.Document.AccountDetails.otherBusiness>();
            foreach (DataRow itm in otherBusiness.Rows)
            {
                acct_dtls.otherBusinesss.Add(new SkelClass.Document.AccountDetails.otherBusiness()
                {
                    regName = StringHelper.CTJ(itm["regName"].ToString().Trim()),
                    nature = itm["nature"].ToString().Trim(),
                    location = StringHelper.CTJ(itm["location"].ToString().Trim()),
                    percentOwnership = itm["percentOwnership"].ToString().Trim()
                });
            }
            #endregion

            // customerEvents
            DataTable customerEvents;
            customerEvents = SqlDbHelper.getDataDT("select * from dbo.customerEvents where ccanum='" + ccanum + "'");
            #region LOAD DATA
            acct_dtls.customerEventss = new List<SkelClass.Document.AccountDetails.customerEvents>();
            foreach (DataRow itm in customerEvents.Rows)
            {
                acct_dtls.customerEventss.Add(new SkelClass.Document.AccountDetails.customerEvents()
                {
                    ch_name = StringHelper.CTJ(itm["name"].ToString().Trim()),
                    ch_event = StringHelper.CTJ(itm["event"].ToString().Trim()),
                    ch_contactnumber = StringHelper.CTJ(itm["contactnumber"].ToString().Trim()),
                    dt_date = UserDefineFunctions.Application.ToNullableDateTime(itm["date"].ToString()),
                    bool_specifiedevent = itm["specialEvent"].ToString() == "T" ? true : false
                });
            }
            #endregion

            //modified oct 17
            DataTable UploadedSalesReport;
            UploadedSalesReport = SqlDbHelper.getDataDT("select SUM(NET_POSTED) as NET_POSTED from dbo.UploadedSalesReport where UploadDateTime in (" +
                                                                                                                                        " select top 1 UploadDateTime " +
                                                                                                                                        " from dbo.UploadedSalesReport " +
                                                                                                                                        " order by uploaddatetime desc) and CardCode='" + acct_dtls.CardCode + "'");
            #region LOAD DATA
            foreach (DataRow itm in UploadedSalesReport.Rows)
            {
                acct_dtls.MTDSales = itm["NET_POSTED"].ToString().Trim();
            }
            #endregion
            //---

            //modified may 05 2015
            DataTable ChannelGroup;
            ChannelGroup = SqlDbHelper.getDataDT("select LEFT(grp_name,2) as grp_name from dbo.ChannelGroup where Area ='" + acct_dtls.area + "'");
            #region LOAD DATA
            foreach (DataRow itm in ChannelGroup.Rows)
            {
                acct_dtls.GrpName = itm["grp_name"].ToString().Trim();
            }
            #endregion

            //modified may 05 2015
            DataTable Activities;
            Activities = SqlDbHelper.getDataDT("SELECT CardCode,ACtion,CntctType,CntctCode,CntctSbjct,dbo.mtc_func_TimeFormat(begintime,cntctDate) BeginTime,dbo.mtc_func_TimeFormat(endtime,cntctDate) EndTime,Notes,Details FROM customeractivities WHERE cardcode='" + acct_dtls.CardCode + "' order by ClgCode desc");
            acct_dtls.activities = new List<SkelClass.Document.AccountDetails.Activities>();
            #region LOAD DATA
            foreach (DataRow itm in Activities.Rows)
            {
                acct_dtls.activities.Add(new SkelClass.Document.AccountDetails.Activities()
                {
                    action = StringHelper.CTJ(itm["action"].ToString().Trim()),
                    contactperson = StringHelper.CTJ(itm["CntctCode"].ToString().Trim()),
                    content = StringHelper.CTJ(itm["notes"].ToString().Trim()),
                    enddatetime = StringHelper.CTJ(itm["endtime"].ToString().Trim()),
                    remarks = StringHelper.CTJ(itm["details"].ToString().Trim()),
                    startdatetme = StringHelper.CTJ(itm["begintime"].ToString().Trim()),
                    subject = StringHelper.CTJ(itm["CntctSbjct"].ToString().Trim()),
                    type = StringHelper.CTJ(itm["CntctType"].ToString().Trim()),
                });
            }
            #endregion

            // get the proposed changes
            DataTable proposedChangesCA;
            proposedChangesCA = SqlDbHelper.getDataDT("select * from proposedChangesCA where ccaNum='" + ccanum + "' and routeType != ''");
            #region INIT DATA
            acct_dtls.proposedChangesCA = new SkelClass.Document.AccountDetails._proposedChangesCA();
            acct_dtls.proposedChangesCA.acctName = "";
            acct_dtls.proposedChangesCA.acctOffcr = "";
            acct_dtls.proposedChangesCA.territory = "";
            acct_dtls.proposedChangesCA.area = "";
            acct_dtls.proposedChangesCA.region = "";
            acct_dtls.proposedChangesCA.regBusName = "";
            acct_dtls.proposedChangesCA.bussAdd = "";
            acct_dtls.proposedChangesCA.delAdd = "";

            /* Code added by Billy Jay (04/23/2015) */
            acct_dtls.proposedChangesCA.propCredTermsArchitecturalBrand = "";
            acct_dtls.proposedChangesCA.propCredTermsEcoforLumber = "";
            acct_dtls.proposedChangesCA.propCredTermsEcoforPlywood = "";

            acct_dtls.proposedChangesCA.CredTermRemarksArchitecturalBrand = "";
            acct_dtls.proposedChangesCA.CredTermRemarksEcoforLumber = "";
            acct_dtls.proposedChangesCA.CredTermRemarksEcoforPlywood = "";

            acct_dtls.proposedChangesCA.probOrderLimit_AB = "";
            acct_dtls.proposedChangesCA.probOrderLimit_TR = "";

            acct_dtls.proposedChangesCA.OrderLimitRemarks_AB = "";
            acct_dtls.proposedChangesCA.OrderLimitRemarks_TR = "";
            /* End Code added by Billy Jay (04/23/2015) */

            acct_dtls.proposedChangesCA.propCredTerms = "";
            acct_dtls.proposedChangesCA.propCredLimit = "";
            acct_dtls.proposedChangesCA.CredTermRemarks = "";
            acct_dtls.proposedChangesCA.CredLimitRemarks = "";
            acct_dtls.proposedChangesCA.pl_priceListCode_mw = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_mw = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_mw = "";
            acct_dtls.proposedChangesCA.pl_remarks_mw = "";
            acct_dtls.proposedChangesCA.pl_priceListCode_ww = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_ww = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_ww = "";
            acct_dtls.proposedChangesCA.pl_remarks_ww = "";
            acct_dtls.proposedChangesCA.pl_priceListCode_pwf = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_pwf = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_pwf = "";
            acct_dtls.proposedChangesCA.pl_remarks_pwf = "";
            acct_dtls.proposedChangesCA.pl_priceListCode_pwr = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_pwr = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_pwr = "";
            acct_dtls.proposedChangesCA.pl_remarks_pwr = "";
            acct_dtls.proposedChangesCA.pl_priceListCode_gw = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_gw = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_gw = "";
            acct_dtls.proposedChangesCA.pl_remarks_gw = "";
            acct_dtls.proposedChangesCA.pl_priceListCode_tw = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_tw = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_tw = "";
            acct_dtls.proposedChangesCA.pl_remarks_tw = "";

            acct_dtls.proposedChangesCA.pl_priceListCode_mz = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_mz = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_mz = "";
            acct_dtls.proposedChangesCA.pl_remarks_mz = "";

            acct_dtls.proposedChangesCA.pl_priceListCode_nw = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_nw = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_nw = "";
            acct_dtls.proposedChangesCA.pl_remarks_nw = "";

            acct_dtls.proposedChangesCA.pl_priceListCode_ec = "";
            acct_dtls.proposedChangesCA.pl_codeDesc_ec = "";
            acct_dtls.proposedChangesCA.pl_CommDisc_ec = "";
            acct_dtls.proposedChangesCA.pl_remarks_ec = "";

            acct_dtls.proposedChangesCA.route_type = "";

            acct_dtls.curr_doc_DocChangesStatus = "1";
            #endregion
            #region LOAD DATA
            foreach (DataRow itm in proposedChangesCA.Rows)
            {
                acct_dtls.proposedChangesCA.acctName = StringHelper.CTJ(itm["acctName"].ToString().Trim());
                acct_dtls.proposedChangesCA.acctOffcr = itm["acctOffcr"].ToString().Trim();
                acct_dtls.proposedChangesCA.territory = itm["territory"].ToString().Trim();
                acct_dtls.proposedChangesCA.area = itm["area"].ToString().Trim();
                acct_dtls.proposedChangesCA.region = itm["region"].ToString().Trim();
                acct_dtls.proposedChangesCA.regBusName = StringHelper.CTJ(itm["regBusName"].ToString().Trim());
                acct_dtls.proposedChangesCA.bussAdd = StringHelper.CTJ(itm["bussAdd"].ToString().Trim());
                acct_dtls.proposedChangesCA.delAdd = StringHelper.CTJ(itm["delAdd"].ToString().Trim());

                /* Code added by Billy Jay (04/23/2015) */
                //acct_dtls.initialPODetails.
                acct_dtls.proposedChangesCA.propCredTermsArchitecturalBrand = itm["propCredTermsArchitecturalBrand"].ToString().Trim();
                acct_dtls.proposedChangesCA.propCredTermsEcoforLumber = itm["propCredTermsEcoforLumber"].ToString().Trim();
                acct_dtls.proposedChangesCA.propCredTermsEcoforPlywood = itm["propCredTermsEcoforPlywood"].ToString().Trim();

                acct_dtls.proposedChangesCA.CredTermRemarksArchitecturalBrand = StringHelper.CTJ(itm["CredTermRemarksArchitecturalBrand"].ToString().Trim());
                acct_dtls.proposedChangesCA.CredTermRemarksEcoforLumber = StringHelper.CTJ(itm["CredTermRemarksEcoforLumber"].ToString().Trim());
                acct_dtls.proposedChangesCA.CredTermRemarksEcoforPlywood = StringHelper.CTJ(itm["CredTermRemarksEcoforPlywood"].ToString().Trim());

                acct_dtls.proposedChangesCA.probOrderLimit_AB = Decimal.TryParse(StringHelper.CTJ(itm["probOrderLimit_AB"].ToString().Trim()), out d) ? Convert.ToDecimal(StringHelper.CTJ(itm["probOrderLimit_AB"].ToString().Trim())).ToString("#,0") : "0";
                acct_dtls.proposedChangesCA.probOrderLimit_TR = Decimal.TryParse(StringHelper.CTJ(itm["probOrderLimit_TR"].ToString().Trim()), out d) ? Convert.ToDecimal(StringHelper.CTJ(itm["probOrderLimit_TR"].ToString().Trim())).ToString("#,0") : "0";

                acct_dtls.proposedChangesCA.OrderLimitRemarks_AB = StringHelper.CTJ(itm["OrderLimitRemarks_AB"].ToString().Trim());
                acct_dtls.proposedChangesCA.OrderLimitRemarks_TR = StringHelper.CTJ(itm["OrderLimitRemarks_TR"].ToString().Trim());
                /* End Code added by Billy Jay (04/23/2015) */
                acct_dtls.proposedChangesCA.propCredTerms = itm["propCredTerms"].ToString().Trim();
                acct_dtls.proposedChangesCA.propCredLimit = Decimal.TryParse(StringHelper.CTJ(itm["propCredLimit"].ToString().Trim()), out d) ? Convert.ToDecimal(itm["propCredLimit"].ToString().Trim()).ToString("#,0") : "0";
                acct_dtls.proposedChangesCA.CredTermRemarks = StringHelper.CTJ(itm["CredTermRemarks"].ToString().Trim());
                acct_dtls.proposedChangesCA.CredLimitRemarks = StringHelper.CTJ(itm["CredLimitRemarks"].ToString().Trim());
                acct_dtls.proposedChangesCA.pl_priceListCode_mw = itm["pl_priceListCode_mw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_mw = itm["pl_codeDesc_mw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_mw = itm["pl_CommDisc_mw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_mw = StringHelper.CTJ(itm["pl_remarks_mw"].ToString().Trim());
                acct_dtls.proposedChangesCA.pl_priceListCode_ww = itm["pl_priceListCode_ww"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_ww = itm["pl_codeDesc_ww"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_ww = itm["pl_CommDisc_ww"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_ww = StringHelper.CTJ(itm["pl_remarks_ww"].ToString().Trim());
                acct_dtls.proposedChangesCA.pl_priceListCode_pwf = itm["pl_priceListCode_pwf"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_pwf = itm["pl_codeDesc_pwf"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_pwf = itm["pl_CommDisc_pwf"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_pwf = StringHelper.CTJ(itm["pl_remarks_pwf"].ToString().Trim());
                acct_dtls.proposedChangesCA.pl_priceListCode_pwr = itm["pl_priceListCode_pwr"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_pwr = itm["pl_codeDesc_pwr"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_pwr = itm["pl_CommDisc_pwr"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_pwr = StringHelper.CTJ(itm["pl_remarks_pwr"].ToString().Trim());
                acct_dtls.proposedChangesCA.pl_priceListCode_gw = itm["pl_priceListCode_gw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_gw = itm["pl_codeDesc_gw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_gw = itm["pl_CommDisc_gw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_gw = StringHelper.CTJ(itm["pl_remarks_gw"].ToString().Trim());
                acct_dtls.proposedChangesCA.pl_priceListCode_tw = itm["pl_priceListCode_tw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_tw = itm["pl_codeDesc_tw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_tw = itm["pl_CommDisc_tw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_tw = StringHelper.CTJ(itm["pl_remarks_tw"].ToString().Trim());

                acct_dtls.proposedChangesCA.pl_priceListCode_mz = itm["pl_priceListCode_mz"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_mz = itm["pl_codeDesc_mz"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_mz = itm["pl_CommDisc_mz"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_mz = StringHelper.CTJ(itm["pl_remarks_mz"].ToString().Trim());

                acct_dtls.proposedChangesCA.pl_priceListCode_nw = itm["pl_priceListCode_nw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_nw = itm["pl_codeDesc_nw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_nw = itm["pl_CommDisc_nw"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_nw = StringHelper.CTJ(itm["pl_remarks_nw"].ToString().Trim());

                acct_dtls.proposedChangesCA.pl_priceListCode_ec = itm["pl_priceListCode_ec"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_ec = itm["pl_codeDesc_ec"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_ec = itm["pl_CommDisc_ec"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_ec = StringHelper.CTJ(itm["pl_remarks_ec"].ToString().Trim());

                acct_dtls.proposedChangesCA.pl_priceListCode_ecu = itm["pl_priceListCode_ecu"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_codeDesc_ecu = itm["pl_codeDesc_ecu"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_CommDisc_ecu = itm["pl_CommDisc_ecu"].ToString().Trim();
                acct_dtls.proposedChangesCA.pl_remarks_ecu = StringHelper.CTJ(itm["pl_remarks_ecu"].ToString().Trim());

                acct_dtls.proposedChangesCA.is_sent_back = itm["is_sent_back"].ToString().Trim();
                acct_dtls.proposedChangesCA.route_type = itm["routetype"].ToString().Trim();

                acct_dtls.curr_doc_DocChangesStatus = itm["status"].ToString().Trim();
            }
            #endregion



            DataTable deliveryAddress;
            deliveryAddress = SqlDbHelper.getDataDT("SELECT ch_address FROM CustDelAddress WHERE cCanum='" + ccanum + "' order by ch_address");
            #region LOAD DATA DELIVERY ADDRESS
            acct_dtls.delAddresses = new List<SkelClass.Document.AccountDetails.delivery_addresses>();
            foreach (DataRow itm in deliveryAddress.Rows)
            {
                acct_dtls.delAddresses.Add(new SkelClass.Document.AccountDetails.delivery_addresses()
                {
                    ch_address = StringHelper.CTJ(itm["ch_address"].ToString().Trim())
                });
            }

            acct_dtls.proposedDelAddresses = new List<SkelClass.Document.AccountDetails.delivery_addresses>();
            DataTable propDelAddDT;
            propDelAddDT = SqlDbHelper.getDataDT("select ch_address from proposedaddress where cCanum='" + ccanum + "' order by ch_address");
            foreach (DataRow itm in propDelAddDT.Rows)
            {
                acct_dtls.proposedDelAddresses.Add(new SkelClass.Document.AccountDetails.delivery_addresses()
                {
                    ch_address = StringHelper.CTJ(itm["ch_address"].ToString().Trim())
                });
            }

            #endregion
            ViewData["AccountDetailsInfo"] = acct_dtls;

            return View();
        }

        public string GetTypeOfAccount(string typeOfAccount)
        {
            if (typeOfAccount == "R")
                return "Retained Account";
            if (typeOfAccount == "U")
                return "Unretained Account";

            return "";
        }

        public string GetAccountStatus(string CardCode)
        {
            string tmp_res = "INACTIVE";

            DataTable AccountStatus_TBL = SqlDbHelper.getDataDT("select Status from customerAccStatus where ccanum = '" + CardCode + "'");
            foreach (DataRow itm in AccountStatus_TBL.Rows)
            {
                tmp_res = itm["Status"].ToString();
            }

            return tmp_res;
        }

        public ActionResult AccountsDetailsPending()
        {
            return View();
        }

        public ActionResult AccountsWChanges()
        {
            return View();
        }

        public ActionResult LeadAccounts()
        {
            return View();
        }

        public ActionResult LeadAccountsDetails()
        {
            /* TESTING */
            /* CHECK IF THIS WILL CLEANUP THE HTML */
            customerLeadI _customerLeadI = new customerLeadI();

            DataTable _tbl_data = SqlDbHelper.getDataDT("select *, convert(varchar(10), ExhibitDate, 101) as 'ExhibitDateFormatted', convert(varchar(10), DateEncoded, 101) as 'DateEncodedFormatted', convert(varchar(10), InqDate, 101) as 'InqDateFormatted', case when right(ProposedChannel,1) = 'L' then 'Luzon' else 'Vismin' end 'Region' from customerLeadI where RequestId='" + Request.QueryString["RequestId"].ToString() + "'");
            foreach (DataRow item in _tbl_data.Rows)
            {
                _customerLeadI.RequestId = Request.QueryString["RequestId"].ToString().Trim();
                _customerLeadI.ProposedChannel = item["ProposedChannel"].ToString().Trim();
                _customerLeadI.Status = item["Status"].ToString().Trim();
                _customerLeadI.ccaNum = item["ccaNum"].ToString().Trim();
                _customerLeadI.BkToSender = item["BkToSender"].ToString().Trim();
                _customerLeadI.Region = item["Region"].ToString().Trim();
                _customerLeadI.EncodedBy = item["EnCodedBy"].ToString().Trim();
            }

            return View(_customerLeadI);
        }

        public ActionResult LeadAccountsDetailsPending()
        {
            return View();
        }

        public ActionResult MMAgreements()
        {
            return View();
        }


    }
}
