using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;
using ARMS_W.SkelClass;

namespace ARMS_W.UserDefineFunctions
{
    public class Objective
    {
        public static List<page_param.ObjectiveHdr> getObjectives()
        {
            var DATABASE = new Models.ARMSTestEntities();
            var res = new List<page_param.ObjectiveHdr>();
            
            try
            {

                var qry = (from a in DATABASE.ObjectiveHdrs
                           select a);


                foreach (var itm in qry)
                {

                    var qry1 = (from a in DATABASE.Objectives
                                where a.objectiveCode == itm.objectiveCode
                                select a);

                    var objectives_dtls = new List<page_param.ObjectiveHdr.Objectives>();
                    foreach (var itm1 in qry1)
                    {
                        objectives_dtls.Add(new page_param.ObjectiveHdr.Objectives()
                        {
                            objectiveCode = itm1.objectiveCode,
                            FieldName = itm1.FieldName,
                            isUsed = itm1.isUsed == "Y" ? true : false
                        });
                    }

                    res.Add(new page_param.ObjectiveHdr()
                    {
                        objectiveCode = itm.objectiveCode,
                        objectiveDesc = itm.objectiveDesc,
                        objective_list = objectives_dtls
                    });

                }


            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            DATABASE.Dispose();

            return res;
        }

        public static List<page_param.ObjectiveHdr.Objectives> getListObjectives()
        {
            var DATABASE = new Models.ARMSTestEntities();

            var res = new List<page_param.ObjectiveHdr.Objectives>();

            var qry = (from a in DATABASE.Objectives
                        select a);

            foreach (var itm in qry)
            {
                res.Add(new page_param.ObjectiveHdr.Objectives()
                {
                    objectiveCode = itm.objectiveCode,
                    FieldName = itm.FieldName,
                    isUsed = itm.isUsed == "Y" ? true : false
                });
            }

            return res;
        }
    }
}