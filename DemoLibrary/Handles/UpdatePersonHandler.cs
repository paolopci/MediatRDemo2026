using DemoLibrary.Commands;
using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Handles
{
    public class UpdatePersonHandler:IRequestHandler<UpdatePersonCommandClass,PersonModel>
    {
        private readonly IDemoDataAccess _data;

        public UpdatePersonHandler(IDemoDataAccess data)
        {
            _data = data;
        }

        public Task<PersonModel> Handle(UpdatePersonCommandClass request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.UpdatePerson(request.Id,request.FirstName, request.LastName));
        }
    }
}
