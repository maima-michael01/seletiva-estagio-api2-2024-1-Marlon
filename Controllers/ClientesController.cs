using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClientesRepository _repository;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(ClientesRepository repository, ILogger<ClientesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<List<Cliente>> GetClientes()
    {
        return await _repository.GetClientesAsync();
    }
}