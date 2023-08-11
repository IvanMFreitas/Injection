using Injection.Entities;
using Injection.Entities.ViewModel;
using Injection.Services.Interface;
using Injection.Data.Repositories;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public async Task<string> GenerateJwtToken(string userEmail)
        {
            var person = await _personRepository.FindOneAsync(x => x.Email == userEmail);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9uUemxx8pb3i0Kw3ovb4V1k7bMuyAA9h"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var listClaims = new List<Claim>
            {
                new Claim(ClaimTypes.SerialNumber, person.Id.ToString()),
                new Claim(ClaimTypes.Email, person.Email),
                new Claim(ClaimTypes.Role, person.IsAdmin ? "admin" : "user")
            };

            var token = new JwtSecurityToken(
                issuer: "InjectionApi",
                audience: "InjectionApi",
                claims: listClaims,
                expires: DateTime.UtcNow.AddDays(1), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}