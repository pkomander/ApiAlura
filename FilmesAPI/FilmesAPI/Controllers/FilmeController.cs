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

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeServices _filmeService;
        
        public FilmeController(FilmeServices filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readDto = _filmeService.AdicionaFilme(filmeDto);
            
            return CreatedAtAction(nameof(RecuperarFilmePorId), new { id = readDto.Id}, readDto);
        }

        [HttpGet]
        public IActionResult RecuperarFilmes([FromQuery] int? classificaoEtaria = null)
        {
            List<ReadFilmeDto> readDto = _filmeService.RecuperaFilmes(classificaoEtaria);

            if(readDto != null)
            {
                return Ok(readDto);
            }
            else
            {
                return NotFound();
            }
            //return _context.Filmes;
        }

        //Recuperando filme por id
        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            ReadFilmeDto readDto = _filmeService.RecuperarFilmePorId(id);
            
            if(readDto != null)
            {
                return Ok(readDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result updateDto = _filmeService.AtualizaFilme(filmeDto, id);
            
            if(updateDto.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Result deletaFilme = _filmeService.DeletaFilme(id);

            if (deletaFilme.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
