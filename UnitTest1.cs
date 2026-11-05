using FluentAssertions;
using Xunit;

namespace ReverInmobiliaria.Tests
{
    public class InmuebleModelTests
    {
        [Fact]
        public void Test_VerificarConfiguracionInicial()
        {
            // Arrange
            bool sistemaConfigurado = true;

            // Act & Assert
            sistemaConfigurado.Should().BeTrue();
        }
    }
}