using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class FilmeServices
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeServices(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> RecuperaFilmes(int? classificaoEtaria)
        {
            List<Filme> filmes = new List<Filme>();

            if (classificaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context.Filmes.Where(x => x.ClassificacaoEtaria <= classificaoEtaria).ToList();
            }

            if (filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readDto;
            }

            return null;
        }

        public ReadFilmeDto RecuperarFilmePorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return filmeDto;
            }
            else
            {
                return null;
            }
        }

        public Result AtualizaFilme(UpdateFilmeDto filmeDto, int id)
        {
            Filme filme = _context.Filmes.Where(x => x.Id == id).FirstOrDefault();
            if (filme == null)
            {
                return Results.Fail("Filme não encontrado");
            }
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return Results.Ok();
        }

        public Result DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.Where(x => x.Id == id).FirstOrDefault();
            if (filme == null)
            {
                return Results.Fail("Filme não encontrado");
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return Results.Ok();
        }
    }
}
