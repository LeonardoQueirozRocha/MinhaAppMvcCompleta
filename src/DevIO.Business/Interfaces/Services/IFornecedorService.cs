using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task AddAsync(Fornecedor fornecedor);
        Task UpdateAsync(Fornecedor fornecedor);
        Task DeleteAsync(Guid id);
        Task UpdateEnderecoAsync(Endereco endereco);
    }
}
