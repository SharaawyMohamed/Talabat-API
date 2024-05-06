using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface IPaymentService
	{
		Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto input);
		Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId);
		Task<OrderResultDto> UpdatePaymentStatusFailed(string PaymentIntentId);
		Task<OrderResultDto> UpdatePaymentStatueSucceeded(string PaymentIntentId);
	}
}
