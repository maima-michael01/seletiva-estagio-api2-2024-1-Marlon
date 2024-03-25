using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class HistoricoController : ControllerBase
{
    private readonly HistoricoRepository _repository;
    private readonly ILogger<HistoricoController> _logger;

    public HistoricoController(HistoricoRepository repository, ILogger<HistoricoController> logger)
    {
        _repository = repository;
        _logger = logger;
    }   

    [HttpGet]
    public List<Historico> GetProdutos(int idCliente)
    {
        return _repository.GetHistoricosAsync(idCliente);
    }

    [HttpPost]
    public async Task<IActionResult> PostLoadHistorico([FromBody] Cliente cliente)
    {
        bool resultado = await _repository.LoadDadosUtilizacaoPorProdutoAsync(cliente);

        if (resultado)
            return Ok();
        else
            return BadRequest();
    }
}