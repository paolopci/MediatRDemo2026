using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using DemoLibrary.Queries;
using MediatR;

namespace DemoLibrary.Handles
{
    public class GetPersonListHandler:IRequestHandler<GetPersonListQuery,List<PersonModel>>
    {
        private readonly IDemoDataAccess _data;

        public GetPersonListHandler(IDemoDataAccess data)
        {
            _data = data;
        }

        public Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.GetPeople());
        }
    }
}
