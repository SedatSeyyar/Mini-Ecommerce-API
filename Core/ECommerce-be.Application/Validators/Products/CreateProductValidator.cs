using ECommerce_be.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerce_be.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ürün adı boş geçilemez.")
                .MaximumLength(200)
                .MinimumLength(3)
                .WithMessage("Ürün adı 3 ile 200 karakter arasında olmalıdır.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Stok bilgisi boş geçilemez.")
                .Must(s => s >= 0)
                .WithMessage("Stok değeri 0'dan büyük olmalıdır.");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Fiyat bilgisi boş geçilemez.")
                .Must(s => s >= 0)
                .WithMessage("Fiyat değeri 0'dan büyük olmalıdır.");

            RuleFor(p => p.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ürün açıklaması boş geçilemez.")
                .MaximumLength(500)
                .MinimumLength(15)
                .WithMessage("Ürün açıklaması 15 ile 500 karakter arasında olmalıdır.");
        }
    }
}
