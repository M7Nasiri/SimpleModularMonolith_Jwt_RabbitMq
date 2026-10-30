using System;
using System.Collections.Generic;
using System.Text;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Services;

namespace Application.Users;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _repository;

    public UserDomainService(IUserRepository repository)
    {
        _repository = repository;
    }

    public bool IsEmailExist(string email)
    {
        return _repository.Exists(r => r.Email == email);
    }

    public bool IsPhoneNumberExist(string phoneNumber)
    {
        return _repository.Exists(r => r.PhoneNumber == phoneNumber);
    }

}
