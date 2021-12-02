using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using FluentAssertions;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _outPut;
        private readonly string _descricao;
        private readonly string _nome;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly int _cargaHoraria;
        private readonly double _valor;

        public CursoTest(ITestOutputHelper outPut)
        {
            _outPut = outPut;
            _outPut.WriteLine("Construtor sendo executado");

            var fake = new Faker();

            _nome = fake.Random.Word();
            _publicoAlvo = PublicoAlvo.Estudante;
            _cargaHoraria = fake.Random.Int(1, 1000);
            _valor = fake.Random.Double(1, 1000);
            _descricao = fake.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _outPut.WriteLine("Dispose sendo executado");
        }

        [Fact(DisplayName = "DeveCriarCurso")]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = _nome,
                PublicoAlvo = _publicoAlvo,
                CargaHoraria = _cargaHoraria,
                Valor = _valor,
                Descricao = _descricao
            };

            //act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //assert
            curso.Should().BeEquivalentTo(cursoEsperado);
        }

        [Theory(DisplayName = "NaoDeveCurtoTerNomeInvalido")]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCurtoTerNomeInvalido(string nomeInvalido)
        {
            //act
            Action act = () =>
            {
                CursoBuilder.Novo().ComNome(nomeInvalido).Build();
            };

            //assert
            act.Should().Throw<ArgumentException>().WithMessage("Nome Inválido");
        }

        [Theory(DisplayName = "NaoDeveCurtoTerCargaHorariaMenorQue1")]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCurtoTerCargaHorariaMenorQue1(int cargaHorariaIinvalida)
        {
            //act
            Action act = () =>
            {
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaIinvalida).Build();
            };

            //assert
            act.Should().Throw<ArgumentException>().WithMessage("Carga horária Inválida");
        }

        [Theory(DisplayName = "NaoDeveCurtoTerValorInvalido")]
        [InlineData(0)]
        [InlineData(-1.0)]
        [InlineData(-100.00)]
        public void NaoDeveCurtoTerValorInvalido(double valorInvalido)
        {
            //act
            Action act = () =>
            {
                CursoBuilder.Novo().ComValor(valorInvalido).Build();
            };

            //assert
            act.Should().Throw<ArgumentException>().WithMessage("Valor Inválido");
        }
    }
}