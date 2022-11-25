using AutoMapper;
using MediatR;
using MyGymApp.Aplication.Common.Enums;
using MyGymApp.Aplication.Common.Interfaces;
using MyGymApp.Domain.Common.BaseEntity;

namespace MyGymApp.Aplication.Common.GenericHandler;

public class BaseCommandHandler<TCommand, TEntity, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : BaseCommand<TResponse>
    where TEntity : BaseEntity
    where TResponse : class
{
    protected readonly IBaseCrudService<TEntity> _baseCrudService;
    protected readonly IMapper _mapper;

    public BaseCommandHandler(IBaseCrudService<TEntity> baseCrudService, IMapper mapper)
    {
        _baseCrudService = baseCrudService;
        _mapper = mapper;
    }

    public virtual async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(request);

        var operationResult = request.ActionType switch
        {
            ActionTypes.Create => await _baseCrudService.Add(entity),
            ActionTypes.Update => await _baseCrudService.Update(entity),
            ActionTypes.Delete => await _baseCrudService.Delete(entity.Id),
            _ => throw new Exception($"Action Type '{request.ActionType}' is not supported, you can only Create, Update or Delete")
        };

        return _mapper.Map<TResponse>(operationResult);
    }
}