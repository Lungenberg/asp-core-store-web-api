using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPCoreWebApplication.Models;
using ASPCoreWebApplication.Services;
using ASPCoreWebApplication.Dto;

namespace ASPCoreWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService musicService) => _musicService = musicService;

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDto>>> Get(
            [FromQuery] string? title,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection)
        {
            var albums = await _musicService.GetAllAsync(title, sortBy, sortDirection);
            return Ok(albums);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CategoryResponseDto>> Get(string id)
        {
            var albums = await _musicService.GetAsync(id);

            if (albums == null)
            {
                return NotFound(); // 404
            }

            return Ok(albums); // автоматом обернёт в 200 ОК и сериализует в JSON
        }


        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> Post(CategoryCreateDto musicDto)
        {
            var entity = new Category
            {
                AlbumName = musicDto.AlbumName,
                Price = musicDto.Price,
                Genre = musicDto.Genre,
                Author = musicDto.Author
            };

            await _musicService.CreateAsync(entity); // монго автоматически проставляет id

            var response = new CategoryResponseDto
            {
                Id = entity.Id!,
                AlbumName = entity.AlbumName,
                Price = entity.Price,
                Genre = entity.Genre,
                Author = entity.Author
            };

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryUpdateDto updatedDto)
        {
            var current = await _musicService.GetAsync(id);
            if (current == null)
            {
                return NotFound();
            }

            current.AlbumName = updatedDto.AlbumName;
            current.Price = updatedDto.Price;
            current.Genre = updatedDto.Genre;
            current.Author = updatedDto.Author;

            current.Id = id;

            await _musicService.UpdateAsync(id, current);
            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var album = await _musicService.GetAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            await _musicService.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<CategoryResponseDto>>> Search([FromQuery] string? title)
        {
            var albums = await _musicService.SearchByTitleAsync(title);
            return Ok(albums);
        }

        [HttpGet("count")]
        public async Task<ActionResult<CategoryResponseDto>> GetCount()
        {
            var count = await _musicService.GetCountAsync();

            return Ok(new { count });
        }
    }
}
