using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private EnderecoServices _enderecoService;

        public EnderecoController(EnderecoServices enderecoServices)
        {
            _enderecoService = enderecoServices;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto readDto = _enderecoService.AdicionaEndereco(enderecoDto);
            
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = readDto.Id }, readDto);
        }

        //[HttpGet]
        //public IEnumerable<Endereco> RecuperaEnderecos()
        //{
        //    ReadEnderecoDto readDto = _enderecoService.RecuperaEnderecos();
        //    return _context.Enderecos;
        //}

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            ReadEnderecoDto readDto = _enderecoService.RecuperaEnderecosPorId(id);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Result updateDto = _enderecoService.AtualizaFilme(enderecoDto, id);

            if(updateDto.IsFailed)
            {
                return NotFound();
            }
            
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Result deletaEndereco = _enderecoService.DeletaEndereco(id);

            if (deletaEndereco.IsFailed)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}