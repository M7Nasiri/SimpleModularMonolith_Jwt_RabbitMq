using Common.Application;
using Common.Application.SecurityUtil;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Services;
using RabbitMQ.Client;


namespace Application.Users.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IEventBus _eventBus;

    public RegisterUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IEventBus eventBus)
    {
        _repository = repository;
        _domainService = domainService;
        _eventBus = eventBus;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.RegisterUser(request.PhoneNumber, Sha256Hasher.Hash(request.Password), _domainService);

        _repository.Add(user);
        await _repository.Save();

        await _eventBus.PublishAsync(new UserRegistered()
        {
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Family = user.Family,
            Name = user.Name,
            Password = user.Password,
            Id = user.Id,
            CreationDate = user.CreationDate
        }, null, Exchanges.UserTopicExchange, ExchangeType.Topic, "user.registered");

        return OperationResult.Success();
    }
}