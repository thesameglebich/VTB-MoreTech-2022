using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Response.User.Dtos;

namespace WebApi.Models.DTO
{
    /*
    public class UserCompanyDto
    {
        public UserDto User { get; set; }
        public CompanyDto Company { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsSatisfiedWith { get; set; }
        public static Expression<Func<DataAccessLayer.Models.UserCompany, UserCompanyDto>> Projection
        {
            get
            {
                return uc => new UserCompanyDto()
                {
                    Company = new CompanyDto()
                    {
                        Id = uc.Company.Id,
                        Name = uc.Company.Name
                    },
                    IsCurrent = uc.IsCurrent,
                    IsSatisfiedWith = uc.IsSatisfiedWith,
                    // ToDo: Fix to use UserDto Projection
                    User = new UserDto()
                    {
                        Email = uc.User.Email,
                        FullName = uc.User.FullName,
                        Id = uc.User.Id
                    }
                };
            }
        }
    }*/
}
