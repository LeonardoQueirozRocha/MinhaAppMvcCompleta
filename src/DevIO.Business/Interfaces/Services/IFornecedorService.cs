using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Services
{
    public interface IFornecedorService
    {
        Task AddAsync(Fornecedor fornecedor);
        Task UpdateAsync(Fornecedor fornecedor);
        Task DeleteAsync(Guid id);
        Task UpdateEnderecoAsync(Endereco endereco);
    }
}
