using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
	public record AddStripeCard(
		  string Email,
		  string Name,
		  string CardNumber,
		  string ExpirationYear,
		  string ExpirationMonth,
		  string Cvc
	);

	public record AddStripeCustomer(
	string Email,
	string Name,
	AddStripeCard CreditCard,
	int OrderId,
	string ProductsIds,
	int PaymentMethodType,
	bool IsShippingDifferentAddress,
	string OrderStatus,
	string OrderNumber,
	string Deliverytime,
	string orderdate,
	string AdditionalInfo,
	int UserId,
	string ProductName,
	string MobileNumber,
	string CityName,
	string Address,
	List<PurchasedProductsColor> productsColors

	);
	public record AddStripePayment(
		string CustomerId,
		string ReceiptEmail,
		string Description,
		string Currency,
		long Amount);


	public record StripeCustomer(
		string Name,
		string Email,
		string CustomerId);



	public record StripePayment(
	string CustomerId,
	string ReceiptEmail,
	string Description,
	string Currency,
	long Amount,
	string PaymentId);



	public class AddStripePaymentclass
	{

		public string CustomerId { get; set; }
		public string ReceiptEmail { get; set; }
		public string Description { get; set; }
		public string Currency { get; set; }
		public long Amount { get; set; }

		public AddStripePaymentclass(
			   string customerId,
		string receiptEmail,
		string description,
		string currency,
		long amount)
		{
			CustomerId = customerId;
			ReceiptEmail = receiptEmail;

			Description = description;
			Currency = currency;
			Amount = amount;


		}




	}





	public class CreditCard
	{
		public string Name { get; set; }
		public string CardNumber { get; set; }
		public string ExpirationYear { get; set; }
		public string ExpirationMonth { get; set; }
		public string CVC { get; set; }
	}

	public class User
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public CreditCard CreditCard { get; set; }
	}

	public class AddStripeCardForModelService {
        public string Email { get; set; }

		public string Name { get; set; }


		public string CardNumber { get; set; }
		public string ExpirationYear { get; set; }
		public string ExpirationMonth { get; set; }
		public string CVC { get; set; }

        public string note { get; set; }
    }			  
		  

}
