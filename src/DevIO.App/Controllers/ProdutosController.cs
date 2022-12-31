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

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.GetProdutosFornecedoresAsync()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View(await FillFornecedoresAsync(new ProdutoViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await FillFornecedoresAsync(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            await _produtoRepository.AddAsync(_mapper.Map<Produto>(produtoViewModel));

            return View(produtoViewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(produtoViewModel);

            await _produtoRepository.UpdateAsync(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await GetProdutoAsync(id);

            if (produtoViewModel != null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
    }
}
