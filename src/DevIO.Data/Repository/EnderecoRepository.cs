using DevIO.Business.Interfaces.Repository;
using DevIO.Business.Models;
using DevIO.Data.Context;
using DevIO.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext context) : base(context) { }

        public async Task<Endereco> GetEnderecoByFornecedorAsync(Guid fornecedorId)
        {
            return await Db.Enderecos
                .AsNoTracking()
                .FirstOrDefaultAsync(endereco => endereco.FornecedorId == fornecedorId);
        }
    }
}
