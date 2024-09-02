﻿using Architecture.Application.Common.Models;
using System.Net;

namespace Architecture.Application.Carts
{
    public static class CartsErrors
    {
        public static Error CartNotFoundError => new(HttpStatusCode.NotFound, "Cart Not Found");
        public static Error CartItemNotFoundError => new(HttpStatusCode.NotFound, "Cart Item Not Found");
        public static Error CartEmptyError => new(HttpStatusCode.NoContent, "Cart Empty");

    }
}
