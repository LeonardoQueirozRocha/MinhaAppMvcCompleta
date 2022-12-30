using DevIO.Business.Interfaces.Repository;
using DevIO.Business.Models;
using DevIO.Data.Context;
using DevIO.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext context) : base(context) { }

        public async Task<Fornecedor> GetFornecedorEnderecoAsync(Guid id)
        {
            return await Db.Fornecedores
                .AsNoTracking()
                .Include(fornecedor => fornecedor.Endereco)
                .FirstOrDefaultAsync(fornecedor => fornecedor.Id == id);
        }

        public async Task<Fornecedor> GetFornecedorProdutosEnderecoAsync(Guid id)
        {
            return await Db.Fornecedores
                .AsNoTracking()
                .Include(fornecedor => fornecedor.Produtos)
                .Include(fornececor => fornececor.Endereco)
                .FirstOrDefaultAsync(fornecedor => fornecedor.Id == id);
        }
    }
}
