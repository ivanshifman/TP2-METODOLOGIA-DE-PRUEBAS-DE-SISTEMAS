using NUnit.Framework;
using GestionProductos.Core;

namespace TP2_METODOLOGIA_DE_ESTUDIO.Tests;

[TestFixture]
public class ProductManagerTests
{
    // ─── campos compartidos entre tests ───────────────────────────────────────
    private ProductManager _manager = null!;
    private Product _electronico = null!;
    private Product _alimento    = null!;

    // ─── SetUp: se ejecuta antes de CADA test ─────────────────────────────────
    [SetUp]
    public void SetUp()
    {
        _manager     = new ProductManager();
        _electronico = new Product(1, "Laptop", 1000m, "Electrónica");
        _alimento    = new Product(2, "Arroz",    50m, "Alimentos");
    }

    // ─── 1. Creación correcta de un producto ──────────────────────────────────
    [Test]
    public void Product_CreadoConDatosValidos_TieneCorrectasLasPropiedades()
    {
        var producto = new Product(10, "Celular", 500m, "Electrónica");

        Assert.That(producto.Id,       Is.EqualTo(10));
        Assert.That(producto.Name,     Is.EqualTo("Celular"));
        Assert.That(producto.Price,    Is.EqualTo(500m));
        Assert.That(producto.Category, Is.EqualTo("Electrónica"));
    }

    // ─── 2. AddProduct agrega el producto a la lista interna ──────────────────
    [Test]
    public void AddProduct_ProductoValido_SeAgregaALaLista()
    {
        _manager.AddProduct(_electronico);

        Assert.That(_manager.GetAllProducts(), Has.Count.EqualTo(1));
        Assert.That(_manager.GetAllProducts()[0].Name, Is.EqualTo("Laptop"));
    }

    // ─── 3. FindProductByName encuentra correctamente varios productos ─────────
    [Test]
    public void FindProductByName_VariosProductosAgregados_EncuentraCadaUno()
    {
        var tercero = new Product(3, "Televisor", 800m, "Electrónica");
        _manager.AddProduct(_electronico);   // Laptop
        _manager.AddProduct(_alimento);      // Arroz
        _manager.AddProduct(tercero);        // Televisor

        var resultado1 = _manager.FindProductByName("Laptop");
        var resultado2 = _manager.FindProductByName("Arroz");
        var resultado3 = _manager.FindProductByName("Televisor");

        Assert.That(resultado1, Is.Not.Null);
        Assert.That(resultado1!.Id, Is.EqualTo(1));

        Assert.That(resultado2, Is.Not.Null);
        Assert.That(resultado2!.Id, Is.EqualTo(2));

        Assert.That(resultado3, Is.Not.Null);
        Assert.That(resultado3!.Id, Is.EqualTo(3));
    }

    // ─── 4. CalculateTotalPrice – categoría Electrónica (+10%) ───────────────
    [Test]
    public void CalculateTotalPrice_CategoriaElectronica_AplicaImpuesto10PorCiento()
    {
        decimal total = _manager.CalculateTotalPrice(_electronico);

        Assert.That(total, Is.EqualTo(1100m));
    }

    // ─── 5. CalculateTotalPrice – categoría Alimentos (+5%) ──────────────────
    [Test]
    public void CalculateTotalPrice_CategoriaAlimentos_AplicaImpuesto5PorCiento()
    {
        decimal total = _manager.CalculateTotalPrice(_alimento);

        Assert.That(total, Is.EqualTo(52.50m));
    }

    // ─── Extra: precio negativo lanza excepción ───────────────────────────────
    [Test]
    public void AddProduct_PrecioNegativo_LanzaArgumentException()
    {
        var productoInvalido = new Product(99, "Raro", -10m, "Alimentos");

        Assert.Throws<ArgumentException>(() => _manager.AddProduct(productoInvalido));
    }

    // ─── Extra: nombre inexistente devuelve null ──────────────────────────────
    [Test]
    public void FindProductByName_NombreInexistente_DevuelveNull()
    {
        _manager.AddProduct(_electronico);

        var resultado = _manager.FindProductByName("ProductoQueNoExiste");

        Assert.That(resultado, Is.Null);
    }
}