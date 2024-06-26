﻿using Microsoft.AspNetCore.Http;

namespace ECommerce_be.Application.ViewModels.Products;
public class VM_Create_Product
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public List<FormFile> Images { get; set; }
}
