using DevIO.Business.Interfaces.Repository.Base;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Repository
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> GetFornecedorEnderecoAsync(Guid id);
        Task<Fornecedor> GetFornecedorProdutosEnderecoAsync(Guid id);
    }
}
