using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.UserAgg.Services
{
    public interface IUserDomainService
    {
        bool IsEmailExist(string email);
        bool IsPhoneNumberExist(string phoneNumber);
    }
}
