using AutoMapper;
using DevIO.App.Controllers.Base;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces.Repository;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(
            IProdutoRepository produtoRepository,
            IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.GetProdutosFornecedoresAsync()));
        }

        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            return View(await FillFornecedoresAsync(new ProdutoViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo-produto")]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await FillFornecedoresAsync(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = $"{Guid.NewGuid()}_";

            if (!await UploadFileAsync(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoRepository.AddAsync(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");
        }

        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoBase = await GetProdutoAsync(id);
            produtoViewModel.Fornecedor = produtoBase.Fornecedor;
            produtoViewModel.Imagem = produtoBase.Imagem;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadFileAsync(produtoViewModel.ImagemUpload, imgPrefixo)) 
                    return View(produtoViewModel);

                produtoBase.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoBase.Nome = produtoViewModel.Nome;
            produtoBase.Descricao = produtoViewModel.Descricao;
            produtoBase.Valor = produtoViewModel.Valor;
            produtoBase.Ativo = produtoViewModel.Ativo;

            await _produtoRepository.UpdateAsync(_mapper.Map<Produto>(produtoBase));

            return RedirectToAction("Index");
        }

        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel != null) return NotFound();

            await _produtoRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> GetProdutoAsync(Guid id)
        {
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoRepository.GetProdutoFornecedorAsync(id));
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.GetAllAsync());
            return produtoViewModel;
        }

        private async Task<ProdutoViewModel> FillFornecedoresAsync(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.GetAllAsync());
            return produtoViewModel;
        }

        private async Task<bool> UploadFileAsync(IFormFile file, string imgPrefixo)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return true;
        }
    }
}
