using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Linq;

namespace FilmesAPI.Services
{
    public class EnderecoServices
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoServices(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AdicionaEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public ReadEnderecoDto RecuperaEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

                return enderecoDto;
            }
            else
            {
                return null;
            }
        }

        public Result AtualizaFilme(UpdateEnderecoDto enderecoDto, int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return Results.Fail("Filme não encontrado");
            }
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return Results.Ok();
        }

        public Result DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return Results.Fail("Filme não encontrado");
            }
            _context.Remove(endereco);
            _context.SaveChanges();
            return Results.Ok();
        }
    }
}
