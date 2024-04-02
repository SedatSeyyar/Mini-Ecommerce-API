﻿using MediatR;

namespace ECommerce_be.Application.Features.Queries.GetAllProduct
{
    public  class GetAllProductQueryRequest: IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
