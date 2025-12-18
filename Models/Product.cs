using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama Produk harus diisi.")]
    [StringLength(100, ErrorMessage = "Nama tidak boleh lebih dari 100 karakter.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Harga produk harus diisi.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Nilai harus lebih dari 0.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Kategori produk harus diisi.")]
    [StringLength(100, ErrorMessage = "Kategori tidak boleh lebih dari 100 karakter.")]
    public string Category { get; set; } = string.Empty;
    public string? Image { get; set; }
    public string? Description { get; set; }
  }
}
