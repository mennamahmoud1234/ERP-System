using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications
{
    public interface ISpecifications<T> where T : class
    {
        //Where
        public Expression<Func<T, bool>> Criteria { get; set; }

        //Includes
        public List<Expression<Func<T, object>>> Includes { get; set; }


        //ThenIncludes
         public List<string> IncludeStrings { get; }

        //OrderByAscending
        public Expression<Func<T, object>> OrderBy { get; set; }

        //OrderByDescending
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        //Skip
        public int Skip { get; set; }
        //Take
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
