using dotenv.net;
using HotelEasy.Services.Common;
using Checkout = Stripe.Checkout;

namespace HotelEasy.Services;

public class PaymentsServices
{

    public PaymentsServices()
    {
    }

    public async Task<ServiceResult<string>> CreateCheckoutSessionAsync(decimal amount, string title, string successUrl, string cancelUrl)
    {
        DotEnv.Load();
        var result = new ServiceResult<string>();

        try
        {
            var options = new Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<Checkout.SessionLineItemOptions>
                {
                    new Checkout.SessionLineItemOptions
                    {
                        PriceData = new Checkout.SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100),
                            Currency = "bgn",
                            ProductData = new Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = title
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new Checkout.SessionService();
            var session = await service.CreateAsync(options);

            result.Data = session.Url;
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}
