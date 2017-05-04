using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Data;


namespace ARMS_W.Class
{
    public class ReportHelper
    {
        public static void GetRegions(string username, string Param_name, ref ParameterCollection ParamColl)
        {
            _User Ousr = new _User(username);
            ParameterCollection Params = new ParameterCollection();
            if (Ousr.HasPositionOf("csr") != -1 || Ousr.HasPositionOf("cnc") != -1 || Ousr.HasPositionOf("fnm") != -1 || Ousr.HasPositionOf("csm") != -1 || Ousr.HasPositionOf("vw1") != -1)
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    foreach (string val in rls.Region)
                    {
                        Parameter Param = new Parameter();
                        Param.Name = Param_name;

                        if (val == "VISMIN") Param.DefaultValue = "CV";
                        else if (val == "LUZON") Param.DefaultValue = "CL";
                        else Param.DefaultValue = "C";
                        ParamColl.Add(Param);
                    }
                }
            }
        }

        public static void GetAreas(string username, string Param_name, ref ParameterCollection ParamColl)
        {
            ParameterCollection Params = new ParameterCollection();

            _User Ousr = new _User(username);
            if (Ousr.HasPositionOf("asm") != -1)
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    foreach (string val in rls.Area)
                    {
                        Parameter Param = new Parameter();

                        Param.Name = Param_name;
                        Param.DefaultValue = val;
                        ParamColl.Add(Param);
                    }
                }
            }
        }

        public static void GetChannels(string username, string Param_name, ref ParameterCollection ParamColl)
        {
            ParameterCollection Params = new ParameterCollection();
            _User Ousr = new _User(username);
            if (Ousr.HasPositionOf("ca") != -1 || Ousr.HasPositionOf("chm") != -1 || Ousr.HasPositionOf("cmg") != -1)
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    foreach (string val in rls.Channel)
                    {
                        Parameter Param = new Parameter();
                        Param.Name = Param_name;

                        Param.DefaultValue = val;
                        ParamColl.Add(Param);
                    }
                }
            }
        }

        public static void GetSOs(string username, string Param_name, ref ParameterCollection ParamColl)
        {
            _User Ousr = new _User(username);
            if (Ousr.HasPositionOf("so") != -1)
            {
                string strquery = "select lastname +', '+ firstname from userheader where username='" + username + "' and lastname +', '+ firstname in (select distinct acctoffcr from customerheader)";
                DataTable gtable = SqlDbHelper.getDataDT(strquery);

                foreach (DataRow val in gtable.Rows)
                {
                    Parameter Param = new Parameter();
                    Param.Name = Param_name;

                    Param.DefaultValue = val[0].ToString();
                    ParamColl.Add(Param);
                }
            }

        }

        public static void GetBrands(string username, string Param_name, ref ParameterCollection ParamColl)
        {
            ParameterCollection Params = new ParameterCollection();
            _User Ousr = new _User(username);
            if (Ousr.HasPositionOf("brm") != -1)
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    foreach (String val in rls.Brand)
                    {
                        Parameter Param = new Parameter();
                        Param.Name = Param_name;

                        Param.DefaultValue = val;
                        ParamColl.Add(Param);
                    }

                }
            }
        }

        public static void GetRegions(string username, string param_name, ref CrystalDecisions.Web.Parameter[] iparameters)
        {
            string[] Region_users = { "csr", "cnc", "fnm" };
            _User Ousr = new _User(username);
            int param_count = iparameters.Length;
            if (Ousr.HasPositionsOf(Region_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Region_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Region)
                        {
                            int new_array_size = param_count + 1;
                            int new_array_index = new_array_size - 1;
                            Array.Resize(ref iparameters, new_array_size);

                            iparameters[new_array_index] = new CrystalDecisions.Web.Parameter();
                            iparameters[new_array_index].Name = param_name;
                            iparameters[new_array_index].DefaultValue = val;
                            param_count++;
                        }
                }
            }
        }

        public static void GetChannels(string username, string param_name, ref CrystalDecisions.Web.Parameter[] iparameters)
        {
            string[] Channel_users = { "chm", "ca" };
            _User Ousr = new _User(username);
            int param_count = iparameters.Length;
            if (Ousr.HasPositionsOf(Channel_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Channel_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Channel)
                        {
                            int new_array_size = param_count + 1;
                            int new_array_index = new_array_size - 1;
                            Array.Resize(ref iparameters, new_array_size);

                            iparameters[new_array_index] = new CrystalDecisions.Web.Parameter();
                            iparameters[new_array_index].Name = param_name;
                            iparameters[new_array_index].DefaultValue = val;
                            param_count++;
                        }
                }
            }
        }

        public static void GetAreas(string username, string param_name, ref CrystalDecisions.Web.Parameter[] iparameters)
        {
            string[] Area_users = { "asm" };
            _User Ousr = new _User(username);
            int param_count = iparameters.Length;
            if (Ousr.HasPositionsOf(Area_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Area_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Area)
                        {
                            int new_array_size = param_count + 1;
                            int new_array_index = new_array_size - 1;
                            Array.Resize(ref iparameters, new_array_size);

                            iparameters[new_array_index] = new CrystalDecisions.Web.Parameter();
                            iparameters[new_array_index].Name = param_name;
                            iparameters[new_array_index].DefaultValue = val;
                            param_count++;
                        }
                }
            }
        }

        public static void GetRegions(string username, string param_name, ref ParameterFields pfield, ref ParameterDiscreteValue[] par_disc)
        {
            string[] Region_users = { "csr", "cnc", "fnm" };
            _User Ousr = new _User(username);
            if (Ousr.HasPositionsOf(Region_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Region_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Region)
                        {
                            int new_array_size = par_disc.Length + 1;
                            int new_array_index = new_array_size - 1;

                            Array.Resize(ref par_disc, new_array_size);

                            par_disc[new_array_index] = new ParameterDiscreteValue();
                            par_disc[new_array_index].Value = val;
                            pfield[param_name].CurrentValues.Add(par_disc[new_array_index]);
                        }
                }
            }
        }

        public static void GetChannels(string username, string param_name, ref ParameterFields pfield, ref ParameterDiscreteValue[] par_disc)
        {
            string[] Channel_users = { "chm", "ca" };
            _User Ousr = new _User(username);
            if (Ousr.HasPositionsOf(Channel_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Channel_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Channel)
                        {
                            int new_array_size = par_disc.Length + 1;
                            int new_array_index = new_array_size - 1;

                            Array.Resize(ref par_disc, new_array_size);

                            par_disc[new_array_index] = new ParameterDiscreteValue();
                            par_disc[new_array_index].Value = val;
                            pfield[param_name].CurrentValues.Add(par_disc[new_array_index]);
                        }
                }
            }
        }

        public static void GetAreas(string username, string param_name, ref ParameterFields pfield, ref ParameterDiscreteValue[] par_disc)
        {
            string[] Area_users = { "asm" };
            _User Ousr = new _User(username);
            if (Ousr.HasPositionsOf(Area_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Area_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Area)
                        {
                            int new_array_size = par_disc.Length + 1;
                            int new_array_index = new_array_size - 1;

                            Array.Resize(ref par_disc, new_array_size);

                            par_disc[new_array_index] = new ParameterDiscreteValue();
                            par_disc[new_array_index].Value = val;
                            pfield[param_name].CurrentValues.Add(par_disc[new_array_index]);
                        }
                }
            }
        }

        public static void GetBrands(string username, string param_name, ref ParameterFields pfield, ref ParameterDiscreteValue[] par_disc)
        {
            string[] Brand_users = { "brm" };
            _User Ousr = new _User(username);
            if (Ousr.HasPositionsOf(Brand_users))
            {
                foreach (_Roles rls in Ousr.Roles)
                {
                    string str_positions = string.Join(",", Brand_users).ToUpper();
                    if (str_positions.IndexOf(rls.Position.ToUpper()) != -1)
                        foreach (string val in rls.Brand)
                        {
                            int new_array_size = par_disc.Length + 1;
                            int new_array_index = new_array_size - 1;

                            Array.Resize(ref par_disc, new_array_size);

                            par_disc[new_array_index] = new ParameterDiscreteValue();
                            par_disc[new_array_index].Value = val;
                            pfield[param_name].CurrentValues.Add(par_disc[new_array_index]);
                        }
                }
            }
        }


  
    }
}
