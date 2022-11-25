using System.Text.Json.Serialization;
using MediatR;
using MyGymApp.Aplication.Common.Enums;

namespace MyGymApp.Aplication.Common.GenericHandler;

public class BaseCommand<TResponse> : IRequest<TResponse> where TResponse : class
{
    [JsonIgnore]
    public virtual ActionTypes ActionType { get; set; }
    public int Id { get; set; }
}