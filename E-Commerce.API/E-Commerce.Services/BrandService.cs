using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
	public class BrandService : IBrandService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BrandTypeDTO>> GetAllBrandsAsync()
		{
			var types = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
			return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandTypeDTO>>(types);
		}

	}
}
