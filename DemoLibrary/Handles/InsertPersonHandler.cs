using DemoLibrary.Commands;
using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Handles
{
    public class InsertPersonHandler:IRequestHandler<InsertPersonCommand,PersonModel>
    {
        private IDemoDataAccess _data;

        public InsertPersonHandler(IDemoDataAccess data)
        {
            _data = data;
        }

        public Task<PersonModel> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.InsertPerson(request.FirstName, request.LastName));
        }
    }
}
