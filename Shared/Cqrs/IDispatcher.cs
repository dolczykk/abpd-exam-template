namespace Apbd.Shared.Cqrs;

public interface IDispatcher
{
    Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> request);
}