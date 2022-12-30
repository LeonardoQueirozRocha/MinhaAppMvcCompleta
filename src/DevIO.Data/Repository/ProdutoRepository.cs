using DevIO.Business.Interfaces.Repository;
using DevIO.Business.Models;
using DevIO.Data.Context;
using DevIO.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }
        
        public async Task<Produto> GetProdutoFornecedorAsync(Guid id)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Include(produto => produto.Fornecedor)
                .FirstOrDefaultAsync(produto => produto.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetProdutosFornecedoresAsync()
        {
            return await Db.Produtos
                .AsNoTracking()
                .Include(produto => produto.Fornecedor)
                .OrderBy(produto => produto.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosByFornecedorAsync(Guid fornecedorId)
        {
            return await SearchAsync(produto => produto.FornecedorId == fornecedorId);
        }
    }
}
