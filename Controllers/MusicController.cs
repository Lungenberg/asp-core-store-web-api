using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPCoreWebApplication.Models;
using ASPCoreWebApplication.Services;

namespace ASPCoreWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService musicService) => _musicService = musicService;

        [HttpGet]
        public async Task<List<Category>> Get() => await _musicService.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var music = await _musicService.GetAsync(id);

            if (music == null)
            {
                return NotFound();
            }

            return music;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Category newMusic)
        {
            await _musicService.CreateAsync(newMusic);
            return CreatedAtAction(nameof(Get), new { id = newMusic.Id }, newMusic);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Category updatedMusic)
        {
            var music = await _musicService.GetAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            updatedMusic.Id = music.Id;

            await _musicService.UpdateAsync(id, updatedMusic);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var music = await _musicService.GetAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            await _musicService.RemoveAsync(id);
            return NoContent();
        }

    }
}
