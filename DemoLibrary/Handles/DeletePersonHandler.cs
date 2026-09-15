using System;
using System.Collections.Generic;
using System.Text;
using DemoLibrary.Commands;
using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Handles
{
    public class DeletePersonHandler : IRequestHandler<DeletePersonCommandClass, PersonModel>
    {
        private IDemoDataAccess _data;

        public DeletePersonHandler(IDemoDataAccess data)
        {
            _data = data;
        }

        public Task<PersonModel> Handle(DeletePersonCommandClass request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.DeletePerson(request.Id));
        }
    }
}
