using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Services
{
    public interface IProdutoService : IDisposable
    {
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(Guid id);
    }
}
