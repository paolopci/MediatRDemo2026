using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Commands
{
    public record DeletePersonCommandClass(int Id) : IRequest<PersonModel>;


}
