using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Controllers.Models;

namespace MyWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HangHoaController : ControllerBase
	{
		public static List<HangHoa> HangHoas = new List<HangHoa>();
		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(HangHoas);
		}
		[HttpGet("{id}")]
		public IActionResult GetbyId(string id)
		{
			try
			{
				//LinQ [Object] Query
				var hangHoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
				if (hangHoa == null)
				{
					return NotFound();
				}
				return Ok(hangHoa);
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpPost]
		public IActionResult Create(HangHoaVM hangHoaVM)
		{
			var hanghoa = new HangHoa
			{
				MaHangHoa = Guid.NewGuid(),
				TenHangHoa = hangHoaVM.TenHangHoa,
				DonGia = hangHoaVM.DonGia,
			};
			HangHoas.Add(hanghoa);
			return Ok(new
			{
				succes = true,
				Data = hanghoa
			});
		}
		[HttpPut("{id}")]
		public IActionResult EditbyId(string id, HangHoa hangHoaEdit)
		{
			try
			{
				var hangHoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
				if (hangHoa == null)
				{
					return NotFound();
				}
				if (id != hangHoa.MaHangHoa.ToString())
				{
					return BadRequest();
				}
				//Update
				hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
				hangHoa.DonGia = hangHoaEdit.DonGia;

				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeletebyId(string id)
		{
			try
			{
				var hangHoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
				if (hangHoa == null)
				{
					return NotFound();
				}
				if (id != hangHoa.MaHangHoa.ToString())
				{
					return BadRequest();
				}
				//DELETE
				HangHoas.Remove(hangHoa);

				return Ok();

			}
			catch
			{
				return BadRequest();
			}
		}




	}
}

