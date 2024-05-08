using RecruitmentTask.Domain.Abstractions;
using MediatR;

namespace RecruitmentTask.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}
