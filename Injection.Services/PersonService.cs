using Injection.Entities;
using Injection.Entities.ViewModel;
using Injection.Services.Interface;
using Injection.Data.Repositories;
using AutoMapper;

namespace Injection.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;
        public PersonService(IRepository<Person> personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonViewModel> GetByIdAsync(Guid id){
            var person = await _personRepository.GetByIdAsync(id);
            return _mapper.Map<PersonViewModel>(person);
        }

    }
}