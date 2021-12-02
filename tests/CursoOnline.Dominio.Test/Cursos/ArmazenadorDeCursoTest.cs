using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Word(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Int(1, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact()]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(p =>
                p.Adicionar(It.Is<Curso>(p => p.Nome == _cursoDto.Nome && p.Descricao == _cursoDto.Descricao))
            );
        }

        [Fact(DisplayName = "NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo")]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Action act = () =>
            {
                _armazenadorDeCurso.Armazenar(_cursoDto);
            };

            act.Should().Throw<ArgumentException>().WithMessage("Nome do curso já consta no banco de dados");

        }

        [Fact()]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            //Arrange
            var publicoAlvoInvaldido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvaldido;

            //act
            Action act = () =>
            {
                _armazenadorDeCurso.Armazenar(_cursoDto);
            };

            //assert
            act.Should().Throw<ArgumentException>().WithMessage("Publico alvo Inválido");
        }
    }
}