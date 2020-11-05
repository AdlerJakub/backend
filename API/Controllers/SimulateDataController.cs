using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Data.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SimulateDataController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public SimulateDataController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<RegisterDto>> Simulate()
        {
            RegisterDto registerDto = new RegisterDto()
            {
                Username = "jan.kowalski@email.pl",
                Password = "qwerty"
            };
            AccountController _accountController = new AccountController(_context, _tokenService);
            await _accountController.Register(registerDto);

            SavingObjectiveDto savingObjectiveDto = new SavingObjectiveDto()
            {
                username = "jan.kowalski@email.pl",
                savingObjectives = JsonSerializer.Deserialize<SavingObjective[]>("[{\"name\":\"Astra J OPC\",\"description\":\"Samoch\u00F3d sportowy w przyst\u0119pnych cenach. Silnik turbodo\u0142adowany benzynowy 2.0 280KM. Niestety wymaga paliwa 98.\",\"imagePath\":\"https://img.chceauto.pl/opel/astra/opel-astra-hatchback-3-drzwiowy-3178-27828_head.jpg\",\"incomes\":[{\"date\":\"2020-09-07\",\"amount\":5000},{\"date\":\"2020-09-09\",\"amount\":1000},{\"date\":\"2020-09-22\",\"amount\":1000},{\"date\":\"2020-09-30\",\"amount\":125},{\"date\":\"2020-10-14\",\"amount\":1278},{\"date\":\"2020-10-22\",\"amount\":25000},{\"date\":\"2020-11-02\",\"amount\":1000},{\"date\":\"2020-11-05\",\"amount\":500}]},{\"name\":\"Rewolwer czarnoprochowy\",\"description\":\"Rewolwer czarnoprochowy bezpozwoleniowy\",\"imagePath\":\"https://8.allegroimg.com/s1024/0cf9f0/13ee1b044bf4a5b5ff6531e7a298\",\"incomes\":[{\"date\":\"2020-10-22\",\"amount\":250},{\"date\":\"2020-10-29\",\"amount\":350},{\"date\":\"2020-11-02\",\"amount\":50},{\"date\":\"2020-11-05\",\"amount\":100}]}]")
            };
            
            SavingObjectivesController _savingObjectivesController = new SavingObjectivesController(_context);
            await _savingObjectivesController.Objectives(savingObjectiveDto);

            return registerDto;
        }
    }
}