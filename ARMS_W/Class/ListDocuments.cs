using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS_W.Class
{
    public class ListDocuments
    {

        public List<string[]> DocList { get{ return _DOCLIST; } }
        private List<string[]> _DOCLIST;

        public ListDocuments() 
        { 
            _DOCLIST = new List<string[]>();
        }

        public int Add( string[] _columns ) 
        {
            try 
            {
                _DOCLIST.Add(_columns);
                return 0;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }



    }
}