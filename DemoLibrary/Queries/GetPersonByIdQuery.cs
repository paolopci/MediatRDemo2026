using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Queries
{
    public record GetPersonByIdQuery(int id) : IRequest<PersonModel>;


    //public class GetPersonByIdQuery : IRequest<PersonModel>
    //{
    //    public int Id { get; set; }

    //    public GetPersonByIdQuery(int id)
    //    {
    //        Id = id;
    //    }
    //}
}
