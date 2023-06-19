using Marketplace.Services.Cart.Services.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
    }
}
