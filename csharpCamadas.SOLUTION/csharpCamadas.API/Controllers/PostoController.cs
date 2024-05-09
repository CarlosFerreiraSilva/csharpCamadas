using csharpCamadas.API.DAL;
using csharpCamadas.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharpCamadas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostoController : ControllerBase
    {
        protected readonly csharpCamadasContext _context;

        public PostoController(csharpCamadasContext context)
        {
            _context = context;
        }

        [HttpGet("/Posto")]
        public async Task<IActionResult> PegarTodosOsPostos()
        {
            try
            {
                var dados = await _context.Set<TiposDeCombustivel>().ToListAsync();
                var dadosPosto = await _context.Set<Posto>().ToListAsync();
                List<Posto> lista = new List<Posto>();

                foreach (var item in dadosPosto)
                {
                    Posto Model = new Posto
                    {
                        PosId = item.PosId,
                        PosNome = item.PosNome,
                        PosCidade = item.PosCidade,
                        PosEndereco = item.PosEndereco,
                        TiposDeCombustivels = dados.Where(x => x.PosId == item.PosId).ToList(),
                    };
                    lista.Add(Model);
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest("erro: " + ex);
            }
        }


        [HttpGet("/Posto/{id}")]
        public async Task<IActionResult> PegarPostoPorId(int id)
        {
            try
            {
                return Ok(await _context.Set<Posto>().FindAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorrou o um erro na busca error: " + ex);
            }
        }

        [HttpPost("/Posto/Create")]
        public async Task<IActionResult> AdicionarPosto(Posto Model)
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

        [HttpPut("/Posto/Atualizar")]
        public async Task<IActionResult> AtualizarPosto(Posto Model)
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


        [HttpDelete("/Posto/Remove/{id}")]
        public async Task<IActionResult> ApagarPosto(int id)
        {
            try
            {
                var Model = await _context.Set<Posto>().FindAsync(id);
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
