﻿namespace barboek.Interface.Models.API;

public struct PriceType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}