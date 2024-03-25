using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutosRepository _repository;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(ProdutosRepository repository, ILogger<ProdutosController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public List<Produto> Get()
    {
        return _repository.GetProdutos();
    }

    // [HttpGet("PorCliente")]
    // public List<Produto> GetByCliente(int idCliente)
    // {
    //     return _repository.GetProdutosByCliente(idCliente);
    // }

    [HttpPost]
    public async Task<IActionResult> PostProduto([FromBody] Produto produto)
    {
        bool resultado = await _repository.PostProduto(produto);

        if (resultado)
            return Ok();
        else
            return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> PutProduto([FromBody] Produto produto)
    {
        bool resultado = await _repository.PutProduto(produto);

        if (resultado)
            return Ok();
        else
            return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> PutProduto(int id)
    {
        bool resultado = await _repository.DeleteProduto(id);

        if (resultado)
            return Ok();
        else
            return BadRequest();
    }
}