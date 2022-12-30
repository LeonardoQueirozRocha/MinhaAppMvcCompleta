using DevIO.Business.Interfaces.Repository.Base;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosByFornecedorAsync(Guid fornecedorId);
        Task<IEnumerable<Produto>> GetProdutosFornecedoresAsync();
        Task<Produto> GetProdutoFornecedorAsync(Guid id);
    }
}
