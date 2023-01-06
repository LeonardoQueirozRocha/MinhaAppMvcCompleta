using DevIO.Business.Models.Enums;
using DevIO.Business.Models.Validations.Documents;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(fornecedor => fornecedor.Nome)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(fornecedor => fornecedor.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(fornecedor => fornecedor.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(fornecedor => CpfValidacao.Validar(fornecedor.Documento))
                    .Equal(true)
                        .WithMessage("O documento fornecido é inválido.");
            });

            When(fornecedor => fornecedor.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(fornecedor => fornecedor.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(fornecedor => CnpjValidacao.Validar(fornecedor.Documento))
                    .Equal(true)
                        .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}
