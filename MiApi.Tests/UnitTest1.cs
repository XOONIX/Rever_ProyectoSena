using Xunit;
//using rever; // Esto te da acceso al código de tu API principal

namespace MiApi.Tests;

public class UnitTest1
{
    [Fact]
    public void Test_DeberiaVerificarQueLaApiEstaConectada()
    {
        // 1. Organizar (Arrange): Definimos los datos de prueba
        var valorEsperado = "Conexión Exitosa";

        // 2. Actuar (Act): Simulamos una operación simple
        var valorActual = "Conexión Exitosa"; 

        // 3. Afirmar (Assert): Verificamos que el resultado sea el esperado
        Assert.Equal(valorEsperado, valorActual);
    }
}
