using AutoMapper;
using Businesslogic.Models;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businesslogic.UserService
{
    public class UserService: IUserService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly Mapper _autoMapper;

        public UserService(IApplicationDbContext applicationDbContex)
        {
            _applicationDbContext = applicationDbContex;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
               
            });
            _autoMapper = new Mapper(mapperConfig);
        }

        public string AddUser(UserDto userDto)
        {
            var resultSearch = _applicationDbContext.Users.FirstOrDefault(u => u.Login == userDto.Login);

            if (resultSearch == null)
            {
                var user = new User
                {
                    Login = userDto.Login,
                    Password = userDto.Password                    
                };

                _applicationDbContext.Users.Add(user);
                _applicationDbContext.SaveChanges();
                return user.Id;
            }
            return "Login is not unique";
        }

        public List<UserDto> GetAllUsers()
        {
            var Users = _applicationDbContext?.Users.ToList();
            var resultList = _autoMapper.Map<List<User>, List<UserDto>>(Users);
            return resultList;
        }

        public UserDto GetUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
