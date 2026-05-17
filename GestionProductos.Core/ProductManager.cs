namespace GestionProductos.Core;

/// <summary>
/// Gestiona una colección de productos y aplica reglas de negocio.
/// </summary>
public class ProductManager
{
    // Lista interna de productos
    private readonly List<Product> _products = new();

    /// <summary>
    /// Agrega un nuevo producto al sistema.
    /// Lanza excepción si el precio base es negativo.
    /// </summary>
    public void AddProduct(Product product)
    {
        if (product.Price < 0)
            throw new ArgumentException("El precio base de un producto no puede ser negativo.");

        _products.Add(product);
    }

    /// <summary>
    /// Calcula el precio total con impuestos según la categoría:
    ///   - Electrónica: +10%
    ///   - Alimentos:   +5%
    /// Lanza excepción si la categoría no es reconocida.
    /// </summary>
    public decimal CalculateTotalPrice(Product product)
    {
        return product.Category switch
        {
            "Electrónica" => product.Price * 1.10m,
            "Alimentos"   => product.Price * 1.05m,
            _ => throw new ArgumentException($"Categoría desconocida: {product.Category}")
        };
    }

    /// <summary>
    /// Busca un producto por su nombre (insensible a mayúsculas).
    /// Devuelve null si no existe.
    /// </summary>
    public Product? FindProductByName(string name)
    {
        return _products.Find(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Expone la lista de productos (solo lectura) para verificaciones en tests.
    /// </summary>
    public IReadOnlyList<Product> GetAllProducts() => _products.AsReadOnly();
}