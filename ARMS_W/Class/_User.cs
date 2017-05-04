using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.Class
{
    public class _User
    {

        private string _EmailAdd;
        public string EmailAdd { get { return _EmailAdd; } }

        private string _EmployeeIdNo;
        public string EmployeeIdNo { get { return _EmployeeIdNo; } }

        private string _UserName;
        public string UserName { get { return _UserName; } }

        private string _SlpName;
        public string SlpName { get { return _SlpName; } }

        private IList<_Roles> __Roles;
        public IList<_Roles> Roles { get { return __Roles; } }

        public _User(string username) {

            if (username == "") 
            {
                throw new Exception("No Username!");
            }

            __Roles = new List<_Roles>();

            DataRowCollection mtb_row = SqlDbHelper.GetData_dr("userHeader", "username='" + username + "'");

            // username
            this._UserName = username;

            foreach (DataRow item in mtb_row) {

                // Email Add
                this._EmailAdd = item["emailAdd"].ToString();

                // Employee ID
                this._EmployeeIdNo = item["empIdNo"].ToString();

            }

            string strQuery = "" +
                "select c.roleCode as 'position', c.rolename as 'positionname', b.lastname + ', ' + b.firstname as 'acctoffcrname' from apprvrDesig a, userheader b , apprvrRole c " +
                "where a.counterid=b.counterid and c.roleid=a.roleid and b.username='" + username + "' " +
                "group by c.rolecode, c.rolename, b.lastname + ', ' + b.firstname " +
                "";
            DataTable tbRoles = SqlDbHelper.getDataDT(strQuery);
            
            foreach (DataRow itm in tbRoles.Rows) 
            { 
                // SLPNAME
                this._SlpName = itm["acctoffcrname"].ToString();

                // ROLES
                _Roles tmpRole = new _Roles(itm["position"].ToString(), itm["positionname"].ToString());

                // REGION
                DataTable uRegions = GetRegions(username, tmpRole.Position);
                foreach (DataRow sItem in uRegions.Rows) 
                {
                    tmpRole.Region.Add(sItem["region"].ToString());
                }

                // CHANNEL
                DataTable uChannels = GetChannels(username, tmpRole.Position);
                foreach (DataRow sItem in uChannels.Rows)
                {
                    tmpRole.Channel.Add(sItem["channel"].ToString());
                }

                // CHANNEL GROUP
                DataTable uChannelGroup = GetChannelGroup(username, tmpRole.Position);
                foreach (DataRow sItem in uChannelGroup.Rows)
                {
                    tmpRole.ChannelGroup.Add(sItem["grp_name"].ToString());
                }

                // AREA
                DataTable uAreas = GetAreas(username, tmpRole.Position);
                foreach (DataRow sItem in uAreas.Rows)
                {
                    tmpRole.Area.Add(sItem["area"].ToString());
                }

                // BRAND
                DataTable uBrands = GetBrands(username, tmpRole.Position);
                foreach (DataRow sItem in uBrands.Rows)
                {
                    tmpRole.Brand.Add(sItem["brand"].ToString());
                }

                DataTable uAccounts = IsUnderSO(username, tmpRole.Position);
                foreach (DataRow sItem in uAccounts.Rows)
                {
                    tmpRole.Accounts.Add(sItem["acctcode"].ToString());
                }

                // this.Roles.Add(tmpRole);
                this.__Roles.Add(tmpRole);

            }
            
        }

        private DataTable GetRegions(string username, string rolecode)
        {
            string strQuery = "" +
                "select  " +
                "case when a.branch = 'LZ' then 'LUZON' when a.branch = 'VM' then 'VISMIN' else '' end as 'region' " +
                "from apprvrDesig a, userheader b , apprvrRole c " +
                "where a.counterid=b.counterid and c.roleid=a.roleid  " +
                "and b.username='" + username + "' and c.rolecode='" + rolecode + "' group by a.branch " +
                "";

            DataTable sList = SqlDbHelper.getDataDT(strQuery);

            return sList;
        }

        private DataTable GetChannels(string username, string rolecode)
        {
            string strQuery = "" +
                "select  " +
                "a.channel " +
                "from apprvrDesig a, userheader b , apprvrRole c " +
                "where a.counterid=b.counterid and c.roleid=a.roleid  " +
                "and b.username='" + username + "' and c.rolecode='" + rolecode + "' and a.channel is not null and a.channel != '' group by a.channel " +
                "";

            DataTable sList = SqlDbHelper.getDataDT(strQuery);

            return sList;
        }

        private DataTable GetChannelGroup(string username, string rolecode) 
        {
            string strQuery = @"
                select 
                d.grp_name 
                from apprvrDesig a, userheader b , apprvrRole c , channelgroup d 
                where a.counterid=b.counterid and c.roleid=a.roleid and d.channel=a.channel 
                and b.username='" + username + @"' and c.rolecode='" + rolecode + @"' and a.channel is not null and a.channel != '' group by d.grp_name 
                ";

            DataTable sList = SqlDbHelper.getDataDT(strQuery);

            return sList;
        }

        private DataTable GetAreas(string username, string rolecode)
        {
            string strQuery = @"
                select 
                a.area 
                from apprvrDesig a, userheader b , apprvrRole c 
                where a.counterid=b.counterid and c.roleid=a.roleid 
                and b.username='" + username + @"' and c.rolecode='" + rolecode + @"' and a.area is not null and a.area != '' group by a.area 
                ";

            DataTable sList = SqlDbHelper.getDataDT(strQuery);

            return sList;
        }

        private DataTable GetBrands(string username, string rolecode)
        {
            string strQuery = @"
                select a.brand 
                from apprvrDesig a, userheader b , apprvrRole c 
                where a.counterid=b.counterid and c.roleid=a.roleid 
                and b.username='" + username + @"' and c.rolecode='" + rolecode + @"'  and a.brand != '' group by a.brand 
                ";

            DataTable sList = SqlDbHelper.getDataDT(strQuery);

            return sList;
        }

        private DataTable IsUnderSO(string username, string rolecode)
        {
            string strQuery = @"
                select a.sapacctcode 'acctcode' from customerheader a 
                inner join SAPSERVER.MATIMCO.dbo.oslp b 
                on b.slpname = a.acctOffcr collate SQL_Latin1_General_CP1_CI_AS
                inner join userheader c on cast(c.slpcode as int) = b.slpcode 
                inner join apprvrdesig d on c.counterid = d.counterid
                inner join apprvrrole e on e.roleid = d.roleid
                where a.status = '1000' and c.username = '" + username + @"' and e.rolecode = '" + rolecode + @"'
                ";

            DataTable sList = SqlDbHelper.getDataDT(strQuery);

            return sList;
        }

        public int HasPositionOf(string position)
        {
            int icounter = 0;
            foreach(_Roles rls in this.Roles)
            {
                if (rls.Position.ToUpper() == position.ToUpper()) 
                {
                    return icounter;
                }
                icounter++;
            }

            // -1 MEANS NOT FOUND
            return -1;
        }

        public List<string> GetRoleList()
        { 
            List<string> roleList = new List<string>();
            foreach(_Roles rls in this.Roles)
            {
                roleList.Add(rls.Position);
            }

            return roleList;
        }

        public bool HasPositionsOf(string[] positions) 
        {
            string str_positions = string.Join(",", positions).ToUpper();
            
            foreach (_Roles rls in this.Roles)
            {
                if (str_positions.IndexOf(rls.Position) > -1) return true;
            }
            return false;
        }

        public bool HasPosAndRegionsOf(string[] positions, string region) 
        {
            string str_positions = string.Join(",", positions).ToUpper();
            foreach (_Roles rls in this.Roles)
            {
                if (str_positions.IndexOf(rls.Position) > -1 && rls.Region.IndexOf(region.ToUpper()) > -1 ) return true;
            }
            return false;
        }

        public bool HasPosAndChannelsOf(string[] positions, string channel)
        {
            string str_positions = string.Join(",", positions).ToUpper();
            foreach (_Roles rls in this.Roles)
            {
                if (str_positions.IndexOf(rls.Position) > -1 && rls.Channel.IndexOf(channel.ToUpper()) > -1) return true;
            }
            return false;
        }

        public bool HasPosAndChannelGroupOf(string[] positions, string channel_group)
        {
            string str_positions = string.Join(",", positions).ToUpper();
            foreach (_Roles rls in this.Roles)
            {
                if (str_positions.IndexOf(rls.Position) > -1 && rls.ChannelGroup.IndexOf(channel_group.ToUpper()) > -1) return true;
            }
            return false;
        }

        public bool HasPosAndAreasOf(string[] positions, string area)
        {
            string str_positions = string.Join(",", positions).ToUpper();
            foreach (_Roles rls in this.Roles)
            {
                if (str_positions.IndexOf(rls.Position) > -1 && rls.Area.IndexOf(area.ToUpper()) > -1) return true;
            }
            return false;
        }

        public int HasPositionNameOf(string positionName)
        {
            int icounter = 0;
            foreach (_Roles rls in this.Roles)
            {
                if (rls.PositionName.ToUpper() == positionName.ToUpper())
                {
                    return icounter;
                }
                icounter++;
            }

            // -1 MEANS NOT FOUND
            return -1;
        }

        public bool HasRegionOf(string RoleCode, string region) 
        {
            foreach (_Roles rls in this.Roles)
            {
                if (rls.Position.ToUpper() == RoleCode.ToUpper() && rls.Region.IndexOf(region) != -1) 
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasChannelOf(string RoleCode, string channel)
        {
            foreach (_Roles rls in this.Roles)
            {
                if (rls.Position.ToUpper() == RoleCode.ToUpper() && rls.Channel.IndexOf(channel) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasAreaOf(string RoleCode, string area)
        {
            foreach (_Roles rls in this.Roles)
            {
                if (rls.Position.ToUpper() == RoleCode.ToUpper() && rls.Area.IndexOf(area) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasAccountsOf(string RoleCode, string acctcode)
        {
            foreach (_Roles rls in this.Roles)
            {
                if (rls.Position.ToUpper() == RoleCode.ToUpper() && rls.Accounts.IndexOf(acctcode) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasBrandOf(string RoleCode, string brand) 
        {
            

            return false;
        }



    }

}
