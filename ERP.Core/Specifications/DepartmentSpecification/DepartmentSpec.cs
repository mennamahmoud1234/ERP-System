using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.DepartmentSpecification
{
    public class DepartmentSpec : BaseSpecifications<Department>
    {

        public DepartmentSpec() : base()
        {

            Includes.Add(D => D.JobPositions);
            Includes.Add(D => D.Employees);
            Includes.Add(D => D.ChildDepartment);
            Includes.Add(D => D.ParentDepartment);



        }


        public DepartmentSpec(int Id) : base(D => D.Id == Id)
        {

            Includes.Add(D => D.JobPositions);
            Includes.Add(D => D.Employees);
            Includes.Add(D => D.ChildDepartment);
            Includes.Add(D => D.ParentDepartment);

        }

        public DepartmentSpec(string name) : base(D => D.DepartmentName == name)
        {

            Includes.Add(D => D.JobPositions);
            Includes.Add(D => D.Employees);
            Includes.Add(D => D.ChildDepartment);
            Includes.Add(D => D.ParentDepartment);

        }

        public DepartmentSpec(string name, int id) : base(D => D.DepartmentName == name && D.ParentDepartmentId == id)
        {

            Includes.Add(D => D.JobPositions);
            Includes.Add(D => D.Employees);
            Includes.Add(D => D.ChildDepartment);
            Includes.Add(D => D.ParentDepartment);

        }

    }
}
