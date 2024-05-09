using csharpCamadas.API.DAL;
using csharpCamadas.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharpCamadas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MostoristaController : ControllerBase
    {
        private readonly csharpCamadasContext _context;

        public MostoristaController(csharpCamadasContext context)
        {
            _context = context;
        }

        [HttpGet("/Motorista")]
        public async Task<IActionResult> PegarTodosMotoristas()
        {
            try
            {
                var dados = await _context.Set<Motoristum>().ToListAsync();

                List<Motoristum> list = new List<Motoristum>();

                foreach (var item in dados)
                {
                    Motoristum Model = new Motoristum
                    {
                        MotId = item.MotId,
                        MotIdade = item.MotIdade,
                        MotNome = item.MotNome,
                        VeiId = item.VeiId,
                        Vei = await _context.Set<Veiculo>().FindAsync(item.VeiId)
                    };
                    list.Add(Model);
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex);
            }
        }


        [HttpGet("/Motorista/{id}")]
        public async Task<IActionResult> PegarMotoristaPorId(int id)
        {
            try
            {
                return Ok(await _context.Set<Motoristum>().FindAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorrou o um erro na busca error: " + ex);
            }
        }

        [HttpPost("/Motorista/Create")]
        public async Task<IActionResult> AdicionarMotorista(Motoristum Model)
        {
            try
            {
                await _context.AddAsync(Model);
                await _context.SaveChangesAsync();
                return Ok("Veiculo Adicionado");
            }
            catch (Exception)
            {
                return BadRequest("Veiculo Não Adicionado");
            }
        }

        [HttpPut("/Motorista/Atualizar")]
        public async Task<IActionResult> AtualizarMotorista(Motoristum Model)
        {
            try
            {
                _context.Update(Model);
                await _context.SaveChangesAsync();
                return Ok("Veiculo Atualizao");
            }
            catch (Exception)
            {
                return BadRequest("Veiculo Não Atualizado");
            }
        }


        [HttpDelete("/Motorista/Remove/{id}")]
        public async Task<IActionResult> ApagarMotorista(int id)
        {
            try
            {
                var Model = await _context.Set<Motoristum>().FindAsync(id);
                _context.Remove(Model);
                await _context.SaveChangesAsync();
                return Ok("Veiculo Apagado");
            }
            catch (Exception)
            {
                return BadRequest("Veiculo Não Apagado");
            }
        }
    }
}
