using Refit;
using SharpPayStack.Models;

namespace SharpPayStack.Data;

public interface IPaystackCustomerApi
{
    /// <summary>
    /// <b>Create a paystack customer</b>
    /// </summary>
    [Headers("Content-Type: application/json")]
    [Post("/customer")]
    Task<CreateCustomerResponseDto> CreateCustomer(
        PaystackCreateCustomerDto customerDto, [Authorize("Bearer")] string paystackSecret
    );

    /// <summary>
    /// <b>Create a virtual account for a customer</b>
    /// </summary>
    [Headers("Content-Type: application/json")]
    [Get("/dedicated_account")]
    Task<VirtualAccountResponse> CreateCustomerVirtualAccount(
        PaystackCreateVirtualAccountDto virtualAccountCreateDto, [Authorize("Bearer")] string paystackSecret
    );

    /// <summary>
    /// <b>Fetch a customer from paystack</b>
    /// </summary>
    [Get("/customer/{email}")]
    Task<VirtualAccountResponse> GetCustomer(string email, [Authorize("Bearer")] string paystackSecret);

    /// <summary>
    /// <b>Get products by category</b>
    /// </summary>
    // [Get("/products/{categoryUrl}")]
    // Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);

    // /// <summary>
    // /// <b>Get search products suggestions</b>
    // /// </summary>
    // [Get("/products/search/suggestions")]
    // Task<ServiceResponse<List<string>>> GetProductsSuggestions([Query] ProductQueryParams queryParams);

    // /// <summary>
    // /// <b>Get product search results.</b>
    // /// </summary>
    // [Get("/products/search")]
    // Task<ServiceResponse<ProductSearchDto>> SearchProducts([Query] ProductQueryParams queryParams);
}

public interface IPaystackTransferApi
{
    /// <summary>
    /// <b>Transfer funds to a recipient</b>
    /// </summary>
    [Headers("Content-Type: application/json")]
    [Post("/transfer")]
    Task<PaystackTransferResponse> TransferFunds(
        PaystackTransferDto transferDto, [Authorize("Bearer")] string paystackSecret
    );

}

public interface IPaystackApi : IPaystackCustomerApi, IPaystackTransferApi
{
}