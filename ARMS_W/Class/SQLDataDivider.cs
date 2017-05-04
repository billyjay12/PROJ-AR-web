using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ARMS_W.Class
{
    public class SQLDataDivider
    {
        private int viewlength;
        private int datacount;
        private int maxpagecount;
        private int pagecount;
        DataRow[,] datalist;
        
 
        public SQLDataDivider(int viewlength, int totaldatalength)
        {
            this.viewlength = viewlength;
            maxpagecount = ((int)(totaldatalength / viewlength)) + 1;
            datalist = new DataRow[maxpagecount, viewlength];
            datacount = 0;
            pagecount = 0;
        }

        public void addData(DataRow data)
        {
            if (datacount >= viewlength)
            {
                datacount = 0;
                pagecount++;
            }
            datalist[pagecount,datacount++] = data;
        }

        public int TotalPage { get { return pagecount + 1; } } 

        public DataRow[] getData(int page)
        {
            DataRow[] viewablerows = new DataRow[(page - 1 == pagecount ? datacount : viewlength)];

            for (int i = 0; i < (page-1 == pagecount ? datacount: viewlength); i++)
            {
                viewablerows[i] = datalist[page-1, i];
            }
            return viewablerows;
        }
    }
}