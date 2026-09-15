using DemoLibrary.Commands;
using DemoLibrary.Handles;
using DemoLibrary.Models;
using DemoLibrary.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IMediator _mediator;

        // Constructor injected IMediator instance
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<List<PersonModel>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<PersonModel> GetById(int id)
        {
            return await _mediator.Send(new GetPersonByIdQuery(id));
        }

        // POST api/<PersonController>
        [HttpPost]
        public async  Task<PersonModel> Post([FromBody] PersonModel newPerson)
        {
            return await _mediator.Send(new InsertPersonCommand(newPerson.FirstName, newPerson.LastName));
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<PersonModel> Put(int id, string firstName, string lastName)
        {
            return await _mediator.Send(new UpdatePersonCommandClass(id, firstName, lastName));
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<PersonModel> Delete(int id)
        {
            return await _mediator.Send(new DeletePersonCommandClass(id));
        }
    }
}
