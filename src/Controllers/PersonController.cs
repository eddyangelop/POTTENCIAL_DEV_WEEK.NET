using Microsoft.AspNetCore.Mvc;
using src.Models;

using Microsoft.EntityFrameworkCore;
using src.Persistence;

namespace src.Controllers;

[ApiController]
[Route("[Controller]")]
public class PersonController : ControllerBase
{

  private DatabaseContext _context { get; set; }

  public PersonController(DatabaseContext context)
  {
    this._context = context;
  }

  [HttpGet]
  public List<Pessoa> Get()
  {
    //codigo comentado para servir como referência
    //Pessoa pessoa = new Pessoa("Eddy", 33, "123456789");
    //Contrato novoContrato = new Contrato("abc123", 50.46);
    //pessoa.contratos.Add(novoContrato);
    //return pessoa;

    return _context.Pessoas.Include(p => p.contratos).ToList();
  }

  [HttpPost]
  public Pessoa Post([FromBody] Pessoa pessoa)
  {
    _context.Pessoas.Add(pessoa);
    _context.SaveChanges();

    return pessoa;
  }

  [HttpPut("{id}")]
  public string Update([FromRoute] int id, [FromBody] Pessoa pessoa)
  {

    _context.Pessoas.Update(pessoa);
    _context.SaveChanges();

    return "Dados do id " + id + " atualizados";
  }

  [HttpDelete("{id}")]
  public string Delete([FromRoute] int id)
  {
    return "Deletado pessoa do Id " + id;

  }

}