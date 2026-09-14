using System;
using System.Collections.Generic;
using System.Text;
using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using DemoLibrary.Queries;
using MediatR;

namespace DemoLibrary.Handles
{
    public class GetPersonByIdHandler:IRequestHandler<GetPersonByIdQuery,PersonModel>
    {
        private readonly IMediator _mediator;

        public GetPersonByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery());
            return results.FirstOrDefault(p => p.Id == request.id);
        }
    }
}
