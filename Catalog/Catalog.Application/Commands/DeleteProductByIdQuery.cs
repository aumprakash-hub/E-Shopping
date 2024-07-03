using MediatR;

namespace Catalog.Application.Commands;

public class DeleteProductByIdQuery: IRequest<bool>
{
    public string Id { get; set; }

    public DeleteProductByIdQuery(string id)
    {
        Id = id;
    }
}