using RecruitmentTask.Domain.Abstractions;
using MediatR;

namespace RecruitmentTask.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
