using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;


namespace E_Commerce.Services
{
	public class CategoryService : IBrandCategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<IEnumerable<BrandTypeDTO>>? GetAllBrandsOrCategoriesAsync()
		{
			var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
			if (types == null) return null!;

			return _mapper.Map<IReadOnlyList<BrandTypeDTO>>(types);
		}
		public async Task<BrandTypeDTO>? GetBrandOrCategoryByIdAsync(int Id)
		{
			var category = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(Id);
			if (category == null) return null!;

			return _mapper.Map<BrandTypeDTO>(category);
		}
		public async Task<bool> DeleteBrandOrCategory(BrandTypeDTO entity)
		{
			var category = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(entity.Id);
			if (category != null)
			{
				_unitOfWork.Repository<ProductType, int>().Delete(category);
				var deleted = await _unitOfWork.CompleteAsync();
				return deleted > 0;
			}
			return false;
		}
		public async Task<bool> AddBrandOrCategory(BrandTypeDTO entity)
		{
			await _unitOfWork.Repository<ProductType, int>().AddAsync(_mapper.Map<ProductType>(entity));
			var state = await _unitOfWork.CompleteAsync();
			return state > 0;
		}
		public async Task<bool> UpdateBrandOrCategory(BrandTypeDTO category)
		{
			var productType = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(category.Id);
			if (productType == null) return false;

			productType.Name = category.Name;
			//TODO:Image
			await _unitOfWork.CompleteAsync();
			return true;
		}

	}
}
