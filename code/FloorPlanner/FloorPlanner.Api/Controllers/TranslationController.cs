using Core.Pagination.Models;
using Core.Translation.Models;
using Core.Translation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanner.Api.Controllers;

public class TranslationController : BaseController
{
    private readonly ITranslationService _translationService;

    public TranslationController(ITranslationService translationService)
        => _translationService = translationService;

    [HttpGet]
    public async Task<PaginationResponse<TranslationDto>> GetTranslationsAsync([FromQuery] TranslationSearchDto searchDto)
        => await _translationService.GetTranslationsAsync(searchDto);

    [HttpGet]
    public async Task<TranslationsDto> GetAllTranslationsAsync()
        => await _translationService.GetAllTranslationsAsync();

    [HttpPut]
    public async Task UpdateTranslationAsync(TranslationUpdateDto dto)
        => await _translationService.UpdateTranslationAsync(dto);

    [HttpGet("/assets/i18n/{language}.json")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFrontendLocalizationAsync(string language)
        => Content(await _translationService.GetCurrentTranslationsInJsonAsync(language));
}