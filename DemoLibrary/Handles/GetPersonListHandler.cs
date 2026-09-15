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

        public async Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_data.GetPeople());
        }
    }
}
