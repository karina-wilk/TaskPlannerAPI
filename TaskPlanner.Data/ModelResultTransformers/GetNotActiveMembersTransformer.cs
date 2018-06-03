using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data.ModelResultTransformers
{
    class GetNotActiveMembersTransformer : NHibernate.Transform.IResultTransformer
    {
        public IList TransformList(IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var model = new FamilyMember();
            for (int i = 0; i < aliases.Length; i++)
            {
                var columnName = aliases[i];
                var value = tuple[i];
                switch (columnName)
                {
                    case "Id":
                        model.Id = (int)value;
                        break;
                    case "IsActive":
                        model.IsActive = (bool)value;
                        break;
                    case "FamilyId":
                        if(model.Family == null)
                        {
                            model.Family = new Family();
                        }
                        model.Family.Id = (int)value;
                        break;
                    case "FirstName":
                        model.FirstName = (string)value;
                        break;
                    case "LastName":
                        model.LastName = (string)value;
                        break;
                }
            }
            return model;
        }
    }
    
}
