using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using ARMS_W.Class;

namespace ARMS_W.Reports.Page
{
    public class PRDEF_USER 
    {
        public string UserName { get; set; }
        public List<string> AREA { get; set; }
        public List<string> CHANNEL { get; set; }
    }

    public partial class salesorderstatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                _User current_user = (_User)Session["Ousr"];

                StringBuilder filters = new StringBuilder(1000);

                List<PRDEF_USER> defined_user_access = new List<PRDEF_USER>();
                #region CS LUZON
                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "KVICTORIA",
                    CHANNEL = new List<string>() { "LDI - LUZON DIY", "LIS - LUZON INSTITUTIONAL SALES", "LTS - LUZON TRADE SALES", "LWC - LUZON WOOD CENTER" },
                    AREA = new List<string>() { }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "CPANGANIBAN",
                    CHANNEL = new List<string>() { "LIS - LUZON INSTITUTIONAL SALES", "LWC - LUZON WOOD CENTER" },
                    AREA = new List<string>() { }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "RDUQUE", CHANNEL = new List<string>() {  }, AREA = new List<string>() { "AR012" }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "TPAULE", CHANNEL = new List<string>() { }, AREA = new List<string>() { "AR011" }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "KMAGDAOG",
                    CHANNEL = new List<string>() { },
                    AREA = new List<string>() { "AR031", "AR014" }
                });
                #endregion
                #region GENERAL TRADE
                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "ATADEO",
                    CHANNEL = new List<string>() { "LTS - LUZON TRADE SALES", "LTK - LUZON TRADE KEY ACCOUNTS" },
                    AREA = new List<string>() { }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "LSY",
                    CHANNEL = new List<string>() { "VTK - VISMIN TRADE KEY ACCOUNTS", "VTS - VISMIN TRADE SALES" },
                    AREA = new List<string>() { }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "MDELOSREYES",
                    CHANNEL = new List<string>() { "LTS - LUZON TRADE SALES", "VTK - VISMIN TRADE KEY ACCOUNTS", "VTS - VISMIN TRADE SALES" },
                    AREA = new List<string>() { }
                });
                #endregion
                #region ISWC
                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "ESAMONTE",
                    CHANNEL = new List<string>() { "LIS - LUZON INSTITUTIONAL SALES", "LWC - LUZON WOOD CENTER", "VIS - VISMIN INSTITUTIONAL SALES", "VWC - VISMIN WOOD CENTER" },
                    AREA = new List<string>() { }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "DVILLAPANDO",
                    CHANNEL = new List<string>() { "LIS - LUZON INSTITUTIONAL SALES", "LWC - LUZON WOOD CENTER", "VIS - VISMIN INSTITUTIONAL SALES", "VWC - VISMIN WOOD CENTER" },
                    AREA = new List<string>() { }
                });
                #endregion
                #region MODERN TRADE
                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "MDILANGALEN",
                    CHANNEL = new List<string>() { "LDI - LUZON DIY", "VDI - VISMIN DIY" },
                    AREA = new List<string>() { }
                });
                #endregion
                #region CS VISMIN
                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "AFLOR",
                    CHANNEL = new List<string>() { "VDI - VISMIN DIY", "VIS - VISMIN INSTITUTIONAL SALES", "VTK - VISMIN TRADE KEY ACCOUNTS", "VTS - VISMIN TRADE SALES", "VWC - VISMIN WOOD CENTER" },
                    AREA = new List<string>() { }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "YMAISOG", CHANNEL = new List<string>() { }, AREA = new List<string>() { "AR060", "AR041" }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "DCATANDA", CHANNEL = new List<string>() { }, AREA = new List<string>() { "AR004" }
                });

                defined_user_access.Add(new PRDEF_USER()
                {
                    UserName = "RMANRIQUEZ", CHANNEL = new List<string>() { }, AREA = new List<string>() { "AR001" }
                });
                #endregion

                bool UserIsInPreDef = false;

                foreach (PRDEF_USER itm in defined_user_access) 
                {
                    if (current_user.UserName.ToUpper() == itm.UserName.ToUpper()) 
                    {
                        UserIsInPreDef = true;
                        break;
                    }
                }

                if (UserIsInPreDef)
                {
                    #region NOT NORMAL

                    foreach (PRDEF_USER itm in defined_user_access) 
                    {
                        if (itm.UserName.ToUpper() == current_user.UserName.ToUpper()) 
                        {
                            if (itm.AREA.Count > 0)
                            {
                                filters.Append(" and area in (");
                                bool hasarea1 = false;
                                foreach (string area in itm.AREA)
                                {
                                    if (hasarea1) filters.Append(",");
                                    filters.Append("'" + area + "'");
                                    hasarea1 = true;
                                }
                                filters.Append(")");
                            }

                            if (itm.CHANNEL.Count > 0)
                            {
                                filters.Append(" and channel in (");
                                bool haschannel1 = false;
                                foreach (string channel in itm.CHANNEL)
                                {
                                    if (haschannel1) filters.Append(",");
                                    filters.Append("'" + channel.Substring(0,3) + "'");
                                    haschannel1 = true;
                                }
                                filters.Append(")");
                            }
                        }
                        
                    }

                    #endregion
                }
                else
                {
                    #region NORMAL
                    if (current_user.HasPositionOf("SO") > -1)
                    {
                        // is so
                        filters.Append(" and cardcode in (");
                        foreach (_Roles role in current_user.Roles)
                            if (role.Position == "SO")
                            {
                                bool hasaccount = false;
                                foreach (string cardcode in role.Accounts)
                                {
                                    if (hasaccount) filters.Append(",");
                                    filters.Append("'");
                                    filters.Append(cardcode);
                                    filters.Append("'");
                                    hasaccount = true;
                                }
                            }
                        filters.Append(")");
                    }
                    else if (current_user.HasPositionOf("ASM") > -1)
                    {
                        // is asm
                        filters.Append(" and area in (");
                        foreach (_Roles role in current_user.Roles)
                            if (role.Position == "ASM")
                            {
                                bool hasarea = false;
                                foreach (string area in role.Area)
                                {
                                    if (hasarea) filters.Append(",");
                                    filters.Append("'");
                                    filters.Append(area.Substring(0, 5).Trim());
                                    filters.Append("'");
                                    hasarea = true;
                                }
                            }
                        filters.Append(")");
                    }
                    else
                    {
                        // has other roles
                        filters.Append("");
                    }
                    #endregion
                }

                DataTable mtable = null;
                mtable = SqlDbHelper.getDataDT(@" 
                    declare @latest_date datetime;
                    select @latest_date = isnull((select max(UploadDateTime) from UploadedSalesReport), getdate() )
                    select * from UploadedSalesReport where convert(varchar(10), UploadDateTime, 101) = convert(varchar(10), @latest_date, 101)
                " + filters.ToString());

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("rpt_salesorder", mtable));
                ReportViewer1.LocalReport.Refresh();

            }
        }
    }
}