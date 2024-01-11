using AutoMapper;
using CommentsApp.Entities;
using CommentsApp.Models;

namespace CommentsApp.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UsersDTO>();
            CreateMap<UsersDTO, User>();

        }
    }
}
