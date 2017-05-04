using System;
using ARMS_W.Class;

namespace ARMS_W.GLOBALS
{
    public struct SActionResult
    {
        public const string Error = "<!--error-->";
        public const string Success = "<!--success-->";
    }

    /* Customer Creation Account */
    public class CCA
    {

        public enum EDocumentChanges
        {
            route_1,
            route_2,
            route_3,
            route_4,
            none
        }

        public enum EAccountType
        {
            INDIRECT,
            DIRECT
        }

        public enum EAccountClassification
        {
            WALKIN,
            REGULAR,
            LEAD,
            NONE
        }

        public enum EDocActionType
        {
            APPROVE,
            DISAPPROVE,
            SEND_BACK_TO_REQUESTER,
            PROPOSE_CHANGES,
            NONE,
            SEND_BACK_TO_CNC
        }

        public enum EUserPosID
        {
            none = 0,
            csr = 1,
            asm = 2,
            chm = 3,
            brm = 4,
            brd = 5,
            vpbsm = 6,
            cnc = 7,
            fnm = 8,
            vptfi = 9,
            tmg = 10,
            fsp = 11,
            csm = 12,
            so = 13,
            bmt = 14
        }

        /* for document Changes */
        public class DocumentChanges
        {
            public static string NextRoute(EDocumentChanges route_type, string curr_docstatus)
            {
                if (route_type == EDocumentChanges.route_1)
                {
                    if (curr_docstatus == AppHelper.GetUserPositionId("csr")) { return AppHelper.GetUserPositionId("asm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("asm")) { return AppHelper.GetUserPositionId("chm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("chm")) { return AppHelper.GetUserPositionId("vpbsm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("vpbsm")) { return AppHelper.GetUserPositionId("cnc"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("cnc")) { return AppHelper.GetUserPositionId("fnm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("fnm")) { return AppHelper.GetUserPositionId("vptfi"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("vptfi")) { return "1000"; }
                    else { return "0"; }
                }

                if (route_type == EDocumentChanges.route_2)
                {
                    if (curr_docstatus == AppHelper.GetUserPositionId("csr")) { return AppHelper.GetUserPositionId("asm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("asm")) { return AppHelper.GetUserPositionId("chm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("chm")) { return "1000"; }
                    else { return "0"; }
                }

                if (route_type == EDocumentChanges.route_3)
                {
                    if (curr_docstatus == AppHelper.GetUserPositionId("csr")) { return AppHelper.GetUserPositionId("cnc"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("cnc")) { return AppHelper.GetUserPositionId("fnm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("fnm")) { return AppHelper.GetUserPositionId("vptfi"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("vptfi")) { return "1000"; }
                    else { return "0"; }
                }

                if (route_type == EDocumentChanges.route_4)
                {
                    if (curr_docstatus == AppHelper.GetUserPositionId("csr")) { return AppHelper.GetUserPositionId("fnm"); }
                    else if (curr_docstatus == AppHelper.GetUserPositionId("fnm")) { return "1000"; }
                    else { return "0"; }
                }

                return "";
            }

            public static EDocumentChanges GetRouteType(
                    string old_acct_name, string new_acct_name,
                    string old_acct_acct_officer, string new_acct_acct_officer,
                    string old_acct_territory, string new_acct_territory,
                    string old_acct_area, string new_acct_area,
                    string old_acct_region, string new_acct_region,
                    string old_acct_reg_name, string new_acct_reg_name,
                    string old_acct_business_add, string new_acct_business_add,
                    string old_acct_delivery_add, string new_acct_delivery_add,

                    /* Code added by Billy Jay (04/23/2015) */
                    string old_acct_prop_credit_term_architectural_brand, string new_acct_prop_credit_term_architectural_brand,
                    string old_acct_prop_credit_term_ecofor_lumber, string new_acct_prop_credit_term_ecofor_lumber,
                    string old_acct_prop_credit_term_ecofor_plywood, string new_acct_prop_credit_term_ecofor_plywood,
                    string old_acct_prop_credit_term_remarks_architectural_brand, string new_acct_credit_term_remarks_architectural_brand,
                    string old_acct_prop_credit_term_remarks_ecofor_lumber, string new_acct_credit_term_remarks_ecofor_lumber,
                    string old_acct_prop_credit_term_remarks_ecofor_plywood, string new_acct_credit_term_remarks_ecofor_plywood,

                    string old_acct_prop_order_limit_ab, string new_acct_prop_order_limit_ab,
                    string old_acct_prop_order_limit_tr, string new_acct_prop_order_limit_tr,

                    string old_acct_prop_order_limit_remarks_ab, string new_acct_prop_order_limit_remarks_ab,
                    string old_acct_prop_order_limit_remarks_tr, string new_acct_prop_order_limit_remarks_tr,
                /* End Code added by Billy Jay (04/23/2015) */

                    string old_acct_prop_credit_term, string new_acct_prop_credit_term,
                    string old_acct_prop_credit_limit, string new_acct_prop_credit_limit,

                    string old_acct_prop_credit_term_remarks, string new_acct_prop_credit_term_remarks,
                    string old_acct_prop_credit_limit_remarks, string new_acct_prop_credit_limit_remarks,

                    string new_acct_mw_price_code, string old_acct_mw_price_code,
                    string new_acct_mw_price_desc, string old_acct_mw_price_desc,
                    string new_acct_mw_price_commision_disc, string old_acct_mw_price_commision_disc,
                    string new_acct_mw_price_remarks, string old_acct_mw_price_remarks,
                    string new_acct_ww_price_code, string old_acct_ww_price_code,
                    string new_acct_ww_price_desc, string old_acct_ww_price_desc,
                    string new_acct_ww_price_commision_disc, string old_acct_ww_price_commision_disc,
                    string new_acct_ww_price_remarks, string old_acct_ww_price_remarks,
                    string new_acct_pwf_price_code, string old_acct_pwf_price_code,
                    string new_acct_pwf_price_desc, string old_acct_pwf_price_desc,
                    string new_acct_pwf_price_commision_disc, string old_acct_pwf_price_commision_disc,
                    string new_acct_pwf_price_remarks, string old_acct_pwf_price_remarks,
                    string new_acct_pwr_price_code, string old_acct_pwr_price_code,
                    string new_acct_pwr_price_desc, string old_acct_pwr_price_desc,
                    string new_acct_pwr_price_commision_disc, string old_acct_pwr_price_commision_disc,
                    string new_acct_pwr_price_remarks, string old_acct_pwr_price_remarks,
                    string new_acct_gw_price_code, string old_acct_gw_price_code,
                    string new_acct_gw_price_desc, string old_acct_gw_price_desc,
                    string new_acct_gw_price_commision_disc, string old_acct_gw_price_commision_disc,
                    string new_acct_gw_price_remarks, string old_acct_gw_price_remarks,
                    string new_acct_tw_price_code, string old_acct_tw_price_code,
                    string new_acct_tw_price_desc, string old_acct_tw_price_desc,
                    string new_acct_tw_price_commision_disc, string old_acct_tw_price_commision_disc,
                    string new_acct_tw_price_remarks, string old_acct_tw_price_remarks,

                    string new_acct_mz_price_code, string old_acct_mz_price_code,
                    string new_acct_mz_price_desc, string old_acct_mz_price_desc,
                    string new_acct_mz_price_commision_disc, string old_acct_mz_price_commision_disc,
                    string new_acct_mz_price_remarks, string old_acct_mz_price_remarks,

                    string new_acct_nw_price_code, string old_acct_nw_price_code,
                    string new_acct_nw_price_desc, string old_acct_nw_price_desc,
                    string new_acct_nw_price_commision_disc, string old_acct_nw_price_commision_disc,
                    string new_acct_nw_price_remarks, string old_acct_nw_price_remarks,

                    string new_acct_ec_price_code, string old_acct_ec_price_code,
                    string new_acct_ec_price_desc, string old_acct_ec_price_desc,
                    string new_acct_ec_price_commision_disc, string old_acct_ec_price_commision_disc,
                    string new_acct_ec_price_remarks, string old_acct_ec_price_remarks,

                    string new_acct_ecu_price_code, string old_acct_ecu_price_code,
                    string new_acct_ecu_price_desc, string old_acct_ecu_price_desc,
                    string new_acct_ecu_price_commision_disc, string old_acct_ecu_price_commision_disc,
                    string new_acct_ecu_price_remarks, string old_acct_ecu_price_remarks
                )
            {

                if (
                    (
                        (old_acct_prop_credit_term != new_acct_prop_credit_term || old_acct_prop_credit_limit != new_acct_prop_credit_limit ||
                    /* Code added by Billy Jay (04/23/2015) */
                             old_acct_prop_credit_term_architectural_brand != new_acct_prop_credit_term_architectural_brand || old_acct_prop_credit_term_ecofor_lumber != new_acct_prop_credit_term_ecofor_lumber ||
                             old_acct_prop_credit_term_ecofor_plywood != new_acct_prop_credit_term_ecofor_plywood ||
                             old_acct_prop_order_limit_ab != new_acct_prop_order_limit_ab || old_acct_prop_order_limit_tr != new_acct_prop_order_limit_tr
                    /* End Coded by Billy Jay (04/23/2015) */
                        ) ||
                        (old_acct_prop_credit_term_remarks != new_acct_prop_credit_term_remarks || old_acct_prop_credit_limit_remarks != new_acct_prop_credit_limit_remarks ||
                           old_acct_prop_credit_term_remarks_architectural_brand != new_acct_credit_term_remarks_architectural_brand ||
                             old_acct_prop_credit_term_remarks_ecofor_lumber != new_acct_credit_term_remarks_ecofor_lumber || old_acct_prop_credit_term_remarks_ecofor_plywood != new_acct_credit_term_remarks_ecofor_plywood ||
                             old_acct_prop_order_limit_remarks_ab != new_acct_prop_order_limit_remarks_ab || old_acct_prop_order_limit_remarks_tr != new_acct_prop_order_limit_remarks_tr
                             )
                    ) == true &&
                    (
                        old_acct_mw_price_code != new_acct_mw_price_code ||
                        old_acct_mw_price_desc != new_acct_mw_price_desc ||
                        old_acct_mw_price_commision_disc != new_acct_mw_price_commision_disc ||
                        old_acct_mw_price_remarks != new_acct_mw_price_remarks ||
                        old_acct_ww_price_code != new_acct_ww_price_code ||
                        old_acct_ww_price_desc != new_acct_ww_price_desc ||
                        old_acct_ww_price_commision_disc != new_acct_ww_price_commision_disc ||
                        old_acct_ww_price_remarks != new_acct_ww_price_remarks ||
                        old_acct_pwf_price_code != new_acct_pwf_price_code ||
                        old_acct_pwf_price_desc != new_acct_pwf_price_desc ||
                        old_acct_pwf_price_commision_disc != new_acct_pwf_price_commision_disc ||
                        old_acct_pwf_price_remarks != new_acct_pwf_price_remarks ||
                        old_acct_pwr_price_code != new_acct_pwr_price_code ||
                        old_acct_pwr_price_desc != new_acct_pwr_price_desc ||
                        old_acct_pwr_price_commision_disc != new_acct_pwr_price_commision_disc ||
                        old_acct_pwr_price_remarks != new_acct_pwr_price_remarks ||
                        old_acct_gw_price_code != new_acct_gw_price_code ||
                        old_acct_gw_price_desc != new_acct_gw_price_desc ||
                        old_acct_gw_price_commision_disc != new_acct_gw_price_commision_disc ||
                        old_acct_gw_price_remarks != new_acct_gw_price_remarks ||
                        old_acct_tw_price_code != new_acct_tw_price_code ||
                        old_acct_tw_price_desc != new_acct_tw_price_desc ||
                        old_acct_tw_price_commision_disc != new_acct_tw_price_commision_disc ||
                        old_acct_tw_price_remarks != new_acct_tw_price_remarks ||

                        old_acct_mz_price_code != new_acct_mz_price_code ||
                        old_acct_mz_price_desc != new_acct_mz_price_desc ||
                        old_acct_mz_price_commision_disc != new_acct_mz_price_commision_disc ||
                        old_acct_mz_price_remarks != new_acct_mz_price_remarks ||

                        old_acct_nw_price_code != new_acct_nw_price_code ||
                        old_acct_nw_price_desc != new_acct_nw_price_desc ||
                        old_acct_nw_price_commision_disc != new_acct_nw_price_commision_disc ||
                        old_acct_nw_price_remarks != new_acct_nw_price_remarks ||

                        old_acct_ec_price_code != new_acct_ec_price_code ||
                        old_acct_ec_price_desc != new_acct_ec_price_desc ||
                        old_acct_ec_price_commision_disc != new_acct_ec_price_commision_disc ||
                        old_acct_ec_price_remarks != new_acct_ec_price_remarks ||

                        old_acct_ecu_price_code != new_acct_ecu_price_code ||
                        old_acct_ecu_price_desc != new_acct_ecu_price_desc ||
                        old_acct_ecu_price_commision_disc != new_acct_ecu_price_commision_disc ||
                        old_acct_ecu_price_remarks != new_acct_ecu_price_remarks

                    ) == false
                )
                {
                    return EDocumentChanges.route_3;
                }
                else if (
                    (
                        (old_acct_prop_credit_term != new_acct_prop_credit_term || old_acct_prop_credit_limit != new_acct_prop_credit_limit ||
                    /* Code added by Billy Jay (04/23/2015) */
                         old_acct_prop_credit_term_architectural_brand != new_acct_prop_credit_term_architectural_brand || old_acct_prop_credit_term_ecofor_lumber != new_acct_prop_credit_term_ecofor_lumber ||
                         old_acct_prop_credit_term_ecofor_plywood != new_acct_prop_credit_term_ecofor_plywood ||
                         old_acct_prop_order_limit_ab != new_acct_prop_order_limit_ab || old_acct_prop_order_limit_tr != new_acct_prop_order_limit_tr
                    /* End Coded by Billy Jay (04/23/2015) */
                    ) ||
                        (old_acct_prop_credit_term_remarks != new_acct_prop_credit_term_remarks || old_acct_prop_credit_limit_remarks != new_acct_prop_credit_limit_remarks ||
                         old_acct_prop_credit_term_remarks_architectural_brand != new_acct_credit_term_remarks_architectural_brand ||
                         old_acct_prop_credit_term_remarks_ecofor_lumber != new_acct_credit_term_remarks_ecofor_lumber || old_acct_prop_credit_term_remarks_ecofor_plywood != new_acct_credit_term_remarks_ecofor_plywood ||
                         old_acct_prop_order_limit_remarks_ab != new_acct_prop_order_limit_remarks_ab || old_acct_prop_order_limit_remarks_tr != new_acct_prop_order_limit_remarks_tr)
                    ) == true &&
                    (
                        old_acct_mw_price_code != new_acct_mw_price_code ||
                        old_acct_mw_price_desc != new_acct_mw_price_desc ||
                        old_acct_mw_price_commision_disc != new_acct_mw_price_commision_disc ||
                        old_acct_mw_price_remarks != new_acct_mw_price_remarks ||
                        old_acct_ww_price_code != new_acct_ww_price_code ||
                        old_acct_ww_price_desc != new_acct_ww_price_desc ||
                        old_acct_ww_price_commision_disc != new_acct_ww_price_commision_disc ||
                        old_acct_ww_price_remarks != new_acct_ww_price_remarks ||
                        old_acct_pwf_price_code != new_acct_pwf_price_code ||
                        old_acct_pwf_price_desc != new_acct_pwf_price_desc ||
                        old_acct_pwf_price_commision_disc != new_acct_pwf_price_commision_disc ||
                        old_acct_pwf_price_remarks != new_acct_pwf_price_remarks ||
                        old_acct_pwr_price_code != new_acct_pwr_price_code ||
                        old_acct_pwr_price_desc != new_acct_pwr_price_desc ||
                        old_acct_pwr_price_commision_disc != new_acct_pwr_price_commision_disc ||
                        old_acct_pwr_price_remarks != new_acct_pwr_price_remarks ||
                        old_acct_gw_price_code != new_acct_gw_price_code ||
                        old_acct_gw_price_desc != new_acct_gw_price_desc ||
                        old_acct_gw_price_commision_disc != new_acct_gw_price_commision_disc ||
                        old_acct_gw_price_remarks != new_acct_gw_price_remarks ||
                        old_acct_tw_price_code != new_acct_tw_price_code ||
                        old_acct_tw_price_desc != new_acct_tw_price_desc ||
                        old_acct_tw_price_commision_disc != new_acct_tw_price_commision_disc ||
                        old_acct_tw_price_remarks != new_acct_tw_price_remarks ||

                        old_acct_mz_price_code != new_acct_mz_price_code ||
                        old_acct_mz_price_desc != new_acct_mz_price_desc ||
                        old_acct_mz_price_commision_disc != new_acct_mz_price_commision_disc ||
                        old_acct_mz_price_remarks != new_acct_mz_price_remarks ||

                        old_acct_nw_price_code != new_acct_nw_price_code ||
                        old_acct_nw_price_desc != new_acct_nw_price_desc ||
                        old_acct_nw_price_commision_disc != new_acct_nw_price_commision_disc ||
                        old_acct_nw_price_remarks != new_acct_nw_price_remarks ||

                        old_acct_ec_price_code != new_acct_ec_price_code ||
                        old_acct_ec_price_desc != new_acct_ec_price_desc ||
                        old_acct_ec_price_commision_disc != new_acct_ec_price_commision_disc ||
                        old_acct_ec_price_remarks != new_acct_ec_price_remarks ||

                        old_acct_ecu_price_code != new_acct_ecu_price_code ||
                        old_acct_ecu_price_desc != new_acct_ecu_price_desc ||
                        old_acct_ecu_price_commision_disc != new_acct_ecu_price_commision_disc ||
                        old_acct_ecu_price_remarks != new_acct_ecu_price_remarks

                    ) == true
                )
                {
                    return EDocumentChanges.route_1;
                }
                else if (
                    (
                        (old_acct_prop_credit_term != new_acct_prop_credit_term || old_acct_prop_credit_limit != new_acct_prop_credit_limit ||
                    /* Code added by Billy Jay (04/23/2015) */
                             old_acct_prop_credit_term_architectural_brand != new_acct_prop_credit_term_architectural_brand || old_acct_prop_credit_term_ecofor_lumber != new_acct_prop_credit_term_ecofor_lumber ||
                             old_acct_prop_credit_term_ecofor_plywood != new_acct_prop_credit_term_ecofor_plywood ||
                              old_acct_prop_order_limit_ab != new_acct_prop_order_limit_ab || old_acct_prop_order_limit_tr != new_acct_prop_order_limit_tr
                    /* End Coded by Billy Jay (04/23/2015) */
                        ) ||
                        (old_acct_prop_credit_term_remarks != new_acct_prop_credit_term_remarks || old_acct_prop_credit_limit_remarks != new_acct_prop_credit_limit_remarks ||
                         old_acct_prop_credit_term_remarks_architectural_brand != new_acct_credit_term_remarks_architectural_brand ||
                        old_acct_prop_credit_term_remarks_ecofor_lumber != new_acct_credit_term_remarks_ecofor_lumber || old_acct_prop_credit_term_remarks_ecofor_plywood != new_acct_credit_term_remarks_ecofor_plywood ||
                        old_acct_prop_order_limit_remarks_ab != new_acct_prop_order_limit_remarks_ab || old_acct_prop_order_limit_remarks_tr != new_acct_prop_order_limit_remarks_tr
                    )
                    ) == false &&
                    (
                        old_acct_mw_price_code != new_acct_mw_price_code ||
                        old_acct_mw_price_desc != new_acct_mw_price_desc ||
                        old_acct_mw_price_commision_disc != new_acct_mw_price_commision_disc ||
                        old_acct_mw_price_remarks != new_acct_mw_price_remarks ||
                        old_acct_ww_price_code != new_acct_ww_price_code ||
                        old_acct_ww_price_desc != new_acct_ww_price_desc ||
                        old_acct_ww_price_commision_disc != new_acct_ww_price_commision_disc ||
                        old_acct_ww_price_remarks != new_acct_ww_price_remarks ||
                        old_acct_pwf_price_code != new_acct_pwf_price_code ||
                        old_acct_pwf_price_desc != new_acct_pwf_price_desc ||
                        old_acct_pwf_price_commision_disc != new_acct_pwf_price_commision_disc ||
                        old_acct_pwf_price_remarks != new_acct_pwf_price_remarks ||
                        old_acct_pwr_price_code != new_acct_pwr_price_code ||
                        old_acct_pwr_price_desc != new_acct_pwr_price_desc ||
                        old_acct_pwr_price_commision_disc != new_acct_pwr_price_commision_disc ||
                        old_acct_pwr_price_remarks != new_acct_pwr_price_remarks ||
                        old_acct_gw_price_code != new_acct_gw_price_code ||
                        old_acct_gw_price_desc != new_acct_gw_price_desc ||
                        old_acct_gw_price_commision_disc != new_acct_gw_price_commision_disc ||
                        old_acct_gw_price_remarks != new_acct_gw_price_remarks ||
                        old_acct_tw_price_code != new_acct_tw_price_code ||
                        old_acct_tw_price_desc != new_acct_tw_price_desc ||
                        old_acct_tw_price_commision_disc != new_acct_tw_price_commision_disc ||
                        old_acct_tw_price_remarks != new_acct_tw_price_remarks ||

                        old_acct_mz_price_code != new_acct_mz_price_code ||
                        old_acct_mz_price_desc != new_acct_mz_price_desc ||
                        old_acct_mz_price_commision_disc != new_acct_mz_price_commision_disc ||
                        old_acct_mz_price_remarks != new_acct_mz_price_remarks ||

                        old_acct_nw_price_code != new_acct_nw_price_code ||
                        old_acct_nw_price_desc != new_acct_nw_price_desc ||
                        old_acct_nw_price_commision_disc != new_acct_nw_price_commision_disc ||
                        old_acct_nw_price_remarks != new_acct_nw_price_remarks ||

                        old_acct_ec_price_code != new_acct_ec_price_code ||
                        old_acct_ec_price_desc != new_acct_ec_price_desc ||
                        old_acct_ec_price_commision_disc != new_acct_ec_price_commision_disc ||
                        old_acct_ec_price_remarks != new_acct_ec_price_remarks ||

                        old_acct_ecu_price_code != new_acct_ecu_price_code ||
                        old_acct_ecu_price_desc != new_acct_ecu_price_desc ||
                        old_acct_ecu_price_commision_disc != new_acct_ecu_price_commision_disc ||
                        old_acct_ecu_price_remarks != new_acct_ecu_price_remarks
                    ) == true
                )
                {
                    return EDocumentChanges.route_1;
                }

                if (
                    (old_acct_name != new_acct_name ||
                    old_acct_acct_officer != new_acct_acct_officer ||
                    old_acct_territory != new_acct_territory ||
                    old_acct_area != new_acct_area ||
                    old_acct_region != new_acct_region ||
                    old_acct_reg_name != new_acct_reg_name ||
                    old_acct_business_add != new_acct_business_add) == true //||
                    //old_acct_delivery_add != new_acct_delivery_add
                    &&
                    (old_acct_delivery_add != new_acct_delivery_add) == false
                )
                {
                    return EDocumentChanges.route_2;
                }

                if (old_acct_delivery_add != new_acct_delivery_add)
                {
                    return EDocumentChanges.route_4;
                }

                return EDocumentChanges.none;
            }
        }

        /* for Document Creation */
        public class DocumentCreation
        {
            public static string NextRoute(EAccountClassification AcctClassF, EAccountType AcctType, int curr_docstatus)
            {
                if (AcctClassF == EAccountClassification.WALKIN)
                {
                    if (curr_docstatus == 1)
                    {
                        return Convert.ToString(2);
                    }
                }
                else if (AcctClassF == EAccountClassification.REGULAR)
                {
                    if (AcctType == EAccountType.INDIRECT)
                    {
                        if (curr_docstatus == 1)
                        {
                            return Convert.ToString(2);
                        }
                    }
                    else if (AcctType == EAccountType.DIRECT)
                    {
                        if (curr_docstatus == 1)
                        {
                            return Convert.ToString(2);
                        }
                        else if (curr_docstatus == 2)
                        {
                            return Convert.ToString(3);
                        }
                        else if (curr_docstatus == 3)
                        {
                            return Convert.ToString(4);
                        }
                        else if (curr_docstatus == 4)
                        {
                            return Convert.ToString(5);
                        }
                        else if (curr_docstatus == 5)
                        {
                            return Convert.ToString(6);
                        }
                        else if (curr_docstatus == 6)
                        {
                            return Convert.ToString(7);
                        }
                        else if (curr_docstatus == 7)
                        {
                            return Convert.ToString(8);
                        }
                    }
                }

                // USE 1000 as APPROVED
                return Convert.ToString(1000);

            }



        }

        /*  */

    }


    // another DOCTYPE


}