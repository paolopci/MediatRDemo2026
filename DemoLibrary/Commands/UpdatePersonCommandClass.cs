using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Commands
{
    public record UpdatePersonCommandClass(int Id,string FirstName, string LastName) : IRequest<PersonModel>;

}
