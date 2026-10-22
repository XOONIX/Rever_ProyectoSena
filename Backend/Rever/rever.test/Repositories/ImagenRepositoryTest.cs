using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class ImagenRepositoryTest
    {
        [Fact]
        public async Task GetImagen_ExistenImagenes_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Imagen.AddRange(
                new Imagen { IdImagen = 1, Url = "http://img1.jpg", IdInmueble = 1, Portada = true },
                new Imagen { IdImagen = 2, Url = "http://img2.jpg", IdInmueble = 1, Portada = false }
            );
            await context.SaveChangesAsync();

            var repository = new ImagenRepository(context);

            // ACT
            var resultado = await repository.GetImagen();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetImagenById_Existe_RetornaImagen()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Imagen.Add(new Imagen { IdImagen = 10, Url = "http://foto.jpg", IdInmueble = 2 });
            await context.SaveChangesAsync();

            var repository = new ImagenRepository(context);

            // ACT
            var resultado = await repository.GetImagenById(10);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Url.Should().Be("http://foto.jpg");
        }

        [Fact]
        public async Task GetImagenById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ImagenRepository(context);

            // ACT
            var resultado = await repository.GetImagenById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostImagen_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ImagenRepository(context);
            var nueva = new Imagen { IdImagen = 3, Url = "http://nueva.png", IdInmueble = 1 };

            // ACT
            var resultado = await repository.PostImagen(nueva);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Imagen.FirstOrDefaultAsync(i => i.IdImagen == 3);
            enBd.Should().NotBeNull();
            enBd!.Url.Should().Be("http://nueva.png");
        }

        [Fact]
        public async Task PutImagen_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var img = new Imagen { IdImagen = 1, Url = "http://antigua.jpg", IdInmueble = 1 };
            context.Imagen.Add(img);
            await context.SaveChangesAsync();

            var repository = new ImagenRepository(context);
            img.Url = "http://actualizada.jpg";

            // ACT
            var resultado = await repository.PutImagen(img);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Imagen.FirstOrDefaultAsync(i => i.IdImagen == 1);
            enBd!.Url.Should().Be("http://actualizada.jpg");
        }

        [Fact]
        public async Task PutImagen_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ImagenRepository(context);
            var img = new Imagen { IdImagen = 999, Url = "http://noexiste.jpg", IdInmueble = 1 };

            // ACT
            var resultado = await repository.PutImagen(img);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteImagen_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var img = new Imagen { IdImagen = 1, Url = "http://borrar.jpg", IdInmueble = 1 };
            context.Imagen.Add(img);
            await context.SaveChangesAsync();

            var repository = new ImagenRepository(context);

            // ACT
            var resultado = await repository.DeleteImagen(img);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Imagen.FirstOrDefaultAsync(i => i.IdImagen == 1);
            enBd.Should().BeNull();
        }
    }
}
