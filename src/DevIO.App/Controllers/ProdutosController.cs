using AutoMapper;
using DevIO.App.Controllers.Base;
using DevIO.App.Extensions.Attributes.Authorization;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces.Notifications;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers
{
    [Authorize]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public ProdutosController(
            IProdutoService produtoService,
            IFornecedorService fornecedorService,
            IMapper mapper,
            INotifier notifier) : base(notifier)
        {
            _produtoService = produtoService;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoService.GetProdutosFornecedoresAsync()));
        }

        [AllowAnonymous]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            return View(await FillFornecedoresAsync(new ProdutoViewModel()));
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await FillFornecedoresAsync(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = $"{Guid.NewGuid()}_";

            if (!await UploadFileAsync(produtoViewModel.ImagemUpload, imgPrefixo)) return View(produtoViewModel);

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoService.AddAsync(_mapper.Map<Produto>(produtoViewModel));

            if (!IsValid()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
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

            await _produtoService.UpdateAsync(_mapper.Map<Produto>(produtoBase));

            if (!IsValid()) return View(produtoViewModel);


            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            await _produtoService.DeleteAsync(id);

            if (!IsValid()) return View(produtoViewModel);

            TempData["Success"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> GetProdutoAsync(Guid id)
        {
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoService.GetProdutoFornecedorAsync(id));
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorService.GetAllAsync());
            return produtoViewModel;
        }

        private async Task<ProdutoViewModel> FillFornecedoresAsync(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorService.GetAllAsync());
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
