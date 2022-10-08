using System.Collections.Generic;
using System.Linq;
using WebApi.DataAccessLayer;
using WebApi.Models.CommonModels;
using WebApi.Models.DTO;

namespace WebApi.Models.Response.User
{
    public class UserIndexResponseModel
    {
        private const int pageSize = 10;
        
        public Result<List<UserBaseInfoResponseModel>> Init(DB ctx, int page = 1)
        {
            
            var users = PaginatedList<UserBaseInfoResponseModel>.Create(
                ctx.Users
                .Select(x => new UserBaseInfoResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    MiddleName = x.MiddleName,
                    Phone = x.Phone,
                    PublicKey = x.PublicKey,
                    Email = x.Email
                }), page, pageSize);

            return new Result<List<UserBaseInfoResponseModel>>(new List<UserBaseInfoResponseModel>());
        }
        
        public Result<List<UserBaseInfoResponseModel>> InitStudents(DB ctx)
        {
            var users = ctx.Users
                .Select(u => new UserBaseInfoResponseModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    MiddleName = u.MiddleName,
                    //YearOfEducation = u.YearOfEducation,
                    //UserCompanies = u.UserCompanies.AsQueryable().Select(UserCompanyDto.Projection).ToList()
                }).ToList();

            return new Result<List<UserBaseInfoResponseModel>>(users);
        }
    }
}
