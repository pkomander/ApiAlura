using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class CinemaServices
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaServices(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperaCinema(string nomeDoFilme)
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();

            if (cinemas == null)
            {
                return null;
            }

            if (!String.IsNullOrEmpty(nomeDoFilme))
            {
                IEnumerable<Cinema> query = from cinema in cinemas
                where cinema.Sessoes.Any(sessao => sessao.Filme.Titulo == nomeDoFilme) select cinema;
                cinemas = query.ToList();
            }

            List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            return readDto;
        }

        //public ReadEnderecoDto RecuperaEnderecosPorId(int id)
        //{
        //    Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        //    if (endereco != null)
        //    {
        //        ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

        //        return enderecoDto;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
