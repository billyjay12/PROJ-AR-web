using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ARMS_W.Class;

namespace ARMS_W.Class
{
    public class _Roles 
    {
        private string _Position;
        public string Position { get { return _Position; } }

        private string _PositionName;
        public string PositionName { get { return _PositionName; } }

        public IList<string> Region;

        public IList<string> Channel;

        public IList<string> ChannelGroup;

        public IList<string> Area;

        public IList<string> Brand;

        public IList<string> Accounts;

        public _Roles(string _RoleCode, string _RoleName) 
        {
            this._Position = _RoleCode;
            this._PositionName = _RoleName;

            Region = new List<string>();
            Channel = new List<string>();
            ChannelGroup = new List<string>();
            Area = new List<string>();
            Brand = new List<string>();
            Accounts = new List<string>();
        }

        public bool HasRegionOf(string RegionName) 
        {
            foreach (string region_name in this.Region) 
            {
                if (RegionName == region_name) return true;
            }
            return false;
        }

        public bool HasChannelGroupOf(string ChannelGroupName)
        {
            foreach (string channel_grp_name in this.ChannelGroup)
            {
                if (ChannelGroupName == channel_grp_name) return true;
            }
            return false;
        }

        public bool HasAreaOf(string AreaName)
        {
            foreach (string area_name in this.Area)
            {
                if (area_name == AreaName) return true;
            }
            return false;
        }

        public bool HasBrandOf(string BrandName) 
        {
            foreach (string brand_name in this.Brand)
            {
                if (brand_name == BrandName) return true;
            }
            return false;
        }

        public bool HasAccountsOf(string Acctcodes)
        {
            foreach (string acctcode in this.Accounts)
            {
                if (acctcode == Acctcodes) return true;
            }
            return false;
        }

    }
}