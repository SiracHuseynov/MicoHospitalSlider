using MicoHospital.Business.Exceptions;
using MicoHospital.Business.Services.Abstracts;
using MicoHospital.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicoHospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders = _sliderService.GetAllSliders();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _sliderService.AddAsyncSlider(slider);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var slider = _sliderService.GetSlider(x => x.Id == id);

            if (slider == null)
                return NotFound();

            return View(slider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _sliderService.UpdateSlider(slider.Id, slider);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var slider = _sliderService.GetSlider(x => x.Id == id);

            if (slider == null)
                return NotFound();

            return View(slider);
        }


        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _sliderService.DeleteSlider(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }


    }
}
