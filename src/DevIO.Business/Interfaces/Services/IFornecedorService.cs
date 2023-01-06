using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task<IEnumerable<Fornecedor>> GetAllAsync();
        Task<Fornecedor> GetFornecedorEnderecoAsync(Guid id);
        Task<Fornecedor> GetFornecedorProdutosEnderecoAsync(Guid id);
        Task AddAsync(Fornecedor fornecedor);
        Task UpdateAsync(Fornecedor fornecedor);
        Task DeleteAsync(Guid id);
        Task UpdateEnderecoAsync(Endereco endereco);
    }
}
