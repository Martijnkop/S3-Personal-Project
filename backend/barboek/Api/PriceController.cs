using barboek.Api.Models;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace barboek.Api;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    //private IPriceService _priceService;
    private IPriceTypeService _priceTypeService;
    private ITaxTypeService _taxTypeService;

    public PriceController(IPriceTypeService priceTypeService, ITaxTypeService taxTypeService)
    {
        //_priceService = priceService;
        _priceTypeService = priceTypeService;
        _taxTypeService = taxTypeService;
    }

    #region price

    [HttpGet("{itemId}")]
    public ActionResult<List<Price>> GetPricesByItemId(Guid itemId, [FromQuery] DateTime? time = null, [FromQuery] Guid? priceTypeId = null)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{itemId}")]
    public ActionResult<Price> CreatePrice(Guid itemId, [FromBody] ApiPriceDetails priceDetails)
    {
        throw new NotImplementedException();
    }
    #endregion


    // get current item price
    // get current item prices
    // get past item price
    // get past item prices

    #region taxtype
    // hoog, laag, 0, toekomstbestendig

    [HttpGet("taxtype")]
    public ActionResult<List<TaxType>> GetTaxTypes()
    {
        return Ok(_taxTypeService.GetAll());
    }

    [HttpGet("taxtype/{id}")]
    public ActionResult<TaxType> GetTaxTypeById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();

        TaxType taxType = _taxTypeService.GetById(id);

        if (taxType.Id == Guid.Empty) return NotFound();

        return Ok(taxType);
    }

    [HttpGet("taxtype/{name}")]
    public ActionResult<TaxType> GetTaxTypeByName(string name)
    {
        throw new NotImplementedException();
    }

    [HttpPost("taxtype/{name}")]
    public ActionResult<TaxType> CreateTaxType(string name, [FromBody] ApiTaxTypeInstanceDetails taxTypeDetails)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("name is invalid");

        return Ok(_taxTypeService.Create(name, taxTypeDetails.Percentage, taxTypeDetails.BeginTime, taxTypeDetails.EndTime));
    }

    [HttpPut("taxtype/edit/{id}")]
    public ActionResult<TaxType> ChangeTaxTypeName(Guid id, [FromBody] string name)
    {
        ActionResult<TaxType> getByIdResult = GetTaxTypeById(id);

        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        if (string.IsNullOrEmpty(name)) return BadRequest("name is invalid");

        TaxType taxType = _taxTypeService.ChangeName(id, name);

        if (taxType.Id == Guid.Empty) return NotFound();

        return Ok(taxType);
    }

    [HttpPost("taxtypeinstance/{taxTypeId}")]
    public ActionResult<TaxType> CreateTaxTypeInstance(Guid taxTypeId, [FromBody] ApiTaxTypeInstanceDetails taxTypeDetails)
    {
        ActionResult<TaxType> getByIdResult = GetTaxTypeById(taxTypeId);

        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        TaxType taxType = _taxTypeService.CreateInstance(taxTypeId, taxTypeDetails.Percentage, taxTypeDetails.BeginTime, taxTypeDetails.EndTime);

        if (taxType.Id == Guid.Empty) return NotFound();

        return Ok(taxType);
    }

    [HttpPut("taxtypeinstance/edit/{instanceId}")]
    public ActionResult EditTaxTypeInstance(Guid instanceId, [FromBody] ApiTaxTypeInstanceDetails taxTypeDetails)
    {
        bool changed = _taxTypeService.UpdateInstance(instanceId, taxTypeDetails.Percentage, taxTypeDetails.BeginTime, taxTypeDetails.EndTime);

        if (changed) return NotFound();
        return Ok();
    }
    #endregion

    #region pricetype
    // inkoop, ledenprijs, externe prijs

    [HttpGet("pricetype")]
    public ActionResult<List<PriceType>> GetPriceType()
    {
        return Ok(_priceTypeService.GetAll());
    }

    [HttpGet("pricetype/{id}")]
    public ActionResult<PriceType> GetPriceTypeById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();

        PriceType priceType = _priceTypeService.GetById(id);

        if (priceType.Id == Guid.Empty) return NotFound();

        return Ok(priceType);
    }

    [HttpGet("pricetype/{name}")]
    public ActionResult<PriceType> GetPriceTypeByName(string name) {
        throw new NotImplementedException();
    }

    [HttpPost("pricetype/{name}")]
    public ActionResult<PriceType> CreatePriceType(string name)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("name is invalid");

        return Ok(_priceTypeService.Create(name));
    }

    [HttpPut("pricetype/edit/{id}")]
    public ActionResult<PriceType> ChangePriceTypeName(Guid id, [FromBody] string name)
    {
        ActionResult<PriceType> getByIdResult = GetPriceTypeById(id);

        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        if (string.IsNullOrEmpty(name)) return BadRequest("name is invalid");

        PriceType priceType = _priceTypeService.ChangeName(id, name);

        if (priceType.Id == Guid.Empty) return NotFound();

        return Ok(priceType);
    }

    [HttpPut("pricetype/setactive/{id}")]
    public ActionResult<PriceType> SetPriceTypeActive(Guid id, [FromBody] bool? active)
    {
        ActionResult<PriceType> getByIdResult = GetPriceTypeById(id);

        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        if (active == null) active = true;

        PriceType priceType = _priceTypeService.SetActive(id, (bool)active);

        if (priceType.Id == Guid.Empty) return NotFound();

        return Ok(priceType);
    }

    #endregion
}