using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MyClasses.CQRS
{
    public static class GetTodoById
    {
       public record  Query(int id) : IRequest<Response> { }

        public class Handler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public record Response(int id, string task, bool isCompleted);
    }
}
