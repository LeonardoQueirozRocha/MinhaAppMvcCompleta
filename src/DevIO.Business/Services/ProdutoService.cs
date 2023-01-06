using DevIO.Business.Interfaces.Services;
using DevIO.Business.Models;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public Task AddAsync(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
