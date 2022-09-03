using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

using src.Models;
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
  public ActionResult<List<Pessoa>> Get()
  {
    // OK - 200 , NotContent - 204
    //codigo comentado para servir como referência
    //Pessoa pessoa = new Pessoa("Eddy", 33, "123456789");
    //Contrato novoContrato = new Contrato("abc123", 50.46);
    //pessoa.contratos.Add(novoContrato);
    //return pessoa;

    //codigo padrão do Get
    //var result = _context.Pessoas.Include(p => p.contratos).ToList();

    // if (!result.Any())
    // {
    //   return NoContent();
    // }

    //codigo mais profissional sem utilização das chaves
    var result = _context.Pessoas.Include(p => p.contratos).ToList();

    if (!result.Any()) return NoContent();

    return Ok(result);
  }

  [HttpPost]
  public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
  {

    try
    {
      _context.Pessoas.Add(pessoa);
      _context.SaveChanges();
    }
    catch (System.Exception)
    {

      return BadRequest();
    }



    return Created("criado", pessoa);
  }

  [HttpPut("{id}")]
  public ActionResult<object> Update(
    [FromRoute] int id,
    [FromBody] Pessoa pessoa
    )
  {

    var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

    if (result is null)
    {
      return NotFound(new
      {
        msg = "Registro não encontrado",
        status = HttpStatusCode.NotFound
      });
    }

    try
    {
      _context.Pessoas.Update(pessoa);
      _context.SaveChanges();
    }
    catch (System.Exception)
    {

      return BadRequest(new
      {
        msg = "Erro ao enviar a solicitação de atualização do Id "
       + id + " especifico",
        status = HttpStatusCode.OK
      });
    }

    return Ok(new
    {
      msg = "Dados do id " + id + " atualizados",
      status = HttpStatusCode.OK

    });
  }

  [HttpDelete("{id}")]
  // public string Delete([FromRoute] int id)
  // {
  //   var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

  //   _context.Pessoas.Remove(result);
  //   _context.SaveChanges();

  //   return "Deletado pessoa do Id " + id;

  // }

  public ActionResult<Object> Delete([FromRoute] int id)
  {
    var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

    if (result is null)
    {
      return BadRequest(new
      {
        msg = "Conteúdo inexistente, solicitação inválida",
        status = HttpStatusCode.BadRequest
      });
    }
    _context.Pessoas.Remove(result);
    _context.SaveChanges();

    return Ok(new
    {
      msg = "Deletado pessoa do Id " + id,
      status = HttpStatusCode.OK
    });

  }

  // public IActionResult Delete([FromRoute] int id)
  // {
  //    return NotFound();
  // }


}